var app = angular.module('app');

app.controller('viewSimulacionController', ['$scope', '$location', 'SimulacionService', '$rootScope', '$q',
    function ($scope, $location, SimulacionService, $rootScope, $q) {
    	var simulaciones = [];
    	var estado = ["Aceptado", "Rechazado", "A Revisar"];
    	$scope.gridOptions = {
    		enableSorting: true,
    		enableCellEditOnFocus: true,
    		columnDefs: [
    			{ name: "F. Creacion", field: 'FechaCreacion' },
				{ name: "Ulitma Modi.", field: 'FechaUltimaModificacion' },
				{ name: "F. Descuento", field: 'FechaDescuento' },
				{ name: "Gasto Total", field: 'GastoTotal' },
				{ name: "N. Cheques", field: 'CantidadCheques' },
				{ name: "Estado", field: 'Estado' },
				{ name: "Hecho Por", field: 'Legajo.Nombre' },
				{
					name: "Detalles", field: 'Detalles',
					cellTemplate: '<div ng-click="grid.appScope.directTo(row.entity.Id)" class="btn btn-info embebed-button">Ver</div>'
				},
    		],
    		data: []
    	};
    	$scope.GetEstado = function (estadoId) {
    		return estado[estadoId];
    	}

    	$scope.directTo = function (id) {
    		$location.path(id);
    		$rootScope.errorMsg = undefined;
    		$rootScope.successMsg = undefined;
    	}
    	$q.all([SimulacionService.getAllSimulacion(), SimulacionService.getAllEmpleados()]).then(function (results) {
    		angular.forEach(results[0].data, function (value) {
    			value.Estado = estado[value.Estado];
    			value.Legajo = _.filter(results[1].data, function (o) {
    				if (value.Legajo == o.Legajo)
    					return o;
    			})[0];
    		});
    		$scope.gridOptions.data = results[0].data;
    	});

    	$scope.redirect = function () {
    		$location.path('/Simulacion/' + $routeParams);
    	}
    }]);
