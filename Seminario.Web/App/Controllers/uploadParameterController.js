var app = angular.module('app');

app.controller('uploadParameterController', ['$scope', 'fileUpload', function ($scope, fileUpload) {

    $scope.uploadFile = function () {
        var file = $scope.myFile;
        console.log('file is ');
        console.dir(file);
        var uploadUrl = "/api/Upload/Upload";
        fileUpload.uploadFileToUrl(file, uploadUrl);
    };

}]);