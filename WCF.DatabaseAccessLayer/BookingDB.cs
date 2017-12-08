﻿using System;
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
    public class BookingDb
    {
        private readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;



        public Booking GetBooking(int bookingId)
        {
            Booking booking = null;
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT Booking.id, Booking.startDate, Booking.endDate, Booking.bookingType, Booking.user_id, Booking.calendar_Id FROM [Booking] WHERE id = @bookingId";
                    cmd.Parameters.AddWithValue("@bookingId", bookingId);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        booking = new Booking((DateTime)reader["startDate"],
                                                      (DateTime)reader["endDate"],
                                                      (string)reader["bookingType"],
                                                      (int)reader["user_Id"],
                                                      (int)reader["calendar_Id"]
                                                      )
                        {
                            Id = (int)reader["id"]
                        };
                    }
                }

            }
            return booking;
        }

        public IEnumerable<Booking> GetAllBookingSpecificDay(int calendarId, DateTime date)
        {


            Booking booking = null;
            List<Booking> list = new List<Booking>();
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT Booking.id, Booking.startDate, Booking.endDate, Booking.bookingType, Booking.user_id, Booking.calendar_Id FROM [Booking] WHERE calendar_Id = @Calendar_Id AND startDate >= @StartDate AND endDate <@EndDate ORDER BY booking.startDate";
                    cmd.Parameters.AddWithValue("@Calendar_Id", calendarId);
                    cmd.Parameters.AddWithValue("@StartDate", date);
                    cmd.Parameters.AddWithValue("@EndDate", date.AddDays(1.0));
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        booking = new Booking((DateTime)reader["startDate"],
                                                      (DateTime)reader["endDate"],
                                                      (string)reader["bookingType"],
                                                      (int)reader["user_Id"],
                                                      (int)reader["calendar_Id"]
                                                      )
                        {
                            Id = (int)reader["id"]
                        };
                        list.Add(booking);
                    }
                }

            }
            return list;
        }



        public int FindAvaliableCalendar(DateTime startDate, DateTime endDate)
        {
            int found = -1;
            //Set the options for the transaction scope to serializable, such that double bookings cannot occur
            TransactionOptions to = new TransactionOptions { IsolationLevel = IsolationLevel.RepeatableRead };
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, to))
            {
                //Open connction using the connectionstring mentioned earlier
                using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
                {
                    connection.Open();
                    //amount of bookings is used to check how many bookings exist at the currently selected time
                    //(hopefully 0)
                    int amountOfBookings = -1;
                    int amountOfCalendars;
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT Count(*) FROM [Calendar]";
                        amountOfCalendars = (int)cmd.ExecuteScalar();
                    }
                    if (amountOfCalendars > 0)
                    {
                        List<Booking> list;
                        for (int i = 1; i <= amountOfCalendars; i++)
                        {
                            list = GetAllBookingSpecificDay(i, startDate.Date).ToList();
                            foreach (var booking in list)
                            {
                                using (SqlCommand cmd = connection.CreateCommand())
                                {
                                    cmd.CommandText = "SELECT Count(*) FROM [Booking] WHERE Booking.startDate <= @startDate AND Booking.endDate >= @endDate  AND Booking.calendar_Id = @calendarId";
                                    cmd.Parameters.AddWithValue("calendarId", i);
                                    cmd.Parameters.AddWithValue("startDate", booking.StartDate);
                                    cmd.Parameters.AddWithValue("endDate", booking.EndDate);
                                    amountOfBookings = (int)cmd.ExecuteScalar();
                                }
                            }
                                if (amountOfBookings == 0)//There exists no bookings at the selected time, so we can go ahead and book
                                {
                                    found = i;
                                    i = amountOfCalendars;
                                }
                        }
                    }
                }
                return found;
            }
        }
    }
}
