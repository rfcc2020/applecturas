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
    //vista para consultar permiso
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagListaPermiso : ContentPage
    {
        public PagListaPermiso()//constructor
        {
            InitializeComponent();
        }
        //controlador del botón listar
        private async void BtnListar_Clicked(object sender, EventArgs e)
        {
            try
            {
                CtrlPermiso manager = new CtrlPermiso();//instanciar controlador
                var res = await manager.Consultar();//llamada al metodo consultar
                if (res != null) //si la respuesta es no nulo
                {
                    lstPermisos.ItemsSource = res;//cargar en el lista los objetos consultados en la base de datos
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
            ClsPermiso ObjPermiso = new ClsPermiso//nueva instancia de clase clspermiso
            {
                FechaIngreso = DateTime.Now,//asignar fecha actual
                FechaModificacion = DateTime.Now
            };
            ((NavigationPage)this.Parent).PushAsync(new Vista.PagAdminPermiso(ObjPermiso, true));//mostrar la vista adminpermiso con campos vacíos para ingresar nuevo
        }
        //maneja el evento item selected de la lista de objetos
        private void lstPermisos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ClsPermiso ObjPermiso= e.SelectedItem as ClsPermiso;//asignar el objeto seleccionado a la variable obj
            ((NavigationPage)this.Parent).PushAsync(new PagAdminPermiso(ObjPermiso, false));//mostrar la vista adminpermiso con los datos cargados para modificar o eliminar
        }
    }
}