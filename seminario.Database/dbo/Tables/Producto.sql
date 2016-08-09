CREATE TABLE [Producto]

(

[idProducto] int identity(1,1),

[nombre] varchar(30) NULL,
 [codigo] INT NULL, 
    CONSTRAINT [PK__producto] PRIMARY KEY CLUSTERED

([idProducto] ASC)

)

