using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.ServiceModel.Activation;

namespace RESTfulWCF {
    
    //[ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)] 
    public class EmployeeService : IEmployeeService {
        private Repository _repository = new Repository();

        public IList<String> GetDepartments() {
            return _repository.GetDepartments();
        }

        public List<Employee> GetEmployeesByDepartment(string departmentName) {
            return _repository.GetEmployees(departmentName);
        }

        public void AddEmployee(string departmentName, Employee employee) {
            _repository.AddEmployee(departmentName, employee);
        }

        public void DeleteEmployee(string departmentName, string employee) {
            _repository.DeleteEmployee(employee);
        }

        public string Ping() {
            return "ping";
        }

        public Employee Resend(Employee employee) {
            if(employee == null)
                employee = new Employee {Name ="ddd"};
            return employee;
        }
    }

    public class Repository {

        public static Dictionary<Employee, String> _employees = new Dictionary<Employee, String> { 
            { new Employee {Name = "developer1"}, ".NET" },
            { new Employee {Name = "developer2"}, ".NET" },
            { new Employee {Name = "developer3"}, ".NET" },
            { new Employee {Name = "developer4"}, "PHP" },
            { new Employee {Name = "developer5"}, "PHP" }
        };

        public List<String> GetDepartments() {
             return _employees.Values.Distinct().ToList();
        }

        public List<Employee> GetEmployees(string departmentName) {
            return _employees.Where(x => x.Value == departmentName).Select(x => x.Key).ToList();
        }

        public void AddEmployee(string department, Employee employee) {
            _employees.Add(employee, department);
        }

        public void DeleteEmployee(string employeeName) {
            var key = _employees.Keys.FirstOrDefault(x => x.Name == employeeName);
            _employees.Remove(key);
        }
    }
}
