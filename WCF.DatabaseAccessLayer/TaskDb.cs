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
    public class TaskDb : IDbCrud<SupportTask>
    {
        private readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public void Create(SupportTask supportTask)
        {

            TransactionOptions to = new TransactionOptions { IsolationLevel = IsolationLevel.RepeatableRead };
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, to))
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
                {
                    connection.Open();
                    int newId = -1;
                    try
                    {
                        using (SqlCommand cmd = connection.CreateCommand())
                        {
                            cmd.CommandText = "INSERT INTO [Booking] (startDate, endDate, bookingType, user_Id, calendar_Id) OUTPUT INSERTED.ID VALUES(@startDate, @endDate, @bookingType, @user_Id, @calendar_Id)";
                            cmd.Parameters.AddWithValue("startDate", supportTask.StartDate);
                            cmd.Parameters.AddWithValue("endDate", supportTask.EndDate);
                            cmd.Parameters.AddWithValue("bookingType", supportTask.BookingType);
                            cmd.Parameters.AddWithValue("user_Id", supportTask.User_Id);
                            cmd.Parameters.AddWithValue("calendar_Id", supportTask.Calendar_Id);
                            
                            

                            newId = (int) cmd.ExecuteScalar();
                        }

                        using (SqlCommand cmd = connection.CreateCommand())
                        {
                            

                            cmd.CommandText = "INSERT INTO [Task] (id, name, description) VALUES(@id, @name, @description)";
                            cmd.Parameters.AddWithValue("id", newId);
                            cmd.Parameters.AddWithValue("name", supportTask.Name);
                            cmd.Parameters.AddWithValue("description", supportTask.Description);

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

        public SupportTask Get(int Id)
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
        public IEnumerable<SupportTask> GetAllBookingForCalendar(int calendarId)
        {

            SupportTask supportTask = null;
            List<SupportTask> list = new List<SupportTask>();
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT Booking.id, Booking.startDate, Booking.endDate, Booking.bookingType, Booking.user_id, Booking.calendar_Id, Task.name, Task.description FROM [Booking] INNER JOIN [Task] ON Booking.id = Task.id WHERE calendar_Id = @Calendar_Id";
                    cmd.Parameters.AddWithValue("@Calendar_Id", calendarId);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        supportTask = new SupportTask((DateTime)reader["startDate"],
                                                      (DateTime)reader["endDate"],
                                                      (string)reader["bookingType"],
                                                      (int)reader["user_Id"],
                                                      (int)reader["calendar_Id"],
                                                      (string)reader["name"],
                                                      (string)reader["description"]
                                                      )
                                                      {
                                                        Id = (int)reader["id"]
                                                      };
                        list.Add(supportTask);
                    }
                }

            }
            return list;
        }

    }
}
