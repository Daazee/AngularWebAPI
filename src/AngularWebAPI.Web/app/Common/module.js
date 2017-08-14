(function () {
    "use strict";
    var module = angular.module("employeeManagement", ["ngComponentRouter"]);
    module.value("$routerRootComponent", "mainRoute")

    //api url
    module.constant('baseUrl', 'http://localhost:18558/');
}())