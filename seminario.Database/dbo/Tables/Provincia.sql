CREATE TABLE [Provincia](

[idProvincia] int identity(1,1),

[nombre] varchar(50) NULL,

[sellado] decimal(5,2) NULL,

CONSTRAINT [PK__provincia] PRIMARY KEY CLUSTERED

([idProvincia] ASC)
)