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
    //clase para realizar selección, inserción, modificación y eliminación en la tabla perfil de la base de datos.
    public class CtrlPerfil:CtrlBase
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
        //método asíncrono que devuelve un objeto enumerable(lista) de tipo clsperfil del paquete modelo
        public async Task<IEnumerable<ClsPerfil>> Consultar()
        {
            //llamada al script php para consultar los perfiles, devuelve un objeto tipo json de la tabla perfil    
            Url = "http://"+Servidor+"/applecturas/logica/perfil/listar.php";
                HttpClient client = getCliente();
                var resp = await client.GetAsync(Url);
                if (resp.IsSuccessStatusCode)//si el codigo devuelto es satisfactorio se devuelve un objeto enumerable
            {
                    string content = await resp.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<ClsPerfil>>(content);//retorna un objeto json desserializado 
            }               
            return Enumerable.Empty<ClsPerfil>();//devuelve una lista vacía
        }
        //método asíncrono que devuelve un objeto enumerable(lista) de tipo clsperfil filtrado por id, del paquete modelo
        public async Task<IEnumerable<ClsPerfil>> Consultar(int id)
        {

                Url = "http://"+Servidor+"/applecturas/logica/perfil/consultaid.php"+
                    "?id=" + id;
            HttpClient client = getCliente();
            var resp = await client.GetAsync(Url);
            if (resp.IsSuccessStatusCode)
            {
                string content = await resp.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<ClsPerfil>>(content);
            }
            return Enumerable.Empty<ClsPerfil>();
        }
        //método que invoca al script que realiza la inserción de registros en la base de datos en la tabla perfil
        public async Task<IEnumerable<ClsPerfil>> InsertAsync(ClsPerfil Objeto)
        {
            Url = "http://"+Servidor+"/applecturas/logica/perfil/insert.php?"
                + "nombre=" + Objeto.Nombre;
            HttpClient client = getCliente();
            var resp = await client.GetAsync(Url);
            if (resp.IsSuccessStatusCode)
            {
                string content = await resp.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<ClsPerfil>>(content);
            }
            return Enumerable.Empty<ClsPerfil>();
        }
        //método que invoca al script php que actualiza un registro en la base de datos tabla perfil
        public async Task<string> UpdateAsync(ClsPerfil Objeto)
        {
            Url = "http://"+Servidor+"/applecturas/logica/perfil/update.php?"
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
        //método que invoca al script que elimina registros en la tabla perfil de la base de datos
        public async Task<string> DeleteAsync(ClsPerfil Objeto)
        {
            Url = "http://"+Servidor+"/applecturas/logica/perfil/delete.php?"
                + "id=" + Objeto.Id;
            HttpClient client = getCliente();
            var resp = await client.GetAsync(Url);
            if (resp.IsSuccessStatusCode)
            {
                return await resp.Content.ReadAsStringAsync();
            }
            return "false";
        }
        //método que inserta registro en la tabla perfilpermiso de la base de datos
        public async Task<string> AsignaPermisoAsync(ClsPerfil Objeto, ClsPermiso Objeto2)
        {
            Url = "http://"+Servidor+"/applecturas/logica/perfilpermiso/insert.php?"
                + "idperfil=" + Objeto.Id +
                "&idpermiso=" + Objeto2.Id;
            HttpClient client = getCliente();
            var resp = await client.GetAsync(Url);
            if (resp.IsSuccessStatusCode)
            {
                return await resp.Content.ReadAsStringAsync();
            }
            return "false";
        }
        //método que elimina registro en la tabla perfilpermiso de la base de datos
        public async Task<string> QuitaPermisoAsync(ClsPerfil Objeto, ClsPermiso Objeto2)
        {
            Url = "http://"+Servidor+"/applecturas/logica/perfilpermiso/delete.php?"
                + "idperfil=" + Objeto.Id +
                "&idpermiso=" + Objeto2.Id;
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
