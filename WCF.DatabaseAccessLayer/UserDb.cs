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
    public class UserDb : IDbCrud<User> //Implementerer interface, og den bruger User i stedet for T
    {
        private readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString; //Man kan kun læse den, og så bruger den denne connection til db.

        public void Create(User user)
        {
            
            TransactionOptions to = new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }; //Instillinger for hvordan din transaktion skal opføre sig, Serializable låser alt den berører (pessemistisk transaktin)
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, to)) //Laver et scope med transaction options
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION_STRING)) //Åbner forbindelse med vores connection string. Using er midlertidigt, og smider resten væk når den er færdig(Dispose).
                {
                    connection.Open(); //Åbner forbindelsen


                    try //Prøver at create, og går til exception hvis den fejler
                    {
                        using (SqlCommand cmd = connection.CreateCommand()) //Laver et nyt obj som hedder cmd som er en create command i sql
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
                        throw e; //Smider fejlen ud på consollen
                    }
                }
                scope.Complete(); //Lukker scope
            }
            
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            User user = null;
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING)) //Transaction scope skal implementers før denne linje, og det ville være repeatableRead, da man ikke kan redigere data imens det læses
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand()) //Laver ny kommando
                {
                    cmd.CommandText = "SELECT * FROM [User] WHERE id=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader(); //Den får data tilbage, og smider det i reader

                    while (reader.Read()) //Så længe reader læser kører loopet og indsamler data
                    {
                        user = new User((int)reader["id"],
                                        (string)reader["role"],
                                        (string)reader["firstName"],
                                        (string)reader["lastName"],
                                        (string)reader["password"],
                                        (int)reader["department_Id"]
                                       );
                    }
                }

            }
            return user; //Returnerer user hvis der var en, ellers returnerer den null
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
                                        (int)reader["department_Id"]
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
                                        (int)reader["department_Id"]
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

        public IEnumerable<User> GetAllDepSupport(int id) //Department supporters
        {
            User user = null;
            List<User> list = new List<User>();
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM [User] WHERE role = 'Supporter' AND department_Id = @Department_Id";
                    cmd.Parameters.AddWithValue("@Department_Id", id);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        user = new User((int)reader["id"],
                                        (string)reader["role"], 
                                        (string)reader["firstName"],
                                        (string)reader["lastName"],
                                        (string)reader["password"],
                                        (int)reader["department_Id"]
                                       );
                        list.Add(user);
                    }
                }

            }
            return list;
        }

        public User Login(int id, string password)
        {
            try
            {
                User user = Get(id); //Kalder metoden i denne klasse (intern metodekald) og får en user.
                if (user.Password == password)
                {

                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
