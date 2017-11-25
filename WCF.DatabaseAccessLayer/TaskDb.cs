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

                    try
                    {
                        using (SqlCommand cmd = connection.CreateCommand())
                        {
                            cmd.CommandText = "INSERT INTO [Booking] (startDate, endDate, bookingType, user_Id, calendar_Id) VALUES(@startDate, @endDate, @bookingType, @user_Id, @calendar_Id)";
                            //cmd.Parameters.AddWithValue("id", supportTask.Id);
                            cmd.Parameters.AddWithValue("startDate", supportTask.StartDate);
                            cmd.Parameters.AddWithValue("endDate", supportTask.EndDate);
                            cmd.Parameters.AddWithValue("bookingType", supportTask.BookingType);
                            cmd.Parameters.AddWithValue("user_Id", supportTask.User_Id);
                            cmd.Parameters.AddWithValue("calendar_Id", supportTask.Calendar_Id);
                            
                            

                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = connection.CreateCommand())
                        {
                            

                            cmd.CommandText = "INSERT INTO [Task] (id, name, description) VALUES(scope_identity(), @name, @description)";
                            //cmd.Parameters.AddWithValue(, supportTask.Id);
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
    }
}
