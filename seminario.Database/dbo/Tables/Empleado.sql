CREATE TABLE [Empleado]

(

[legajo] bigint NOT NULL,

[nombre] varchar(50) NULL,

[apellido] varchar(50) NULL,

[codigo] bigint NULL,
 CONSTRAINT [PK__empleado] PRIMARY KEY CLUSTERED

([legajo] ASC)


)
