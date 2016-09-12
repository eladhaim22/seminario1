CREATE TABLE [DatosTT]
(
[idDatosTT] int identity(1,1),

[plazo] int NULL,

[tasaVigente] FLOAT NULL,

[idProducto] INT NULL, 
    CONSTRAINT [PK__datosTT] PRIMARY KEY CLUSTERED

([idDatosTT] ASC),
constraint FK__datosTT__idProducto__Producto foreign key (idProducto)

references Producto(idProducto)
)

