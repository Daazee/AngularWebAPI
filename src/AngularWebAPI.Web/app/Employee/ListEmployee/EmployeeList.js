(function () {
   "use strict"
   var module = angular.module("employeeManagement");

   function controller($http) {

       function fetchEmployees($http) {
           return $http.get("http://localhost/AngularWebAPI.WebAPI/api/Employee")
           .then(function(response){

});
       }

       var model = this;
       model.employees = [];
       model.$onInit = function () {
           fetchEmployees($http).then(function(employees){
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