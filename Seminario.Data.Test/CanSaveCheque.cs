using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Seminario.Model;
using Seminario.NHibernate;
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
                Name = "elad",
                Amount = 100
            };
        }

        [TestMethod]
        public void CanAddNewCheque()
        {
            using (var session = FluentNHibernateHelper.OpenSession())
            {

                var cheque = new Cheque { Name = "elad", Amount = 199 };

                session.SaveOrUpdate(cheque);

            }
        }
    }
}

