using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Web.Mvc;
using BookOwner.Controllers;
using BookOwner.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using Newtonsoft.Json;

namespace BookOwnerTest
{
    public class Tests
    {
        private HomeController _homeController;

        [SetUp]
        public void SetUp()
        {
            _homeController = new HomeController();
        }

        [Test]
        public async Task TestHomeControllerBook()
        {
            var expectedOwner = new Owner
            {
                //Name="Ankita",
                //Age= 23,
                OwnerAdult = new List<book>

                { 

                new book { Name = "Book 1",Type="Hardcover" },
                new book { Name = "Book 2", Type="PaperCover"},
                new book { Name = "Book 3",Type="Hardcover" },
                new book { Name = "Book 4", Type="PaperCover"},
                new book { Name = "Book 5",Type="Hardcover" },
                new book { Name = "Book 6", Type="PaperCover"},

                },
                OwnerChild = new List<book>

                {

                new book { Name = "Book 7",Type="Hardcover"},

                new book { Name = "Book 8" ,Type="PaperCover"},

                new book { Name = "Book 9" ,Type="PaperCover"},

                new book { Name = "Book 9" ,Type="PaperCover"},


                },
                
            };
            
            var result = await _homeController.Book() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            int ActualAdult = 0;
            int ExpectedAdult = 0;
            if (result.ViewBag.Adults != null)
            {
                foreach (var owner in result.ViewBag.Adults)
                {
                    foreach (var books in owner.books)
                    {
                        ActualAdult++;


                    }
                }
                foreach (var expoadult in expectedOwner.OwnerAdult)
                {
                    ExpectedAdult++;
                }
                var actualcollection = ActualAdult;
                var expectedcollection = ExpectedAdult;


                Assert.AreEqual(expectedcollection, actualcollection);
            }
            else
            {
                Assert.Fail("Execution for " + NUnit.Framework.TestContext.Error + " is aborted as the  test case value not available.");

            }
            int ActualChild = 0;
            int ExpectedChild = 0;
            if (result.ViewBag.child != null)
            {
                foreach (var owner in result.ViewBag.child)
                {
                    foreach (var books in owner.books)
                    {
                        ActualChild++;


                    }
                }
                foreach (var expoadult in expectedOwner.OwnerChild)
                {
                    ExpectedChild++;
                }
                var actualchildcollection = ActualChild;
                var expectedchildcollection = ExpectedChild;


                Assert.AreEqual(expectedchildcollection, actualchildcollection);
            }
            else
            {
                Assert.Fail("Execution for " + NUnit.Framework.TestContext.Error + " is aborted as the  test case value not available.");

            }
            //CollectionAssert.AreEqual(expectedOwner.OwnerChild, ((Owner)result.ViewBag).OwnerChild);

        }

        [Test]
        public async Task TesthomeControllerError()
        {
            var expectedViewName = "Error";

            var result =  _homeController.Error() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(expectedViewName, result.ViewName);
        }
        public async Task TesthomeControllerAllBook()
        {
            var expectedViewName = "AllBook";

            var result = _homeController.Error() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(expectedViewName, result.ViewName);
        }

        public async Task TesthomeControllerHardCoverBooks()
        {
            var expectedViewName = "HardCoverBooks";

            var result = _homeController.Error() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(expectedViewName, result.ViewName);
        }
    }
}
