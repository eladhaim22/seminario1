﻿CREATE TABLE [Simulacion]
(
[idSimulacion] int Identity(1,1),

[cuitCliente] varchar(30) NULL,

[torCliente] varchar(20) NULL,

[fechaDescuento] datetime NULL,

[importeTotal] FLOAT(15) NULL,

[comision] FLOAT(15) NULL,

[interesTotal] FLOAT(15) NULL,

[comisionTotal] FLOAT(10) NULL,

[selladoTotal] FLOAT(10) NULL,

[ivaTotal] FLOAT(10) NULL,

[gastoTotal] FLOAT(10) NULL,

[TT] FLOAT(10) NULL,

[TNAV] FLOAT(10) NULL,

[netoLiquidarTotal] FLOAT(15) NULL,

[importePonderadoTotal] FLOAT(10) NULL,




[fechaVencimientoPond] FLOAT(10) NULL,

[spreadTotal] FLOAT(10) NULL,

[netoTotal] FLOAT(15) NULL,

[tasaIIBB] FLOAT(10) NULL,

[tasaIva] FLOAT(10) NULL,

[tasaSellado] FLOAT(10) NULL,

[estado] INT NOT NULL,

[tipoIva] INT NOT NULL,

[legajo] INT NULL,

[idProvincia] int NULL, 

[tipoCategoria] NCHAR(30) NULL, 
    [idProducto] INT NULL, 
    [fechaCreacion] DATETIME NOT NULL, 
    [fechaUltimaModificacion] DATETIME NOT NULL, 
    CONSTRAINT [PK__simulacion] PRIMARY KEY CLUSTERED
([idSimulacion] ASC),
constraint FK__simulacion__legajo__empleado foreign key (legajo) references

Empleado([EmpleadoId]),

constraint FK__simulacion__idProvincia__provincia foreign key (idProvincia)

references Provincia(idProvincia),

constraint FK__simulacion__idProducto__producto foreign key (idProducto) references

Producto([idProducto])

)

