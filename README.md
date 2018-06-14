# RIDGID.Common.Api

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
