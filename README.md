# RIDGID.Common.Api
RIDGID.Common.Api is a REST Api response formatting system that adds an error ID to the response message when an error has occurred. It does model validation, and testing utilities are included.

## To Use In Your API
1. Make controller inherit from RidgidApiController
2. Add the `[RidgidModelValidation]` attribute to the controller method
3. Add any of the custom attributes to the property on the model being passed into the controller, e.g., `[RidgidRequired(ErrorId)]`
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
  
 ## To Build Nuget Package
 ```nuget pack TestingUtilities.csproj -Version 1.0.0.0 -properties Configuration=Release -IncludeReferencedProjects```
 
 
