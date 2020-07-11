using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coConsultRNC
{
    public class ResultRnc
    {
        public string CedulaRnc { get; set; }
        public string Nombre { get; set; }
        public string NombreComercial { get; set; }
        public string Categoria { get; set; }
        public string RegimenDePago { get; set; }
        public string Estado { get; set; }
    }

     public string GetNombre(string rncCedula)
    {
        string resultado = "";
        try
        {
            var result = new ResultRnc();
            var client = new RestClient("http://www.dgii.gov.do/app/WebApps/ConsultasWeb/consultas/");
            var request = new RestRequest("Rnc.aspx", Method.POST);

            //Esto en realidad es un force. Son valores que ASP.NET espera en el HTTP Request. 
            //Denle las gracias a Web Forms por esta ;)
            request.AddParameter("__EVENTTARGET", "");
            request.AddParameter("__EVENTARGUMENT", "");
            request.AddParameter("__VIEWSTATE", "/wEPDwUKMTkxNDA2Nzc4Nw9kFgJmD2QWAgIBD2QWAgIDD2QWAmYPZBYCAgEPZBYCAgUPZBYEAgUPPCsADwEADxYEHgtfIURhdGFCb3VuZGceC18hSXRlbUNvdW50ZmRkAgcPPCsADQBkGAIFH2N0bDAwJGNwaE1haW4kZ3ZCdXNjUmF6b25Tb2NpYWwPZ2QFI2N0bDAwJGNwaE1haW4kZHZEYXRvc0NvbnRyaWJ1eWVudGVzD2dkM6A6zdloNYs/efZ4JU/LIN3TDKM=");

            request.AddParameter("__EVENTVALIDATION", "/wEWBQLj6vfLDALqq//bBAKC/r/9AwKhwMi7BAKKnIvVCUYxKuo9/DDpyc38di1xIRjCtI3M");

            //Estos son los valores
            //  request.AddParameter("rbtnlTipoBusqueda", "0");
            request.AddParameter("ctl00$cphMain$txtRNCCedula", rncCedula);
            request.AddParameter("ctl00$cphMain$btnBuscarPorRNC", "Buscar");

            // HTTP Headers
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            var response = client.Execute(request);

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(response.Content);
            var trs = doc.GetElementbyId("ctl00_cphMain_dvDatosContribuyentes");

            if (trs != null)
            {
                var valores = trs.Descendants("td").ToList();

                result.CedulaRnc = valores.Skip(1).First().InnerText;
                result.Nombre = valores.Skip(3).First().InnerText;
                result.NombreComercial = valores.Skip(5).First().InnerText;
                result.Categoria = valores.Skip(7).First().InnerText;
                result.RegimenDePago = valores.Skip(9).First().InnerText;
                result.Estado = valores.Skip(11).First().InnerText;
            }
            resultado = System.Net.WebUtility.HtmlDecode(result.Nombre.ToString());
            if (result.NombreComercial.ToString() != "&nbsp;")
            {
                resultado = System.Net.WebUtility.HtmlDecode(result.NombreComercial.ToString());
            }

            return resultado;
        }
        catch (Exception)
        {

            return resultado;
        }
    }

    
}
