'use script'
var module = angular.module("employeeManagement");
module.factory('authInterceptorService', ['$q', '$location', function ($q, $location) {
    var authInterceptorServiceFactory = {};
    var _request = function(config) {
        config.headers = config.headers || {};
        var authData = localStorage.getItem('authorizationData');
        if(authData){
            config.headers.Authorization = 'Bearer ' + authData;
        }
        return config
    }
    var _responseError = function (rejection) {
        if (rejection.status == 401 || rejection.status == 400 || rejection.status==404) {
            $location.path('/login');
        }
        return $q.reject(rejection);
    };

    authInterceptorServiceFactory.request = _request;
    authInterceptorServiceFactory.responseError = _responseError;
    return authInterceptorServiceFactory;
}]);