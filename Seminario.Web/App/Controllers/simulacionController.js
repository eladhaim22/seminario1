var app = angular.module('app');

app.controller('simulacionController', ['$scope', '$timeout', 'SimulacionService', '$routeParams', '$rootScope',
function ($scope, $timeout, SimulacionService, $routeParams, $rootScope) {

        var nosisState = ["Sin Verficar", "Aceptado", "Rechazado"];

        $scope.editable = false;
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
            { id: 2, nombre: "Resp. Insc c/Excl. AFIP", value: 0.21 }
        ];

        if ($routeParams.id) {
            SimulacionService.getSimulacionById($routeParams.id).then(function (response) {
                response.data.FechaDescuento = new Date(response.data.FechaDescuento);
                angular.forEach(response.data.Cheques, function (value) {
                    value.FechaAcreditacion = new Date(value.FechaAcreditacion);
                }); 
                $scope.simulacion = response.data;
                $scope.editable = true;
                activeWatch();
            });
        }
        else {
            //set simulacion
            $scope.simulacion = {
                CuitCliente: "",
                TorCliente: undefined,
                FechaDescuento: "",
                ComisionAdministrativa: undefined,
                ValorNominal: 0, //ImporteTotal
                Intereses: 0, //interesTotal
                Comision: 0, //ComisionTotal
                Sellado: 0, //SelladoTotal
                Iva: 0, //IvaTotal
                GastoTotal: 0,
                TT: 0,
                TNAV: 0,
                NetoLiquidar: 0, //NetoLiquidarTotal
                ImportePonderadoTotal: 0,
                TipoCateg: "", //condicion Iva
                CantidadCheques: 0, //cantidad a comprar
                CodProd: "",
                FechaVencimientoPond: 0,
                SpreadTotal: 0,
                NetoTotal: 0,
                TasaIIBB: 0,
                TasaIva: 0,
                TasaSellado: 0,
                Estado: "Simulando",
                Legajo: $rootScope.legajo,
                IdProvincia: "",
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

    $scope.consultarTor = function (cuit) {
        SimulacionService.consultarTor(cuit).then(function (response) {
            $scope.simulacion.TorCliente = response.data + "%";
        });
    }
     $scope.setNosisInitialState = function ($index) {
        $scope.simulacion.Cheques[$index].nosis = nosisState[0];
    }
      
    $scope.consultarNosis = function () {
        SimulacionService.consularNosis(_.map($scope.simulacion.Cheques, 'Documento').toString().split(',')).then(function (response) {
            angular.forEach(response.data, function (value, index) {
                $scope.simulacion.Cheques[index].Nosis = nosisState[value];
            });
        });
    }

    $scope.addCheque = function () {
        var cheque = {
            FechaAcreditacion: "", 
            Banco: "", 
            Documento: "", 
            Nombre: "", 
            Importe:0, 
            Plazo:0, 
            OtrosDias:null, 
            Nosis:"Sin Verificar",
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
        $scope.popupArray.push({"opened":false});
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
        if (moment($scope.simulacion.Cheques[index].FechaAcreditacion).isValid() &&
            moment($scope.simulacion.FechaDescuento).isValid()) {
            var fechaStart = moment($scope.simulacion.Cheques[index].FechaAcreditacion);
            var fechaEnd = moment($scope.simulacion.FechaDescuento);
            $scope.simulacion.Cheques[index].Plazo = parseInt(fechaStart.diff(fechaEnd, "days"));
        }
    }



    function activeWatch() {
        $scope.flag = false;
        $scope.$watch('simulacion', function (newVal, oldVal) {
            $scope.flag = !$scope.flag;
            if ($scope.flag && newVal!=oldVal) {
                $scope.simulacion.Intereses = 0;
                $scope.simulacion.Comision = 0;
                $scope.simulacion.Sellado = 0;
                $scope.simulacion.Iva = 0;
                $scope.simulacion.ImportePonderadoTotal = 0;
                $scope.simulacion.NetoTotal = 0;
                $scope.simulacion.FechaVencimientoPond = 0;
                $scope.simulacion.ValorNominal = 0;
                $scope.GastoTotal = 0;
                $scope.simulacion.NetoLiquidar = 0;
                $scope.SpreadTotal = 0;
                $scope.simulacion.Estado = "Simulando";
                if ($scope.simulacionForm.$valid && $scope.ChequesForm.$valid && $scope.simulacion.Cheques.length > 0) {
                    angular.forEach($scope.simulacion.Cheques, function (cheque, index) {
                        cheque.DiasOps = cheque.OtrosDias ? cheque.Plazo + cheque.OtrosDias : cheque.Plazo;
                        cheque.TEOps = $scope.simulacion.TNAV / 365 * cheque.DiasOps;
                        cheque.TEAdelantada = cheque.TEOps / (1 + cheque.TEOps);
                        cheque.TNAA = cheque.TEAdelantada * 365 / cheque.DiasOps;
                        cheque.Intereses = cheque.Importe - (1 - cheque.TEAdelantada) * cheque.Importe;
                        cheque.Comision = cheque.Importe * $scope.simulacion.ComisionAdministrativa;
                        cheque.Sellado = _.filter($scope.provincias, function (o) { return o.Id === $scope.simulacion.IdProvincia })[0].Sellado * cheque.Importe / 365 * cheque.Plazo;
                        cheque.Iva = (cheque.Intereses + cheque.Comision) * (_.filter($scope.estadoFiscal, function (o) { return o.id === $scope.simulacion.TipoCateg })[0].value);
                        var GastoTotal = cheque.Intereses + cheque.Comision + cheque.Sellado + cheque.Iva;
                        cheque.NetoLiquidar = cheque.Importe - cheque.Intereses + cheque.Comision + cheque.Sellado + cheque.Iva;
                        cheque.TT = 0.89; //CAMBIER MOCK
                        cheque.Spread = ((cheque.Intereses + cheque.Comision) / cheque.Importe / cheque.DiasOps * 365) - cheque.TT;
                        cheque.Cft = (Math.pow((1 + GastoTotal / cheque.Importe), (365 / cheque.DiasOps)) - 1);
                        cheque.CftMes = Math.pow(1 + cheque.Cft, 0.0821917808219178) - 1;
                        cheque.Ponderado = cheque.Importe * cheque.Plazo;
                        cheque.TETT = cheque.TT / 365 * cheque.DiasOps;
                        cheque.TEATT = cheque.TETT / (1 + cheque.TETT);
                        cheque.IIBB = $scope.simulacion.TasaIIBB * (cheque.Intereses + cheque.Comision);
                        cheque.Costo = cheque.Importe - (1 - cheque.TEATT) * cheque.Importe;
                        cheque.Neto = cheque.Intereses + cheque.Comision - cheque.IIBB - cheque.Costo;
                        $scope.simulacion.ValorNominal += parseInt(cheque.Importe);
                        $scope.simulacion.Intereses += cheque.Intereses;
                        $scope.simulacion.Comision += cheque.Comision;
                        $scope.simulacion.Sellado += cheque.Sellado;
                        $scope.simulacion.Iva += cheque.Iva;
                        $scope.simulacion.ImportePonderadoTotal += cheque.Ponderado;
                        $scope.simulacion.NetoTotal += cheque.Neto;
                    });
                    $scope.simulacion.GastoTotal = $scope.simulacion.Intereses + $scope.simulacion.Comision + $scope.simulacion.Sellado + $scope.simulacion.Iva;
                    $scope.simulacion.NetoLiquidar = $scope.simulacion.ValorNominal - $scope.simulacion.GastoTotal;
                    $scope.simulacion.FechaVencimientoPond = ($scope.simulacion.ImportePonderadoTotal / $scope.simulacion.ValorNominal).toFixed(0);
                    $scope.simulacion.SpreadTotal = $scope.simulacion.NetoTotal / $scope.simulacion.ValorNominal / $scope.simulacion.FechaVencimientoPond * 365;
                    if ($scope.simulacion.SpreadTotal > 3)
                        $scope.simulacion.Estado = "Aceptado";
                    else
                        $scope.simulacion.Estado = "A Revisar";
                }
            }
        }, true);
    }
    
    $scope.createSimulacion = function () {
        SimulacionService.createSimulacion($scope.simulacion).then(function (response) {
            return response;
        });
    }

    $scope.updateSimulacion = function () {
        SimulacionService.updateSimulacion($scope.simulacion).then(function (response) {
            return response;
        });
    }
}]);