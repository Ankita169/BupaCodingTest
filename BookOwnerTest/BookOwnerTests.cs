using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Web.Mvc;
using BookOwner.Controllers;
using BookOwner.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

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
                Name="Ankita",
                Age= 23,
                OwnerAdult = new List<book>

                { 

                new book { Name = "Book 1",Type="Hardcover" },

                new book { Name = "Book 2", Type="PaperCover"},
                
                },
                OwnerChild = new List<book>

                {

                new book { Name = "Book 3",Type="Hardcover"},

                new book { Name = "Book 4" ,Type="PaperCover"},
               

                },
                Books = new List<book>

                {

                new book { Name = "Book 1", Type = "HardCover" },

                new book { Name = "Book 2", Type = "Paper" },

               
                }
            };
            var result = await _homeController.Book() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(expectedOwner.Name, ((Owner)result.Model).Name);
            Assert.AreEqual(expectedOwner.Age, ((Owner)result.Model).Age);
            CollectionAssert.AreEqual(expectedOwner.OwnerAdult, ((Owner)result.Model).OwnerAdult);

            CollectionAssert.AreEqual(expectedOwner.OwnerChild, ((Owner)result.Model).OwnerChild);

            CollectionAssert.AreEqual(expectedOwner.Books, ((Owner)result.Model).Books);
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
