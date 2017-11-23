using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using WCF.ModelLayer;

namespace WCF.DatabaseAccessLayer
{
    public class ReadyToGoDb : IDbCrud<ReadyToGo>
    {
        private readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public void Create(ReadyToGo readyToGo)
        {
            TransactionOptions to = new TransactionOptions { IsolationLevel = IsolationLevel.RepeatableRead };
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, to))
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
                {
                    connection.Open();

                    try
                    {
                        using (SqlCommand cmd = connection.CreateCommand())
                        {
                            cmd.CommandText = "INSERT INTO [Booking] (id, startDate, endDate, bookingType, user_Id, calendar_Id) VALUES(@id, @startDate, @endDate, @bookingType, @user_Id, @calendar_Id)";
                            cmd.Parameters.AddWithValue("id", readyToGo.Id);
                            cmd.Parameters.AddWithValue("startDate", readyToGo.StartDate);
                            cmd.Parameters.AddWithValue("endDate", readyToGo.EndDate);
                            cmd.Parameters.AddWithValue("lastName", readyToGo.BookingType);
                            cmd.Parameters.AddWithValue("user_Id", readyToGo.User_Id);
                            cmd.Parameters.AddWithValue("calendar_Id", readyToGo.Calendar_Id);

                            cmd.CommandText = "INSERT INTO [ReadyToGo] (id, productNr, appendixNr, contract) VALUES(@id, @productNr, @appendixNr, @contract)";
                            cmd.Parameters.AddWithValue("id", readyToGo.Id);
                            cmd.Parameters.AddWithValue("productNr", readyToGo.ProductNr);
                            cmd.Parameters.AddWithValue("appendixNr", readyToGo.AppendixNr);
                            cmd.Parameters.AddWithValue("contract", readyToGo.Contract);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (SqlException e)
                    {
                        throw e;
                    }
                }
                scope.Complete();
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ReadyToGo Get(int Id)
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
