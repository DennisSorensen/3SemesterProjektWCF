using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCF.BusinessLogicLayer;
using WCF.ModelLayer;

namespace WCFTest
{
    [TestClass]
    public class UserTest
    {

        [TestMethod]
        public void TestCreateUser()
        {
            //Arrange
            UserController userController = new UserController();
            
            //Act
            User testUser = new User(99, "Admin", "Bo", "Larsen", "Password", 1); //Laver en test user til og tjekke med

            userController.Create(testUser); //Laver en ny user, og smider testUser med, for oat se om den bliver oprettet
            
            //Assert
            User user = userController.Get(99); //Finder user med id 1
            
            Assert.AreEqual(user.Id, testUser.Id);            
            Assert.AreEqual(user.FirstName, testUser.FirstName);
            Assert.AreEqual(user.LastName, testUser.LastName);
            Assert.AreEqual(user.Role, testUser.Role);
            Assert.AreEqual(user.Password, testUser.Password);
            
        }
        
        [TestMethod]
        public void TestLoginFail()
        {
            //Arrange
            UserController userController = new UserController();

            //Act
            
            User user = userController.Login(99, "Forkert");
            //Assert

            Assert.IsNull(user);
        }


        [TestMethod]
        public void TestLogin()
        {
            //Arrange
            UserController userController = new UserController();

            //Act

            User user = userController.Login(99, "Password");
            //Assert

            Assert.IsNotNull(user);

        }
    }
}
