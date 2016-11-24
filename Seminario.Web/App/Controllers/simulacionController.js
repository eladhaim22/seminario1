var app = angular.module('app');

app.controller('simulacionController', ['$scope', '$timeout', 'SimulacionService', '$routeParams', '$rootScope', '$location',
function ($scope, $timeout, SimulacionService, $routeParams, $rootScope, $location) {
	var nosisState;
	var estado = ["Aceptada", "Rechazada", "Pendiente","Confirmada"];
	$scope.setup = function () {
		$timeout(function () {
			$scope.torCliente.$setPristine();
			$scope.ChequesForm.$setPristine();
			$scope.simulacionForm.$setPristine();
		})

		nosisState = ["No existe", "Valido", "Invalido"];
		$scope.editable = true;
		$scope.role = $rootScope.role;
		$scope.bancos = [
				{ label: "", value: null },
				{ label: "Banco ICBC", value: "Banco ICBC" },
				{ label: "Banco Galicia", value: "Banco Galicia" },
				{ label: "Banco Frances", value: "Banco Frances" },
				{ label: "Banco Comafi", value: "Banco Comafi" },
				{ label: "Banco Piano", value: "Banco Piano" },
				{ label: "Banco Credicoop", value: "Banco Credicoop" },
				{ label: "Banco Macro", value: "Banco Macro" },
				{ label: "Banco Nacion", value: "Banco Nacion" },
				{ label: "Banco Ciudad", value: "Banco Ciudad" },
				{ label: "Banco Provincia", value: "Banco Provincia" },
				{ label: "Banco HSBC", value: "Banco HSBC" }
		];

		$scope.estadoFiscal = [
			{ id: 1, nombre: "Resp. Insc s/Excl. AFIP", value: 0.12 },
			{ id: 2, nombre: "Resp. Insc c/Excl. AFIP", value: 0.21 },
		    { id: 3, nombre: "IVA Cons. Final", value: 0.21 },
            { id: 4, nombre: "IVA No Categorizado", value: 0.337 }
		];

		if ($routeParams.id) {
			SimulacionService.getSimulacionById($routeParams.id).then(function (response) {
				$scope.ready = true;
				$scope.simulacion = response.data;
				$scope.editable = false;
				$scope.finalState = $scope.simulacion.Estado === 1 || $scope.simulacion.Estado === 3 ? true : false;
				$scope.waitingAnswer = $scope.simulacion.Estado === 2 ? true : false;
				activeWatch();
			});
		}
		else {
			$scope.ready = true;
			//set simulacion
			$scope.simulacion = {
				CuitCliente: undefined,
				TorCliente: undefined,
				FechaDescuento: new Date(),
				ComisionAdministrativa: 0.002,
				ValorNominal: 0, //ImporteTotal
				Intereses: 0, //interesTotal
				Comision: 0, //ComisionTotal
				Sellado: 0, //SelladoTotal
				Iva: 0, //IvaTotal
				GastoTotal: 0,
				TT: 0,
				TNAV: 0.415,
				NetoLiquidar: 0, //NetoLiquidarTotal
				ImportePonderadoTotal: 0,
				//TipoCateg: "", //condicion Iva
				CantidadCheques: 0, //cantidad a comprar
				CodProd: "",
				FechaVencimientoPond: 0,
				SpreadTotal: 0,
				NetoTotal: 0,
				TasaIIBB: 0.069,
				TasaIva: 0,
				TasaSellado: 0,
				Estado: 0,
				Legajo: $rootScope.legajo,
				IdProvincia: "",
                TipoIva:"",
				Cheques: []
			};
			activeWatch();
		}

		$scope.productos = [];
		$scope.provincias = [];

		SimulacionService.fillComboProducto().then(function (response) {
			$scope.productos = response.data;
		});

		SimulacionService.fillComboProvincia().then(function (response) {
			$scope.provincias = response.data;
		})

		SimulacionService.fillLip().then(function (response) {
			$scope.lip = response.data;
		})
	}

	$scope.consultarTor = function (cuit) {
		cuit = cuit == "" ? undefined : cuit;
		SimulacionService.consultarTor(cuit).success(function (data) {
			$scope.simulacion.TorCliente = data.Points;
			$scope.razonSocial = data.RazonSocial;
		}).error(function (error) {
			$scope.simulacion.CuitCliente = undefined;
			$scope.razonSocial = undefined;
			$scope.simulacion.TorCliente = undefined;
			alert(error);
		});
	}
	$scope.setNosisInitialState = function ($index) {
		$scope.simulacion.Cheques[$index].nosis = nosisState[0];
	}

	$scope.consultarNosis = function () {
		var rows = []
		angular.forEach($scope.simulacion.Cheques, function (value, key) {
			rows.push({ "Documento": value.Documento, "RazonSocial": value.Nombre });
		});
		SimulacionService.consularNosis(rows).then(function (response) {
			angular.forEach(response.data, function (value, index) {
				$scope.simulacion.Cheques[index].Nosis = nosisState[value];
			});
		});
	}

	$scope.GetSimulacionEstado = function (estadoId) {
		return estado[estadoId];
	}

	$scope.addCheque = function () {
		var cheque = {
			FechaAcreditacion: "",
			Banco: "",
			Documento: "",
			Nombre: "",
			Importe: null,
			Plazo: 0,
			OtrosDias: null,
			Nosis: "Sin Verificar",
			//variables internas para cálculos
			DiasOps: 0,
			TEOps: 0, //TE
			TEAdelantada: 0, //TEA
			TNAA: 0,
			Intereses: 0,
			Comision: 0,
			Sellado: 0,
			Iva: 0,
			TT: 0, //no se guarda en el cheque, se guarda en la simulacion
			Spread: 0,
			Cft: 0,
			CftMes: 0,
			NetoLiquidar: 0,
			Ponderado: 0,
			TETT: 0,
			TEATT: 0,
			IIBB: 0,
			Costo: 0,
			Neto: 0
		};
		$scope.simulacion.Cheques.push(cheque);
		$scope.popupArray.push({ "opened": false });
	}

	$scope.deleteCheque = function (index) {
		$scope.simulacion.Cheques.splice(index, 1);
		$scope.popupArray.splice(index, 1);
	}

	//simulacion dateTimePicker
	$scope.openDateTime = function () {
		$scope.popup.opened = true;
	};

	$scope.popup = {
		opened: false
	};

	//cheque dateTimePicker
	$scope.today = function () {
		$scope.dt = new Date();
	};
	$scope.today();

	$scope.clear = function () {
		$scope.dt = "";
	};

	$scope.inlineOptions = {
		minDate: new Date(),
		showWeeks: true
	};

	$scope.dateOptions = {
		formatYear: 'yy',
		maxDate: new Date(2020, 5, 22),
		minDate: new Date(),
		startingDay: 1
	};

	$scope.toggleMin = function () {
		$scope.inlineOptions.minDate = $scope.inlineOptions.minDate ? null : new Date();
		$scope.dateOptions.minDate = $scope.inlineOptions.minDate;
	};

	$scope.toggleMin();

	$scope.open = function (index) {
		angular.forEach(function (value) {
			value.opend = false;
		});

		$scope.popupArray[index].opened = true;
	};

	$scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
	$scope.format = $scope.formats[0];
	$scope.altInputFormats = ['M!/d!/yyyy'];

	$scope.popupArray = [];

	$scope.renderCheque = function (index) {
		if (index == null) {
			angular.forEach($scope.simulacion.Cheques, function (value, index) {
				if (moment(value.FechaAcreditacion).isValid() && moment($scope.simulacion.FechaDescuento).isValid()) {
					var fechaStart = moment(value.FechaAcreditacion);
					var fechaEnd = moment($scope.simulacion.FechaDescuento);
					$scope.simulacion.Cheques[index].Plazo = parseInt(fechaStart.diff(fechaEnd, "days"));
				}
			});
		}
		else {
			if (moment($scope.simulacion.Cheques[index].FechaAcreditacion).isValid() &&
             moment($scope.simulacion.FechaDescuento).isValid()) {
				var fechaStart = moment($scope.simulacion.Cheques[index].FechaAcreditacion);
				var fechaEnd = moment($scope.simulacion.FechaDescuento);
				$scope.simulacion.Cheques[index].Plazo = parseInt(fechaStart.diff(fechaEnd, "days"));
			}
		}
	}

	function activeWatch() {
		$scope.flag = false;
		$scope.$watch('simulacion', function (newVal, oldVal) {
			$timeout(function () {
				$scope.flag = !$scope.flag;
				if (newVal != oldVal) {
					$scope.simulacion.Intereses = 0;
					$scope.simulacion.Comision = 0;
					$scope.simulacion.Sellado = 0;
					$scope.simulacion.Iva = 0;
					$scope.simulacion.ImportePonderadoTotal = 0;
					$scope.simulacion.NetoTotal = 0;
					$scope.simulacion.FechaVencimientoPond = 0;
					$scope.simulacion.ValorNominal = 0;
					$scope.simulacion.GastoTotal = 0;
					$scope.simulacion.NetoLiquidar = 0;
					$scope.SpreadTotal = 0;
					$scope.simulacion.Estado = "Simulando";

					if ($scope.simulacionForm.$valid && $scope.ChequesForm.$valid && !$scope.ChequesForm.$pristine && $scope.simulacion.Cheques.length > 0) {
						angular.forEach($scope.simulacion.Cheques, function (cheque, index) {
							$scope.simulacion.ValorNominal += parseFloat(cheque.Importe);
							cheque.Ponderado = cheque.Importe * cheque.Plazo;
							$scope.simulacion.ImportePonderadoTotal += cheque.Ponderado;
						});
						$scope.simulacion.FechaVencimientoPond = ($scope.simulacion.ImportePonderadoTotal / $scope.simulacion.ValorNominal).toFixed(0);
						angular.forEach($scope.simulacion.Cheques, function (cheque, index) {   
                            cheque.DiasOps = cheque.OtrosDias ? cheque.Plazo + cheque.OtrosDias : cheque.Plazo;
						    var TTtemporal = _.filter(_.filter($scope.productos, function (producto) { return producto.Id == $scope.simulacion.CodProd })[0].DatosTT, function (datoTT) {
						        return datoTT.Plazo == cheque.DiasOps
						    })[0]
						    cheque.TT = TTtemporal ? TTtemporal.TasaVigente : _.last(_.filter($scope.productos, function (producto) { return producto.Id == $scope.simulacion.CodProd })[0].DatosTT).TasaVigente;

							cheque.TEOps = $scope.simulacion.TNAV / 365 * cheque.DiasOps;
							cheque.TEAdelantada = cheque.TEOps / (1 + cheque.TEOps);
							cheque.TNAA = cheque.TEAdelantada * 365 / cheque.DiasOps;
							cheque.Intereses = parseFloat((cheque.Importe - (1 - cheque.TEAdelantada) * cheque.Importe).toFixed(5));
							cheque.Comision = parseFloat((cheque.Importe * $scope.simulacion.ComisionAdministrativa).toFixed(5));
							cheque.Sellado = _.filter($scope.provincias, function (o) { return o.Id === $scope.simulacion.IdProvincia })[0].Sellado * cheque.Importe / 365 * cheque.DiasOps;
							cheque.Iva = parseFloat(((cheque.Intereses + cheque.Comision) * (_.filter($scope.estadoFiscal, function (o) { return o.id === $scope.simulacion.TipoIva })[0].value)).toFixed(2));
							var GastoTotal = parseFloat((cheque.Intereses + cheque.Comision + cheque.Sellado + cheque.Iva).toFixed(5));
							cheque.NetoLiquidar = cheque.Importe - GastoTotal;
							cheque.Spread = (((cheque.Intereses + cheque.Comision) / cheque.Importe) / (cheque.DiasOps * 365)) - cheque.TT;
							cheque.Cft = (Math.pow((1 + GastoTotal / cheque.Importe), (365 / cheque.DiasOps)) - 1);
							cheque.CftMes = Math.pow(1 + cheque.Cft, 0.0821917808219178) - 1;
							cheque.TETT = cheque.TT / 365 * cheque.DiasOps;
							cheque.TEATT = cheque.TETT / (1 + cheque.TETT);
							cheque.IIBB = parseFloat(($scope.simulacion.TasaIIBB * (cheque.Intereses + cheque.Comision)).toFixed(5));
							cheque.Costo = parseFloat((cheque.Importe - (1 - cheque.TEATT) * cheque.Importe).toFixed(2));
							cheque.Neto = parseFloat((cheque.Intereses + cheque.Comision - cheque.IIBB - cheque.Costo).toFixed(5));
							$scope.simulacion.Intereses += cheque.Intereses;
							$scope.simulacion.Comision += cheque.Comision;
							$scope.simulacion.Sellado += cheque.Sellado;
							$scope.simulacion.Iva += cheque.Iva;
							$scope.simulacion.NetoTotal += cheque.Neto;
						});
						$scope.simulacion.GastoTotal = $scope.simulacion.Intereses + $scope.simulacion.Comision + $scope.simulacion.Sellado + $scope.simulacion.Iva;
						$scope.simulacion.NetoLiquidar = $scope.simulacion.ValorNominal - $scope.simulacion.GastoTotal;
						$scope.simulacion.SpreadTotal = $scope.simulacion.NetoTotal / $scope.simulacion.ValorNominal / $scope.simulacion.FechaVencimientoPond * 365;
						if ($routeParams != undefined) {
							if ($scope.simulacion.SpreadTotal > 0.035 || $rootScope.role === "Jefe")
								$scope.simulacion.Estado = 3;
							else
								$scope.simulacion.Estado = 2;
						}
					}
				}
			});
		}, true);
	}
	$scope.setup();

	$scope.createSimulacion = function () {
		SimulacionService.createSimulacion($scope.simulacion).success(function (response) {
			$rootScope.errorMsg = undefined;
			$rootScope.successMsg = "la simulacion ha sida salvada exitosamente";
			$timeout(function () { $rootScope.successMsg = undefined; }, 5000);
			$scope.setup();
			//return response;
		}).error(function (error) {
			$rootScope.errorMsg = _.uniq(error.ExceptionMessage.split("\n"));
		});
	}

	$scope.updateSimulacion = function (status) {
		SimulacionService.updateSimulacion({ "simulacion": $scope.simulacion, "state": status }).success(function (response) {
			$rootScope.errorMsg = undefined;
			$rootScope.successMsg = "la simulacion ha sida salvada exitosamente";
			$location.path('/ViewSimulaciones')
			$timeout(function () { $rootScope.successMsg = undefined; }, 5000);
		}).error(function (error) {
			$rootScope.errorMsg = _.uniq(error.ExceptionMessage.split("\n"));
		});
	}

	$scope.confirmarSimulacion = function () {
	    SimulacionService.ConfirmarSimulacion($scope.simulacion).success(function (response) {
	        $rootScope.errorMsg = undefined;
	        $rootScope.successMsg = "la simulacion ha sida confirmada";
	        $location.path('/ViewSimulaciones')
	        $timeout(function () { $rootScope.successMsg = undefined; }, 5000);
	    }).error(function (error) {
	        $rootScope.errorMsg = _.uniq(error.ExceptionMessage.split("\n"));
	    });
	}
}]);
