(function () {
    "use strict";
    var module = angular.module("employeeManagement", ["ngComponentRouter", "ngFileUpload"]);
    module.value("$routerRootComponent", "mainRoute")

    //api url
    module.constant('baseUrl', 'http://employeesystemapi.azurewebsites.net/');
   // module.constant('baseUrl', 'http://localhost:18558/');
    module.config(function ($httpProvider) {
        $httpProvider.defaults.useXDomain = true;
        delete $httpProvider.defaults.headers.common['X-Requested-With'];
        $httpProvider.interceptors.push('authInterceptorService');
    });
}())