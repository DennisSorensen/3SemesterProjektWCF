using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClientTest
{
    [TestClass]
    public class CreateUserTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            BookController bookController = new BookController();

            //Act
            bookController.Create("12345678910", "TestBook", "Gruppe 3", 99.95, 3);

            Book testBook = new Book("12345678910", "TestBook", "Gruppe 3", 99.95, 3); //Opretter bog objekt til og tjekke med.

            //Assert
            Book book = bookController.Find("12345678910");
            Assert.ReferenceEquals(book, testBook); //Tjekker om de to objekter er ens.
            //Assert.AreEqual("TestBook", book.Title);  Er kun til numeriske værdier.

        }

        [TestMethod]
        public void TestCreateUser()
        {
            //Arrange
            UserController userController = new UserController();

            //Act

            //Assert

        }
    }
}
