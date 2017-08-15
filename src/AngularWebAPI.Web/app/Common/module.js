(function () {
    "use strict";
    var module = angular.module("employeeManagement", ["ngComponentRouter", "ngFileUpload"]);
    module.value("$routerRootComponent", "mainRoute")

    //api url
    module.constant('baseUrl', 'http://employeesystemapi.azurewebsites.net/');
}())