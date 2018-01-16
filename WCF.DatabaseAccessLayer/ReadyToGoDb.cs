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
    public class ReadyToGoDb : IDbCrud<ReadyToGo>
    {
        private readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public void Create(ReadyToGo readyToGo)
        {
            //Serializable, forhindre os i at overskriv/sammenskrive data.
            TransactionOptions to = new TransactionOptions { IsolationLevel = IsolationLevel.Serializable };
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, to))
            {
                
                using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
                {
                    connection.Open();
                    int newId = -1; //id fra booking til rtg
                    int amountOfBookings;
                    using (SqlCommand cmd = connection.CreateCommand()) //Tjekker om der eksistere bookinger på det givne tidspunkt
                    {
                        cmd.CommandText = "SELECT Count(*) FROM [Booking] WHERE Booking.startDate <= @startDate AND Booking.endDate >= @endDate  AND Booking.calendar_Id = @calendarId";
                        cmd.Parameters.AddWithValue("calendarId", readyToGo.Calendar_Id);
                        cmd.Parameters.AddWithValue("startDate", readyToGo.StartDate);
                        cmd.Parameters.AddWithValue("endDate", readyToGo.EndDate);
                        amountOfBookings = (int)cmd.ExecuteScalar();
                    }
                    if (amountOfBookings == 0) //Der er ingen booking i tidsrummet
                    {
                        using (SqlCommand cmd = connection.CreateCommand())
                        {
                            cmd.CommandText = "INSERT INTO [Booking] (startDate, endDate, bookingType, user_Id, calendar_Id) OUTPUT INSERTED.ID VALUES(@startDate, @endDate, @bookingType, @user_Id, @calendar_Id)"; //OUTPUT = returnerer id. booking id er auto genereret, og dette skal også bruges i rtg
                            cmd.Parameters.AddWithValue("startDate", readyToGo.StartDate);
                            cmd.Parameters.AddWithValue("endDate", readyToGo.EndDate);
                            cmd.Parameters.AddWithValue("bookingType", readyToGo.BookingType);
                            cmd.Parameters.AddWithValue("user_Id", readyToGo.User_Id);
                            cmd.Parameters.AddWithValue("calendar_Id", readyToGo.Calendar_Id);



                            newId = (int)cmd.ExecuteScalar();
                        }

                        using (SqlCommand cmd = connection.CreateCommand())
                        {


                            cmd.CommandText = "INSERT INTO [ReadyToGo] (id, productNr, appendixNr, contract, additionalServices) VALUES(@id, @productNr, @appendixNr, @contract, @additionalServices)";
                            cmd.Parameters.AddWithValue("id", newId);
                            cmd.Parameters.AddWithValue("productNr", readyToGo.ProductNr);
                            cmd.Parameters.AddWithValue("appendixNr", readyToGo.AppendixNr);
                            cmd.Parameters.AddWithValue("contract", readyToGo.Contract);
                            cmd.Parameters.AddWithValue("additionalServices", readyToGo.AdditionalServices);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        //throws a FaultException
                        throw new FaultException<BookingExistsException>(new BookingExistsException("Booking exists at that time")); //Laver en BookingExistsException, som er vores egen
                    }
                }
            scope.Complete();
        }
    }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ReadyToGo Get(int id)
        {
            ReadyToGo readyToGo = null;
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT Booking.id, Booking.startDate, Booking.endDate, Booking.bookingType, Booking.user_id, Booking.calendar_Id, ReadyToGo.productNr, ReadyToGo.appendixNr, ReadyToGo.contract, ReadyToGo.additionalServices FROM [Booking] INNER JOIN [ReadyToGo] ON Booking.id = ReadyToGo.id WHERE ReadyToGo.id = @id"; //Id'er som passer sammen bliver joinet. inner join = det de har til fælles
                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        readyToGo = new ReadyToGo((DateTime)reader["startDate"],
                                                  (DateTime)reader["endDate"],
                                                  (string)reader["bookingType"],
                                                  (int)reader["user_Id"],
                                                  (int)reader["calendar_Id"],
                                                  (string)reader["productNr"],
                                                  (int)reader["appendixNr"],
                                                  (bool)reader["contract"]
                                                 )
                        { //de kommer bagefter fordi de ikke er giver i constructoren
                            Id = (int)reader["id"],
                            AdditionalServices = (string)reader["additionalServices"]
                        };
                    }
                }

            }
            return readyToGo;
        }

        public IEnumerable<ReadyToGo> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(ReadyToGo entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReadyToGo> GetAllBookingForCalendar(int calendarId)
        {

            ReadyToGo readyToGo = null;
            List<ReadyToGo> list = new List<ReadyToGo>();
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT Booking.id, Booking.startDate, Booking.endDate, Booking.bookingType, Booking.user_id, Booking.calendar_Id, ReadyToGo.productNr, ReadyToGo.appendixNr, ReadyToGo.contract, ReadyToGo.additionalServices FROM [Booking] INNER JOIN [ReadyToGo] ON Booking.id = ReadyToGo.id WHERE calendar_Id = @Calendar_Id";
                    cmd.Parameters.AddWithValue("@Calendar_Id", calendarId);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        readyToGo = new ReadyToGo((DateTime)reader["startDate"],
                                                  (DateTime)reader["endDate"],
                                                  (string)reader["bookingType"],
                                                  (int)reader["user_Id"],
                                                  (int)reader["calendar_Id"],
                                                  (string)reader["productNr"],
                                                  (int)reader["appendixNr"],
                                                  (bool)reader["contract"]
                                                 )
                        {
                            Id = (int)reader["id"],
                            AdditionalServices = (string)reader["additionalServices"]
                        };
                        list.Add(readyToGo);
                    }
                }

            }
            return list;
        }

        public IEnumerable<ReadyToGo> GetAllBookingSpecificDay(int calendarId, DateTime date)
        {

            ReadyToGo readyToGo = null;
            List<ReadyToGo> list = new List<ReadyToGo>();
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT Booking.id, Booking.startDate, Booking.endDate, Booking.bookingType, Booking.user_id, Booking.calendar_Id, ReadyToGo.productNr, ReadyToGo.appendixNr, ReadyToGo.contract FROM [Booking] INNER JOIN [ReadyToGo] ON Booking.id = ReadyToGo.id WHERE calendar_Id = @Calendar_Id AND startDate >= @StartDate AND endDate <@EndDate";
                    cmd.Parameters.AddWithValue("@Calendar_Id", calendarId);
                    cmd.Parameters.AddWithValue("@StartDate", date);
                    cmd.Parameters.AddWithValue("@EndDate", date.AddDays(1.0));
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        readyToGo = new ReadyToGo((DateTime)reader["startDate"],
                                                      (DateTime)reader["endDate"],
                                                      (string)reader["bookingType"],
                                                      (int)reader["user_Id"],
                                                      (int)reader["calendar_Id"],
                                                      (string)reader["productNr"],
                                                      (int)reader["appendixNr"],
                                                      (bool)reader["contract"]
                                                      )
                        {
                            Id = (int)reader["id"]
                        };
                        list.Add(readyToGo);
                    }
                }

            }
            return list;
        }

    }
}
