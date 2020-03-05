using AppTesisLecturas.Controlador;
using AppTesisLecturas.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTesisLecturas.Vista
{
    //vista para consultar usuarios
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagListaUsuario : ContentPage
    {
        public PagListaUsuario()//constructor
        {
            InitializeComponent();
        }

        private async void BtnListar_Clicked(object sender, EventArgs e)
        {
            try
            {
                CtrlUsuario manager = new CtrlUsuario();//instanciar controlador
                var res = await manager.Consultar(TxtIdPerfil.Text);//llamada al metodo consultar pasando como parámetro el idperfil ingresado en la caja de texto
                if (res != null)//si la respuesta es no nulo
                {
                    lstUsuarios.ItemsSource = res;//cargar en el lista los objetos consultados en la base de datos
                }
            }
            catch (Exception e1)//control de errores
            {
                await DisplayAlert("Mensaje", e1.Message, "ok");
            }
        }
        //manejador del botón agregar
        private void BtnAgregar_Clicked(object sender, EventArgs e)
        {
            ClsUsuario ObjUsuario = new ClsUsuario();//nueva instancia clase clsusuario
            ClsPersona ObjPersona = new ClsPersona();//nueva instancia clase clspersona
            ClsPerfil ObjPerfil = new ClsPerfil();//nueva instancia clase objperfil
            ObjUsuario.ObjPersona = ObjPersona;//asignar instancias a la instancia del la clase usuario
            ObjUsuario.ObjPerfil = ObjPerfil;
            ObjUsuario.FechaIngreso = DateTime.Now;//asignar fecha actual
            ObjUsuario.FechaModificacion = DateTime.Now;
            ((NavigationPage)this.Parent).PushAsync(new Vista.PagAdminUsuario(ObjUsuario, true));//mostrar la vista adminusuario con campos vacíos para ingresar nuevo
        }
        //maneja el evento item selected de la lista de objetos
        private void lstUsuarios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ClsUsuario ObjUsuario= e.SelectedItem as ClsUsuario;//asignar el objeto seleccionado a la variable objusuario
            ((NavigationPage)this.Parent).PushAsync(new PagAdminUsuario(ObjUsuario, false));//mostrar la vista adminusuario con los datos cargados para modificar o eliminar
        }
    }
}