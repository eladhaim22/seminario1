var app = angular.module('app');

app.factory('SimulacionService', ['$http', function ($http) {
    return {
        consultarTor: function (cuit) {
            return $http.get("/api/Validationes/GetTorState/" + cuit).success(function (data) {
                return data;
            }).error (function (error) {
                alert(error);
            });
        },

        consularNosis: function (data) {
            return $http.post("/api/Validationes/GetNosisState/", { "rows": data }).success(function (data) {
                return data;
            }).error (function (error) {
                alert(error);
            });
        },

        fillComboProducto: function () {
            return $http.get("/api/Setting/GetAllProductos").success(function (data) {
                return data;
            }).error (function (error) {
                alert(error);
            });
        },

        fillComboProvincia: function () {
            return $http.get("/api/Setting/GetAllProvincias").success(function (data) {
                return data;
            }).error (function (error) {
                alert(error);
            });
        },

        saveSimulacion: function (simulacion){
            return $http.post("/api/Simulacion/PostSimulacion/", simulacion).success(function (data) {
                return data;
            }).error (function (error) {
                alert(error);
            });
        }
   }
}]);