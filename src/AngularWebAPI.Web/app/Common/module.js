(function () {
    "use strict";
    var module = angular.module("employeeManagement", ["ngComponentRouter"]);
module.config(function($httpProvider){
    $httpProvider.defaults.useXDomain = true;
    delete $httpProvider.defaults.headers.common['X-Requested-With'];
    //$httpProvider.interceptors.push('authInterceptorService');
});

    module.value("$routerRootComponent","mainRoute")
}())