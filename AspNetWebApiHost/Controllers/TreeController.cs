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
        private readonly IStorage _storage;

        public TreeController(IStorage storage) { _storage = storage; }

        [HttpGet, ActionName("index")]
        public IQueryable<Symptom> GetRootNodes() {
            return _storage.GetRootNodes().AsQueryable();
        }

        [HttpGet, ActionName("index")]
        public Symptom GetNode(Int32 nodeId) {
            Symptom symptom = _storage.GetNode(nodeId);
            if(symptom == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return symptom;
        }

        [HttpGet, ActionName("children")]
        public IEnumerable<Symptom> GetChildren(Int32 nodeId){
            return _storage.GetChildren(nodeId);
        }

        [HttpPost, ActionName("index")]
        public HttpResponseMessage<Symptom> AddRoot(Symptom symptom) {
            if (symptom == null) {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var newSymptom = _storage.CreateNode(symptom.Name, null);
            return BuildCreateMessage(newSymptom);
        }

        [HttpDelete, ActionName("index")]
        public void DeleteNode(Int32 nodeId) {
            _storage.Delete(nodeId);
        }

        [HttpPost, ActionName("children")]
        public HttpResponseMessage<Symptom> AddChild(Int32 nodeId, Symptom child) {
            var parent = _storage.GetNode(nodeId);
            if (parent == null) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var newSymptom = _storage.CreateNode(child.Name, parent);
            return BuildCreateMessage(newSymptom);
        }

        [HttpGet, ActionName("diagnosis")]
        public Diagnosis GetDiagnosis(Int32 nodeId){
            Diagnosis diagnosis = _storage.GetDiagnosis(nodeId);
            if (diagnosis == null) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return diagnosis;
        }

        [HttpPost, ActionName("diagnosis")]
        public void SetDiagnosis(Int32 nodeId, Diagnosis diagnosis) {
            if (diagnosis == null) {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            _storage.SetDiagnosis(nodeId, diagnosis);
        }

        [HttpDelete, ActionName("diagnosis")]
        public void DeleteDiagnosis(Int32 nodeId) {
            _storage.Delete(nodeId);
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
