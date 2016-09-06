using LumenWorks.Framework.IO.Csv;
using Seminario.Model;
using Seminario.WebServices;
using Seminario.WebServices.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace Seminario.Web.Http
{
    public class UploadController : ApiController
    {
        public IProductoService ProductoService { get; set; }

        public IDatosTTService DatosTTService { get; set; }
    
        public UploadController(IProductoService productoService,IDatosTTService datosTTService)
        {
            this.ProductoService = productoService;
            this.DatosTTService = datosTTService;
        }

        [HttpPost]
        public HttpResponseMessage Upload()
        {
            HttpResponseMessage response = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var upload = httpRequest.Files[0];

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

                            var producto = ProductoService.Get(x => x.CodigoProducto ==
                                Convert.ToInt32(csvTable.AsEnumerable().First()[0].ToString().Split(';')[0]));

                            producto.DatosTT.Clear();
                            
                            foreach (var rw in csvTable.AsEnumerable())
                            {
                                producto.DatosTT.Add(new DatosTTDto()
                                {
                                    //Producto = producto,
                                    Plazo = Convert.ToInt32(rw[0].ToString().Split(';').GetValue(1)),
                                    TasaVigente = Convert.ToDecimal(rw[0].ToString().Split(';').GetValue(2))
                                });
                            }

                            ProductoService.Update(producto);

                        }
                        else
                        {
                            ModelState.AddModelError("File", "This file format is not supported");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("File", "Please Upload Your file");
                    }
                }

                response = ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
               
            }
            else
            {
                response = ControllerContext.Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return response;
        }
    }
}
