using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using AspNetWebApiHost;
using System.Net.Http;
using AspNetWebApiHost.Models;

namespace AspNetWebApiHost.Controllers {
    public class EmployeesController : ApiController {
        private static Repository _repository = new Repository();

        [HttpGet]
        public IEnumerable<String> GetAllDepartments() {
            return _repository.GetDepartments();
        }

        [HttpGet]
        public IEnumerable<Employee> GetEmployeeByDepartment(string department) {
            return _repository.GetEmployees(department);
        }

        [HttpGet]
        public Employee GetEmployee(string department, string id) {
            return _repository.GetEmployees(department).FirstOrDefault(x => x.Name == id);
        }

        [HttpPost]
        public HttpResponseMessage<Employee> PostEmployee(string department, Employee employee) {
            _repository.AddEmployee(department, employee);
            var response = new HttpResponseMessage<Employee>(employee);
            response.StatusCode = System.Net.HttpStatusCode.Created;

            return response;
        }

        [HttpDelete]
        public void Delete(string department, string id) {
            _repository.DeleteEmployee(id);
        }
    }

    public class Repository {

        public Dictionary<Employee, String> _employees = new Dictionary<Employee, String> { 
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
