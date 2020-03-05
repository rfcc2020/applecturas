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
    //vista para consultar perfiles
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagListaPerfil : ContentPage
    {
        public PagListaPerfil()//constructor
        {
            InitializeComponent();
        }
        //controlador del botón listar
        private async void BtnListar_Clicked(object sender, EventArgs e)
        {
            try
            {
                CtrlPerfil manager = new CtrlPerfil();//instanciar controlador
                var res = await manager.Consultar();//llamada al metodo consultar
                if (res != null)//si la respuesta es no nulo
                {
                    lstPerfiles.ItemsSource = res;//cargar en el lista los objetos consultados en la base de datos
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
            ClsPerfil ObjPerfil = new ClsPerfil();//nueva instancia de clase clsperfil
            ObjPerfil.FechaIngreso = DateTime.Now;//asignar fecha actual
            ObjPerfil.FechaModificacion = DateTime.Now;
            ((NavigationPage)this.Parent).PushAsync(new Vista.PagAdminPerfil(ObjPerfil, true));//mostrar la vista adminperfil con campos vacíos para ingresar nuevo
        }
        //maneja el evento item selected de la lista de objetos
        private void lstPerfiles_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ClsPerfil ObjPerfil= e.SelectedItem as ClsPerfil;//asignar el objeto seleccionado a la variable obj
            ((NavigationPage)this.Parent).PushAsync(new PagAdminPerfil(ObjPerfil, false));
        }
    }
}