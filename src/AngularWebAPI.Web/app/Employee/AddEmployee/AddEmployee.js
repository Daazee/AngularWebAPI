(function () {
    "use strict";
    var module = angular.module("employeeManagement");

    module.component("createEmployee", {
        templateUrl: "app/Employee/AddEmployee/AddEmployee.html",
        controllerAs: "model",
        controller: function(){
            var model = this;
            model.addEmployee = function () {
                 
            alert("My fullname is " + model.Lastname + ' ' + model.Firstname);
        };

        }
    });
}());