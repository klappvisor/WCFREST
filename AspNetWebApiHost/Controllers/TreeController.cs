using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using AspNetWebApiHost;
using System.Net;
using DAL;
using System.Net.Http;

namespace AspNetWebApiHost.Controllers {
    public class TreeController : ApiController {
        private static Repository _repository = new Repository();

        [HttpGet, ActionName("index")]
        public IEnumerable<Symptom> GetRootNodes() {

            return _repository.GetRootNodes();
        }

        [HttpGet, ActionName("index")]
        public Symptom GetNode(string nodeId) {
            Int32 id;
            if (!Int32.TryParse(nodeId, out id)) {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return _repository.GetNode(id);
        }

        [HttpGet, ActionName("children")]
        public IEnumerable<Symptom> GetChildren(string nodeId) {
            Int32 id;
            if (!Int32.TryParse(nodeId, out id)) {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return _repository.GetChildren(id);
        }

        [HttpPost]
        public HttpResponseMessage<Symptom> AddRoot(Symptom symptom) {
            if (symptom == null) {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var newSymptom = _repository.CreateNode(symptom.Name, null);
            return BuildCreateMessage(newSymptom);
        }

        [HttpPost, ActionName("children")]
        public HttpResponseMessage<Symptom> AddChild(string nodeId, Symptom child) {
            Int32 id;
            if (child == null || !Int32.TryParse(nodeId, out id)) {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var parent = _repository.GetNode(id);
            var newSymptom = _repository.CreateNode(child.Name, parent);
            return BuildCreateMessage(newSymptom);
        }

        [HttpDelete]
        public void DeleteNode(string nodeId) {
            Int32 id;
            if (!Int32.TryParse(nodeId, out id)) {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            _repository.Delete(id);
        }

        [HttpGet, ActionName("diagnosis")]
        public Diagnosis GetDiagnosis(string nodeId) {
            Int32 id;
            if (!Int32.TryParse(nodeId, out id)) {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return _repository.GetDiagnosis(id);
        }

        [HttpPost, ActionName("diagnosis")]
        public void SetDiagnosis(string nodeId, Diagnosis diagnosis) {
            Int32 id;
            if (!Int32.TryParse(nodeId, out id)) {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            _repository.SetDiagnosis(id, diagnosis);
        }

        private HttpResponseMessage<Symptom> BuildCreateMessage(Symptom s) {
            var response = new HttpResponseMessage<Symptom>(s, HttpStatusCode.Created);
            string relativeUri = Url.Route("DefaultAPI", new { nodeId = s.ID });
            Uri baseUri = new Uri(String.Format("{0}://{1}", Request.RequestUri.Scheme, Request.RequestUri.Authority));
            response.Headers.Location = new Uri(baseUri, relativeUri);
            return response;
        }
    }
}
