Insert Into Producto (nombre,codigo)
Select 'Linea Banco Central',510 Union all
select 'Linea Normal',530
go

Insert Into dbo.Provincia (nombre,sellado)
SELECT 'Capital Federal',0.05 UNION ALL
SELECT 'Buenos Aires',0.05	UNION ALL
SELECT 'Catamarca',0.05 UNION ALL
SELECT 'Córdoba',0.05 UNION ALL
SELECT 'Corrientes',0.05 UNION ALL
SELECT 'Chaco',0.05 UNION ALL
SELECT 'Chubut',0.05 UNION ALL
SELECT 'Entre Ríos',0.05 UNION ALL
SELECT 'Formosa',0.05 UNION ALL
SELECT 'Jujuy',0.05 UNION ALL
SELECT 'La Pampa',0.05 UNION ALL
SELECT 'Santa Rosa',0.05 UNION ALL
SELECT 'La Rioja',0.05 UNION ALL
SELECT 'Mendoza',0.05 UNION ALL
SELECT 'Misiones',0.05 UNION ALL
SELECT 'Neuquén',0.05 UNION ALL
SELECT 'Río Negro',0.05 UNION ALL
SELECT 'Salta',0.05 UNION ALL
SELECT 'San Juan',0.05 UNION ALL
SELECT 'San Luis',0.05 UNION ALL
SELECT 'Santa Cruz',0.05 UNION ALL
SELECT 'Santa Fe',0.05 UNION ALL
SELECT 'Santiago del Estero',0.05 UNION ALL
SELECT 'Tierra del Fuego,',0.05 UNION ALL
SELECT 'Antártida e Islas del Atlántico Sur',0.05 UNION ALL
SELECT 'Ushuaia',0.05 UNION ALL
SELECT 'Tucumán',0.05 
go


Insert Into webpages_Roles
SELECT 1,'Jefe' UNION ALL
SELECT 2,'Oficial'
go


Insert Into webpages_Membership 
SELECT 1,GETDATE(),NULL,1,NULL,0,'ABWyhDxGJEs0fwaeM8GFP93pyAbpVGA3pPg8gz6TfGY4qUrxOSDqsr0hqlGmaIfC5Q==',GETDATE(),'',NULL,NULL UNION ALL
SELECT 2,GETDATE(),NULL,1,NULL,0,'AEd4YANdu64caW0NJytmlRbkfzuRvhfrjqUxOFQqkUctEERa6PjuhtpGHvCo+ES8og==',GETDATE(),'',NULL,NULL
go

Insert Into Empleado 
Select 1,'Elad','legajoJefeProducto' Union all
select 2,'Elad1','legajoOficial'

go
 
Insert Into webpages_UsersInRoles
SELECT 1,1 union all
select 2,2  
