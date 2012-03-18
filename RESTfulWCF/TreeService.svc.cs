using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.ServiceModel.Activation;
using DAL;
using System.Web;
using System.Net;

namespace RESTfulWCF {

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class TreeService : ITreeService {
        private static Storage _storage = new Storage();

        public IEnumerable<Symptom> GetRootNodes() {
            return _storage.GetRootNodes();
        }

        public Symptom GetNode(string nodeId) {
            Int32 id;
            if (!Int32.TryParse(nodeId, out id)) {
                throw new WebFaultException(HttpStatusCode.BadRequest);
            }
            Symptom symptom = _storage.GetNode(id);
            if (symptom == null) {
                throw new WebFaultException(HttpStatusCode.NotFound);
            }
            return symptom;
        }

        public IEnumerable<Symptom> GetChildren(string nodeId) {
            Int32 id;
            if(!Int32.TryParse(nodeId, out id)){
                throw new WebFaultException(HttpStatusCode.BadRequest);
            }
            return _storage.GetChildren(id);
        }

        public Symptom AddRoot(Symptom symptom) {
            if(symptom == null) {
                throw new WebFaultException(HttpStatusCode.BadRequest);
            }
            return _storage.CreateNode(symptom.Name, null);
        }

        public Symptom AddChild(string nodeId, Symptom child) {
            Int32 id;
            if (child == null || !Int32.TryParse(nodeId, out id)) {
                throw new WebFaultException(HttpStatusCode.BadRequest);
            }
            var parent = _storage.GetNode(id);
            return _storage.CreateNode(child.Name, parent);
        }

        public void DeleteNode(string nodeId) {
            Int32 id;
            if (!Int32.TryParse(nodeId, out id)) {
                throw new WebFaultException(HttpStatusCode.BadRequest);
            }
            _storage.Delete(id);
        }

        public Diagnosis GetDiagnosis(string nodeId) {
            Int32 id;
            if (!Int32.TryParse(nodeId, out id)) {
                throw new WebFaultException(HttpStatusCode.BadRequest);
            }
            return _storage.GetDiagnosis(id);
        }

        public void SetDiagnosis(string nodeId, Diagnosis diagnosis) {
            Int32 id;
            if (!Int32.TryParse(nodeId, out id)) {
                throw new WebFaultException(HttpStatusCode.BadRequest);
            }
            _storage.SetDiagnosis(id, diagnosis);
        }
    }
}
