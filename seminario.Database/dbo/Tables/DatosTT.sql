CREATE TABLE [DatosTT]
(
[idDatosTT] int identity(1,1),

[plazo] int NOT NULL,

[tasaVigente] decimal(5,2) NULL,




[idProducto] INT NOT NULL, 
    CONSTRAINT [PK__datosTT] PRIMARY KEY CLUSTERED

([idDatosTT] ASC), 
    CONSTRAINT [FK_DatosTT_Producto] FOREIGN KEY ([idProducto]) REFERENCES [Producto]([idProducto])
)

