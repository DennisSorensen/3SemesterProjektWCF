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
            userController.Create(1, "Admin", "Person", "Password"); //Laver en ny user

            User testUser = new User(1, "Admin", "Person", "Password"); //Laver en test user til og tjekke med

            //Assert
            User user = userController.Get(1); //Finder user med id 1
            

            //Den virker ikke rigtig
            //Assert.Equals(user, testUser); //Sammenligner user med den lavet testUser
            //Den skulle virke
            Assert.AreEqual(user.Id, testUser.Id);
            Assert.AreEqual(user.Name, testUser.Name);
            Assert.AreEqual(user.Role, testUser.Role);
            Assert.AreEqual(user.Password, testUser.Password);
        }
    }
}
