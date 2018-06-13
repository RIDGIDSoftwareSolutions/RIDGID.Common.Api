using System;
using System.Collections.Generic;
using System.Reflection;
using RIDGID.Common.Api.Core.Attributes;
using RIDGID.Common.Api.TestingUtilities.Exceptions;
using RIDGID.Common.Api.TestingUtilities.FieldValidations;
using Shouldly;

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

        private static void CheckValidation<T>(RidgidFieldValidation fieldValidation)
        {
            var property = GetPropertyForFieldName<T>(fieldValidation);
            switch (fieldValidation.ValidationType)
            {
                case RidgidValidationType.EmailAddressAttribute:
                {
                    ValidateEmailAttribute<T>(fieldValidation, property);
                    break;
                }
                case RidgidValidationType.MaxLengthAttribute:
                {
                    ValidateMaxLengthAttribute<T>(fieldValidation, property);
                    break;
                }
                case RidgidValidationType.MinLengthAttribute:
                {
                    ValidateMinLengthAttribute<T>(fieldValidation, property);
                    break;
                }
                case RidgidValidationType.RegularExpressionAttribute:
                {
                    ValidateRegularExpressionAttribute<T>(fieldValidation, property);
                    break;
                }
                case RidgidValidationType.RequiredAttribute:
                {
                    ValidateRequiredAttribute<T>(fieldValidation, property);
                    break;
                }
                case RidgidValidationType.StringLengthAttribute:
                {
                    ValidateStringLengthAttribute<T>(fieldValidation, property);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void ValidateStringLengthAttribute<T>(RidgidFieldValidation fieldValidation, PropertyInfo property)
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
        }

        private static void ValidateRequiredAttribute<T>(RidgidFieldValidation fieldValidation, PropertyInfo property)
        {
            var attribute = (RidgidRequiredAttribute) property.GetCustomAttribute(
                                typeof(RidgidRequiredAttribute), false) ??
                            throw new RidgidRequiredAttributeNotFoundException(property.Name);
            attribute.ErrorId.ShouldBe(fieldValidation.ErrorId);
            attribute.CustomErrorMessage.ShouldBe(fieldValidation.ErrorMessage);
        }

        private static void ValidateRegularExpressionAttribute<T>(RidgidFieldValidation fieldValidation, PropertyInfo property)
        {
            var attribute = (RidgidRegularExpressionAttribute) property.GetCustomAttribute(
                                typeof(RidgidRegularExpressionAttribute), true) ??
                            throw new RidgidRegularExpressionAttributeNotFoundException(property.Name);
            attribute.ErrorId.ShouldBe(fieldValidation.ErrorId);
            attribute.CustomErrorMessage.ShouldBe(fieldValidation.ErrorMessage);
            attribute.Pattern.ShouldBe(((RidgidRegularExpressionFieldValidation) fieldValidation).Regex);
        }

        private static void ValidateMinLengthAttribute<T>(RidgidFieldValidation fieldValidation, PropertyInfo property)
        {
            var attribute = (RidgidMinLengthAttribute) property.GetCustomAttribute(
                                typeof(RidgidMinLengthAttribute), true) ??
                            throw new RidgidMinLengthAttributeNotFoundException(property.Name);
            attribute.ErrorId.ShouldBe(fieldValidation.ErrorId);
            attribute.CustomErrorMessage.ShouldBe(fieldValidation.ErrorMessage);
            attribute.Length.ShouldBe(((RidgidMinLengthFieldValidation) fieldValidation).MinLength);
        }

        private static void ValidateMaxLengthAttribute<T>(RidgidFieldValidation fieldValidation, PropertyInfo property)
        {
            var attribute = (RidgidMaxLengthAttribute) property.GetCustomAttribute(
                                typeof(RidgidMaxLengthAttribute), true) ??
                            throw new RidgidMaxLengthAttributeNotFoundException(property.Name);
            attribute.ErrorId.ShouldBe(fieldValidation.ErrorId);
            attribute.CustomErrorMessage.ShouldBe(fieldValidation.ErrorMessage);
            attribute.Length.ShouldBe(((RidgidMaxLengthFieldValidation) fieldValidation).MaxLength);
        }

        private static void ValidateEmailAttribute<T>(RidgidFieldValidation fieldValidation, PropertyInfo property)
        {
            var attribute = (RidgidEmailAddressAttribute) property.GetCustomAttribute(
                                typeof(RidgidEmailAddressAttribute), false) ??
                            throw new RidgidEmailAddressAttributeNotFoundException(property.Name);
            attribute.ErrorId.ShouldBe(fieldValidation.ErrorId);
            attribute.CustomErrorMessage.ShouldBe(fieldValidation.ErrorMessage);
        }


        private static PropertyInfo GetPropertyForFieldName<T>(RidgidFieldValidation fieldValidation)
        {
            return typeof(T).GetProperty(fieldValidation.FieldName) ??
                   throw new FieldNotFoundException(fieldValidation.FieldName);
        }
    }
}
