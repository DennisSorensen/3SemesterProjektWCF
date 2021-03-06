﻿using System;
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
    public class CalendarDb : IDbCrud<Calendar>
    {
        private readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public void Create(Calendar calendar)
        {
            TransactionOptions to = new TransactionOptions { IsolationLevel = IsolationLevel.Serializable };
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, to))
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
                {
                    connection.Open();
              

                    try
                    {
                        using (SqlCommand cmd = connection.CreateCommand())
                        {
                            cmd.CommandText = "INSERT INTO [Calendar] (user_Id) VALUES(@user_Id)";
                            cmd.Parameters.AddWithValue("@user_Id", calendar.UserId);
                            
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

        public Calendar Get(int user_Id)
        {
            Calendar calendar = null;
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM [Calendar] WHERE user_Id=@user_Id";
                    cmd.Parameters.AddWithValue("@user_Id", user_Id);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        calendar = new Calendar((int)reader["user_Id"])
                        {
                            Id = (int)reader["id"]
                        };
                    }
                }
            }
            return calendar;
        }

        public IEnumerable<Calendar> GetAll()
        {
            List<Calendar> list = new List<Calendar>();
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM [Calendar]";
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Calendar calendar = new Calendar((int)reader["user_Id"])
                        {
                            Id = (int)reader["id"]

                        };
                        list.Add(calendar);
                    }
                }

            }
            return list;
        }

        public void Update(Calendar entity)
        {
            throw new NotImplementedException();
        }
    }
}
