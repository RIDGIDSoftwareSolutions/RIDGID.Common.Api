using System;
using System.Collections.Generic;
using System.Reflection;
using RidgidApiErrorHandlerTestingUtilities.FieldValidation;
using RidgidApiObjects.Attributes;
using RidgidApiObjects.Exceptions;

namespace RidgidApiErrorHandlerTestingUtilities.Extensions
{
    public static class ShouldValidateTheseFieldsExtension
    {
        public static void ShouldValidateTheseFields<TModelType>(this TModelType model, List<RidgidFieldValidation> fieldValidations)
        {
            foreach (var fieldValidation in fieldValidations)
            {
                CheckValidation<TModelType>(fieldValidation);
            }
        }

        private static void CheckValidation<T>(RidgidFieldValidation fieldValidation)
        {
            var property = GetPropertyForFieldName<T>(fieldValidation);
            switch (fieldValidation.ValidationType)
            {
                case RidgidValidationType.EmailAddressAttribute:
                {
                    var attribute = (RidgidEmailAddressAttribute) property.GetCustomAttribute(
                                        typeof(RidgidEmailAddressAttribute), false) ??
                                    throw new RidgidEmailAddressAttributeNotFoundException(property.Name);
                    attribute.ErrorId.ShouldBe(fieldValidation.ErrorId);
                    attribute.CustomErrorMessage.ShouldBe(fieldValidation.ErrorMessage);
                    break;
                }
                case RidgidValidationType.MaxLengthAttribute:
                {
                    var attribute = (RidgidMaxLengthAttribute) property.GetCustomAttribute(
                                        typeof(RidgidMaxLengthAttribute), true) ??
                                    throw new RidgidMaxLengthAttributeNotFoundException(property.Name);
                    attribute.ErrorId.ShouldBe(fieldValidation.ErrorId);
                    attribute.CustomErrorMessage.ShouldBe(fieldValidation.ErrorMessage);
                    attribute.Length.ShouldBe(((RidgidMaxLengthFieldValidation) fieldValidation).MaxLength);
                    break;
                }
                case RidgidValidationType.MinLengthAttribute:
                {
                    var attribute = (RidgidMinLengthAttribute) property.GetCustomAttribute(
                                        typeof(RidgidMinLengthAttribute), true) ??
                                    throw new RidgidMinLengthAttributeNotFoundException(property.Name);
                    attribute.ErrorId.ShouldBe(fieldValidation.ErrorId);
                    attribute.CustomErrorMessage.ShouldBe(fieldValidation.ErrorMessage);
                    attribute.Length.ShouldBe(((RidgidMinLengthFieldValidation) fieldValidation).MinLength);
                    break;
                }
                case RidgidValidationType.RegularExpressionAttribute:
                {
                    var attribute = (RidgidRegularExpressionAttribute) property.GetCustomAttribute(
                                        typeof(RidgidRegularExpressionAttribute), true) ??
                                    throw new RidgidRegularExpressionAttributeNotFoundException(property.Name);
                    attribute.ErrorId.ShouldBe(fieldValidation.ErrorId);
                    attribute.CustomErrorMessage.ShouldBe(fieldValidation.ErrorMessage);
                    attribute.Pattern.ShouldBe(((RidgidRegularExpressionFieldValidation) fieldValidation).Regex);
                    break;
                }
                case RidgidValidationType.RequiredAttribute:
                {
                    var attribute = (RidgidRequiredAttribute) property.GetCustomAttribute(
                                        typeof(RidgidRequiredAttribute), false) ??
                                    throw new RidgidRequiredAttributeNotFoundException(property.Name);
                    attribute.ErrorId.ShouldBe(fieldValidation.ErrorId);
                    attribute.CustomErrorMessage.ShouldBe(fieldValidation.ErrorMessage);
                    break;
                }
                case RidgidValidationType.StringLengthAttribute:
                {
                    var attribute = (RidgidStringLengthAttribute) property.GetCustomAttribute(
                                        typeof(RidgidStringLengthAttribute), false) ??
                                    throw new RidgidStringLengthAttributeNotFoundException(property.Name);
                    attribute.ErrorId.ShouldBe(fieldValidation.ErrorId);
                    attribute.CustomErrorMessage.ShouldBe(fieldValidation.ErrorMessage);
                    attribute.MininumLength.ShouldBe(((RidgidStringLengthFieldValidation) fieldValidation)
                        .MinLength);
                    attribute.MaximumLength.ShouldBe(((RidgidStringLengthFieldValidation) fieldValidation)
                        .MaxLength);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static PropertyInfo GetPropertyForFieldName<T>(RidgidFieldValidation fieldValidation)
        {
            return typeof(T).GetProperty(fieldValidation.FieldName) ??
                   throw new FieldNotFoundException(fieldValidation.FieldName);
        }
    }
}