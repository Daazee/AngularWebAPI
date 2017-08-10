//(function () {
//    "use strict";
//    var module = angular.module("employeeManagement");

//    function controller($http) {
//        debugger;
//        var model = this;
//        model.FirstName = "this";
//        model.addEmployee = function () {
//            alert(model.FirstName)
//            return $http.post("")
//            .then(function (response) {
//                return response.data;
//            })
//        };


//    }
//    module.component("createEmployee", {
//        templateUrl: "app/Employee/AddEmployee/AddEmployee.html",
//        controllerAs: "model",
//        controller: ["$http", controller]
//    });
//}());

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