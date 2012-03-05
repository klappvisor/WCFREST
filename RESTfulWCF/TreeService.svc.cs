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

namespace RESTfulWCF {

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class TreeService : ITreeService {
        private static Repository _repository = new Repository();

        public IEnumerable<Symptom> GetRootNodes() {
            return _repository.GetRootNodes();
        }

        public Symptom GetNode(string nodeId) {
            Int32 id;
            if(!Int32.TryParse(nodeId, out id)){
                throw new HttpRequestValidationException();
            }
            return _repository.GetNode(id);
        }

        public IEnumerable<Symptom> GetChildren(string nodeId) {
            Int32 id;
            if(!Int32.TryParse(nodeId, out id)){
                throw new HttpRequestValidationException();
            }
            return _repository.GetChildren(id);
        }

        public Symptom AddRoot(Symptom symptom) {
            if(symptom == null) {
                throw new HttpRequestValidationException();
            }
            return _repository.CreateNode(symptom.Name, null);
        }

        public Symptom AddChild(string nodeId, Symptom child) {
            Int32 id;
            if (child == null || !Int32.TryParse(nodeId, out id)) {
                throw new HttpRequestValidationException();
            }
            var parent = _repository.GetNode(id);
            return _repository.CreateNode(child.Name, parent);
        }

        public void DeleteNode(string nodeId) {
            Int32 id;
            if (!Int32.TryParse(nodeId, out id))
            {
                throw new HttpRequestValidationException();
            }
            _repository.Delete(id);
        }

        public Diagnosis GetDiagnosis(string nodeId) {
            Int32 id;
            if (!Int32.TryParse(nodeId, out id))
            {
                throw new HttpRequestValidationException();
            }
            return _repository.GetDiagnosis(id);
        }

        public void SetDiagnosis(string nodeId, Diagnosis diagnosis) {
            Int32 id;
            if (!Int32.TryParse(nodeId, out id))
            {
                throw new HttpRequestValidationException();
            }
            _repository.SetDiagnosis(id, diagnosis);
        }
    }
}
