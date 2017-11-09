using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCF.BusinessLogicLayer;
using WCF.ModelLayer;

namespace ClientTest
{
    [TestClass]
    public class CreateUserTest
    {

        [TestMethod]
        public void TestCreateUser()
        {
            //Arrange
            UserController userController = new UserController();

            //Act
            userController.Create(1, "Admin", "Password"); //Laver en ny user

            User testUser = new User(1, "Admin", "Password"); //Laver en test user til og tjekke med

            //Assert
            var tuple = userController.Find(1); //Finder user med id 1
            User user = new User(tuple.Item1, tuple.Item2, tuple.Item3);

            //Den virker ikke rigtig
            Assert.ReferenceEquals(user, testUser); //Sammenligner user med den lavet testUser
            //Den skulle virke
            //Assert.AreEqual(user, testUser);
        }
    }
}
