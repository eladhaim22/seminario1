var _editor; // use a global for the submit and return data rendering in the examples
var _chequeData  = {}; // Object that will contain the local state    
var _rowId = 1;
var _table;
var _simulacionData = {
    cuitCliente: "",
    torCliente: "",
    fechaDescuento: "",
    valorNominal: 0, //importeTotal
    intereses: 0, //interesTotal
    comision: 0, //comisionTotal
    sellado: 0, //selladoTotal
    iva: 0, //ivaTotal
    gastoTotal: 0,
    TT: 0,
    TNAv: 0,
    netoLiquidar: 0, //netoLiquidarTotal
    importePonderadoTotal: 0, 
    tipoCateg: "", //condicion IVA
    cantidadCheques: 0, //cantidad a comprar
    codProd: "", 
    fechaVencimientoPond:0,
    spreadTotal: 0,
    netoTotal: 0,
    tasaIIBB: 0,
    tasaIva: 0,
    tasaSellado: 0,
    estado: "",
    legajo: "",
    idProvincia: ""
};

var productos = {};

$(document).ready(function() {

    createRows();
    fillComboProducto();
    fillComboProvincia();
    $.validate({
        validateOnBlur : false,
        borderColorOnError: '#ccc'
    });

    $( ".triggerCalc" ).change(function() {
        console.log($("#planilla-form").isValid());
        if($("#planilla-form").isValid()){
            actualizarSimulacion();

            if(_simulacionData.cantidadCheques>0){
                $('#btn-confirmar').removeAttr("disabled");
            }else{
                $('#btn-confirmar').attr("disabled", true); 
            }
            
        }else{
            $('#btn-confirmar').attr("disabled", true);
        }
    });

    $( "#btn-consultarTOR" ).click(function() {
        if ($("#cuitCliente").val().length > 1 && $("#nombreCliente").val().length > 1) {
            consultarTOR(($("#cuitCliente").val())).done(function(data)
            { 
                $("#torCliente").val(data.toString());
            }).fail(
            function(error){
                alert(JSON.parse(error.responseText).ExceptionMessage);
            });
            $("#torCliente").change();
        }        
    });

    $( "#btn-nosis" ).click(function(){
        var idAndCuit = Object.keys(_chequeData);
        for(var i=0;i<idAndCuit.length;i++){
            idAndCuit[i] = { "id": i, "documento": _chequeData[i + 1].documento };
        }
        consultarNosis(idAndCuit).done(function (result) {
            for (var i = 0; i < result.rows.length; i++) {
                _chequeData[i + 1].nosis = result.rows[i].documento;
            }
        })
        .fail(function (error) {
            alert(JSON.parse(error.responseText).ExceptionMessage);
        });
        
    });

    /*$('.datepicker').datepicker({
        format: "dd/mm/yyyy",
        language: "es",
        daysOfWeekDisabled: "0,6",
        calendarWeeks: true,        
        daysOfWeekHighlighted: "1,2,3,4,5",
        autoclose: true,
        todayHighlight: true
    });
    $('.datepicker').datepicker('update');  
    */
    _editor = new $.fn.dataTable.Editor( {
        table: '#dataTables',
        fields: [ {
                name: "filaNumero"
            }, {
                name: "fechaAcreditacion",
                type: "datetime",
                opts:  {
                    disableDays: [ 0, 6 ],
                    minDate: new Date()
                },
                format: 'DD/MM/YYYY'
            }, {
                name: "banco",
                type:  "select",
                options: [
                    { label: "---", value: "" },
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
                    { label: "Banco HSBC",  value: "Banco HSBC" }
                ]
            }, {
                name: "documento"
            }, {
                name: "nombre"
            }, {
                name: "importe"
            }, {
                name: "plazo"
            }, {
                name: "otrosDia"
            }, {
                name: "nosis"
            }
        ],
        ajax: function ( method, url, d, successCallback, errorCallback ) {
            var output = { data: [] };
            if ( d.action === 'edit') {
                // Update each edited item with the data submitted
                $.each( d.data, function (id, value) {  

                    value.DT_RowId = id;
                    //value.fechaAcreditacion = ;
                    if(value.banco){
                        value.banco = value.banco.toUpperCase();
                    }
                    //value.documento = ;
                    if(value.nombre){
                        value.nombre = value.nombre.toUpperCase();
                    }
                    if(value.importe){
                        if($.isNumeric(value.importe)){
                            value.importe = parseFloat(value.importe);
                        }else{
                            value.importe = null;
                        }                      
                    }

                    if(value.fechaAcreditacion){
                        var plazoStart = moment(value.fechaAcreditacion, "DD/MM/YYYY");
                        var plazoEnd = moment($("#fechaDescuento").val(), "DD/MM/YYYY");
                        value.plazo = parseInt(plazoStart.diff(plazoEnd, "days"));
                    }
                    if(value.otrosDia){
                        if($.isNumeric(value.otrosDia)){
                            value.otrosDia = parseInt(value.otrosDia);
                        }else{
                            value.otrosDia = null;
                        }
                    }
                    //value.nosis = ;
                    $.extend( _chequeData[id], value );

                    //realizo los cálculos si el formulario es válido
                    if($("#planilla-form").isValid()){
                        actualizarSimulacion(id);
                        if(_simulacionData.cantidadCheques>0){
                            $('#btn-confirmar').removeAttr("disabled");
                        }else{
                            $('#btn-confirmar').attr("disabled", true); 
                        }
                    }else{
                        $('#btn-confirmar').attr("disabled", true);
                    }

                    output.data.push( _chequeData[id] );

                } );
            }
 
            // Show Editor what has changed
            successCallback( output );
        },
        formOptions: {
            inline: {
                onBlur: 'submit'
            }
        },
        i18n: {
            datetime: {
                previous: 'Anterior',
                next:     'Siguiente',
                months:   [ 'Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre' ],
                weekdays: [ 'Dom', 'Lun', 'Mar', 'Mir', 'Jue', 'Vie', 'Sab' ]
            }
        }
    } );

    // Activate an inline edit on click of a table cell
     $('#dataTables').on( 'click', 'tbody td.editable', function (e) {
        _editor.inline( this );
    } );

    // Inline editing on tab focus
    $('#dataTables').on( 'key-focus', function ( e, datatable, cell ) {
        _editor.inline( cell.index() );
    } );
 
    _table = $('#dataTables').DataTable( {
        responsive: true,
        "paging":   false,
        "ordering": false,
        "info":     false,
        "searching": false,
        "scrollY": "200px",
        "scrollX": false,
        "scrollCollapse": true,
        data: $.map( _chequeData , function (value, key) {
            return value;
        } ),
        keys: {
            columns: 'tbody td.editable',
            keys: [ 9 ]
        },
        columns: [
            { data: "filaNumero" },
            { data: "fechaAcreditacion", className: 'editable' },
            { data: "banco", className: 'editable' },
            { data: "documento", className: 'editable' },
            { data: "nombre", className: 'editable' },
            { data: "importe", className: 'editable' , render: $.fn.dataTable.render.number( ',', '.', 2, '$' )},
            { data: "plazo"},
            { data: "otrosDia", className: 'editable' },
            { data: "nosis" }
        ],
        select: true
    } );


function createRows(){
    //observaciones
    //diasCks = plazo
    //TT2 = TT pero para producto 530

    for(i=1; i<=50 ; i++){
        _chequeData [ i ] = { 
            DT_RowId : i, 
            filaNumero: i, 
            fechaAcreditacion: "", 
            banco: "", 
            documento: "", 
            nombre: "", 
            importe:null, 
            plazo:null, 
            otrosDia:null, 
            nosis:"",
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
    }
}

function calculosInternos(id){

    if(id==null){
        for(i=1; i<=50 ; i++){
            calculosInternos(i);
        };
    }else{
        if(_chequeData[id].plazo!=0){
            _chequeData[id].diasOps = parseInt(_chequeData[id].plazo + _chequeData[id].otrosDia);
            _chequeData[id].TEOps = parseFloat((parseFloat($("#tnavPactada").val())/100 / 365 * (_chequeData[id].diasOps)).toFixed(5));
            _chequeData[id].TEAdelantada =  parseFloat(( _chequeData[id].TEOps /  (1 + _chequeData[id].TEOps)).toFixed(5));
            _chequeData[id].TNAA = parseFloat((_chequeData[id].TEAdelantada*365/_chequeData[id].diasOps).toFixed(5));
        }

        //intereses
        if(_chequeData[id].importe){
            _chequeData[id].intereses = parseFloat((_chequeData[id].importe-(1-_chequeData[id].TEAdelantada) * _chequeData[id].importe).toFixed(5));
        }else{
            _chequeData[id].intereses = 0;
        }  

        if(_chequeData[id].importe){
            _chequeData[id].comision = parseFloat((_chequeData[id].importe * parseFloat($("#comisionAdmin").val())/100).toFixed(2));
        }else{
            _chequeData[id].comision = 0;
        }

        //calculo de SELLADO del tipo 1
        if(_chequeData[id].importe && _chequeData[id].plazo){
            _chequeData[id].sellado = parseFloat((obtenerSellado($("#select-provincia").val()) *_chequeData[id].importe / 365 * _chequeData[id].plazo).toFixed(2));
        }else{
            _chequeData[id].sellado = 0;
        }

        //iva
        _chequeData[id].iva = parseFloat(((_chequeData[id].intereses + _chequeData[id].comision) * parseFloat($("#select-iva").val())).toFixed(2));

        //gastoTotal (lo calculo cuando lo necesito)
        var gastoTotal = _chequeData[id].intereses + _chequeData[id].comision + _chequeData[id].sellado + _chequeData[id].iva;

        //neto a liquidar
        if(_chequeData[id].importe){
            _chequeData[id].netoLiquidar = parseFloat(( _chequeData[id].importe - _chequeData[id] .intereses + _chequeData[id] .comision + _chequeData[id] .sellado + _chequeData[id] .iva).toFixed(2));
        }else{
            _chequeData[id].netoLiquidar = 0;
        }

        //TT
        _chequeData[id].TT = obtenerTT($("#select-producto").val(), _chequeData[id].diasOps);

        //spread
        if(_chequeData[id].importe){
            _chequeData[id].spread = parseFloat( ( ( ( ( _chequeData[id].intereses + _chequeData[id].comision ) / _chequeData[id].importe ) / _chequeData[id].diasOps*365 ) - _chequeData[id].TT ).toFixed(5) );
        }else{
            _chequeData[id].spread = 0;
        }

        //CFT
        if(_chequeData[id].importe){
            _chequeData[id].cft = parseFloat( (Math.pow( ( 1+gastoTotal/ _chequeData[id].importe ), ( 365/_chequeData[id].diasOps ) ) -1).toFixed(5));  
        }else{
            _chequeData[id].cft = 0;
        } 

        //CFT mes
        _chequeData[id].cftMes = parseFloat((Math.pow(1+_chequeData[id].cft, 0.0821917808219178)-1).toFixed(5)); 

        //ponderado
        if(_chequeData[id].importe){
            _chequeData[id].ponderado = parseFloat( ( _chequeData[id].importe * _chequeData[id].plazo ).toFixed(5));  
        }else{
            _chequeData[id].ponderado = 0;
        } 

        //TETT
        _chequeData[id].TETT = parseFloat( ( _chequeData[id].TT / 365 * _chequeData[id].diasOps ).toFixed(5));

        //TEATT
        _chequeData[id].TEATT = parseFloat( ( _chequeData[id].TETT/(1+_chequeData[id].TETT) ).toFixed(5));

        //IIBB
        _chequeData[id].IIBB = parseFloat( ( (parseFloat($("#tasaIibb").val())/100)*(_chequeData[id].intereses + _chequeData[id].comision) ).toFixed(5)); //ojo que en la planilla parseFloat($("#tasaIibb").val()) es 0.069

        //costo
        if(_chequeData[id].importe){
            _chequeData[id].costo = parseFloat( ( _chequeData[id].importe - (1-_chequeData[id].TEATT)*_chequeData[id].importe ).toFixed(5));  
        }else{
            _chequeData[id].costo = 0;
        } 

        //neto
        _chequeData[id].neto = parseFloat( ( _chequeData[id].intereses + _chequeData[id].comision - _chequeData[id].IIBB - _chequeData[id].costo).toFixed(5));

        //control, remover al final
        console.log(_chequeData[id]);
    }
}

function calcularResumen(){

    //reseteo valores para volver a calcularlos
    _simulacionData.valorNominal = 0;
    _simulacionData.intereses = 0;   
    _simulacionData.comision = 0;
    _simulacionData.sellado = 0;          
    _simulacionData.iva = 0; 
    _simulacionData.cantidadCheques = 0;
    _simulacionData.importePonderadoTotal = 0; 
    _simulacionData.netoTotal = 0;
    _simulacionData.fechaVencimientoPond =0;

    for(i=1; i<=50 ; i++){
        //valorNominal
        if(_chequeData[i].importe){
            _simulacionData.valorNominal+=parseFloat(_chequeData[i].importe);
            _simulacionData.cantidadCheques++; //cuento un nuevo cheque cuando ingreso el importe
        }

        _simulacionData.intereses+=_chequeData[i].intereses;
        _simulacionData.comision+=_chequeData[i].comision;
        _simulacionData.sellado+=_chequeData[i].sellado;          
        _simulacionData.iva+=_chequeData[i].iva;  
        _simulacionData.importePonderadoTotal+=_chequeData[i].ponderado; 
        _simulacionData.netoTotal+=_chequeData[i].neto;        

    }

    _simulacionData.gastoTotal = _simulacionData.intereses + _simulacionData.comision + _simulacionData.sellado + _simulacionData.iva;
    _simulacionData.netoLiquidar = _simulacionData.valorNominal - _simulacionData.gastoTotal;
    
    if(_simulacionData.valorNominal!=0){
        _simulacionData.fechaVencimientoPond = parseInt((_simulacionData.importePonderadoTotal / _simulacionData.valorNominal).toFixed(0));
    }else{
        _simulacionData.fechaVencimientoPond = 0;
    }
        
    if(_simulacionData.valorNominal!=0){
        _simulacionData.spreadTotal = _simulacionData.netoTotal / _simulacionData.valorNominal / _simulacionData.fechaVencimientoPond *365;
    }else{
        _simulacionData.spreadTotal = 0;
    }   
}

function refrescarCampos(){
    $("#cantidadCheques").text(_simulacionData.cantidadCheques);
    $("#valorNominal").text('$ ' + _simulacionData.valorNominal.toFixed(2).toString());
    $("#intereses").text('$ ' + _simulacionData.intereses.toFixed(2).toString());
    $("#comision").text('$ ' + _simulacionData.comision.toFixed(2).toString());
    $("#sellados").text('$ ' + _simulacionData.sellado.toFixed(2).toString());
    $("#iva").text('$ ' + _simulacionData.iva.toFixed(2).toString());
    $("#gastosTotales").text('$ ' + _simulacionData.gastoTotal.toFixed(2).toString());
    $("#netoALiquidar").text('$ ' + ( _simulacionData.netoLiquidar ).toFixed(2).toString());
    $("#fechaVencimientoPonderada").text(_simulacionData.fechaVencimientoPond.toString());
    $("#spreadPromedio").text(( _simulacionData.spreadTotal*100 ).toFixed(2).toString() + ' %');
    $("#estadoSimulacion").text(_simulacionData.estado); 
}

function obtenerTT(producto, dias){
    if(producto == "530") return 0.89;
    return 0.37;
}

function obtenerSellado(provinciaId){
    return 0.012;
}

function actualizarSimulacion(chequeId){

    //recalculo todo:
    calculosInternos(chequeId);

    //calculo de los totales de la simulacion
    calcularResumen();

    //los valores comentados se calculan previamente:
    _simulacionData.cuitCliente = $("#cuitCliente").val();
    _simulacionData.torCliente = $("#torCliente").val();
    _simulacionData.fechaDescuento = $("#fechaDescuento").val();
    //_simulacionData.valorNominal = 0; //importeTotal
    //_simulacionData.intereses = 0; //interesTotal
    //_simulacionData.comision = 0; //comisionTotal
    //_simulacionData.sellado = 0; //selladoTotal
    //_simulacionData.iva = 0; //ivaTotal
    //_simulacionData.gastoTotal = 0;
    _simulacionData.TT = 0; //este para mi esta al pedo, preguntarle a JAVI
    _simulacionData.TNAv = $("#tnavPactada").val();
    //_simulacionData.netoLiquidar = 0; //netoLiquidarTotal
    //_simulacionData.importePonderadoTotal = 0; 
    _simulacionData.tipoCateg = ""; //condicion IVA  --- ver como esta en la tabla y actualizar
    //_simulacionData.cantidadCheques = 0; //cantidad a comprar
    _simulacionData.codProd = $("#select-producto").val(); 
    //_simulacionData.fechaVencimientoPond =0;
    //_simulacionData.spreadTotal = 0;
    //_simulacionData.netoTotal = 0;
    _simulacionData.tasaIIBB = parseFloat($("#tasaIibb").val());
    _simulacionData.tasaIva = parseFloat($("#select-iva").val());
    _simulacionData.tasaSellado = obtenerSellado($("#select-provincia").val())*100;
    _simulacionData.estado = getEstado();
    _simulacionData.legajo = ""; //el legajo del usuario que accedió!
    _simulacionData.idProvincia = $("#select-provincia").val();

    refrescarCampos();
}

function consultarTOR(cuit){
    return $.ajax("/api/Validationes/GetTorState/" + cuit);  
}

function consultarNosis(data){
    return $.post("/api/Validationes/GetNosisState/", { "rows": data }, null, "json");
}

function getEstado(){
    if(_simulacionData.spreadTotal>=0.035) return "CONFIRMADA";
    if(_simulacionData.spreadTotal<0.035) return "PENDIENTE";
    return "SIMULANDO";
}

function fillComboProducto(){
    var comboBox = $("#select-producto");
    $.ajax("/api/Setting/GetAllProductos").done(function (data) {
        $.each(data, function () {
            comboBox.append($("<option />").val(this.id).text(this.Nombre));
            var itemProducto = {};
        });
    });
    
}

function fillComboProvincia() {
    var comboBox = $("#select-provincia");
    $.ajax("/api/Setting/GetAllProvincias").done(function (data) {
        $.each(data, function () {
            comboBox.append($("<option />").val(this.id).text(this.Nombre));
            var itemProducto = {};
        });
    });

}

$("#btn-confirmar").click(function(){
    $.post("/api/Simulacion/PostSimulacion", _simulacionData, null, "json").done(function (data)
    {}
    );
})

});