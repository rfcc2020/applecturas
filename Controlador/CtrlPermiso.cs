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
    //clase para realizar selección, inserción, modificación y eliminación en la tabla permiso de la base de datos.
    public class CtrlPermiso:CtrlBase
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
        //método asíncrono que devuelve un objeto enumerable(lista) de tipo clspermiso del paquete modelo
        public async Task<IEnumerable<ClsPermiso>> Consultar()
        {
            //llamada al script php para consultar los permisos, devuelve un objeto tipo json de la tabla permiso    
            Url = "http://"+Servidor+"/applecturas/logica/permiso/listar.php";
                HttpClient client = getCliente();
                var resp = await client.GetAsync(Url);
                if (resp.IsSuccessStatusCode)//si el codigo devuelto es satisfactorio se devuelve un objeto enumerable
            {
                    string content = await resp.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<ClsPermiso>>(content);//retorna un objeto json desserializado 
            }               
            return Enumerable.Empty<ClsPermiso>();//devuelve una lista vacía
        }
        //método que invoca al script que realiza la inserción de registros en la base de datos en la tabla permiso
        public async Task<IEnumerable<ClsPermiso>> InsertAsync(ClsPermiso Objeto)
        {
            Url = "http://"+Servidor+"/applecturas/logica/permiso/insert.php?"
                + "nombre=" + Objeto.Nombre;
            HttpClient client = getCliente();
            var resp = await client.GetAsync(Url);
            if (resp.IsSuccessStatusCode)
            {
                string content = await resp.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<ClsPermiso>>(content);
            }
            return Enumerable.Empty<ClsPermiso>();
        }
        //método que invoca al script php que actualiza un registro en la base de datos tabla permiso
        public async Task<string> UpdateAsync(ClsPermiso Objeto)
        {
            Url = "http://localhost/applecturas/logica/permiso/update.php?"
                + "nombre=" + Objeto.Nombre+
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
        //método que invoca al script que elimina registros en la tabla permiso de la base de datos
        public async Task<string> DeleteAsync(ClsPermiso Objeto)
        {
            Url = "http://localhost/applecturas/logica/permiso/delete.php?"
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
