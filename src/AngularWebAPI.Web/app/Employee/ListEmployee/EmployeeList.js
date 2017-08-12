(function () {
   "use strict"
   var module = angular.module("employeeManagement");

   function controller($http) {

       function fetchEmployees($http) {
           return $http.get("http://localhost:18558//api/Employee")
           .then(function (response) {
               console.log(response)
               return response.data

});
       }

       var model = this;
       model.employees = [];
       model.$onInit = function () {
           fetchEmployees($http).then(function(employees){
               model.employees = employees;
               alert(model.employees);
           });
       };
   }

   module.component("employeeList", {
       templateUrl: "app/Employee/ListEmployee/EmployeeList.html",
       controllerAs: "model",
       controller: ["$http", controller]
   });

}())