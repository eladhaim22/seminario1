CREATE TABLE [DatosTT]
(
[idDatosTT] int identity(1,1),

[plazo] int NOT NULL,

[tasaVigente] decimal(5,2) NULL,

[nombre] varchar(30) NULL,

[sellado] decimal(5,2) NULL,

[tipoProducto] int NOT NULL,

CONSTRAINT [PK__datosTT] PRIMARY KEY CLUSTERED

([idDatosTT] ASC),

constraint FK__datosTT__codProd__producto

foreign key (tipoProducto) references Producto(codProd)
)

