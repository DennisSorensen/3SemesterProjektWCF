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
    public class SupportBookingDb : IDbCrud<SupportBooking>
    {
        private readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public void Create(SupportBooking supportBooking)
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
                            cmd.Parameters.AddWithValue("id", supportBooking.Id);
                            cmd.Parameters.AddWithValue("startDate", supportBooking.StartDate);
                            cmd.Parameters.AddWithValue("endDate", supportBooking.EndDate);
                            cmd.Parameters.AddWithValue("lastName", supportBooking.BookingType);
                            cmd.Parameters.AddWithValue("user_Id", supportBooking.User_Id);
                            cmd.Parameters.AddWithValue("calendar_Id", supportBooking.Calendar_Id);

                            cmd.CommandText = "INSERT INTO [SupportBooking] (id, firstName, lastName, phone, description) VALUES(@id, @firstName, @lastName, @phone, @description)";
                            cmd.Parameters.AddWithValue("id", supportBooking.Id);
                            cmd.Parameters.AddWithValue("firstName", supportBooking.FirstName);
                            cmd.Parameters.AddWithValue("lastName", supportBooking.LastName);
                            cmd.Parameters.AddWithValue("phone", supportBooking.Phone);
                            cmd.Parameters.AddWithValue("description", supportBooking.Description);

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

        public SupportBooking Get(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SupportBooking> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(SupportBooking entity)
        {
            throw new NotImplementedException();
        }
    }
}
