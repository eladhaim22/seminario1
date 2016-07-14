CREATE TABLE [Cheque]
(
[idCheque] int identity(1,1),

[fechaAcreditacion] datetime NULL,

[importe] decimal(5,2) NULL,

[plazo] int NULL,

[cuitEmisor] varchar(30) NULL,

[nombreEmisor] varchar(50) NULL,

[estadoNosisEmisor] varchar(20) NULL,

[TE] decimal(5,2) NULL,

[TEA] decimal(5,2) NULL,

[TNAA] decimal(5,2) NULL,

[interes] decimal(5,2) NULL,

[comision] decimal(5,2) NULL,
[sellado] decimal(5,2) NULL,

[iva] decimal(5,2) NULL,

[gastoTotal] decimal(5,2) NULL,

[spread] decimal(5,2) NULL,

[cft] decimal(5,2) NULL,

[cftMes] decimal(5,2) NULL,

[netoLiquidar] decimal(5,2) NULL,

[importePonderado] decimal(5,2) NULL,

[TETT] decimal(5,2) NULL,

[TEATT] decimal(5,2) NULL,

[IIBB] decimal(5,2) NULL,

[costo] decimal(5,2) NULL,

[neto] decimal(5,2) NULL,

[idSimulacion] int NULL,

[otrosDias] INT NULL, 
    [banco] NCHAR(40) NULL, 
    CONSTRAINT [PK__cheque] PRIMARY KEY CLUSTERED

([idCheque] ASC),

constraint FK__cheque__idSimulacion__simulacion foreign key (idSimulacion)

references Simulacion(idSimulacion)
)

