using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;

namespace BasicWCF {
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1 {

        [OperationContract]
        //[WebGet] 
        string GetData(int value);

        [OperationContract]
        //[WebInvoke]
        CompositeType GetDataUsingDataContract(CompositeType composite);

    }

    [ServiceContract]
    public interface IStreamService {
        [OperationContract]
        [WebGet] // to allow get request
        Stream GetStream();

        [OperationContract]
        [WebGet(UriTemplate = "/crossdomain.xml")] 
        Stream GetPolicy();
    }

    [DataContract]
    public class CompositeType {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
