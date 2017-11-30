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
    }
}
