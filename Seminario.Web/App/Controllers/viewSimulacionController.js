var app = angular.module('app');

app.controller('viewSimulacionController', ['$scope', '$location', 'SimulacionService',
    function ($scope, $location,SimulacionService) {

    var simulaciones = [];

    $scope.gridOptions = {
        enableSorting: true,
        enableCellEditOnFocus: true,
        columnDefs: [
          { name: "F. Descuento", field: 'FechaDescuento' },
          { name: "Gasto Total", field: 'GastoTotal' },
          { name: "N. Cheques", field: 'CantidadCheques' },
          { name: "Estado", field: 'Estado' },
          { name: "Legajo", field: 'Legajo' },
          {
              name: "Detalles", field: 'Detalles',
              cellTemplate: '<a href="#/{{row.entity.Id}}" class="btn btn-danger">Ver</div>'
          },
        ],
        data: []
    };

    SimulacionService.getAllSimulacion().then(function (response) {
        $scope.gridOptions.data = response.data;
    });
       
    $scope.redirect = function () {
        $location.path('/Simulacion/' + $routeParams);
    }
    
}]);