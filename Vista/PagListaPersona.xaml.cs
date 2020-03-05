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
    //vista para consultar persona
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagListaPersona : ContentPage
    {
        public PagListaPersona()//constructor
        {
            InitializeComponent();
        }

        private async void BtnListar_Clicked(object sender, EventArgs e)
        {
            try
            {
                CtrlPersona manager = new CtrlPersona();//instanciar controlador
                var res = await manager.Consultar(TxtNombre.Text);//llamada al metodo consultar pasando como parámetro el nombre ingresado en la caja de texto
                if (res != null)//si la respuesta es no nulo
                {
                    lstPersonas.ItemsSource = res;//cargar en el lista los objetos consultados en la base de datos
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
            ClsPersona ObjPersona = new ClsPersona();//nueva instancia de clase clspersona
            ObjPersona.FechaIngreso = DateTime.Now;//asignar fecha actual
            ObjPersona.FechaModificacion = DateTime.Now;
            ((NavigationPage)this.Parent).PushAsync(new Vista.PagAdminPersona(ObjPersona, true));//mostrar la vista adminpersona con campos vacíos para ingresar nuevo
        }
        //maneja el evento item selected de la lista de objetos
        private void lstPersonas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ClsPersona ObjPersona = e.SelectedItem as ClsPersona;//asignar el objeto seleccionado a la variable obj
            ((NavigationPage)this.Parent).PushAsync(new PagAdminPersona(ObjPersona, false));//mostrar la vista adminpersona con los datos cargados para modificar o eliminar
        }
    }
}