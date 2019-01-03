# RIDGID.Common.Api
RIDGID.Common.Api is a REST Api response formatting system that automatically returns all error responses in the clean format listed below. The benefits of this aproach are cleaner response parsing when there are multiple validation errors with the same status code in your API (e.g. multiple missing fields).

Below is an example of a 400 Bad Request with two model validation errors:

```json
{
  "errors": [    
    {
      "debugErrorMessage": "Username must be between 6 and 255 characters long.",
      "errorId": 1
    },
    {
      "debugErrorMessage": "Username cannot contain special characters.",
      "errorId": 2
    }
  ]
}
```

## To Use In Your API
1. Make controller inherit from RidgidApiController (if you want to get access to convenient methods for returning error responses in the above format). E.g.:
  ```
    public class SomeController : RidgidApiController
    {
        public const int ArbitraryValueNotValidError = 1;
        
        public IHttpActionResult SomeAction(string someValue)
        {
            if(ValueChecker.CheckArbitraryExampleValue(someValue))
            {
                return BadRequest(ArbitraryValueNotValidError, "Sorry but this value is not valid. Try a different one.");
            }
        }
    }
  ```
   A request to ```/api/Some/SomeAction?someValue=invalidValue``` will yield the following HTTP 400 Bad Request response:
    
   ```json
    {
      "errors": [    
        {
          "debugErrorMessage": "Sorry but this value is not valid. Try a different one.",
          "errorId": 1
        }
      ]
    }
  ```
  
  
2. Add the `[RidgidValidateModel]` attribute to the controller method
3. Add any of the custom attributes to the property on the model being passed into the controller, e.g., `[RidgidRequired(ErrorId)]`
   Combining #2 and #3 above lets you do stuff like the following:
     ```
    [HttpPost]
    [RidgidValidateModel]
    public IHttpActionResult SomeAction(Person person)
    {
        //--do your stuff here without writing any nasty code about the model state or returning different status codes
        
        return OK();
    }

    public class Person
    {
        public const int SocialSecurityNumberNotSet = 1;
        public const int FirstNameNotSet = 2;
        public const int SecretPassCodeTooShort = 3; 

        [RidgidRequired(SocialSecurityNumberNotSet)]
        public string SocialSecurityNumber { get; set; }

        [RidgidRequired(FirstNameNotSet)]
        public string FirstName { get; set; }

        [RidgidMinLength(SecretPassCodeTooShort)]
        public int NumberOfBoardGamesOwned { get; set; }
    }
    ``` 
   

    
4. Write a Unit Test to test the model by doing:

    ```c#
      model.ShouldValidateTheseFields {
        new RidgidRequiredFieldValidation {
        ErrorId = errorId,
        FieldName = "FieldName"
      },
      ...
    }
    ```

using one of the RidgidFieldValidation subclasses, e.g., RidgidRequiredFieldValidation, RidgidStringLengthFieldValidation

5. For non model validation error responses use:
    ```
    return Conflict(1, "message");
    return BadRequest(1, "message");
    return NotFound(1, "message");
    return GenericHttpErrorResponse(1, "message", HttpStatusCodes.PaymentRequired);
    ```  
    Etc.
  
 
### Snakecase Property Names
To make the response body like the following:

```json
{
  "errors": [    
    {
      "debug_error_message": "Username must be between 6 and 255 characters long.",
      "error_id": 1
    }
  ]
}
```

Add to your API's app.config appSettings:

```xml
<appSettings>
    <add key="snakecase" value="true" />
</appSettings>
```

 ### To Build Nuget Package
 ```nuget pack TestingUtilities.csproj -Version {semantif version} -properties Configuration=Release -IncludeReferencedProjects```
 
