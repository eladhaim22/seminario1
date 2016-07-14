CREATE TABLE [Simulacion]
(
[idSimulacion] int Identity(1,1),

[cuitCliente] varchar(30) NULL,

[torCliente] varchar(20) NULL,

[fechaDescuento] datetime NULL,

[importeTotal] decimal(5,2) NULL,

[interesTotal] decimal(5,2) NULL,

[comisionTotal] decimal(5,2) NULL,

[selladoTotal] decimal(5,2) NULL,

[ivaTotal] decimal(5,2) NULL,

[gastoTotal] decimal(5,2) NULL,

[TT] decimal(5,2) NULL,

[TNAV] decimal(5,2) NULL,

[netoLiquidarTotal] decimal(5,2) NULL,

[importePonderadoTotal] decimal(5,2) NULL,

[tipoCateg] decimal(5,2) NULL,

[cantidadCompra] varchar(20) NULL,

[codProd] int NOT NULL,

[fechaVencimientoPond] datetime NULL,

[spreadTotal] decimal(5,2) NULL,

[netoTotal] decimal(5,2) NULL,

[tasaIIBB] decimal(5,2) NULL,

[tasaIva] decimal(5,2) NULL,

[tasaSellado] decimal(5,2) NULL,

[estado] varchar(20) NOT NULL,

[legajo] bigint NOT NULL,

[idProvincia] int NOT NULL, 

CONSTRAINT [PK__simulacion] PRIMARY KEY CLUSTERED
([idSimulacion] ASC),
constraint FK__simulacion__legajo__empleado foreign key (legajo) references

Empleado(legajo),

constraint FK__simulacion__idProvincia__provincia foreign key (idProvincia)

references Provincia(idProvincia),

constraint FK__simulacion__codProd__producto foreign key (codProd) references

Producto([idProducto])

)

