(function () {
    "use strict"
    var module = angular.module("employeeManagement");
    //var baseUrl = "http://localhost:18558/";
    var baseUrl = "http://employeesystemapi.azurewebsites.net/";
    function fetchEmployees($http) {
        return $http.get(`${baseUrl}api/Employee`)
        .then(function (response) {
            return response.data
        });
    }

    function controller($http) {
        var model = this;
        model.employees = [];
        model.employee = [];

        model.$onInit = function () {
            fetchEmployees($http).then(function (employees) {
                console.log("My employeed")
                console.log(employees)
                model.employees = employees;
            });
        };

        model.deleteEmployee = function (id) {
            console.log(id)
            if (confirm("Are you sure you want to delete this employee?")) {
                return $http.delete(`${baseUrl}api/Employee/DeleteEmployee/${id}`)
                            .then(function (response) {
                                location.reload()
                                //return response.data
                            });
            }
        }

        function fetchEmployee($http, id) {
            return $http.get(`${baseUrl}api/Employee/${id}`)
            .then(function (response) {
                return response.data
            });
        }

        model.getEmployee = function (id) {
            fetchEmployee($http, id).then(function (employee) {
                model.employee = employee;
            });
        }

        model.updateEmployee = function (id) {
            console.log(id)
            var Employee = {};
            Employee.EmployeeID = model.employee.employeeID;
            Employee.Lastname = model.employee.lastname;
            Employee.Firstname = model.employee.firstname;
            Employee.Gender = model.employee.gender;
            Employee.Position = model.employee.position;

            return $http.put(`${baseUrl}api/Employee/UpdateEmployee/${model.employee.employeeID}`, JSON.stringify(Employee))
                     .then(function (response) {
                         model.employeeId = response.data
                         console.log(model.employeeId);
                         return response.data
                         location.reload()
                     });
        }
    }

    module.component("employeeList", {
        templateUrl: "app/Employee/ListEmployee/EmployeeList.html",
        controllerAs: "model",
        controller: ["$http", controller]
    });

}())