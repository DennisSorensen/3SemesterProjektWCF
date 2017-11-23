using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.DatabaseAccessLayer;
using WCF.ModelLayer;

namespace WCF.BusinessLogicLayer
{
    public class ReadyToGoController : IBookingController<ReadyToGo>
    {
        private ReadyToGoDb readyToGoDb;

        public ReadyToGoController()
        {
            readyToGoDb = new ReadyToGoDb();
        }

        public void Create(ReadyToGo readyToGo)
        {
            readyToGoDb.Create(readyToGo);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ReadyToGo Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReadyToGo> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(ReadyToGo entity)
        {
            throw new NotImplementedException();
        }
    }
}
