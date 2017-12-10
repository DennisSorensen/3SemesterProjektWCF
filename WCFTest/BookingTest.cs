using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCF.BusinessLogicLayer;
using WCF.ModelLayer;
using System.Collections.Generic;
using System.Linq;

namespace WCFTest
{
    [TestClass]
    public class BookingTest
    {
        [TestMethod]
        public void TestCreateBookingSupportTask()
        {

            //Arrange
            TaskController taskController = new TaskController();
            BookingController bookingController = new BookingController();
            CalendarController calendarController = new CalendarController();

            DateTime startDate = new DateTime(2017, 12, 24, 9,00,00);
            DateTime endDate = new DateTime(2017, 12, 24, 9,30,00);
            Calendar calendar = calendarController.Get(2);
            int i = 0;
            bool found = true;
            //Act

            SupportTask testSupportTask = new SupportTask(startDate, endDate, "Task", 99, 2, "Test", "Hjælp Test");
            SupportTask supportTask = null;
            try
            {
            taskController.Create(testSupportTask);
            }
            catch
            {
                Console.WriteLine("Booking Exists");
            }
            List<Booking> allSupportTasks = bookingController.GetAllBookingSpecificDay(testSupportTask.Calendar_Id, testSupportTask.StartDate.Date).ToList();

            //Assert 
            while (found || allSupportTasks.Count <= i)
            {
                if (allSupportTasks[i].StartDate == testSupportTask.StartDate && allSupportTasks[i].EndDate == testSupportTask.EndDate)
                {
                    supportTask = taskController.GetSupportTask(allSupportTasks[i].Id);
                    found = false;
                }
                else
                {
                i++;
                }
            }
                Assert.AreEqual(supportTask.StartDate, testSupportTask.StartDate);
                Assert.AreEqual(supportTask.EndDate, testSupportTask.EndDate);
                Assert.AreEqual(supportTask.BookingType, testSupportTask.BookingType);
                Assert.AreEqual(supportTask.User_Id, testSupportTask.User_Id);
                Assert.AreEqual(supportTask.Calendar_Id, testSupportTask.Calendar_Id);
                Assert.AreEqual(supportTask.Name, testSupportTask.Name);
                Assert.AreEqual(supportTask.Description, testSupportTask.Description);
        }

        [TestMethod]
        public void TestCreateBookingSupportBooking()
        {

            //Arrange
            SupportBookingController supportController = new SupportBookingController();
            BookingController bookingController = new BookingController();
            CalendarController calendarController = new CalendarController();

            DateTime startDate = new DateTime(2017, 12, 24, 10, 00, 00);
            DateTime endDate = new DateTime(2017, 12, 24, 10, 30, 00);
            Calendar calendar = calendarController.Get(2);
            int i = 0;
            bool found = true;
            //Act

            SupportBooking testSupportBooking = new SupportBooking(startDate, endDate, "SupportBooking", 99, 2, "Bo", "Jensen", 99999999, "Hjælp med Test");
            SupportBooking supportBooking = null;
            try
            {
                supportController.Create(testSupportBooking);
            }
            catch
            {
                Console.WriteLine("Booking Exists");
            }
            List<Booking> allSupportBookings = bookingController.GetAllBookingSpecificDay(testSupportBooking.Calendar_Id, testSupportBooking.StartDate.Date).ToList();

            //Assert 
            while (found || allSupportBookings.Count <= i)
            {
                if (allSupportBookings[i].StartDate == testSupportBooking.StartDate && allSupportBookings[i].EndDate == testSupportBooking.EndDate)
                {
                    supportBooking = supportController.GetSupportBooking(allSupportBookings[i].Id);
                    found = false;
                }
                else
                {
                    i++;
                }
            }
            Assert.AreEqual(supportBooking.StartDate, testSupportBooking.StartDate);
            Assert.AreEqual(supportBooking.EndDate, testSupportBooking.EndDate);
            Assert.AreEqual(supportBooking.BookingType, testSupportBooking.BookingType);
            Assert.AreEqual(supportBooking.User_Id, testSupportBooking.User_Id);
            Assert.AreEqual(supportBooking.Calendar_Id, testSupportBooking.Calendar_Id);
            Assert.AreEqual(supportBooking.FirstName, testSupportBooking.FirstName);
            Assert.AreEqual(supportBooking.LastName, testSupportBooking.LastName);
            Assert.AreEqual(supportBooking.Phone, testSupportBooking.Phone);
            Assert.AreEqual(supportBooking.Description, testSupportBooking.Description);
        }

        [TestMethod]
        public void TestCreateBookingReadyToGo()
        {
            //Arrange
            ReadyToGoController readyToGoController = new ReadyToGoController();
            BookingController bookingController = new BookingController();
            CalendarController calendarController = new CalendarController();

            DateTime startDate = new DateTime(2017, 12, 24, 11, 00, 00);
            DateTime endDate = new DateTime(2017, 12, 24, 11, 30, 00);
            Calendar calendar = calendarController.Get(2);
            int i = 0;
            bool found = true;
            //Act

            ReadyToGo testReadyToGo = new ReadyToGo(startDate, endDate, "ReadyToGo", 99, 2, "Test1234", 1, true);
            testReadyToGo.AdditionalServices = "Test";
            ReadyToGo readyToGo = null;
            try
            {
                readyToGoController.Create(testReadyToGo);
            }
            catch
            {
                Console.WriteLine("Booking Exists");
            }
            List<Booking> allReadyToGo = bookingController.GetAllBookingSpecificDay(testReadyToGo.Calendar_Id, testReadyToGo.StartDate.Date).ToList();

            //Assert 
            while (found || allReadyToGo.Count <= i)
            {
                if (allReadyToGo[i].StartDate == testReadyToGo.StartDate && allReadyToGo[i].EndDate == testReadyToGo.EndDate)
                {
                    readyToGo = readyToGoController.GetReadyToGo(allReadyToGo[i].Id);
                    found = false;
                }
                else
                {
                    i++;
                }
            }
            Assert.AreEqual(readyToGo.StartDate, testReadyToGo.StartDate);
            Assert.AreEqual(readyToGo.EndDate, testReadyToGo.EndDate);
            Assert.AreEqual(readyToGo.BookingType, testReadyToGo.BookingType);
            Assert.AreEqual(readyToGo.User_Id, testReadyToGo.User_Id);
            Assert.AreEqual(readyToGo.Calendar_Id, testReadyToGo.Calendar_Id);
            Assert.AreEqual(readyToGo.ProductNr, testReadyToGo.ProductNr);
            Assert.AreEqual(readyToGo.AppendixNr, testReadyToGo.AppendixNr);
            Assert.AreEqual(readyToGo.Contract, testReadyToGo.Contract);
        }
    }
}
