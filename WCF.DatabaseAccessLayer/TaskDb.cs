using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
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

            //Set the options for the transaction scope to serializable, such that double bookings cannot occur
            TransactionOptions to = new TransactionOptions { IsolationLevel = IsolationLevel.Serializable };
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, to))
            {
                //Open connction using the connectionstring mentioned earlier
                using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
                {
                    connection.Open();
                    int newId = -1;
                    //amount of bookings is used to check how many bookings exist at the currently selected time
                    //(hopefully 0)
                    int amountOfBookings;
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT Count(*) FROM [Booking] WHERE Booking.startDate <= @startDate AND Booking.endDate >= @endDate  AND Booking.calendar_Id = @calendarId";
                        cmd.Parameters.AddWithValue("calendarId", supportTask.Calendar_Id);
                        cmd.Parameters.AddWithValue("startDate", supportTask.StartDate);
                        cmd.Parameters.AddWithValue("endDate", supportTask.EndDate);
                        amountOfBookings = (int)cmd.ExecuteScalar();
                    }
                    if (amountOfBookings == 0)//There exists no bookings at the selected time, so we can go ahead and book
                    {
                        using (SqlCommand cmd = connection.CreateCommand())
                        {
                            cmd.CommandText = "INSERT INTO [Booking] (startDate, endDate, bookingType, user_Id, calendar_Id) OUTPUT INSERTED.ID VALUES(@startDate, @endDate, @bookingType, @user_Id, @calendar_Id)";
                            cmd.Parameters.AddWithValue("startDate", supportTask.StartDate);
                            cmd.Parameters.AddWithValue("endDate", supportTask.EndDate);
                            cmd.Parameters.AddWithValue("bookingType", supportTask.BookingType);
                            cmd.Parameters.AddWithValue("user_Id", supportTask.User_Id);
                            cmd.Parameters.AddWithValue("calendar_Id", supportTask.Calendar_Id);



                            newId = (int)cmd.ExecuteScalar();
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
                    else
                    {
                        //A booking existed in the selected time period
                        //We log this event (logging is setup in the startup projects app.config, under the element <Diagnostics>)
                        string s = string.Format("User: {0} tried to double book",supportTask.User_Id);
                        Trace.TraceInformation(s);
                        Trace.Flush();
                        //and we throw a FaultException(WCF Specific)
                        //The <T> (type) of FaultException we throw, is one we have implemented ourselves (BookingExistsException).
                        //You can find this exception in the projet RoomBooking.Exceptions
                        //throw new FaultException<BookingExistsException>(new BookingExistsException("Booking exists at that time"));
                    }
                }
                scope.Complete();
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public SupportTask Get(int id)
        {
            SupportTask supportTask = null;
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT Booking.id, Booking.startDate, Booking.endDate, Booking.bookingType, Booking.user_id, Booking.calendar_Id, Task.name, Task.description FROM [Booking] INNER JOIN [Task] ON Booking.id = Task.id WHERE Task.id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
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
                    }
                }

            }
            return supportTask;
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
