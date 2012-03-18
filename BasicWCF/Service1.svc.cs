using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.ServiceModel.Activation;
using System.IO;
using System.Web;
using System.Net;

namespace BasicWCF {
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service1 : IService1, IStreamService {
        public string GetData(int value) {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite) {
            if (composite == null) {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue) {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public Stream GetMp3Stream(string id) {
            var currentHttpContext = HttpContext.Current;
            if (currentHttpContext != null) {
                currentHttpContext.Response.Headers["Content-Type"] = "audio/mpeg";
            }
            var path = @"C:\Ambient042.mp3";
            if (!File.Exists(path)) {
                throw new WebFaultException(HttpStatusCode.NotFound);
            }
            var stream = File.OpenRead(path);
            return stream;
        }

        public Stream GetWavStream(string id) {
            var currentHttpContext = HttpContext.Current;
            if (currentHttpContext != null) {
                currentHttpContext.Response.Headers["Content-Type"] = "audio/wav";
            }
            var path = @"C:\Ambient042.mp3";
            if (!File.Exists(path)) {
                throw new WebFaultException(HttpStatusCode.NotFound);
            }
            var stream = File.OpenRead(path);
            return stream;
        }

        public Stream GetPolicy() {
            String path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            path = System.IO.Path.GetDirectoryName(path);

            StreamReader strReader = File.OpenText(Path.Combine(path, "crossdomain.xml"));
            var str1 = strReader.ReadToEnd();

            WebOperationContext.Current.OutgoingResponse.ContentType = "application/xml";
            WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
            WebOperationContext.Current.OutgoingResponse.ContentLength = strReader.BaseStream.Length;
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(str1));
            return stream;
        }
    }
}
