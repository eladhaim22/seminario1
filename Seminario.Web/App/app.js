var app = angular.module('app', ['ngRoute','ui.bootstrap']);

app.config(function ($routeProvider) {
    $routeProvider
        .when('/', { templateUrl: '/App/AddSimulacion', controller: 'simulacionController' })
        /*.when('/about', {
            templateUrl: 'pages/about.html',
            controller: 'aboutController'
        })*/
});

