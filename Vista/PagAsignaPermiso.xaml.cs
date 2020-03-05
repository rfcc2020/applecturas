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
    //vista para asignar o quitar permiso a un perfil de usuario
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagAsignaPermiso : ContentPage
    {
        private ClsPerfil ObjPerfil;//instancia de la clase ClsPerfil
        public PagAsignaPermiso(ClsPerfil ObjPerfil)
        {
            InitializeComponent();
            this.ObjPerfil = ObjPerfil;//asigna la variable del parámetro con la variable local.
            TxtPerfil.Text = ObjPerfil.Nombre;//mostrar el nombre del perfil en la caja de texto
            ListarPermisos();//llama al método listar permisos que carga los permisos creados para ser o no seleccionados
        }
        //método queconsulta los permisos y los muestra para ser seleccionados
        private async void ListarPermisos()
        {
            try
            {
                CtrlPermiso manager = new CtrlPermiso();
                var res = await manager.Consultar();
                if (res != null)//validación de no nulos
                {
                    lstPermisos.ItemsSource = res;
                }
            }
            catch (Exception e1)
            {
                await DisplayAlert("Mensaje", e1.Message, "ok");
            }
        }
        //manejador del botón guardar
        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            CtrlPerfil CtrlPerfil = new CtrlPerfil();//instancia de clase CtrlPerfil
            try
            {
                foreach (ClsPermiso item in lstPermisos.ItemsSource)//recorrer todos los objetos del listado de permisos
                {
                    if (item.Asignado)//si el permiso está seleccionado
                        await CtrlPerfil.AsignaPermisoAsync(ObjPerfil, item);//se crea un nuevo registro
                    else
                        await CtrlPerfil.QuitaPermisoAsync(ObjPerfil, item);//se elimina el registro si existe
                }
                await DisplayAlert("Mensaje", "Datos Guardados Correctamente", "ok");
            }
            catch (Exception x){ await DisplayAlert("Mensaje", x.Message, "ok"); }//control de errores
        }
    }
}