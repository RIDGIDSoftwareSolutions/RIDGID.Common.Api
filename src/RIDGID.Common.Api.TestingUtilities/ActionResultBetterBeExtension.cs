using Newtonsoft.Json;
using RIDGID.Common.Api.Core.Objects;
using RIDGID.Common.Api.Core.Utilities;
using Shouldly;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Web.Http;

namespace RIDGID.Common.Api.TestingUtilities
{
    public static class ActionResultBetterBeExtension
    {
        public static void BetterBeNull(this IHttpActionResult actionResult, HttpStatusCode expectedStatusCode,
            string expectedLocationHeader = null)
        {
            Content(actionResult, expectedStatusCode, true);
            CheckLocationHeader(actionResult, expectedLocationHeader);
        }

        public static void BetterBe(this IHttpActionResult actionResult, HttpStatusCode expectedStatusCode,
            string expectedDebugErrorMessage, int expectedErrorId)
        {
            BetterBe(actionResult, expectedStatusCode, new ErrorsResponse(expectedDebugErrorMessage, expectedErrorId));
        }
        // Checks that an actionresult is equal to the expected result, and that the status code and optional location
        // header are what is expected
        public static void BetterBe<TModelType>(this IHttpActionResult actionResult,
            HttpStatusCode expectedStatusCode, TModelType expectedResult, string expectedLocationHeader = null)
        {
            var contentAsString = Content(actionResult, expectedStatusCode, false);
            TModelType returnedModel;
            try
            {
                returnedModel = JsonConvert.DeserializeObject<TModelType>(contentAsString,
                    FormatResponseMessage.JsonSerializerSetting());
            }
            catch (JsonSerializationException)
            {
                throw new JsonSerializationException($"{contentAsString} could not be deserialized into the expected object.");
            }
            returnedModel.ShouldNotBeNull();

            AssertThatTwoObjectsAreTheSame(returnedModel, expectedResult);

            CheckLocationHeader(actionResult, expectedLocationHeader);
        }

        private static void AssertThatTwoObjectsAreTheSame<TModelType>(TModelType returnedResult, TModelType expectedResult)
        {
            if (expectedResult == null)
            {
                returnedResult.ShouldBeNull();
            }
            else if (IsSimpleType(expectedResult.GetType()))
            {
                returnedResult.ShouldBe(expectedResult);
            }
            else if (FieldIsEnumerable(expectedResult))
            {
                var returnedList = ((IEnumerable<object>)returnedResult).ToList();
                var expectedList = ((IEnumerable<object>)expectedResult).ToList();

                for (var i = 0; i < returnedList.Count; i++)
                {
                    AssertThatTwoObjectsAreTheSame(returnedList[i], expectedList[i]);
                }
            }
            else // is complex type
            {
                var expectedFieldValues = GetFieldValuesForModel(expectedResult).ToList();
                var returnedFieldValues = GetFieldValuesForModel(returnedResult).ToList();

                for (var i = 0; i < returnedFieldValues.Count; i++)
                {
                    AssertThatTwoObjectsAreTheSame(returnedFieldValues[i], expectedFieldValues[i]);
                }
            }
        }

        private static void CheckLocationHeader(IHttpActionResult actionResult, string expectedLocationHeader)
        {
            if (expectedLocationHeader != null)
            {
                actionResult.ExecuteAsync(new CancellationToken()).Result.Headers.Location
                    .ShouldBe(new Uri(expectedLocationHeader));
            }
        }

        private static string Content(IHttpActionResult actionResult, HttpStatusCode expectedStatusCode, bool contentShouldBeNull)
        {
            var result = actionResult.ExecuteAsync(new CancellationToken()).Result;
            result.StatusCode.ShouldBe(expectedStatusCode);
            if (contentShouldBeNull)
            {
                result.Content.ShouldBeNull();
                return null;
            }
            var contentAsString = result.Content.ReadAsStringAsync().Result;
            contentAsString.ShouldNotBeNull();
            contentAsString.Length.ShouldBeGreaterThan(0);
            return contentAsString;
        }

        private static bool FieldIsEnumerable(object field)
        {
            if (field is string || field == null)
            {
                return false;
            }
            var type = field.GetType();
            var interfaces = type.GetInterfaces();
            var isEnumerable = interfaces.Contains(typeof(IEnumerable));
            return isEnumerable;
        }

        public static IEnumerable<object> GetFieldValuesForModel<TModelType>(TModelType model)
        {
            var modelTypeInfo = model.GetType().GetTypeInfo();
            var properties = modelTypeInfo.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static).ToList();

            var fieldValues = new List<object>();
            foreach (var field in properties)
            {
                fieldValues.Add(field.GetValue(model));
            }
            return fieldValues;
        }

        private static bool IsSimpleType(Type type)
        {
            return
                type.IsPrimitive ||
                new[]
                {
                    typeof(Enum),
                    typeof(string),
                    typeof(decimal),
                    typeof(DateTime),
                    typeof(DateTimeOffset),
                    typeof(TimeSpan),
                    typeof(Guid)
                }.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object ||
                (type.IsGenericParameter && type.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                 IsSimpleType(type.GetGenericArguments()[0]));
        }
    }
}