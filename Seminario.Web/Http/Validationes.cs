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
        public IList<string> rows { get; set; }
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
            var datos = Enumerable.Range(0, data.rows.Count).Select(x => 0).ToList();
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            else
            {
                for(var i=0;i<data.rows.Count;i++)
                {
                    if (data.rows[i] == null || !ValidarCuit(data.rows[i]))
                        datos[i] = 0;
                    else
                        datos[i] = new Random().Next(100) < 20 ? 1 : 2; 
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK,datos);
       
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetTorState(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("cuit");
            }
            try { 
                if(ValidarCuit(id)){
                     return Request.CreateResponse(HttpStatusCode.OK, Convert.ToInt32(new Random().NextDouble() * 100));
                }
                else
                    return Request.CreateResponse(HttpStatusCode.InternalServerError,"El cuit is invalido");
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "El cuit is invalido");
            }
        }
       
        public bool ValidarCuit(string cuit){
            try
            {
                int[] ia = cuit.ToCharArray().Select(n => Convert.ToInt32(n.ToString())).ToArray();
                if (ia.Length == 11)
                {
                    var sum1 = ia[0] * 5 + ia[1] * 4 + ia[2] * 3 + ia[3] * 2 + ia[4] * 7 + ia[5] * 6 + ia[6] * 5 + ia[7] * 4 + ia[8] * 3 + ia[9] * 2;
                    var sum2 = 11 - sum1 % 11;
                    switch (sum2)
                    {
                        case 11: return ia[10] == 0 ? true : false;
                        case 10: return ia[10] == 9 ? true : false;
                        default: return ia[10] == sum2 ? true : false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
