using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.ModelLayer;

namespace WCF.DatabaseAccessLayer
{
    public class UserDb : IDbCrud<User>
    {
        private readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public void Create(User entity)
        {
            /*
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
                            cmd.CommandText = "INSERT INTO Customer (CustomerId, Commercial, Name, Address, Email) VALUES(@CustomerId, @Commercial, @Name, @Address, @Email)";
                            cmd.Parameters.AddWithValue("CustomerId", customer.CustomerId);
                            cmd.Parameters.AddWithValue("Commercial", customer.Commercial);
                            cmd.Parameters.AddWithValue("Name", customer.Name);
                            cmd.Parameters.AddWithValue("Adress", customer.Address);
                            cmd.Parameters.AddWithValue("Email", customer.Email);
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
            */
        }

        public User Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
