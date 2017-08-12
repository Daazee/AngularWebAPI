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
    }

    module.component("employeeList", {
        templateUrl: "app/Employee/ListEmployee/EmployeeList.html",
        controllerAs: "model",
        controller: ["$http", controller]
    });

}())