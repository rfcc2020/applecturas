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
    //vista que maneja la interfaz para administrar usuario(crear, modificar, eliminar)
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagAdminUsuario : ContentPage
    {
        bool opc;
        ClsUsuario ObjUsuario;//instancia de la clase ClsUsuario
        public PagAdminUsuario(ClsUsuario ObjUsuario, bool opc)
        {
            InitializeComponent();
            ConsultaPerfil();//llama al método que consulta los perfiles y los carga en el selector
            this.opc = opc;//variable que define si se está agregando un nuevo o modificando un existente
            this.ObjUsuario = ObjUsuario;//asigna la variable del parámetro con la variable local.
            BindingContext = this.ObjUsuario;//indica que la vista se relacionará con los valores del objeto
        }

        private async void ButGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtPassword.Text) &&
                        !string.IsNullOrWhiteSpace(txtConfirmaPassword.Text) &&
                        (PkrListaPerfil.SelectedItem) != null//validación de no nulos
                        )
                {
                    if (ObjUsuario.ObjPersona.Cedula.Length == 10 &&
                    ObjUsuario.ObjPersona.Nombres.Length > 0 &&
                    ObjUsuario.ObjPersona.Apellidos.Length > 0)//validación de no vacío
                    {
                        if (txtPassword.TextColor == Color.Green &&
                            txtConfirmaPassword.TextColor == Color.Green//Validacío formato correcto
                            )
                        {
                            CtrlUsuario manager = new CtrlUsuario();//instancia de clase control
                            var res = "";
                            if (opc)
                            {
                                var ObjObjeto = await manager.InsertAsync(ObjUsuario);//llamada a método que inserta un nuevo registro
                                if (ObjObjeto != null)
                                {
                                    ObjUsuario = ObjObjeto.First();//asignación de objeto local con datos recién ingresados
                                    txtId.Text = ObjUsuario.Id.ToString();//mostrar id del registro creado
                                    res = "Ok";//respuesta positiva
                                }
                                else
                                    res = null;//respuesta negativa si no se realizó correctamente 
                            }
                            else
                                res = await manager.UpdateAsync(ObjUsuario);//llamada al método actualizar
                            if (res != null)
                            {
                                await DisplayAlert("Mensaje", "Datos Guardados Correctamente", "ok");
                            }
                            else
                                await DisplayAlert("Mensaje", "No se guardó la información, vuelva a intentar más tarde", "ok");
                        }
                        else
                            await DisplayAlert("Mensaje", "Los campos de color rojo tienen formato incorrecto", "ok");
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
            CtrlUsuario manager = new CtrlUsuario();
            if (txtId.Text.Length != 0)//validar id no vacío
            {
                var res = await manager.DeleteAsync(ObjUsuario);//llamada a método eliminar
                if (res != null)
                {
                    await DisplayAlert("Mensaje", "Datos Eliminados Correctamente", "ok");
                }
                else
                    await DisplayAlert("Mensaje", "No se eliminó la información, vuelva a intentar más tarde", "ok");
            }
        }
        //método para cargar los perfiles en el selector
        private async void ConsultaPerfil()
        {
            try
            {
                CtrlPerfil manager = new CtrlPerfil();
                var res = await manager.Consultar();
                if (res != null)
                {
                    PkrListaPerfil.ItemsSource = res.ToList();
                }
            }
            catch (Exception e1)
            {
                await DisplayAlert("Mensaje", e1.Message, "ok");
            }
        }
    }
}