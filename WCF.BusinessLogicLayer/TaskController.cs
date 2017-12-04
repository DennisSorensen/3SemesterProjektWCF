using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.DatabaseAccessLayer;
using WCF.ModelLayer;

namespace WCF.BusinessLogicLayer
{
    public class TaskController : IBookingController<SupportTask>
    {

        private TaskDb taskDb;

        public TaskController()
        {
            taskDb = new TaskDb();
        }

        public void Create(SupportTask supportTask)
        {
            taskDb.Create(supportTask);
        }


        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public SupportTask GetSupportTask(int id)
        {
            return taskDb.Get(id);
        }

        public IEnumerable<SupportTask> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(SupportTask entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SupportTask> GetAllBookingForCalendar(int calendarId)
        {
            return taskDb.GetAllBookingForCalendar(calendarId);
        }
        
        
    }
}
