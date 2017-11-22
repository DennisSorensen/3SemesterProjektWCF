using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCF.ModelLayer;
using WCF.BusinessLogicLayer;

namespace WCFTest
{
    [TestClass]
    public class CalendarTest
    {

        [TestMethod]
        public void TestCreateCalendar()
        {
            //Arrange
            CalendarController calendarController = new CalendarController();

            //Act
            Calendar testCalendar = new Calendar(99);

            calendarController.Create(testCalendar);

            //Assert
            Calendar calendar = calendarController.Get(99);

            Assert.Equals(calendar.Id, testCalendar.Id);
            
        }
    }
}
