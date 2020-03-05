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
    //vista que maneja la interfaz para administrar permisos(crear, modificar, eliminar)
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagAdminPermiso : ContentPage
    {
        bool opc;
        ClsPermiso ObjPermiso;//instancia de la clase ClsPermiso
        public PagAdminPermiso(ClsPermiso ObjPermiso, bool opc)
        {
            InitializeComponent();
            this.opc = opc;//variable que define si se está agregando un nuevo o modificando un existente
            this.ObjPermiso = ObjPermiso;//asigna la variable del parámetro con la variable local.
            BindingContext = this.ObjPermiso;//indica que la vista se relacionará con los valores del objeto 
        }

        private async void ButGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtNombre.Text))//validación de no nulos
                {
                    if (ObjPermiso.Nombre.Length > 0)//validación de no vacío
                    {
                       
                            CtrlPermiso manager = new CtrlPermiso();//instancia de clase control
                        var res = "";
                            if (opc)
                        {
                            var Objeto = await manager.InsertAsync(ObjPermiso);//llamada a método que inserta un nuevo registro
                            if (Objeto != null)
                            {
                                ObjPermiso = Objeto.First();//asignación de objeto local con datos recién ingresados
                                txtId.Text = ObjPermiso.Id.ToString();//mostrar id del registro creado
                                res = "Ok";//respuesta positiva
                            }
                            else
                                res = null;//respuesta negativa si no se realizó correctamente
                        }
                            else
                                res = await manager.UpdateAsync(ObjPermiso);//llamada al método actualizar
                        if (res != null)
                            {
                                await DisplayAlert("Mensaje", "Datos Guardados Correctamente", "ok");
                            }
                            else
                                await DisplayAlert("Mensaje", "No se guardó la información, vuelva a intentar más tarde", "ok");
                        }
                    else
                    {
                        await DisplayAlert("Mensaje", "Faltan Datos Necesarios", "ok");
                    }
                }
                else
                    await DisplayAlert("Mensaje", "Faltan Datos", "ok");
            }
            catch (Exception e1)//control de errores
            {
                await DisplayAlert("Mensaje", e1.Message, "ok");
            }
        }
        //método para eliminar un registro
        private async void ButEliminar_Clicked(object sender, EventArgs e)
        {
            CtrlPermiso manager = new CtrlPermiso();
            if (txtId.Text.Length != 0)//validar id no vacío
            {
                var res = await manager.DeleteAsync(ObjPermiso);//llamada a método eliminar
                if (res != null)
                {
                    await DisplayAlert("Mensaje", "Datos Eliminados Correctamente", "ok");
                }
                else
                    await DisplayAlert("Mensaje", "No se eliminó la información, vuelva a intentar más tarde", "ok");
            }
        }
    }
}