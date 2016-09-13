var app = angular.module('app');

app.directive('validateImporte', function () {
	return {
		require: 'ngModel',
		link: function (scope, element, attrs, ctrl) {
			ctrl.$validators.importe = function (modelValue, viewValue) {
				if (!isNaN(viewValue) && viewValue > 0) {
					return true;
				}
				return false;
			};
		}
	};
});
