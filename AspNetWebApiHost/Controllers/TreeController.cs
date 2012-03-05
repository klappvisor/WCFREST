using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using AspNetWebApiHost;
using System.Net;
using DAL;

namespace AspNetWebApiHost.Controllers {
    public class TreeController : ApiController {
        private static Repository _repository = new Repository();

        [HttpGet, ActionName("")]
        public IEnumerable<Symptom> GetRootNodes()
        {
            return _repository.GetRootNodes();
        }

        [HttpGet]
        public Symptom GetNode(string nodeId)
        {
            Int32 id;
            if (!Int32.TryParse(nodeId, out id))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return _repository.GetNode(id);
        }

        [HttpGet, ActionName("children")]
        public IEnumerable<Symptom> GetChildren(string nodeId)
        {
            Int32 id;
            if (!Int32.TryParse(nodeId, out id))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return _repository.GetChildren(id);
        }

        [HttpPost]
        public Symptom AddRoot(Symptom symptom)
        {
            if (symptom == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return _repository.CreateNode(symptom.Name, null);
        }

        [HttpPost, ActionName("children")]
        public Symptom AddChild(string nodeId, Symptom child)
        {
            Int32 id;
            if (child == null || !Int32.TryParse(nodeId, out id))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var parent = _repository.GetNode(id);
            return _repository.CreateNode(child.Name, parent);
        }

        [HttpDelete]
        public void DeleteNode(string nodeId)
        {
            Int32 id;
            if (!Int32.TryParse(nodeId, out id))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            _repository.Delete(id);
        }

        [HttpGet, ActionName("diagnosis")]
        public Diagnosis GetDiagnosis(string nodeId)
        {
            Int32 id;
            if (!Int32.TryParse(nodeId, out id))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return _repository.GetDiagnosis(id);
        }

        [HttpPost, ActionName("diagnosis")]
        public void SetDiagnosis(string nodeId, Diagnosis diagnosis)
        {
            Int32 id;
            if (!Int32.TryParse(nodeId, out id))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            _repository.SetDiagnosis(id, diagnosis);
        }
    }
}
