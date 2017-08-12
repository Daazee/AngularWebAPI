(function () {
    "use strict"
    var module = angular.module("employeeManagement");
    var baseUrl = "http://localhost:18558/";

    function controller($http) {
        var model = this;
        model.employee = [];
        
        model.addEmployee = function () {
            var Employee = [];
            model.employee.Lastname = model.lastname;
            model.employee.Firstname = model.firstname;
            model.employee.DateOfBirth = model.dateOfBirth;
            model.employee.Gender = model.gender;
            model.employee.Position = model.position;
            alert(model.lastname);

            Employee = model.employee
            return $http.post(`${baseUrl}api/Employee//${Employee}`)
            .then(function (response) {
                console.log(response.data);
                return response.data
            });

           
            //return $http.post(baseUrl + 'api/Employee/' + model.employee, JSON.stringify(model.employee), {
            //    headers: { 'Content-Type': 'application/json', "Access-Control-Allow-Origin": "*" }
            //    }).then(function (response) {
            //        return response;
            //    })

        };
    }

    module.component("createEmployee", {
        templateUrl: "app/Employee/AddEmployee/AddEmployee.html",
        controllerAs: "model",
        controller: ["$http", controller]
    });

}())