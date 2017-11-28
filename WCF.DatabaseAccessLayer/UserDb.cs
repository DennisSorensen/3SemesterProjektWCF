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
    public class UserDb : IDbCrud<User>
    {
        private readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public void Create(User user)
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
                            cmd.CommandText = "INSERT INTO [User] (id, role, firstName, lastName, password, department_Id ) VALUES(@id, @role, @firstName, @lastName, @password, @department_Id)";
                            cmd.Parameters.AddWithValue("id", user.Id);
                            cmd.Parameters.AddWithValue("role", user.Role);
                            cmd.Parameters.AddWithValue("firstName", user.FirstName);
                            cmd.Parameters.AddWithValue("lastName", user.LastName);
                            cmd.Parameters.AddWithValue("password", user.Password);
                            cmd.Parameters.AddWithValue("department_Id", user.DepartmentId);
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

        public User Get(int id)
        {
            User user = null;
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM [User] WHERE id=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        user = new User((int)reader["id"],
                                        (string)reader["role"],
                                        (string)reader["firstName"],
                                        (string)reader["lastName"],
                                        (string)reader["password"],
                                        (string)reader["department"]
                                       );
                    }
                }

            }
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            User user = null;
            List<User> list = new List<User>();
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM [User]";
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        user = new User((int)reader["id"],
                                        (string)reader["role"],
                                        (string)reader["firstName"],
                                        (string)reader["lastName"],
                                        (string)reader["password"],
                                        (string)reader["department"]
                                       );
                        list.Add(user);
                    }
                }

            }
            return list;
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllSupporters()
        {
            User user = null;
            List<User> list = new List<User>();
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM [User] WHERE role = 'Supporter'";
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        user = new User((int)reader["id"],
                                        (string)reader["role"],
                                        (string)reader["firstName"],
                                        (string)reader["lastName"],
                                        (string)reader["password"],
                                        (string)reader["department"]
                                       );
                        list.Add(user);
                    }
                }

            }
            return list;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            Department department = null;
            List<Department> list = new List<Department>();
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM [Department]";
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        department = new Department((int)reader["id"],
                                        (string)reader["name"]
                                       
                                       );
                        list.Add(department);
                    }
                }

            }
            return list;
        }
    }
}
