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
    //clase para realizar selección, inserción, modificación y eliminación en la tabla medidor de la base de datos.
    public class CtrlMedidor:CtrlBase
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
        //método asíncrono que devuelve un objeto enumerable(lista) de tipo clsmedidor del paquete modelo
        public async Task<IEnumerable<ClsMedidor>> Consultar(string Numero)
        {
            //llamada al script php para consultar los medidores por número que devuelve un objeto tipo json de la tabla medidor    
            if (Numero.Length != 0)
            {
                     Url = "http://"+Servidor+"/applecturas/logica/medidor/listar.php" +
                    "?numero=" + Numero;
                HttpClient client = getCliente();
                var resp = await client.GetAsync(Url);
                if (resp.IsSuccessStatusCode)//si el codigo devuelto es satisfactorio se devuelve un objeto enumerable
                {
                    string content = await resp.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<ClsMedidor>>(content);//retorna un objeto json desserializado 
                }
            }                
            return Enumerable.Empty<ClsMedidor>();//devuelve una lista vacía
        }
        //método que invoca al script php que consulta un un medidor filtrado por el id de persona asignada.
        public async Task<IEnumerable<ClsMedidor>> ConsultarIdPersona(int IdPersona)
        {
            if (IdPersona != 0)
            {
                    Url = "http://"+Servidor+"/applecturas/logica/medidor/consultaidpersona.php" +
                   "?idpersona=" + IdPersona;
                HttpClient client = getCliente();
                var resp = await client.GetAsync(Url);
                if (resp.IsSuccessStatusCode)
                {
                    string content = await resp.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<ClsMedidor>>(content);
                }
            }
            return Enumerable.Empty<ClsMedidor>();
        }
        //método que invoca al script que realiza la inserción de registros en la base de datos en la tabla medidor
        public async Task<IEnumerable<ClsMedidor>> InsertAsync(ClsMedidor Objeto)
        {
            Url = "http://"+Servidor+"/applecturas/logica/medidor/insert.php?"
                + "codigo=" + Objeto.Codigo +
                "&numero=" + Objeto.Numero +
                "&idpersona=" + Objeto.ObjPersona.Id;
            HttpClient client = getCliente();
            var resp = await client.GetAsync(Url);
            if (resp.IsSuccessStatusCode)
            {
                string content = await resp.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<ClsMedidor>>(content);
            }             
            return Enumerable.Empty<ClsMedidor>();
        }
        //método que invoca al script php que actualiza un registro en la base de datos tabla medidor
        public async Task<string> UpdateAsync(ClsMedidor Objeto)
        {
            Url = "http://"+Servidor+"/applecturas/logica/medidor/update.php?"
                + "codigo=" + Objeto.Codigo +
                "&numero=" + Objeto.Numero +
                "&idpersona=" + Objeto.ObjPersona.Id+
            "&estado=" + Objeto.Estado +
                "&id=" + Objeto.Id;
            HttpClient client = getCliente();
            var resp = await client.GetAsync(Url);
            if (resp.IsSuccessStatusCode)
            {
                return await resp.Content.ReadAsStringAsync();
            }
            return "false";
        }
        //método que invoca al script que elimina registros en la tabla medidor de la base de datos
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
