using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Seminario.Model;
using Seminario.NHibernate;

using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
namespace Seminario.Data.Test
{
    [TestClass]
    public class CanSaveCheque
    {
        readonly IRepository<Producto> repository;
        readonly IUnitOfWork unitOfWork;

        public CanSaveCheque()
        {
        }        

        [TestMethod]
        public void CanSaveProducto()
        {
            var unitOfWork = new UnitOfWork();
            new Repository<Producto>(unitOfWork).Add(new Producto { Nombre = "5100" });
            unitOfWork.Commit();
        }

        [TestMethod]
        public void CanSaveDatosTT()
        {
            var unitOfWork = new UnitOfWork();
            var producto = new Repository<Producto>(unitOfWork).GetById(2);
            new Repository<DatosTT>(unitOfWork).Add(new DatosTT { Plazo = 4, Producto = producto});
            unitOfWork.Commit();
        }

        [TestMethod]
        public void CanUseWhereClouse()
        {
            var unitOfWork = new UnitOfWork();
            var producto = new Repository<Producto>(unitOfWork).Get(x => x.Nombre == "Linea Normal");
            unitOfWork.Commit();
            Assert.IsNotNull(producto);
        }
        
        [TestMethod]
        public void CanRecoverProducto()
        {
            var unitOfWork = new UnitOfWork();
            var producto = new Repository<Producto>(unitOfWork).Get(x => x.Nombre == "Linea Banco Central");
            Assert.IsNotNull(producto);
        }
    }
}

