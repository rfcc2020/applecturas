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
    //vista que maneja la interfaz para administrar persona(crear, modificar, eliminar)
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagAdminPersona : ContentPage
    {
        bool opc;
        ClsPersona ObjPersona;//instancia de la clase ClsPersona
        public PagAdminPersona(ClsPersona ObjPersona, bool opc)
        {
            InitializeComponent();
            this.opc = opc;//variable que define si se está agregando un nuevo o modificando un existente
            this.ObjPersona = ObjPersona;//asigna la variable del parámetro con la variable local.
            BindingContext = this.ObjPersona;//indica que la vista se relacionará con los valores del objeto
        }

        private async void ButGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtCedula.Text) &&
                        !string.IsNullOrWhiteSpace(txtNombres.Text) &&
                        !string.IsNullOrWhiteSpace(txtApellidos.Text)//validación de no nulos
                        )
                {
                    if (ObjPersona.Cedula.Length == 10 &&
                    ObjPersona.Nombres.Length > 0 &&
                    ObjPersona.Apellidos.Length > 0)//validación de no vacío
                    {
                        if (txtCedula.TextColor == Color.Green &&
                            txtNombres.TextColor == Color.Green &&
                            txtApellidos.TextColor == Color.Green//Validacío formato correcto
                            )
                        {
                            CtrlPersona manager = new CtrlPersona();//instancia de clase control
                            var resp = "";
                            if (opc)
                            {
                                var Objeto = await manager.InsertAsync(ObjPersona);//llamada a método que inserta un nuevo registro
                                if (Objeto != null)
                                {
                                    this.ObjPersona = Objeto.First();//asignación de objeto local con datos recién ingresados
                                    txtId.Text = this.ObjPersona.Id.ToString();//mostrar id del registro creado
                                    resp = "Ok";//respuesta positiva
                                }
                                else resp = null;//respuesta negativa si no se realizó correctamente                                
                            }                                
                            else
                                resp = await manager.UpdateAsync(ObjPersona);//llamada al método actualizar
                            if (resp != null)
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
            CtrlPersona manager = new CtrlPersona();
            if (txtId.Text.Length != 0)//validar id no vacío
            {
                var res = await manager.DeleteAsync(ObjPersona);//llamada a método eliminar
                if (res != null)
                {
                    await DisplayAlert("Mensaje", "Datos Eliminados Correctamente", "ok");
                }
                else
                    await DisplayAlert("Mensaje", "No se eliminó la información, vuelva a intentar más tarde", "ok");
            }
        }
        //manejador del botón asignar usuario
        private void ButAsigUsuario_Clicked(object sender, EventArgs e)
        {
            ClsUsuario ObjUsuario = new ClsUsuario();
            ClsPerfil ObjPerfil = new ClsPerfil();
            ObjUsuario.ObjPersona = ObjPersona;
            ObjUsuario.ObjPerfil = ObjPerfil;
            ObjUsuario.FechaIngreso = DateTime.Now;
            ObjUsuario.FechaModificacion = DateTime.Now;
            ((NavigationPage)this.Parent).PushAsync(new Vista.PagAdminUsuario(ObjUsuario, true));
        }
        //manejador del botón asignar medidor
        private void ButAsignaMedidor_Clicked(object sender, EventArgs e)
        {
            ClsMedidor Obj = new ClsMedidor
            {
                ObjPersona = ObjPersona,
                FechaIngreso = DateTime.Now,
                FechaModificacion = DateTime.Now
            };
            ((NavigationPage)this.Parent).PushAsync(new Vista.PagAdminMedidor(Obj, true));
        }
    }
}