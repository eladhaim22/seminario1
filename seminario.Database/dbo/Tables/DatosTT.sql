CREATE TABLE [DatosTT]
(
[idDatosTT] int identity(1,1),

[plazo] int NOT NULL,

[tasaVigente] decimal(5,2) NULL,


[sellado] decimal(5,2) NULL,

[idProducto] int NOT NULL,

CONSTRAINT [PK__datosTT] PRIMARY KEY CLUSTERED

([idDatosTT] ASC),

constraint FK__datosTT__idProducto

foreign key ([idProducto]) references Producto([idProducto])
)

