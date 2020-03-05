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
    //vista para consultar medidores
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagListaMedidor : ContentPage
    {
        public PagListaMedidor()//constructor
        {
            InitializeComponent();
        }
        //controlador del botón listar
        private async void BtnListar_Clicked(object sender, EventArgs e)
        {
            try
            {
                CtrlMedidor manager = new CtrlMedidor();//instanciar controlador
                var res = await manager.Consultar(TxtNumero.Text);//llamada al metodo consultar pasando como parámetro el número ingresado en la caja de texto
                if (res != null)//si la respuesta es no nulo
                {
                    lstMedidores.ItemsSource = res;//cargar en el lista los objetos consultados en la base de datos
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
            ClsMedidor Obj = new ClsMedidor();//nueva instancia de clase clsmedidor
            Obj.FechaIngreso = DateTime.Now;//asigna fecha actual
            Obj.FechaModificacion = DateTime.Now;
            ((NavigationPage)this.Parent).PushAsync(new Vista.PagAdminMedidor(Obj, true));//mostrar la vista adminmedidor con campos vacíos para ingresar nuevo
        }
        //maneja el evento item selected de la lista de objetos
        private async void lstMedidores_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ClsMedidor Obj = e.SelectedItem as ClsMedidor;//asignar el objeto seleccionado a la variable obj
            CtrlPersona ObjCtrl = new CtrlPersona();//nueva instancia del controlador
            var ConsCtrl = await ObjCtrl.ConsultarId(Obj.IdPersona);//consulta la persona por id
            Obj.ObjPersona = ConsCtrl.First();//asigna persona al objeto medidor
            await ((NavigationPage)this.Parent).PushAsync(new PagAdminMedidor(Obj, false));//mostrar la vista adminmedidor con los datos cargados para modificar o eliminar
        }
    }
}