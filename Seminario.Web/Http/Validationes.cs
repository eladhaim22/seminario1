using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace Seminario.Web.Http
{

    public class DatosNosis
    {
        public IList<DataObject> rows { get; set; }
    }
    public class DataObject{
        public int id { get; set; }
        public string documento { get; set; }
    }
    
    public class ValidationesController : ApiController
    {
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetNosisState(DatosNosis data)
        {
           
            var regex = new Regex(@"^\d{9}$");
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            else
                foreach(DataObject row in data.rows){
                    if (row.documento != null && regex.IsMatch(row.documento))
                        row.documento = "OK";
                    else
                        if (row.documento == null)
                            row.documento = "";
                        else
                            row.documento = "Fail";
                }

            return Request.CreateResponse(HttpStatusCode.OK,data);
       
        }
       
        public HttpResponseMessage GetTorState(string id)
        {
            var regex = new Regex(@"^\d{11}$");
            if (id == null)
            {
                throw new ArgumentNullException("cuit");
            }
            else 
                if (regex.IsMatch(id)){
                    var rand = new Random();
                    return Request.CreateResponse(HttpStatusCode.OK, rand.NextDouble()); ;
                }
                else
                    throw new Exception("El cuit Ingresado es incorrecto");
        }

    }
}
