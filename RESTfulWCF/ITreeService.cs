using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DAL;

namespace RESTfulWCF {
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.

    [ServiceContract]
    public interface ITreeService {
        [OperationContract]
        [WebGet(UriTemplate="/")]
        IEnumerable<Symptom> GetRootNodes();

        [OperationContract]
        [WebInvoke(UriTemplate = "/", Method = "POST")]
        Symptom AddRoot(Symptom employee);

        [OperationContract] 
        [WebGet(UriTemplate = "/{nodeId}")]
        Symptom GetNode(String nodeId);

        [OperationContract]
        [WebInvoke(UriTemplate = "/{nodeId}", Method = "DELETE")]
        void DeleteNode(String nodeId);

        [OperationContract]
        [WebGet(UriTemplate = "/{nodeId}/children")]
        IEnumerable<Symptom> GetChildren(String nodeId);

        [OperationContract]
        [WebInvoke(UriTemplate = "/{nodeId}/children", Method="POST")]
        Symptom AddChild(String nodeId, Symptom child);

        [OperationContract]
        [WebInvoke(UriTemplate = "/{nodeId}/diagnosis", Method = "POST")]
        void SetDiagnosis(String nodeId, Diagnosis diagnosis);

        [OperationContract]
        [WebInvoke(UriTemplate = "/{nodeId}/diagnosis", Method = "DELETE")]
        Diagnosis GetDiagnosis(String nodeId);
    }
}
