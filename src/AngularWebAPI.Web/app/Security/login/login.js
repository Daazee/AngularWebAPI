(function () {
    "use strict"
    var module = angular.module("employeeManagement");

    function controller($http, baseUrl) {
        var model = this;
    }

    module.component("login", {
        templateUrl: "app/Security/login/login.html",
        controllerAs: "model",
        controller: ["$http", "baseUrl", controller]
    });
}())