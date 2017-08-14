(function () {
    "use strict"
    var module = angular.module("employeeManagement");

    function controller($http, baseUrl) {
        var model = this;
        model.employees = [];
        model.employee = [];
        function fetchEmployees() {
            return $http.get(`${baseUrl}api/Employee`)
            .then(function (response) {
                return response.data
            });
        }

        model.$onInit = function () {
            fetchEmployees().then(function (employees) {
                model.employees = employees;
            });
        };

        model.deleteEmployee = function (id) {
            console.log(id)
            if (confirm("Are you sure you want to delete this employee?")) {
                return $http.delete(`${baseUrl}api/Employee/DeleteEmployee/${id}`)
                            .then(function (response) {

                                //Fetch updated employee list
                                fetchEmployees().then(function (employees) {
                                    model.employees = employees;
                                });
                            });
            }
        }

        function fetchEmployee(id) {
            return $http.get(`${baseUrl}api/Employee/${id}`)
            .then(function (response) {
                return response.data
            });
        }

        model.getEmployee = function (id) {
            fetchEmployee(id).then(function (employee) {
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
                         angular.element('#discountModal').modal('hide');

                         //Fetch updated employee list
                         fetchEmployees().then(function (employees) {
                             model.employees = employees;
                         });

                         return response.data
                     });
   
        }
    }

    module.component("employeeList", {
        templateUrl: "app/Employee/ListEmployee/EmployeeList.html",
        controllerAs: "model",
        controller: ["$http", "baseUrl", controller]
    });

}())