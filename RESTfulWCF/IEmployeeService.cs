using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RESTfulWCF {
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IEmployeeService {

        [OperationContract]
        [WebGet(UriTemplate="/")]
        IList<String> GetDepartments();

        [OperationContract] 
        [WebGet(UriTemplate = "/{departmentName}")]
        List<Employee> GetEmployeesByDepartment(String departmentName);

        [OperationContract]
        [WebInvoke(UriTemplate = "/{departmentName}", Method = "POST")]
        void AddEmployee(String departmentName, Employee employee);

        [OperationContract]
        [WebInvoke(UriTemplate = "/{departmentName}/{employeeName}", Method = "DELETE")]
        void DeleteEmployee(String departmentName, string employeeName);
    }

    [DataContract]
    public class Employee {
        [DataMember]
        public String Name { get; set; }
    }
}
