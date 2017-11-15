﻿using System;
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
            User testUser = new User(1, "Admin", "Bo", "Larsen", "Password"); //Laver en test user til og tjekke med

            userController.Create(testUser); //Laver en ny user, og smider testUser med, for oat se om den bliver oprettet
            
            //Assert
            User user = userController.Get(1); //Finder user med id 1
            

            //Den virker ikke rigtig
            //Assert.Equals(user, testUser); //Sammenligner user med den lavet testUser

            //Den skulle virke
            Assert.AreEqual(user.Id, testUser.Id);            
            Assert.AreEqual(user.FirstName, testUser.FirstName);
            Assert.AreEqual(user.LastName, testUser.LastName);
            Assert.AreEqual(user.Role, testUser.Role);
            Assert.AreEqual(user.Password, testUser.Password);
            
        }
    }
}
