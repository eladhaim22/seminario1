var app = angular.module('app');

app.controller('uploadParameterController', ['$scope', '$timeout', 'fileUpload', function ($scope, $timeout, fileUpload) {
	$scope.successMessage = undefined;
	$scope.errorMessage = undefined;

	$scope.uploadFile = function () {
		var file = $scope.myFile;
		var uploadUrl = "/api/Upload/Upload";
		fileUpload.uploadFileToUrl(file, uploadUrl).success(function (successMessage) {
			$scope.successMessage = "Los parametros han sido cargado de forma exitosa";
			$timeout(function () { $scope.successMessage = undefined; }, 3000);
		}).error(function (error) {
			$scope.errorMessage = error.Message;
			$timeout(function () { $scope.errorMessage = undefined; }, 3000);
		});
	};
}]);
