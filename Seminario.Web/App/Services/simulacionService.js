var app = angular.module('app');

app.factory('SimulacionService', ['$http', function ($http) {
	return {
		consultarTor: function (cuit) {
			return $http.get("/api/Validationes/GetTorState/" + cuit).success(function (data) {
				return data;
			}).error(function (error) {
				alert(error);
			});
		},

		consularNosis: function (data) {
			return $http.post("/api/Validationes/GetNosisState/", { "rows": data }).success(function (data) {
				return data;
			}).error(function (error) {
				alert(error);
			});
		},

		fillComboProducto: function () {
			return $http.get("/api/Setting/GetAllProductos").success(function (data) {
				return data;
			}).error(function (error) {
				alert(error);
			});
		},

		fillComboProvincia: function () {
			return $http.get("/api/Setting/GetAllProvincias").success(function (data) {
				return data;
			}).error(function (error) {
				alert(error);
			});
		},

		createSimulacion: function (simulacion) {
			return $http.post("/api/Simulacion/CreateSimulacion/", simulacion).success(function (data) {
				return data;
			}).error(function (error) {
				return error;
			});
		},

		updateSimulacion: function (simulacion) {
			return $http.post("/api/Simulacion/UpadateSimulacion/", simulacion).success(function (data) {
				return data;
			}).error(function (error) {
				alert(error);
			});
		},

		getSimulacionById: function (simulacionId) {
			return $http.get("/api/Simulacion/GetSimulacionById/" + simulacionId).success(function (simulacion) {
				simulacion.FechaDescuento = new Date(simulacion.FechaDescuento);
				angular.forEach(simulacion.Cheques, function (cheque) {
					cheque.FechaAcreditacion = new Date(cheque.FechaAcreditacion);
				});
				return simulacion;
			}).error(function (error) {
				alert(error);
			});
		},

		getAllSimulacion: function (simulacion) {
			return $http.get("/api/Simulacion/GetAllSimulacion").success(function (simulaciones) {
				angular.forEach(simulaciones, function (simulacion) {
					simulacion.FechaDescuento = moment(simulacion.FechaDescuento).format("DD/MM/YYYY");
					simulacion.FechaCreacion = moment(simulacion.FechaCreacion).format("DD/MM/YYYY  HH:mm");
					simulacion.FechaUltimaModificacion = moment(simulacion.FechaUltimaModificacion).format("DD/MM/YYYY HH:mm");
					angular.forEach(simulacion.Cheques, function (cheque) {
						cheque.FechaAcreditacion = new Date(cheque.FechaAcreditacion);
					});
				});
				return simulaciones;
			}).error(function (error) {
				alert(error);
			});
		},

		getAllEmpleados: function () {
			return $http.get("/api/Empleado/GetAllEmpleados/").success(function (data) {
				return data;
			}).error(function (error) {
				alert(error);
			});
		}
	}
}]);
