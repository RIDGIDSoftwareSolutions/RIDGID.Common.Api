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
## To Get Useful Helper Methods in your Web API Controllers
Make your controller inherit from RidgidApiController (if you want to get access to convenient methods for returning error responses in the above format). E.g.:
  ```C#
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
  
  Not all status codes have helper methods, so you can always use:

 ```return GenericHttpErrorResponse(1, "Message you want to give back to client", HttpStatusCodes.PaymentRequired);```

## To Use Fancy Automatic Model Validation
If you want to annotate your models' properties so that you get the above error response schema:
1. Add the `[RidgidValidateModel]` attribute to the controller method. E.g.:

```C#
    [HttpPost]
    [RidgidValidateModel]
    public IHttpActionResult SomeAction(Person person)
    {
        //--do your stuff here without writing any nasty code about the model state or returning different status codes
        
        return OK();
    }

```
2. Annotate your model attributes with the Ridgid* attribute(s) that you want to use for validation. Note that the errorId should be unique for a given endpoint and HTTP status code. 
This way you could have 3 different 400 Bad Request model validation errors that can be easily parsed in the client by their unique Id. E.g.:

```C#
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

## To Unit Test Your Model Validation
This package comes with Shouldly-style extension methods that allow you to test that specific attributes are applied to specific properties. E.g.:

```C#
  model.ShouldValidateTheseFields {
    new RidgidRequiredFieldValidation {
      ErrorId = errorId,
      FieldName = "FieldName"
    }
  }
```
    
## To Have Unhandled Exceptions Use The Same Response Schema
In order to make sure undhandled exceptions use this same response format, just register the following as your IExceptionHandler in your WebApi.config:

```C#
  config.Services.Replace(typeof(IExceptionHandler), new RidgidApiExceptionHandler());
```
 
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
 
