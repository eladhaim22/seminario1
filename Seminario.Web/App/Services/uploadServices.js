var app = angular.module('app');
app.service('fileUpload', ['$http', function ($http) {
	return {
		uploadFileToUrl: function (file, uploadUrl) {
			var fd = new FormData();
			fd.append('file', file);
			return $http.post(uploadUrl, fd, {
				transformRequest: angular.identity,
				headers: { 'Content-Type': undefined }
			})
			.success(function (data) {
				return;
			})
			.error(function (error) {
				return error;
			});
		}
	}
}]);
