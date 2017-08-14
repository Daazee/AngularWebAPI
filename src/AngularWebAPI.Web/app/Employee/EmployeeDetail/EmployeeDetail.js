(function () {
    "use strict"
    var module = angular.module("employeeManagement");

   

    function controller($http, baseUrl) {

        var model = this;
        model.employee = [];
        model.dependants = [];
        model.$routerOnActivate = function (next) {
            model.id = next.params.id
            console.log("params" + model.id)

            function fetchEmployee(id) {
                return $http.get(`${baseUrl}api/Employee/${id}`)
                .then(function (response) {
                    console.log("employee detail")
                    return response.data
                });
            }

            function fetchDependantsByEmployeeID(id) {
                return $http.get(`${baseUrl}/api/EmployeeDependant/GetDependantsByEmployeeID/${id}`)
                .then(function (response) {
                    return response.data
                });
            }

            fetchEmployee(model.id).then(function (employee) {
                console.log(employee)
                model.employee = employee;
            });

            fetchDependantsByEmployeeID(model.id).then(function (dependants) {
                console.log("My dependants")
                console.log(dependants)
                model.dependants = dependants;
            });
        }
    }



    module.component("employeeDetail", {
        templateUrl: "app/Employee/EmployeeDetail/EmployeeDetails.html",
        controllerAs: "model",
        controller: ["$http", "baseUrl", controller]
    });

}())