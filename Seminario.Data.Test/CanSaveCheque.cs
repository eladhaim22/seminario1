using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Seminario.Model;
using Seminario.NHibernate;

using FluentValidation;
using FluentValidation.Results;
namespace Seminario.Data.Test
{
    [TestClass]
    public class CanSaveCheque
    {
        private Cheque NewCheque;

        public CanSaveCheque()
        {
            var NewCheque = new Cheque
            {
           
            };
        }

        [TestMethod]
        public void CanAddNewCheque()
        {
            var unitOfWork = new UnitOfWork();
            var cheque = new Cheque
            {
                CFT = 10,
                CFTMes = 10,
                Comision = 10,
                Costo = 10,
                CuitEmisor = "w213123",
                EstadoNosisEmisor = "asdasd",
                FechaAcreditacion = DateTime.Now,
                IIBB = 10,
                GastoTotal = 10,
                IVA = 10,
                Sellado = 10,
                Spread = 10,
                Importe = 10,
                ImportePonderado = 10,
                Interes = 10,
                Neto = 10,
                NetoLiquidar = 10,
                NombreEmisor = "elad",
                Plazo = 10,
                TE = 10,
                TEA = 10,
                TEATT = 10,
                TETT = 10,
                TNAA = 10
            };
            new Repository<Cheque>(unitOfWork).Create(cheque);
            unitOfWork.Commit();
            //var chequeValidator = new ChequeValidator();
            //chequeValidator.ValidateAndThrow(cheque);
        }
    }
}

