using RIDGID.Common.Api.Core.Attributes;
using RIDGID.Common.Api.Core.Exceptions;
using RIDGID.Common.Api.TestingUtilities.Exceptions;
using RIDGID.Common.Api.TestingUtilities.FieldValidations;
using Shouldly;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace RIDGID.Common.Api.TestingUtilities
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

        private static void CheckValidation<TModelType>(RidgidFieldValidation fieldValidation)
        {
            ValidateAttributeTypes<TModelType>();

            var property = GetPropertyForFieldName<TModelType>(fieldValidation);

            switch (fieldValidation.ValidationType)
            {
                case RidgidValidationType.EmailAddressAttribute:
                    {
                        ValidateEmailAttribute(fieldValidation, property);
                        break;
                    }
                case RidgidValidationType.MaxLengthAttribute:
                    {
                        ValidateMaxLengthAttribute(fieldValidation, property);
                        break;
                    }
                case RidgidValidationType.MinLengthAttribute:
                    {
                        ValidateMinLengthAttribute(fieldValidation, property);
                        break;
                    }
                case RidgidValidationType.RegularExpressionAttribute:
                    {
                        ValidateRegularExpressionAttribute(fieldValidation, property);
                        break;
                    }
                case RidgidValidationType.RequiredAttribute:
                    {
                        ValidateRequiredAttribute(fieldValidation, property);
                        break;
                    }
                case RidgidValidationType.StringLengthAttribute:
                    {
                        ValidateStringLengthAttribute(fieldValidation, property);
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void ValidateAttributeTypes<TModelType>()
        {
            foreach (var property in GetProperties<TModelType>())
            {
                if (property.GetCustomAttributes(typeof(RidgidValidationAttribute), false).Length >
                    property.GetCustomAttributes(typeof(ValidationAttribute), false).Length)
                {
                    throw new InvalidModelAttributesException();
                }
            }
        }

        private static void ValidateStringLengthAttribute(RidgidFieldValidation fieldValidation, PropertyInfo property)
        {
            var attribute = (RidgidStringLengthAttribute)property.GetCustomAttribute(
                                typeof(RidgidStringLengthAttribute), false) ??
                            throw new RidgidStringLengthAttributeNotFoundException(property.Name);
            attribute.ErrorId.ShouldBe(fieldValidation.ErrorId);
            attribute.CustomErrorMessage.ShouldBe(fieldValidation.ErrorMessage);
            attribute.MininumLength.ShouldBe(((RidgidStringLengthFieldValidation)fieldValidation)
                .MinLength);
            attribute.MaximumLength.ShouldBe(((RidgidStringLengthFieldValidation)fieldValidation)
                .MaxLength);
        }

        private static void ValidateRequiredAttribute(RidgidFieldValidation fieldValidation, PropertyInfo property)
        {
            var attribute = (RidgidRequiredAttribute)property.GetCustomAttribute(
                                typeof(RidgidRequiredAttribute), false) ??
                            throw new RidgidRequiredAttributeNotFoundException(property.Name);
            attribute.ErrorId.ShouldBe(fieldValidation.ErrorId);
            attribute.CustomErrorMessage.ShouldBe(fieldValidation.ErrorMessage);
        }

        private static void ValidateRegularExpressionAttribute(RidgidFieldValidation fieldValidation, PropertyInfo property)
        {
            var attribute = (RidgidRegularExpressionAttribute)property.GetCustomAttribute(
                                typeof(RidgidRegularExpressionAttribute), true) ??
                            throw new RidgidRegularExpressionAttributeNotFoundException(property.Name);
            attribute.ErrorId.ShouldBe(fieldValidation.ErrorId);
            attribute.CustomErrorMessage.ShouldBe(fieldValidation.ErrorMessage);
            attribute.Regex.ShouldBe(((RidgidRegularExpressionFieldValidation)fieldValidation).Regex);
        }

        private static void ValidateMinLengthAttribute(RidgidFieldValidation fieldValidation, PropertyInfo property)
        {
            var attribute = (RidgidMinLengthAttribute)property.GetCustomAttribute(
                                typeof(RidgidMinLengthAttribute), true) ??
                            throw new RidgidMinLengthAttributeNotFoundException(property.Name);
            attribute.ErrorId.ShouldBe(fieldValidation.ErrorId);
            attribute.CustomErrorMessage.ShouldBe(fieldValidation.ErrorMessage);
            attribute.Length.ShouldBe(((RidgidMinLengthFieldValidation)fieldValidation).MinLength);
        }

        private static void ValidateMaxLengthAttribute(RidgidFieldValidation fieldValidation, PropertyInfo property)
        {
            var attribute = (RidgidMaxLengthAttribute)property.GetCustomAttribute(
                                typeof(RidgidMaxLengthAttribute), true) ??
                            throw new RidgidMaxLengthAttributeNotFoundException(property.Name);
            attribute.ErrorId.ShouldBe(fieldValidation.ErrorId);
            attribute.CustomErrorMessage.ShouldBe(fieldValidation.ErrorMessage);
            attribute.Length.ShouldBe(((RidgidMaxLengthFieldValidation)fieldValidation).MaxLength);
        }

        private static void ValidateEmailAttribute(RidgidFieldValidation fieldValidation, PropertyInfo property)
        {
            var attribute = (RidgidEmailAddressAttribute)property.GetCustomAttribute(
                                typeof(RidgidEmailAddressAttribute), false) ??
                            throw new RidgidEmailAddressAttributeNotFoundException(property.Name);
            attribute.ErrorId.ShouldBe(fieldValidation.ErrorId);
            attribute.CustomErrorMessage.ShouldBe(fieldValidation.ErrorMessage);
        }


        private static PropertyInfo GetPropertyForFieldName<TModelType>(RidgidFieldValidation fieldValidation)
        {
            return typeof(TModelType).GetProperty(fieldValidation.FieldName) ??
                   throw new FieldNotFoundException(fieldValidation.FieldName);
        }

        private static PropertyInfo[] GetProperties<TModelType>()
        {
            return typeof(TModelType).GetProperties();
        }
    }
}
