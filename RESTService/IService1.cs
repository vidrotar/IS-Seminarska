using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RESTService
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        [WebGet(UriTemplate = "Messages", ResponseFormat = WebMessageFormat.Json)]
        List<Message> GetMessages();

        [OperationContract]
        [WebGet(UriTemplate = "Osebe", ResponseFormat = WebMessageFormat.Json)]
        List<Oseba> VrniSeznamOseb();

        [OperationContract]
        [WebGet(UriTemplate = "Oseba/{id}", ResponseFormat = WebMessageFormat.Json)]
        Oseba VrniOsebo(string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "Oseba", ResponseFormat = WebMessageFormat.Json)]
        void DodajOsebo(Oseba oseba);

        [OperationContract]
        [WebInvoke(UriTemplate = "Oseba/{id}", ResponseFormat = WebMessageFormat.Json, Method = "DELETE")]
        void IzbrisiOsebo(string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "Oseba/{id}", ResponseFormat = WebMessageFormat.Json, Method = "PUT")]
        void PosodobiOsebo(Oseba oseba, string id);

    }


    [DataContract]
    public class Message
    {
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Time { get; set; }
        [DataMember]
        public string Text { get; set; }
    }

    [DataContract]
    public class Oseba
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string City { get; set; }
    }
}
