(function () {
    "use strict"
    var module = angular.module("employeeManagement");
    var baseUrl = "http://localhost:18558/";

    function controller($http) {
        var model = this;
        model.employee = [];
        model.employeeId = 0;
        model.showPersonal1 = false;
        model.showPersonal2 = false;
        model.showPersonal3 = false;

        model.addEmployee = function () {
            var Employee = {};
            Employee.Lastname = model.lastname;
            Employee.Firstname = model.firstname;
            Employee.DateOfBirth = model.dateOfBirth;
            Employee.Gender = model.gender;
            Employee.Position = model.position;

            return $http.post(`${baseUrl}api/Employee/AddEmployee`, JSON.stringify(Employee))
                    .then(function (response) {
                        model.employeeId = response.data
                        console.log(model.employeeId);
                        return response.data
                    });
        }



        model.addDependant = function () {
            var Dependant = {};
            Dependant.Lastname = model.dependantLastname;
            Dependant.Firstname = model.dependantFirstname;
            Dependant.Gender = model.dependantGender;
            Dependant.dependantRelationship = model.dependantRelationship;

            return $http.post(`${baseUrl}api/EmployeeDependant/AddDependant`, JSON.stringify(Dependant))
                   .then(function (response) {
                       model.employeeId = response.data
                       console.log(model.employeeId);
                       return response.data
                   });
        };

    }

    module.component("createEmployee", {
        templateUrl: "app/Employee/AddEmployee/AddEmployee.html",
        controllerAs: "model",
        controller: ["$http", controller]
    });

}())