(function () {
    "use strict"
    var module = angular.module("employeeManagement");
    var baseUrl = "http://localhost:18558/";

    function fetchEmployee(id) {
        return $http.get(`${baseUrl}api/Employee/${id}`)
        .then(function (response) {
            console.log("employee detail")
            console.log(response.data)
            return response.data
        });
    }

    function controller($http) {
        var model = this;
        model.employee;
        model.$routerOnActivate = function (next) {
            model.id = next.params.id
            console.log("params" + model.id)
        model.$onInit = function () {
            //fetchEmployees($http).then(function (employees) {
            //    console.log(employees)
            //    model.employees = employees;
            //});
            model.employee = fetchEmployee(model.id)
            console.log (model.employee)
        };

        }
    }



    module.component("employeeDetail", {
        templateUrl: "app/Employee/EmployeeDetail/EmployeeDetails.html",
        controllerAs: "model",
        controller: ["$http", controller]
    });

}())