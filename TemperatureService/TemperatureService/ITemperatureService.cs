using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TemperatureService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ITemperatureService
    {
        [OperationContract]
        void SaveTemperature(TemperatureMessage temperatureMessage);

        [OperationContract]
        void ClearTemperature();
        [OperationContract]
        List<TemperatureMessage> GetAllTemperatures();

        [OperationContract]
        List<TemperatureMessage> GetLastDayTemperatures();

        [OperationContract]
        void Dumy();
        [OperationContract]
        List<string> ExceptionsLog();
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "TemperatureService.ContractType".
    /*[DataContract]
    [Table("TemperatureTableeee")]*/
    public class TemperatureMessage
    {
       // [DataMember, Key]
        public DateTime TimeStamp { get; set; }

        //[DataMember]
        public String Temperature { get; set; }
        //[DataMember]
        public String Sender { get; set; }
    }
}
