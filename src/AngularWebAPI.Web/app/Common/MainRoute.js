(function () {
    "use strict";
    var module = angular.module("employeeManagement");

    module.component("mainRoute", {
        templateUrl: "app/Common/MainRoute.html",
        $routeConfig: [
            { path: "/EmployeeList", component: "employeeList", name: "EmployeeList" },
            { path: "/AddEmployee", component: "createEmployee", name: "AddEmployee" },
            { path: "/EmployeeDetail/:id", component: "employeeDetail", name: "EmployeeDetail" },
            { path: "/**", redirectTo: ["EmployeeList"] },

        ]
    });
}());