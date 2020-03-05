using AppTesisLecturas.Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppTesisLecturas.Controlador
{
    //clase para realizar selección, inserción, modificación y eleiminación de registros de la tabla lectura de la base de datos
    public class CtrlLectura:CtrlBase
    {
        string Url;
        //método para crear la variable cliente que realizará la conexión al servidor usando el protocolo http
        private HttpClient getCliente()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Connection", "close");
            return client;
        }
        //método asíncrono que devuelve un objeto enumerable(lista) de tipo clslectura del paquete modelo
        public async Task<IEnumerable<ClsLectura>> Consultar(DateTime f1, DateTime f2)
        {
            if (f1 != null && f2 != null)
            {
                //llamada al script pbp para consultar las lecturas entre fechas  que devuelve un objeto tipo json de la tabla lectura               
                     Url = "http://"+Servidor+"/applecturas/logica/medidor/listar.php" +
                     "?fecha1=" + f1.ToShortDateString() +
                     "&fecha2=" + f2.ToShortDateString();
                HttpClient client = getCliente();
                var resp = await client.GetAsync(Url);
                if (resp.IsSuccessStatusCode)//si el codigo devuelto es satisfactorio se devuelve un objeto enumerable
                {
                    string content = await resp.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<ClsLectura>>(content);//retorna un objeto json desserializado 
                }
            }                
            return Enumerable.Empty<ClsLectura>();//devuelve una lista vacía
        }
        //método que invoca al script php que consulta la lectura anterior filtrado por el código de medidor de la tabla lectura
        public async Task<IEnumerable<ClsLectura>> ConsultarAnterior(int IdMedidor)
        {
            if (IdMedidor != 0)
            {
                    Url = "http://"+Servidor+"/applecturas/logica/lectura/consanterior.php" +
                   "?idmedidor=" + IdMedidor;
                HttpClient client = getCliente();
                var resp = await client.GetAsync(Url);
                if (resp.IsSuccessStatusCode)
                {
                    string content = await resp.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<ClsLectura>>(content);
                }
            }
            return Enumerable.Empty<ClsLectura>();
        }
        //método que invoca al script que realiza la inserción de registros en la base de datos en la tabla lectura
        public async Task<IEnumerable<ClsLectura>> InsertAsync(ClsLectura Objeto)
        {
                Url = "http://"+Servidor+"/applecturas/logica/lectura/insert.php?"
                + "anterior=" + Objeto.Anterior +
                "&actual=" + Objeto.Actual +
                "&consumo=" + Objeto.Consumo +
                "&basico=" + Objeto.Basico +
                "&exceso=" + Objeto.Exceso +
                "&observacion=" + Objeto.Observacion +
                "&idmedidor=" + Objeto.ObjMedidor.Id+
                "&fecha=" + Objeto.Fecha.ToShortDateString();
            HttpClient client = getCliente();
            var resp = await client.GetAsync(Url);
            if (resp.IsSuccessStatusCode)
            {
                string content = await resp.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<ClsLectura>>(content);
            }             
            return Enumerable.Empty<ClsLectura>();
        }
        //método que invoca al script que actualiza un registro en la base de datos tabla lectura
        public async Task<string> UpdateAsync(ClsLectura Objeto)
        {
            Url = "http://"+Servidor+"/applecturas/logica/lectura/update.php?"
                + "anterior=" + Objeto.Anterior +
                "&actual=" + Objeto.Actual +
                "&consumo=" + Objeto.Consumo +
                "&basico=" + Objeto.Basico +
                "&exceso=" + Objeto.Exceso +
                "&observacion=" + Objeto.Observacion +
                "&idmedidor=" + Objeto.ObjMedidor.Id +
                "&fecha=" + Objeto.Fecha.ToShortDateString()+
                "&estado=" + Objeto.Estado +
                "&idlectura=" + Objeto.Id;
            HttpClient client = getCliente();
            var resp = await client.GetAsync(Url);
            if (resp.IsSuccessStatusCode)
            {
                return await resp.Content.ReadAsStringAsync();
            }
            return "false";
        }
        //método que invoca al script que elimina registros en la tabla lectura de la base de datos
        public async Task<string> DeleteAsync(ClsMedidor Objeto)
        {
            Url = "http://"+Servidor+"/applecturas/logica/medidor/delete.php?"
                + "id=" + Objeto.Id;
            HttpClient client = getCliente();
            var resp = await client.GetAsync(Url);
            if (resp.IsSuccessStatusCode)
            {
                return await resp.Content.ReadAsStringAsync();
            }
            return "false";
        }
    }
}
