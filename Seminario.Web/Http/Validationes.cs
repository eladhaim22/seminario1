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

namespace Seminario.Web.Http
{
	public class DatosNosis
	{
		public IList<string> rows { get; set; }
	}

	public class DataObject
	{
		public int id { get; set; }
		public string documento { get; set; }
	}

	public class BankClientes
	{
		public string Id { get; set; }
		public string Points { get; set; }
		public int State { get; set; }
		public string RazonSocial { get; set; }
	}

	public class ValidationesController : ApiController
	{
		public IList<BankClientes> Clients { get; set; }

		public ValidationesController()
		{
			Clients = new List<BankClientes>();
			FillClients();
		}

		[HttpPost]
		public HttpResponseMessage GetNosisState(DatosNosis data)
		{
			var datos = Enumerable.Range(0, data.rows.Count).Select(x => 0).ToList();
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			else
			{
				for (var i = 0; i < data.rows.Count; i++)
				{
					if (data.rows[i] == null || !ValidarCuit(data.rows[i]))
						datos[i] = 0;
					else
						datos[i] = new Random().Next(100) > 20 ? 1 : 2;
				}
			}
			return Request.CreateResponse(HttpStatusCode.OK, datos);
		}

		[HttpGet]
		public HttpResponseMessage GetTorState(string id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("cuit");
			}
			try
			{
				var cliente = Clients.First(x=> x.Id == id);
				if (cliente != null)
				{
					return Request.CreateResponse(HttpStatusCode.OK,  });
				}
				else
					return Request.CreateResponse(HttpStatusCode.InternalServerError, "El cuit is invalido");
			}

			catch (Exception)
			{
				return Request.CreateResponse(HttpStatusCode.InternalServerError, "El cuit is invalido");
			}
		}

		private void FillClients()
		{
			Clients.Add(new BankClientes() { Id = "25560365", Points = "213", State = 1	,RazonSocial="Elad Haim"});
			Clients.Add(new BankClientes() { Id = "95163005", Points = "332", State = 1	,RazonSocial="Nicolas Tenconi"});
			Clients.Add(new BankClientes() { Id = "56541233", Points = "255", State = 1 ,RazonSocial="Agustin Vago"});
			Clients.Add(new BankClientes() { Id = "23424442", Points = "456", State = 1 ,RazonSocial="Javier Rodriguez"});
			Clients.Add(new BankClientes() { Id = "53435225", Points = "567", State = 1 ,RazonSocial="Juan Perez"});
			Clients.Add(new BankClientes() { Id = "24212341", Points = "876", State = 0 ,RazonSocial="Alejandro Musconi"});
			Clients.Add(new BankClientes() { Id = "54564565", Points = "943", State = 0 ,RazonSocial="Alberto Rodriguez"});
			Clients.Add(new BankClientes() { Id = "21356546", Points = "456", State = 0 ,RazonSocial="Cristian Lopez"});
			Clients.Add(new BankClientes() { Id = "78898799", Points = "214", State = 1	,RazonSocial="Catalina Perez" });
			Clients.Add(new BankClientes() { Id = "21234566", Points = "548", State = 1 ,RazonSocial="Juan Martin Lopez"});
			Clients.Add(new BankClientes() { Id = "54687964", Points = "613", State = 0 ,RazonSocial="Nicolas Martino"});
			Clients.Add(new BankClientes() { Id = "33235545", Points = "328", State = 1 ,RazonSocial="Ariel Martinez"});
			Clients.Add(new BankClientes() { Id = "48999799", Points = "498", State = 1 ,RazonSocial="Clarin S.A"});
			Clients.Add(new BankClientes() { Id = "21345564", Points = "156", State = 1 ,RazonSocial="CableVision S.A"});
			Clients.Add(new BankClientes() { Id = "16464986", Points = "874", State = 1 ,RazonSocial="Arcor S.A"});
			Clients.Add(new BankClientes() { Id = "25560365", Points = "213", State = 0 ,RazonSocial="Microsoft S.A"});
			Clients.Add(new BankClientes() { Id = "95163005", Points = "332", State = 0 ,RazonSocial="Google"});
			Clients.Add(new BankClientes() { Id = "56541233", Points = "255", State = 1 ,RazonSocial="SanCor S.A"});
			Clients.Add(new BankClientes() { Id = "23424442", Points = "456", State = 0 ,RazonSocial="Serenissima S.A"});
			Clients.Add(new BankClientes() { Id = "53435225", Points = "567", State = 1 ,RazonSocial="Grupo Plaza S.A"});
			Clients.Add(new BankClientes() { Id = "24212341", Points = "876", State = 0 ,RazonSocial="Acindar S.A"});
			Clients.Add(new BankClientes() { Id = "54564565", Points = "943", State = 0 ,RazonSocial="Telecom S.A"});
			Clients.Add(new BankClientes() { Id = "21356546", Points = "456", State = 1, RazonSocial ="Peugeot S.A" });
			Clients.Add(new BankClientes() { Id = "78898799", Points = "214", State = 0, RazonSocial = "G&L Group S.A" });
			Clients.Add(new BankClientes() { Id = "21234566", Points = "548", State = 1 ,RazonSocial="Direct TV S.A"});
			Clients.Add(new BankClientes() { Id = "54687964", Points = "613", State = 0 ,RazonSocial="Shell S.A"});
			Clients.Add(new BankClientes() { Id = "33235545", Points = "328", State = 1, RazonSocial = "Siderar S.A" });
			Clients.Add(new BankClientes() { Id = "48999799", Points = "498", State = 0, RazonSocial = "Philips Argentina S.A" });
			Clients.Add(new BankClientes() { Id = "21345564", Points = "156", State = 1 ,RazonSocial="OCA S.A"});
		}

		/*public bool ValidarCuit(string cuit){
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
		}*/
	}
}
