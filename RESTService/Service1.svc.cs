using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RESTService
{
    public class Service1 : IService1
    {
        string cs = ConfigurationManager.ConnectionStrings["SlivkoDatabaseConnectionString"].ConnectionString;
        public List<Message> GetMessages()
        {
            var retVal = new List<Message>();

            retVal.Add(new Message { Username = "slavkoz", Time = "19.12.2014", Text = "Zivjo!" });
            retVal.Add(new Message { Username = "mirkoj", Time = "21.12.2014", Text = "Ola!" });
            retVal.Add(new Message { Username = "janezl", Time = "21.12.2014", Text = "Hoj!" });
            retVal.Add(new Message { Username = "slavkoz", Time = "21.12.2014", Text = "Kako kej?" });
            retVal.Add(new Message { Username = "mirkoj", Time = "22.12.2014", Text = "Pa v redu ..." });
            retVal.Add(new Message { Username = "janezl", Time = "22.12.2014", Text = "Aha" });
            retVal.Add(new Message { Username = "mirkoj", Time = "23.12.2014", Text = "Kj novga?" });
            retVal.Add(new Message { Username = "slavkoz", Time = "24.12.2014", Text = "Nc ..." });
            retVal.Add(new Message { Username = "janezl", Time = "24.12.2014", Text = "Kdo za kosilo dons?" });
            retVal.Add(new Message { Username = "mirkoj", Time = "24.12.2014", Text = "Jaaaaaaaaa....." });

            return retVal;
        }

        private bool AuthenticateUser()
        {
            WebOperationContext ctx = WebOperationContext.Current;
            string authHeader = ctx.IncomingRequest.Headers[HttpRequestHeader.Authorization];
            if (authHeader == null)
                return false;

            string[] loginData = authHeader.Split(':');
            if (loginData.Length == 2 && Login(loginData[0], loginData[1]))
                return true;
            return false;
        }


        public bool Login(string username, string password)
        {
            if (username.Equals("jernejvid") && password.Equals("Leibniz1"))
                return true;
            return false;
        }



        public Oseba VrniOsebo(string id)
        {
            Oseba oseba = new Oseba();



            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string sql = "SELECT * FROM Persons WHERE PersonID=@param1";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(new SqlParameter("param1", id));

                using (SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleRow))
                {
                    if (reader.Read())
                    {
                        oseba.Id = Convert.ToInt32(reader[0]);
                        oseba.LastName = reader.GetString(1);
                        oseba.FirstName = reader.GetString(2);
                        oseba.Address = reader.GetString(3);
                        oseba.City = reader.GetString(4);
                    }
                }
                con.Close();
                return oseba;
            }
        }


        /*
        public Oseba VrniOsebo()
        {
            string result = "";
            string address = "";
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = cs;

                connection.Open();
                // Pool A is created.

                SqlCommand command = new SqlCommand("Select * from Persons where PersonID=1", connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // while there is another record present
                    while (reader.Read())
                    {
                        result = result + reader[1];
                        address = address + reader[3];
                    }
                }
                return new Oseba { Id = 1, LastName = result, FirstName = "", Address = address, City = "" };
            }


            //return new Oseba { Id = 1, LastName = "bla", FirstName = "bla1", Address = "bla1", City = "skloka" };

        }

        */

        public List<Oseba> VrniSeznamOseb()
        {
            var retVal = new List<Oseba>();

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string sql = "SELECT * FROM PERSONS";
                SqlCommand cmd = new SqlCommand(sql, con);


                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        retVal.Add(new Oseba
                        {
                            Id = Convert.ToInt32(reader[0]),
                            LastName = reader.GetString(1),
                            FirstName = reader.GetString(2),
                            Address = reader.GetString(3),
                            City = reader.GetString(4)
                        });

                    }
                }
                con.Close();

                return retVal;
            }
        }

        public void DodajOsebo(Oseba oseba)
        {

            if (!AuthenticateUser())
                throw new FaultException("Napačno uporabniško ime ali geslo.");

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string sql =
                    "INSERT INTO Persons (PersonID, FirstName, LastName, Address, City) VALUES (@0, @1, @2, @3, @4)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(new SqlParameter("0", oseba.Id));
                cmd.Parameters.Add(new SqlParameter("1", oseba.LastName));
                cmd.Parameters.Add(new SqlParameter("2", oseba.FirstName));
                cmd.Parameters.Add(new SqlParameter("3", oseba.Address));
                cmd.Parameters.Add(new SqlParameter("4", oseba.City));
                cmd.ExecuteNonQuery();
                con.Close();

            }

        }

        public void IzbrisiOsebo(string id)
        {
            if (!AuthenticateUser())
                throw new FaultException("Napačno uporabniško ime ali geslo.");

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string sql = "DELETE FROM PERSONS WHERE PersonID=@param1";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(new SqlParameter("param1", id));
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void PosodobiOsebo(Oseba oseba, string id)
        {
            if (!AuthenticateUser())
                throw new FaultException("Napačno uporabniško ime ali geslo.");

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string sql =
                    "UPDATE Persons set FirstName=@1, LastName=@2, Address=@3, City=@4 WHERE PersonID=@0";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(new SqlParameter("0", id));
                cmd.Parameters.Add(new SqlParameter("1", oseba.LastName));
                cmd.Parameters.Add(new SqlParameter("2", oseba.FirstName));
                cmd.Parameters.Add(new SqlParameter("3", oseba.Address));
                cmd.Parameters.Add(new SqlParameter("4", oseba.City));
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }



    }
}
