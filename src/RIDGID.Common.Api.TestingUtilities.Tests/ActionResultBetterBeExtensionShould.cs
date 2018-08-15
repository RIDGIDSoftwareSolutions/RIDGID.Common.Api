using Newtonsoft.Json;
using NUnit.Framework;
using RIDGID.Common.Api.Core;
using RIDGID.Common.Api.Core.Objects;
using RIDGID.Common.Api.Core.Utilities;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace RIDGID.Common.Api.TestingUtilities.Tests
{
    [TestFixture]
    public class ActionResultBetterBeExtensionShould
    {
        [Test]
        public void TestResponseAssertsTrueForValidErrorMessage()
        {
            //--Arrange
            var actionResult =
                new RidgidApiController().HttpGenericErrorResponse(1, "ErrorMessage", HttpStatusCode.BadRequest);


            var expectedResult = new ErrorsResponse
            {
                Errors = new List<ErrorMessage>
                {
                    new ErrorMessage
                    {
                        ErrorId = 1,
                        DebugErrorMessage = "ErrorMessage"
                    }
                }
            };
            //--Act/Assert
            actionResult.BetterBe(HttpStatusCode.BadRequest, expectedResult);
        }

        [Test]
        public void TestResponseAssertsFalseForInvalidErrorMessage()
        {
            //--Arrange
            var actionResult =
                new RidgidApiController().HttpGenericErrorResponse(1, "ErrorMessage", HttpStatusCode.BadRequest);


            var expectedResult = new ErrorsResponse
            {
                Errors = new List<ErrorMessage>
                {
                    new ErrorMessage
                    {
                        ErrorId = 1,
                        DebugErrorMessage = "ErrorMessage1"
                    }
                }
            };

            //--Act/Assert
            Should.Throw<ShouldAssertException>(() => actionResult.BetterBe(HttpStatusCode.BadRequest, expectedResult));
        }

        [Test]
        public void TestResponseAssertsFalseForInvalidErrorId()
        {
            //--Arrange
            var actionResult =
                new RidgidApiController().HttpGenericErrorResponse(1, "ErrorMessage", HttpStatusCode.BadRequest);


            var expectedResult = new ErrorsResponse
            {
                Errors = new List<ErrorMessage>
                {
                    new ErrorMessage
                    {
                        ErrorId = 2,
                        DebugErrorMessage = "ErrorMessage"
                    }
                }
            };

            //--Act/Assert
            Should.Throw<ShouldAssertException>(() => actionResult.BetterBe(HttpStatusCode.BadRequest, expectedResult));
        }


        [Test]
        public void TestResponseAssertsTrueForMatchingObjects()
        {
            //--Arrange
            var expectedResult = new TestObject
            {
                TestField = "hola"
            };

            var actionResult = new HttpGenericResult(new RidgidApiController(), HttpStatusCode.BadRequest, expectedResult);

            //--Act/Assert
            actionResult.BetterBe(HttpStatusCode.BadRequest, expectedResult);
        }

        [Test]
        public void TestResponseAssertsFalseForNotMatchingObjects()
        {
            //--Arrange
            var expectedResult = new TestObject
            {
                TestField = "hola"
            };

            var returnedResult = new TestObject()
            {
                TestField = "hey"
            };

            var actionResult = new HttpGenericResult(new RidgidApiController(), HttpStatusCode.BadRequest, returnedResult);

            //--Act/Assert
            Should.Throw<ShouldAssertException>(() => actionResult.BetterBe(HttpStatusCode.BadRequest, expectedResult));
        }

        [Test]
        public void TestResponseAssertsFalseForNotMatchingObjectsOfDifferentTypes()
        {
            //--Arrange
            var expectedResult = new TestObject
            {
                TestField = "hola"
            };

            var returnedResult = "hey";

            var actionResult = new HttpGenericResult(new RidgidApiController(), HttpStatusCode.BadRequest, returnedResult);

            //--Act/Assert
            Should.Throw<JsonSerializationException>(() => actionResult.BetterBe(HttpStatusCode.BadRequest, expectedResult));
        }

        [Test]
        public void TestResponseAssertsTrueForMatchingObjectsWithBaseClassThatAlsoHasBaseClassThatDoesNotHaveAFieldSet()
        {
            //--Arrange
            var expectedResult = new TestMessage
            {
                Email = "hola",
                FirstName = "hola1",
                Username = "hola2",
                LastName = "hola3"
            };

            var actionResult = new HttpGenericResult(new RidgidApiController(), HttpStatusCode.BadRequest, expectedResult);

            //--Act/Assert
            actionResult.BetterBe(HttpStatusCode.BadRequest, expectedResult);
        }

        [Test]
        public void TestResponseAssertsFalseForNotMatchingObjectsWithBaseClass()
        {
            //--Arrange
            var expectedResult = new TestMessage
            {
                Email = "hola",
                FirstName = "hola1",
                Username = "hola2",
                LastName = "hola3"
            };

            var returnedResult = new TestMessage
            {
                Email = "hola",
                FirstName = "hola1",
                Username = "hola2",
                LastName = "hola4"
            };

            var actionResult = new HttpGenericResult(new RidgidApiController(), HttpStatusCode.BadRequest, returnedResult);

            //--Act/Assert
            Should.Throw<ShouldAssertException>(() => actionResult.BetterBe(HttpStatusCode.BadRequest, expectedResult));
        }

        [Test]
        public void TestResponseAssertsFalseForMatchingObjectsWithBaseClassThatHasBaseClassToo()
        {
            //--Arrange
            var expectedResult = new TestMessage
            {
                Email = "hola",
                FirstName = "hola1",
                Username = "hola2",
                LastName = "hola3",
                Id = "hola5"
            };

            var returnedResult = new TestMessage
            {
                Email = "hola",
                FirstName = "hola1",
                Username = "hola2",
                LastName = "hola3",
                Id = "hola4"
            };

            var actionResult = new HttpGenericResult(new RidgidApiController(), HttpStatusCode.BadRequest, returnedResult);

            //--Act/Assert
            Should.Throw<ShouldAssertException>(() => actionResult.BetterBe(HttpStatusCode.BadRequest, expectedResult));
        }


        [Test]
        public void StillWorksWhenFieldIsEmpty()
        {
            //--Arrange
            var actionResult = new HttpGenericResult(new RidgidApiController(), HttpStatusCode.BadRequest, null);

            //--Act/Assert
            actionResult.BetterBeNull(HttpStatusCode.BadRequest);
        }


        [Test]
        public void StillThrowsExceptionWhenFieldIsEmpty()
        {
            //--Arrange
            var actionResult = new HttpGenericResult(new RidgidApiController(), HttpStatusCode.Conflict, null);

            //--Act/Assert
            Should.Throw<ShouldAssertException>(() => actionResult.BetterBeNull(HttpStatusCode.BadRequest));
        }


        [Test]
        public void ReturnAssertExceptionForNotMatchingResponseThatContainsList()
        {
            //--Arrange
            var expectedResult = new TestObjectThatContainsList
            {
                ItemList = new List<TestObject>
                {
                    new TestObject
                    {
                        TestField = "Hello"
                    }
                }
            };

            var returnedResult = new TestObjectThatContainsList
            {
                ItemList = new List<TestObject>
                {
                    new TestObject
                    {
                        TestField = "Hallo"
                    }
                }
            };

            var actionResult = new HttpGenericResult(new RidgidApiController(), HttpStatusCode.BadRequest, returnedResult);

            //--Act/Assert
            Should.Throw<ShouldAssertException>(() => actionResult.BetterBe(HttpStatusCode.BadRequest, expectedResult));
        }

        [Test]
        public void NotThrowExceptionForMatchingResponseThatContainsList()
        {
            //--Arrange
            var expectedResult = new TestObjectThatContainsList
            {
                ItemList = new List<TestObject>
                {
                    new TestObject
                    {
                        TestField = "Hello"
                    }
                }
            };

            var actionResult = new HttpGenericResult(new RidgidApiController(), HttpStatusCode.BadRequest, expectedResult);

            //--Act/Assert
            actionResult.BetterBe(HttpStatusCode.BadRequest, expectedResult);
        }


        [Test]
        public void ReturnAssertExceptionForNotMatchingResponseThatContainsListInsideList()
        {
            //--Arrange
            var expectedResult = new TestObjectThatContainsListInsideList
            {
                ParentList = new List<TestObjectThatContainsList>
                {
                    new TestObjectThatContainsList
                    {
                        ItemList = new List<TestObject>
                        {
                            new TestObject
                            {
                                TestField = "Hello"
                            }
                        }
                    }
                }
            };

            var returnedResult = new TestObjectThatContainsListInsideList
            {
                ParentList = new List<TestObjectThatContainsList>
                {
                    new TestObjectThatContainsList
                    {
                        ItemList = new List<TestObject>
                        {
                            new TestObject
                            {
                                TestField = "Hallo"
                            }
                        }
                    }
                }
            };

            var actionResult = new HttpGenericResult(new RidgidApiController(), HttpStatusCode.BadRequest, returnedResult);

            //--Act/Assert
            Should.Throw<ShouldAssertException>(() => actionResult.BetterBe(HttpStatusCode.BadRequest, expectedResult));
        }

        [Test]
        public void NotReturnAssertExceptionForMatchingResponseThatContainsListInsideList()
        {
            //--Arrange
            var expectedResult = new TestObjectThatContainsListInsideList
            {
                ParentList = new List<TestObjectThatContainsList>
                {
                    new TestObjectThatContainsList
                    {
                        ItemList = new List<TestObject>
                        {
                            new TestObject
                            {
                                TestField = "Hello"
                            }
                        }
                    }
                }
            };
            var actionResult =
                new HttpGenericResult(new RidgidApiController(), HttpStatusCode.BadRequest, expectedResult);

            //--Act/Assert
            actionResult.BetterBe(HttpStatusCode.BadRequest, expectedResult);
        }

        [Test]
        public void ReturnAssertExceptionForMatchingResponseThatIsAListNotAnObject()
        {
            //--Arrange
            var expectedResult = new List<TestObject>
            {
                new TestObject
                {
                    TestField = "Hello"
                }
            };
            var actualResult = new List<TestObject>
            {
                new TestObject
                {
                    TestField = "HelloNo!"
                }
            };

            var actionResult =
                new HttpGenericResult(new RidgidApiController(), HttpStatusCode.BadRequest, expectedResult);

            //--Act/Assert
            Should.Throw<ShouldAssertException>(() => actionResult.BetterBe(HttpStatusCode.BadRequest, actualResult));
        }

        [Test]
        public void AssertFalseForInvalidLocationHeader()
        {
            //--Arrange
            IHttpActionResult actionResult = new TestActionResult();

            //--Act/Assert
            Should.Throw<ShouldAssertException>(() => actionResult.BetterBeNull(HttpStatusCode.Created, "https://notlink"));
        }

        [Test]
        public void AssertTrueForValidLocationHeader()
        {
            //--Arrange
            IHttpActionResult actionResult = new TestActionResult();

            //--Act/Assert
            actionResult.BetterBeNull(HttpStatusCode.Created, "https://link");
        }

        [Test]
        public void GetFieldValuesForModelReturnsCorrectNumberOfFieldsForTestFieldObjectWithBaseClass()
        {
            //--Arrange
            var expectedResult = new TestMessage
            {
                Email = "hola",
                FirstName = "hola1",
                Username = "hola2",
                LastName = "hola3"
            };

            //--Act
            var result = ActionResultBetterBeExtension.GetFieldValuesForModel(expectedResult);

            //--Assert
            result.Count().ShouldBe(5);
        }
    }

    public class TestActionResult : IHttpActionResult
    {
        internal HttpResponseMessage Execute()
        {
            var response = FormatResponseMessage.CreateMessage(null, HttpStatusCode.Created);
            response.RequestMessage = new HttpRequestMessage();
            response.Headers.Location = new Uri("https://link");
            return response;
        }

        public Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }
    }
    public class TestObjectThatContainsListInsideList
    {
        public List<TestObjectThatContainsList> ParentList { get; set; }
    }

    public class TestObjectThatContainsList
    {
        public List<TestObject> ItemList { get; set; }
    }

    public class TestObject
    {
        public string TestField { get; set; }
    }

    public class TestBaseMessageBaseMessage
    {
        public string Id { get; set; }
    }

    public class TestMessage : TestBaseMessage
    {
        public string Email { get; set; }

        public string Username { get; set; }
    }

    public class TestBaseMessage : TestBaseMessageBaseMessage
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
