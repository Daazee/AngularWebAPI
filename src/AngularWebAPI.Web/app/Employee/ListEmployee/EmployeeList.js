(function () {
    "use strict"
    var module = angular.module("employeeManagement");
    var baseUrl = "http://localhost:18558/";

    function fetchEmployees($http) {
        return $http.get(`${baseUrl}api/Employee`)
        .then(function (response) {
            return response.data
        });
    }

    function controller($http) {
        var model = this;
        model.employees = [];
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
    }

    module.component("employeeList", {
        templateUrl: "app/Employee/ListEmployee/EmployeeList.html",
        controllerAs: "model",
        controller: ["$http", controller]
    });

}())