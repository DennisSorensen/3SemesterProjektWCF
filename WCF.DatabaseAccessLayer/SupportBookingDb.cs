using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using WCF.Exceptions;
using WCF.ModelLayer;

namespace WCF.DatabaseAccessLayer
{
    public class SupportBookingDb : IDbCrud<SupportBooking>
    {
        private readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public void Create(SupportBooking supportBooking)
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
                        cmd.Parameters.AddWithValue("calendarId", supportBooking.Calendar_Id);
                        cmd.Parameters.AddWithValue("startDate", supportBooking.StartDate);
                        cmd.Parameters.AddWithValue("endDate", supportBooking.EndDate);
                        amountOfBookings = (int)cmd.ExecuteScalar();
                    }
                    if (amountOfBookings == 0)//There exists no bookings at the selected time, so we can go ahead and book
                    {
                        using (SqlCommand cmd = connection.CreateCommand())
                        {
                            cmd.CommandText = "INSERT INTO [Booking] (startDate, endDate, bookingType, user_Id, calendar_Id) OUTPUT INSERTED.ID VALUES(@startDate, @endDate, @bookingType, @user_Id, @calendar_Id)";
                            cmd.Parameters.AddWithValue("startDate", supportBooking.StartDate);
                            cmd.Parameters.AddWithValue("endDate", supportBooking.EndDate);
                            cmd.Parameters.AddWithValue("bookingType", supportBooking.BookingType);
                            cmd.Parameters.AddWithValue("user_Id", supportBooking.User_Id);
                            cmd.Parameters.AddWithValue("calendar_Id", supportBooking.Calendar_Id);



                            newId = (int)cmd.ExecuteScalar();
                        }
                        using (SqlCommand cmd = connection.CreateCommand())
                        {


                            cmd.CommandText = "INSERT INTO [SupportBooking] (id, firstName, lastName, phone, description) VALUES(@id, @firstName, @lastName, @phone, @description)";
                            cmd.Parameters.AddWithValue("id", newId);
                            cmd.Parameters.AddWithValue("firstName", supportBooking.FirstName);
                            cmd.Parameters.AddWithValue("lastName", supportBooking.LastName);
                            cmd.Parameters.AddWithValue("phone", supportBooking.Phone);
                            cmd.Parameters.AddWithValue("description", supportBooking.Description);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        //and we throw a FaultException(WCF Specific)
                        //The <T> (type) of FaultException we throw, is one we have implemented ourselves (BookingExistsException).
                        //You can find this exception in the projet RoomBooking.Exceptions
                        throw new FaultException<BookingExistsException>(new BookingExistsException("Booking exists at that time"));

                    }
                }
                scope.Complete();
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public SupportBooking Get(int id)
        {
            SupportBooking supportBooking = null;
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT Booking.id, Booking.startDate, Booking.endDate, Booking.bookingType, Booking.user_id, Booking.calendar_Id, SupportBooking.firstName, SupportBooking.lastName, SupportBooking.phone, SupportBooking.description FROM [Booking] INNER JOIN [SupportBooking] ON Booking.id = SupportBooking.id WHERE SupportBooking.id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        supportBooking = new SupportBooking((DateTime)reader["startDate"],
                                                      (DateTime)reader["endDate"],
                                                      (string)reader["bookingType"],
                                                      (int)reader["user_Id"],
                                                      (int)reader["calendar_Id"],
                                                      (string)reader["firstName"],
                                                      (string)reader["lastName"],
                                                      (int)reader["phone"],
                                                      (string)reader["description"]
                                                      )
                        {
                            Id = (int)reader["id"]
                        };
                    }
                }

            }
            return supportBooking;
        }

        public IEnumerable<SupportBooking> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(SupportBooking entity)
        {
            throw new NotImplementedException();
        }
        
        public IEnumerable<SupportBooking> GetAllBookingForCalendar(int calendarId)
        {

            SupportBooking supportBooking = null;
            List<SupportBooking> list = new List<SupportBooking>();
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT Booking.id, Booking.startDate, Booking.endDate, Booking.bookingType, Booking.user_id, Booking.calendar_Id, SupportBooking.firstName, SupportBooking.lastName, SupportBooking.phone, SupportBooking.description FROM [Booking] INNER JOIN [SupportBooking] ON Booking.id = SupportBooking.id WHERE calendar_Id = @Calendar_Id";
                    cmd.Parameters.AddWithValue("@Calendar_Id", calendarId);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        supportBooking = new SupportBooking((DateTime)reader["startDate"],
                                                      (DateTime)reader["endDate"],
                                                      (string)reader["bookingType"],
                                                      (int)reader["user_Id"],
                                                      (int)reader["calendar_Id"],
                                                      (string)reader["firstName"],
                                                      (string)reader["lastName"],
                                                      (int)reader["phone"],
                                                      (string)reader["description"]
                                                      )
                        {
                            Id = (int)reader["id"]
                        };
                        list.Add(supportBooking);
                    }
                }

            }
            return list;
        }

        public IEnumerable<SupportBooking> GetAllBookingSpecificDay(int calendarId, DateTime date)
        {

            SupportBooking supportBooking = null;
            List<SupportBooking> list = new List<SupportBooking>();
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT Booking.id, Booking.startDate, Booking.endDate, Booking.bookingType, Booking.user_id, Booking.calendar_Id, SupportBooking.firstName, SupportBooking.lastName, SupportBooking.phone, SupportBooking.description FROM [Booking] INNER JOIN [SupportBooking] ON Booking.id = SupportBooking.id WHERE calendar_Id = @Calendar_Id AND startDate >= @StartDate AND endDate <@EndDate";
                    cmd.Parameters.AddWithValue("@Calendar_Id", calendarId);
                    cmd.Parameters.AddWithValue("@StartDate", date);
                    cmd.Parameters.AddWithValue("@EndDate", date.AddDays(1.0));
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        supportBooking = new SupportBooking((DateTime)reader["startDate"],
                                                      (DateTime)reader["endDate"],
                                                      (string)reader["bookingType"],
                                                      (int)reader["user_Id"],
                                                      (int)reader["calendar_Id"],
                                                      (string)reader["firstName"],
                                                      (string)reader["lastName"],
                                                      (int)reader["phone"],
                                                      (string)reader["description"]
                                                      )
                        {
                            Id = (int)reader["id"]
                        };
                        list.Add(supportBooking);
                    }
                }

            }
            return list;
        }
    }
}
