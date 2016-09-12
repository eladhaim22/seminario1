var app = angular.module('app');

app.directive("percent", function ($filter) {
	var p = function (viewValue) {
		var m = viewValue.match(/^(\d+)/);
		if (m !== null)
			return $filter('number')(parseFloat(viewValue) / 100);
	};

	var f = function (modelValue) {
		return $filter('number')(parseFloat(modelValue) * 100) != 0 ? $filter('number')(parseFloat(modelValue) * 100) + '%' : $filter('number')(parseFloat(modelValue) * 100);
	};

	return {
		require: 'ngModel',
		link: function (scope, ele, attr, ctrl) {
			ctrl.$parsers.unshift(p);
			ctrl.$formatters.unshift(f);

			ele.bind('blur', function () {
				if (!isNaN(ele.val()) && ele.val() != "")
					ele.val(ele.val() + '%');
			});

			ele.bind('focus', function () {
				ele.val(ele.val().replace('%', ''));
			});
		}
	};
});
