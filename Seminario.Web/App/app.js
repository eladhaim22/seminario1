var app = angular.module('app', ['ngRoute', 'ui.bootstrap', 'ui.grid', 'ngSanitize']);

app.config(function ($routeProvider) {
	$routeProvider
        .when('/Upload', { templateUrl: '/App/Upload', controller: 'uploadParameterController' })
        .when('/ViewSimulaciones', { templateUrl: '/App/ViewSimulacion', controller: 'viewSimulacionController' })

        .when('/Simulacion/:id?', { templateUrl: '/App/AddSimulacion', controller: 'simulacionController' })
		.otherwise({ redirectTo: '/Simulacion/:id?' });
	//.when('/Simulacion/:id'), { templateUrl: '/App/AddSimulacion', controller: 'simulacionController' }
	/*.when('/about', {
		templateUrl: 'pages/about.html',
		controller: 'aboutController'
	})*/
});

app.run(function ($rootScope) {
	$rootScope.legajo = window.legajo;
	$rootScope.role = window.role;
});
