(function () {
    "use strict"
    var module = angular.module("employeeManagement");

    function controller($http, baseUrl, Upload, $timeout) {
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
            var EmployeeImage = {};
           

            //var f = document.getElementById('file').files[0],
            //    r = new FileReader();

            //r.onloadend = function (e) {
            //    var data = e.target.result;

            //    EmployeeImage.EmployeeId = 1;
            //    EmployeeImage.Image = data;
            //    console.log(data);
            //    return $http.post(`${baseUrl}api/EmployeeImage/UploadImage`, JSON.stringify(EmployeeImage))
            //      .then(function (response) {
            //          console.log(response.data);
            //          return response.data;
            //      });



            //}
            //r.readAsBinaryString(f);

            var file = document.getElementById("imageFile").files[0];
            var r = new FileReader();
            r.onloadend = function (e) {


                var arr = Array.from(new Uint8Array(e.target.result));

                    EmployeeImage.EmployeeId = 1;
                    EmployeeImage.Image = arr;

                $http.post(`${baseUrl}api/EmployeeImage/UploadImage`, EmployeeImage)
                .then(
                function (response) {
                    console.log(response);
                },

                function (reason) {

                    console.log(reason);
                })
            }
            r.readAsArrayBuffer(file);
           
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

        // module.controller('MyCtrl', ['$scope', 'Upload', '$timeout', function ($scope, Upload, $timeout) {
        model.uploadPic = function (file) {
            alert('uploading')
            file.upload = Upload.upload({
                url: `${baseUrl}api/EmployeeImage/UploadImage`,
                data: { EmployeeID: 1, file: file },
            });

            file.upload.then(function (response) {
                $timeout(function () {
                    file.result = response.data;
                });
            }, function (response) {
                if (response.status > 0)
                    //$scope.errorMsg = response.status + ': ' + response.data;
                    console.log();
            }, function (evt) {
                // Math.min is to fix IE which reports 200% sometimes
                file.progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
            });
        }
        //}]);

    }

   
    module.component("createEmployee", {
        templateUrl: "app/Employee/AddEmployee/AddEmployee.html",
        controllerAs: "model",
        controller: ["$http", "baseUrl", "Upload", "$timeout", controller]
    });
}())