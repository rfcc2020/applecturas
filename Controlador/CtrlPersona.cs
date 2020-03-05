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
    //clase para realizar selección, inserción, modificación y eliminación en la tabla persona de la base de datos.
    public class CtrlPersona:CtrlBase
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
        //método asíncrono que devuelve un objeto enumerable(lista) de tipo clspersona del paquete modelo filtrado por nombre
        public async Task<IEnumerable<ClsPersona>> Consultar(string nombre)
        {
            if (nombre.Length != 0)
            {
                //llamada al script php para consultar las personas, devuelve un objeto tipo json de la tabla persona    
                Url = "http://"+Servidor+"/applecturas/logica/personas/listar.php" +
                    "?nombres=" + nombre;
                HttpClient client = getCliente();
                var resp = await client.GetAsync(Url);
                if (resp.IsSuccessStatusCode)//si el codigo devuelto es satisfactorio se devuelve un objeto enumerable
                {
                    string content = await resp.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<ClsPersona>>(content);//retorna un objeto json desserializado 
                }
            }                
            return Enumerable.Empty<ClsPersona>();//devuelve una lista vacía
        }
        //método asíncrono que devuelve un objeto enumerable(lista) de tipo clspersona del paquete modelo filtrado por id
        public async Task<IEnumerable<ClsPersona>> ConsultarId(int id)
        {
            if (id != 0)
            {
                    Url = "http://"+Servidor+"/applecturas/logica/personas/buscarid.php" +
                   "?id=" + id;
                HttpClient client = getCliente();
                var resp = await client.GetAsync(Url);
                if (resp.IsSuccessStatusCode)
                {
                    string content = await resp.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<ClsPersona>>(content);
                }
            }
            return Enumerable.Empty<ClsPersona>();
        }
        //método asíncrono que devuelve un objeto enumerable(lista) de tipo clspersona filtrado por cédula, del paquete modelo
        public async Task<IEnumerable<ClsPersona>> ConsultarCi(string Ci)
        {
            if (Ci.Length != 0)
            {
                    Url = "http://"+Servidor+"/applecturas/logica/personas/buscarci.php" +
                   "?ci=" + Ci;
                HttpClient client = getCliente();
                var resp = await client.GetAsync(Url);
                if (resp.IsSuccessStatusCode)
                {
                    string content = await resp.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<ClsPersona>>(content);
                }
            }
            return Enumerable.Empty<ClsPersona>();
        }
        //método que invoca al script que realiza la inserción de registros en la base de datos en la tabla persona
        public async Task<IEnumerable<ClsPersona>> InsertAsync(ClsPersona Objeto)
        {
            Url = "http://"+Servidor+"/applecturas/logica/personas/insert.php?"
                + "cedula=" + Objeto.Cedula +
                "&nombres=" + Objeto.Nombres +
                "&apellidos=" + Objeto.Apellidos +
                "&telefono=" + Objeto.Telefono +
                "&email=" + Objeto.Email;
            HttpClient client = getCliente();
            var resp = await client.GetAsync(Url);
            if (resp.IsSuccessStatusCode)
            {
                string content = await resp.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<ClsPersona>>(content);
            }
            return Enumerable.Empty<ClsPersona>();
        }
        //método que invoca al script php que actualiza un registro en la base de datos tabla persona
        public async Task<string> UpdateAsync(ClsPersona Objeto)
        {
            Url = "http://"+Servidor+"/applecturas/logica/personas/update.php?" +
                "id=" + Objeto.Id +
                "&cedula=" + Objeto.Cedula +
                "&nombres=" + Objeto.Nombres +
                "&apellidos=" + Objeto.Apellidos +
                "&telefono=" + Objeto.Telefono +
                "&email=" + Objeto.Email+
                "&estado=" + Objeto.Estado;
            HttpClient client = getCliente();
            var resp = await client.GetAsync(Url);
            if (resp.IsSuccessStatusCode)
            {
                return await resp.Content.ReadAsStringAsync();
            }
            return "false";
        }
        //método que invoca al script que elimina registros en la tabla persona de la base de datos
        public async Task<string> DeleteAsync(ClsPersona Objeto)
        {
            Url = "http://"+Servidor+"/applecturas/logica/personas/delete.php?"
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
