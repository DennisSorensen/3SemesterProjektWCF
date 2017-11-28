using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCF.BusinessLogicLayer;
using WCF.ModelLayer;

namespace WCFTest
{
    [TestClass]
    public class BookingTest
    {
        [TestMethod]
        public void TestCreateBookingSupportTask()
        {
            /*
            //Arrange
            TaskController taskController = new TaskController();

            DateTime startDate = new DateTime(2017, 12, 24);
            DateTime endDate = new DateTime(2017, 12, 24);
            //Act
            SupportTask testSupportTask = new SupportTask(startDate, endDate, "Task", 99, 99, "Test", "Hjælp Test");

            taskController.Create(testSupportTask);

            //Assert
            SupportTask supportTask = taskController.Get();

            Assert.AreEqual(supportTask.Id, testSupportTask.Id);
            Assert.AreEqual(supportTask.StartDate, testSupportTask.StartDate);
            Assert.AreEqual(supportTask.EndDate, testSupportTask.EndDate);
            Assert.AreEqual(supportTask.BookingType, testSupportTask.BookingType);
            Assert.AreEqual(supportTask.User_Id, testSupportTask.User_Id);
            Assert.AreEqual(supportTask.Calendar_Id, testSupportTask.Calendar_Id);
            Assert.AreEqual(supportTask.Name, testSupportTask.Name);
            Assert.AreEqual(supportTask.Description, testSupportTask.Description);
            */
        }

        [TestMethod]
        public void TestCreateBookingSupportBooking()
        {
            /*
            //Arrange
            SupportBookingController supportBookingController = new SupportBookingController();

            DateTime startDate = new DateTime(2017, 12, 24);
            DateTime endDate = new DateTime(2017, 12, 24);
            //Act
            SupportBooking testSupportBooking = new SupportBooking(startDate, endDate, "Task", 99, 99, "Bo", "Jensen", 99999999, "TestDescription");

            supportBookingController.Create(testSupportBooking);

            //Assert
            SupportBooking supportTask = supportBookingController.Get();

            Assert.AreEqual(supportTask.Id, testSupportBooking.Id);
            Assert.AreEqual(supportTask.StartDate, testSupportBooking.StartDate);
            Assert.AreEqual(supportTask.EndDate, testSupportBooking.EndDate);
            Assert.AreEqual(supportTask.BookingType, testSupportBooking.BookingType);
            Assert.AreEqual(supportTask.User_Id, testSupportBooking.User_Id);
            Assert.AreEqual(supportTask.Calendar_Id, testSupportBooking.Calendar_Id);
            Assert.AreEqual(supportTask.FirstName, testSupportBooking.FirstName);
            Assert.AreEqual(supportTask.LastName, testSupportBooking.LastName);
            Assert.AreEqual(supportTask.Phone, testSupportBooking.Phone);
            Assert.AreEqual(supportTask.Description, testSupportBooking.Description);
            */
        }

        [TestMethod]
        public void TestCreateBookingReadyToGo()
        {
            /*
            //Arrange
            ReadyToGoController readyToGoController = new ReadyToGoController();

            DateTime startDate = new DateTime(2017, 12, 24);
            DateTime endDate = new DateTime(2017, 12, 24);
            //Act
            ReadyToGo testReadyToGo = new ReadyToGo(startDate, endDate, "Task", 99, 1, "ProductNrTest", 1, true);

            readyToGoController.Create(testReadyToGo);

            //Assert
            ReadyToGo readyToGo = readyToGoController.Get();

            Assert.AreEqual(readyToGo.Id, testReadyToGo.Id);
            Assert.AreEqual(readyToGo.StartDate, testReadyToGo.StartDate);
            Assert.AreEqual(readyToGo.EndDate, testReadyToGo.EndDate);
            Assert.AreEqual(readyToGo.BookingType, testReadyToGo.BookingType);
            Assert.AreEqual(readyToGo.User_Id, testReadyToGo.User_Id);
            Assert.AreEqual(readyToGo.Calendar_Id, testReadyToGo.Calendar_Id);
            Assert.AreEqual(readyToGo.AppendixNr, testReadyToGo.AppendixNr);
            Assert.AreEqual(readyToGo.ProductNr, testReadyToGo.ProductNr);
            Assert.AreEqual(readyToGo.Contract, testReadyToGo.Contract);
            */
        }
    }
}
