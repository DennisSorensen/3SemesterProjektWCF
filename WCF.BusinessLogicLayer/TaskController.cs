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
        DataTable table = new DataTable();

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

        public SupportTask Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SupportTask> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(SupportTask entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SupportTask> GetAllBookingForUser(int userId)
        {
            return taskDb.GetAllBookingForUser(userId);
        }
        
        
    }
}
