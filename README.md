# RIDGID.Common.Api
RIDGID.Common.Api is a REST Api response formatting system that adds an error ID to the response message when an error has occurred. It does model validation and testing utilities are included.

## To Use In Your API
1. Make controller inherit from RidgidApiController
2. Create ErrorId Object (optional if you want to hardcode instead)
3. Create ErrorMessages Content (optional if you want to hardcode instead)
4. Add the `[RidgidModelValidation]` attribute to the controller method
5. Add any of the custom attributes to the property on the model being passed into the controller, e.g., `[RidgidRequired(ErrorId)]`
6. Write a Unit Test to test the model by doing:

    ```c#
      model.ShouldValidateTheseFields {
      `new RidgidRequiredFieldValidation {
        ErrorId = errorId,
        FieldName = "FieldName"
      },
      ...
    }
    ```

using one of the RidgidFieldValidation subclasses, e.g., RidgidRequiredFieldValidation, RidgidStringLengthFieldValidation

7. For non model validation error responses use:
    ```
    return Conflict(1, "message");
    return BadRequest(1, "message");
    return NotFound(1, "message");
    ```
  
  Etc.
  
 ## To Build Nuget Package
 ```nuget pack TestingUtilities.csproj -Version 1.0.0.0 -properties Configuration=Release -IncludeReferencedProjects```
 
 
