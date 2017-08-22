var MyApp = angular.module("MyApp", []);
MyApp.filter('reverse', [function () {
    return function (string) {
        return string.split('').reverse().join('');
    }
}]);