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
    //clase para realizar selección, inserción, modificación y eliminación en la tabla usuario de la base de datos.
    public class CtrlUsuario:CtrlBase
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
        //método asíncrono que devuelve un objeto enumerable(lista) de tipo clsusuario del paquete modelo filtrado por id de perfil
        public async Task<IEnumerable<ClsUsuario>> Consultar(string idperfil)
        {
            if (idperfil.Length != 0)
            {
                //llamada al script php para consultar los usuarios, devuelve un objeto tipo json de la tabla usuario    
                Url = "http://"+Servidor+"/applecturas/logica/usuario/listar.php" +
                    "?idperfil=" + idperfil;
                HttpClient client = getCliente();
                var resp = await client.GetAsync(Url);
                if (resp.IsSuccessStatusCode)//si el codigo devuelto es satisfactorio se devuelve un objeto enumerable
                {
                    string content = await resp.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<ClsUsuario>>(content);//retorna un objeto json desserializado
                }
            }                
            return Enumerable.Empty<ClsUsuario>();//devuelve una lista vacía
        }
        //método asíncrono que devuelve un objeto enumerable(lista) de tipo clsusuario del paquete modelo filtrado por email y password (login)
        public async Task<IEnumerable<ClsUsuario>> LoginUsr(string email, string password)
        {
            if (email.Length != 0 && password.Length != 0)
            {
                    Url = "http://"+Servidor+"/applecturas/logica/usuario/login.php" +
                    "?email=" + email +
                    "&password=" + password;
                    HttpClient client = getCliente();
                    var resp = await client.GetAsync(Url);
                    if (resp.IsSuccessStatusCode)
                    {
                        string content = await resp.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<IEnumerable<ClsUsuario>>(content);
                    }
                }
                return Enumerable.Empty<ClsUsuario>();
        }
        //método que invoca al script que realiza la inserción de registros en la base de datos en la tabla usuario
        public async Task<IEnumerable<ClsUsuario>> InsertAsync(ClsUsuario Objeto)
        {
            Url = "http://"+Servidor+"/applecturas/logica/usuario/insert.php?"
                + "password=" + Objeto.Password +
                "&idpersona=" + Objeto.ObjPersona.Id +
                "&idperfil=" + Objeto.ObjPerfil.Id;
            HttpClient client = getCliente();
            var resp = await client.GetAsync(Url);
            if (resp.IsSuccessStatusCode)
            {
                string content = await resp.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<ClsUsuario>>(content);
            }
            return Enumerable.Empty<ClsUsuario>();
        }
        //método que invoca al script php que actualiza un registro en la base de datos tabla usuario
        public async Task<string> UpdateAsync(ClsUsuario Objeto)
        {
            Url = "http://"+Servidor+"/applecturas/logica/usuario/update.php?"
                + "password=" + Objeto.Password +
                "&idpersona=" + Objeto.ObjPersona.Id +
                "&idperfil=" + Objeto.ObjPerfil.Id +
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
        //método que invoca al script que elimina registros en la tabla usuario de la base de datos
        public async Task<string> DeleteAsync(ClsUsuario Objeto)
        {
            Url = "http://localhost/applecturas/logica/usuario/delete.php?"
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
