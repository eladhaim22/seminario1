var app = angular.module('app');

app.controller('simulacionController', ['$scope', '$timeout', 'SimulacionService', function ($scope, $timeout, SimulacionService) {
    //set simulacion
    $scope.simulacion = {
        cuitCliente: undefined,
        torCliente: undefined,
        fechaDescuento: "",
        comisionAdministrativa: undefined,
        valorNominal: 0, //importeTotal
        intereses: undefined, //interesTotal
        comision: undefined, //comisionTotal
        sellado: undefined, //selladoTotal
        iva: undefined, //ivaTotal
        gastoTotal: 0,
        TT: 0,
        TNAv: 0,
        netoLiquidar: 0, //netoLiquidarTotal
        importePonderadoTotal: 0,
        tipoCateg: "", //condicion IVA
        cantidadCheques: 0, //cantidad a comprar
        codProd: "",
        fechaVencimientoPond: 0,
        spreadTotal: 0,
        netoTotal: 0,
        tasaIIBB: 0,
        tasaIva: 0,
        tasaSellado: 0,
        estado: "",
        legajo: "",
        idProvincia: "",
        cheques : []
    };


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
            $scope.simulacion.torCliente = response.data + "%";
        });
    }
    var nosisState = ["Sin Verficar", "Aceptado", "Rechazado"];
    $scope.setNosisInitialState = function ($index) {
        $scope.simulacion.cheques[$index].nosis = nosisState[0];
    }

    $scope.estadoFiscal = [
        {id:1,nombre:"Resp. Insc s/Excl. AFIP",value:0.12},
        {id:2,nombre:"Resp. Insc c/Excl. AFIP",value:0.21}
    ];
      
    $scope.consultarNosis = function () {
        SimulacionService.consularNosis(_.map($scope.simulacion.cheques, 'documento').toString().split(',')).then(function (response) {
            angular.forEach(response.data, function (value, index) {
                $scope.simulacion.cheques[index].nosis = nosisState[value];
            });
        });
    }

    $scope.addCheque = function () {
        var cheque = {
            fechaAcreditacion: "", 
            banco: "", 
            documento: "", 
            nombre: "", 
            importe:null, 
            plazo:null, 
            otrosDia:null, 
            nosis:"Sin Verificar",
            //variables internas para cálculos
            diasOps: 0,
            TEOps: 0, //TE
            TEAdelantada: 0, //TEA
            TNAA: 0,
            intereses: 0,
            comision: 0,
            sellado: 0,
            iva: 0,
            TT: 0, //no se guarda en el cheque, se guarda en la simulacion
            spread: 0,
            cft: 0,
            cftMes: 0,
            netoLiquidar: 0,
            ponderado: 0,
            TETT: 0,
            TEATT: 0,
            IIBB: 0,
            costo: 0,
            neto: 0
        };
        $scope.simulacion.cheques.push(cheque);
        $scope.popupArray.push({"opened":false});
    }

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

    $scope.deleteCheque = function (index) {
        $scope.simulacion.cheques.splice(index, 1);
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
        if (moment($scope.simulacion.cheques[index].fechaAcreditacion).isValid() &&
            moment($scope.simulacion.fechaDescuento).isValid()) {
            var fechaStart = moment($scope.simulacion.cheques[index].fechaAcreditacion);
            var fechaEnd = moment($scope.simulacion.fechaDescuento);
            $scope.simulacion.cheques[index].plazo = parseInt(fechaStart.diff(fechaEnd, "days"));
        }
    }

    $scope.$watch('simulacion.cheques.fechaAcreditacion',function(oldValue,newValue){
        if (moment($scope.simulacion.fechaDescuento).isValid()) {
            var plazoStart = moment($scope.simulacion.fechaAcreditacion);
            var plazoEnd = moment(newValue.fechaDescuento);
            $scope.simulacion.plazo = plazoStart.diff(plazoEnd, "days");
        }
    });

    $scope.$watch('simulacion.fechaDescuento', function (oldValue,newValue) {
        angular.forEach($scope.simulacion.cheques, function (value, key) {
            $scope.renderCheque(key);
        });
    });
    
    function renderCheque(cheque) {
        if (cheque.plazo) {
            cheque.diasOps = cheque.plazo ? cheque.plazo + cheque.otrosDia : 0;
            cheque.TEOps = parseFloat((parseFloat($("#tnavPactada").val()) / 100 / 365 * (cheque.diasOps)).toFixed(5));
            cheque.TEAdelantada = parseFloat((cheque.TEOps / (1 + cheque.TEOps)).toFixed(5));
            cheque.TNAA = parseFloat((cheque.TEAdelantada * 365 / cheque.diasOps).toFixed(5));
        }
    }

    $scope.$watch("simulacion", function (newVal, oldVal) {
        $scope.simulacion.intereses = 0;
        $scope.simulacion.comision = 0;
        $scope.simulacion.sellado = 0;
        $scope.simulacion.iva = 0;
        $scope.simulacion.importePonderadoTotal = 0;
        $scope.simulacion.netoTotal = 0;
        $scope.simulacion.fechaVencimientoPond = 0;
        $scope.gastoTotal = 0;
        if ($scope.simulacionForm.$valid && $scope.chequesForm.$valid && $scope.simulacion.cheques.length > 0) {
            angular.forEach($scope.simulacion.cheques, function (cheque, index) {
                cheque.diasOps = cheque.plazo + cheque.otrosDia;
                cheque.TEOps = $scope.simulacion.TNAv / 100 / 365 * cheque.diasOps;
                cheque.TEAdelantada = cheque.TEOps / (1 + cheque.TEOps);
                cheque.TNAA = cheque.TEAdelantada * 365 / cheque.diasOps;
                cheque.intereses = cheque.importe - (1 - cheque.TEAdelantada) * cheque.importe;
                cheque.comision = cheque.importe * $scope.simulacion.comisionAdministrativa / 100;
                cheque.sellado = _.filter($scope.provincias,function(o){return o.Id === $scope.simulacion.idProvincia })[0].Sellado * cheque.importe / 365 * cheque.plazo;
                cheque.iva =  (cheque.intereses + cheque.comision) * (_.filter($scope.estadoFiscal,function(o){return o.id === $scope.simulacion.tipoCateg })[0].value);
                var gastoTotal = cheque.intereses + cheque.comision + cheque.sellado + cheque.iva;
                cheque.netoLiquidar = cheque.importe - cheque.intereses + cheque.comision + cheque.sellado + cheque.iva;
                cheque.TT = 0.89; //CAMBIER MOCK
                cheque.spread = ((cheque.intereses + cheque.comision) / cheque.importe / cheque.diasOps * 365) - cheque.TT;
                cheque.cft = (Math.pow((1 + gastoTotal / cheque.importe), (365 / cheque.diasOps)) - 1);
                cheque.cftMes = Math.pow(1 + cheque.cft, 0.0821917808219178) - 1;
                cheque.ponderado = cheque.importe * cheque.plazo;
                cheque.TETT = cheque.TT / 365 * cheque.diasOps;
                cheque.TEATT = cheque.TETT / (1 + cheque.TETT);
                cheque.IIBB = $scope.simulacion.IIBB / 100 * (cheque.intereses + cheque.comision);
                cheque.costo = cheque.importe - (1 - cheque.TEATT) * cheque.importe;
                cheque.neto = cheque.intereses + cheque.comision - cheque.IIBB - cheque.costo;
                $scope.simulacion.valorNominal += cheque.importe;
                $scope.simulacion.intereses += cheque.intereses;
                $scope.simulacion.comision += cheque.comision;
                $scope.simulacion.sellado += cheque.sellado;
                $scope.simulacion.iva += cheque.iva;
                $scope.simulacion.importePonderadoTotal += cheque.ponderado;
                $scope.simulacion.netoTotal += cheque.neto;
            });
            $scope.simulacion.gastoTotal = $scope.simulacion.intereses + $scope.simulacion.comision + $scope.simulacion.sellado + $scope.simulacion.iva;
            $scope.simulacion.netoLiquidar = $scope.simulacion.valorNominal - $scope.simulacion.gastoTotal;
            $scope.simulacion.fechaVencimientoPond = ($scope.simulacion.importePonderadoTotal / $scope.simulacion.valorNominal).toFixed(0);
            $scope.simulacion.spreadTotal = $scope.simulacion.netoTotal / $scope.simulacion.valorNominal / $scope.simulacion.fechaVencimientoPond * 365;
        }
    }, true);
    
    $scope.postSimulacion = function () {
        SimulacionService.saveSimulacion($scope.simulacion).then(function (response) {
            return response;
        });
    }


   /* function calculosInternos(id) {
           _chequeData[id].sellado = parseFloat((obtenerSellado($("#select-provincia").val()) * _chequeData[id].importe / 365 * _chequeData[id].plazo).toFixed(2));

            //iva
            _chequeData[id].iva = parseFloat(((_chequeData[id].intereses + _chequeData[id].comision) * parseFloat($("#select-iva").val())).toFixed(2));

            //TT
            _chequeData[id].TT = obtenerTT($("#select-producto").val(), _chequeData[id].diasOps);

            //IIBB
            _chequeData[id].IIBB = parseFloat(((parseFloat($("#tasaIibb").val()) / 100) * (_chequeData[id].intereses + _chequeData[id].comision)).toFixed(5)); //ojo que en la planilla parseFloat($("#tasaIibb").val()) es 0.069
        }
    }
    */
    
}]);