﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCF.BusinessLogicLayer;
using WCF.ModelLayer;

namespace WCF.Service
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CalendarService" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select CalendarService.svc or CalendarService.svc.cs at the Solution Explorer and start debugging.
	public class CalendarService : ICalendarService
	{
        private CalendarController calendarController = new CalendarController();

        public void Create(Calendar calendar)
        {
            calendarController.Create(calendar);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(Calendar calendar)
        {
            throw new NotImplementedException();
        }

        public Calendar Get(int id)
        {
            return calendarController.Get(id);
        }

        public IEnumerable<Calendar> GetAll()
        {
            return calendarController.GetAll();
        }
    }
}
