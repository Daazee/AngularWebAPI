(function () {
    "use strict"
    var module = angular.module("employeeManagement");
    var baseUrl = "http://localhost:18558/";

    function controller($http) {
        var model = this;
        model.employee = [];

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

            var request = $http({
                url: baseUrl + "api/Employee/AddEmployee",
                method: "post",
                contentType: 'application/json',
                data: JSON.stringify(Employee),
                success: successFunc(),
                error: errorFunc()
            });

            function successFunc(response) {
                console.log(response);
                alert(response);
                model.showPersonal1 = true;
                model.showPersonal2 = true;
            }

            function errorFunc(error) {
                //alert("Error occured");//To avoid alerting if no internet connection.
                console.log(error);
            }
        };

    }

    module.component("createEmployee", {
        templateUrl: "app/Employee/AddEmployee/AddEmployee.html",
        controllerAs: "model",
        controller: ["$http", controller]
    });

}())