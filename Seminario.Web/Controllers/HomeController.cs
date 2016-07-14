using LumenWorks.Framework.IO.Csv;
using Seminario.Dto;
using Seminario.MapperProject;
using Seminario.Model;
using Seminario.NHibernate;
using Seminario.Web.Models;
using Seminario.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Seminario.Web.Controllers
{
    public class HomeController : Controller
    {
        readonly IRepository<Producto> ProductoRepository;
        readonly IUnitOfWork UnitOfWork;
        readonly IRepository<DatosTT> DatosTTRepository;
        public HomeController(IRepository<Producto> ProductoRepository, IRepository<DatosTT> datosTTRepository,IUnitOfWork unitOfWork)
        {
            this.ProductoRepository = ProductoRepository;
            this.DatosTTRepository = datosTTRepository;
            this.UnitOfWork = unitOfWork;
        }

        [Authorize(Roles = "Jefe,Oficial")]
        public ActionResult Index(string id)
        {
            if (id == null)
            {
                return View(new SimulacionViewModel{
                    Simulacion = new SimulacionDto() 
                });
            }
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            return View();
        }

        [Authorize(Roles = "Jefe,Oficial")]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [Authorize(Roles = "Jefe,Oficial")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize(Roles = "Jefe")]
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Jefe")]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            
            if (ModelState.IsValid)
            {

                if (upload != null && upload.ContentLength > 0)
                {

                    if (upload.FileName.EndsWith(".csv"))
                    {
                        Stream stream = upload.InputStream;
                        DataTable csvTable = new DataTable();
                        using (CsvReader csvReader =
                            new CsvReader(new StreamReader(stream), true))
                        {
                            csvTable.Load(csvReader);
                        }

                        var producto = ProductoRepository.Get(x => x.Nombre == 
                            Convert.ToString(csvTable.AsEnumerable().First()[0].ToString().Split(';')[0]));
                        var productos = DatosTTRepository.GetMany(x => x.Producto == producto);
                        foreach (var prod in productos)
                        {
                            DatosTTRepository.Remove(prod);
                        }
 
                        var convertedList = (from rw in csvTable.AsEnumerable()
                               select new DatosTT()
                                {
                                    Producto = producto,
                                    Plazo = Convert.ToInt32(rw[0].ToString().Split(';').GetValue(1)),
                                    TasaVigente = Convert.ToDecimal(rw[0].ToString().Split(';').GetValue(2))
                                });

                        foreach (var cL in convertedList)
                        {
                            
                            DatosTTRepository.Add(cL);
                        }
                        
                        UnitOfWork.Commit();
                        return View(csvTable);
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
            }
            return View();
        }
    }
}
