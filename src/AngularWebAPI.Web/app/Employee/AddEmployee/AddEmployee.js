(function () {
    "use strict"
    var module = angular.module("employeeManagement");

    function controller($http, baseUrl) {
        var model = this;
        model.employee = [];
        model.dependants = [];
        model.employeeId = 0;
        model.showPersonal1 = false;
        model.showPersonal2 = false;
        model.showPersonal3 = false;

        model.addEmployee = function () {
            var Employee = {};
            Employee.Lastname = model.lastname;
            Employee.Firstname = model.firstname;
            Employee.DateOfBirth = model.dateOfBirth;
            Employee.Gender = model.gender;
            Employee.Position = model.position;

            return $http.post(`${baseUrl}api/Employee/AddEmployee`, JSON.stringify(Employee))
                    .then(function (response) {
                        model.employeeId = response.data
                        console.log(model.employeeId);
                        model.showPersonal1 = true;
                        model.showPersonal2 = true;
                        return response.data
                    });
        }

        model.uploadPicture = function () {
            var id = 1;
           // //return $http.post(`${baseUrl}api/EmployeeImage/UploadImage/${id}`, model.file)
           // //       .then(function (response) {
           // //           console.log(response.data);
           // //           return response.data
           // //       });
           //$http.post({
           //    url: '${baseUrl}api/EmployeeImage/UploadImage',
           //    //data: { EmployeeID: id },
           //     file: model.file, // or list of files ($files) for html5 only
           // }).progress(function (evt) {
           //     //console.log('percent: ' + parseInt(100.0 * evt.loaded / evt.total));
           // }).success(function (data, status, headers, config) {
           //     alert('Uploaded successfully ' + file.name);
           // }).error(function (err) {
           //     alert('Error occured during upload');
           // });
            model.showPersonal2 = false;
            model.showPersonal3 = true;
        };

        // upload on file select or drop
        model.upload = function (file) {
            Upload.upload({
                url: 'upload/url',
                data: { file: file, 'username': $scope.username }
            }).then(function (resp) {
                console.log('Success ' + resp.config.data.file.name + 'uploaded. Response: ' + resp.data);
            }, function (resp) {
                console.log('Error status: ' + resp.status);
            }, function (evt) {
                var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                console.log('progress: ' + progressPercentage + '% ' + evt.config.data.file.name);
            });
        };


        model.addDependant = function () {
            var Dependant = {};
            Dependant.EmployeeId = model.employeeId;
            Dependant.Lastname = model.dependantLastname;
            Dependant.Firstname = model.dependantFirstname;
            Dependant.Gender = model.dependantGender;
            Dependant.Relationship = model.dependantRelationship;

            return $http.post(`${baseUrl}api/EmployeeDependant/AddDependant`, JSON.stringify(Dependant))
                   .then(function (response) {
                       console.log("Added Dependants");
                       console.log(response.data);
                       model.employeeId = response.data.employeeID
                       fetchDependantsByEmployeeID(model.employeeId).then(function (dependants) {
                           console.log("My dependants")
                           console.log(dependants)
                           model.dependants = dependants;
                       });
                   });
        };



        function fetchDependantsByEmployeeID(id) {
            return $http.get(`${baseUrl}/api/EmployeeDependant/GetDependantsByEmployeeID/${id}`)
            .then(function (response) {
                return response.data
            });
        }

    }

    module.component("createEmployee", {
        templateUrl: "app/Employee/AddEmployee/AddEmployee.html",
        controllerAs: "model",
        controller: ["$http", "baseUrl", controller]
    });
}())