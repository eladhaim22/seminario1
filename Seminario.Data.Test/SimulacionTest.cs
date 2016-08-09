using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Seminario.NHibernate;
using Seminario.Model;
using System.Collections.Generic;

namespace Seminario.Data.Test
{
    [TestClass]
    public class SimulacionTest
    {
        private IUnitOfWork UnitOfWork;
        private IRepository<Simulacion> SimulacionRepository;
        
        public SimulacionTest()
        {    
        }

        [TestMethod]
        public void CanSaveSimulacion()
        {
            var Simulacion = new Simulacion
            {
                ComisionTotal = 0.2F,
                CuitCliente = "2312313",
                Estado = "Active",
                FechaDescuento = DateTime.Now,
                FechaVencimientoPond = 52F,
                GastoTotal = 12313F,
                ImportePonderadoTotal = 323F,
                ImporteTotal = 123123F,
                InteresTotal = 123F,
                TNAV = 0.2F,
                NetoLiquidarTotal = 13444F,
                IvaTotal = 4242F,
                NetoTotal = 213F,
                SelladoTotal = 1333F,
                SpreadTotal = 0.34F,
                TasaIIBB = 0.2F,
                TasaSellado = 0.2F,
                TipoCategoria = "ASDFASD",
                TT = 0.33F,
                TasaIva = 0.15F,
                TorCliente = "asda",
                Cheques = new List<Cheque>(),
                Empleado = new Empleado
                {
                    Legajo = "12313",
                    Nombre = "sdfsa"
                },
                Producto = new Producto
                {
                    Nombre = "5100"
                },
                Provincia = new Provincia
                {
                    Nombre = "FASDF",
                    Sellado = 213F
                }
            };
            
            Simulacion.Cheques.Add(new Cheque
            {
                Banco = "elad",
                CFT = 12F,
                CFTMes = 13F,
                Comision = 0.2F,
                Costo = 23133F,
                CuitEmisor = "23133",
                EstadoNosisEmisor = "a",
                FechaAcreditacion = DateTime.Now,
                IIBB = 0.2F,
                Importe = 12312F,
                ImportePonderado = 123F,
                Interes = 1.2F,
                IVA = 0.15F,
                Neto = 1231F,
                NetoLiquidar = 1221F,
                NombreEmisor = "DFAF",
                OtrosDias = 1,
                Plazo = 13,
                Sellado = 12F,
                Spread = 0.2F,
                TE = 0.2F,
                TEA = 0.2F,
                TEATT = 0.2F,
                TETT = 0.2F,
                TNAA = 0.3F
            });

            var unitOfWork = new UnitOfWork();
            new Repository<Simulacion>(unitOfWork).Add(Simulacion);
            unitOfWork.Commit();
        }
    }
}
