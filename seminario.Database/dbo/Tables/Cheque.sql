CREATE TABLE [Cheque]
(
[idCheque] int identity(1,1),

[fechaAcreditacion] datetime NULL,

[importe] FLOAT NULL,

[plazo] int NULL,

[cuitEmisor] varchar(30) NULL,

[nombreEmisor] varchar(50) NULL,

[estadoNosisEmisor] varchar(20) NULL,

[TE] FLOAT NULL,

[TEA] FLOAT NULL,

[TNAA] FLOAT NULL,

[interes] FLOAT NULL,

[comision] FLOAT NULL,
[sellado] FLOAT NULL,

[iva] FLOAT NULL,


[spread] FLOAT NULL,

[cft] FLOAT NULL,

[cftMes] FLOAT NULL,

[netoLiquidar] FLOAT NULL,

[importePonderado] FLOAT NULL,

[TETT] FLOAT NULL,

[TEATT] FLOAT NULL,

[IIBB] FLOAT NULL,

[costo] FLOAT NULL,

[neto] FLOAT NULL,

[idSimulacion] int NULL,

[otrosDias] INT NULL, 
    [banco] INT NULL, 
    CONSTRAINT [PK__cheque] PRIMARY KEY CLUSTERED

([idCheque] ASC),

constraint FK__cheque__idSimulacion__simulacion foreign key (idSimulacion)

references Simulacion(idSimulacion)
)

