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
    //vista que maneja la interfaz para administrar medidores(crear, modificar, eliminar)
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagAdminMedidor : ContentPage
    {
        bool opc;
        ClsMedidor ObjMedidor;//instancia de la clase Clsmedidor
        public PagAdminMedidor(ClsMedidor ObjMedidor, bool opc)//constructor
        {
            InitializeComponent();
            this.opc = opc;//variable que define si se está agregando un nuevo o modificando un existente
            this.ObjMedidor = ObjMedidor;//asigna la variable del parámetro con la variable local.
            BindingContext = this.ObjMedidor;//indica que la vista se relacionará con los valores del objeto 
        }

        private async void ButGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtNumero.Text) &&
                        !string.IsNullOrWhiteSpace(txtCodigo.Text) &&
                        !string.IsNullOrWhiteSpace(txtidPersona.Text)//validación de no nulos
                        )
                {
                    if (ObjMedidor.Numero.Length > 0  &&
                    ObjMedidor.Codigo.Length > 0 &&
                    ObjMedidor.ObjPersona.Id > 0)//validación de no vacío
                    {
                        if (txtNumero.TextColor == Color.Green &&
                            txtCodigo.TextColor == Color.Green &&
                            txtidPersona.TextColor == Color.Green//validación de formato correcto
                            )
                        {
                            CtrlMedidor manager = new CtrlMedidor();//instancia de clase control
                            var res = "";
                            
                            if (opc)
                            {
                                var varObjeto = await manager.InsertAsync(ObjMedidor);//llmada a método que inserta un nuevo registro
                                if (varObjeto != null)
                                {
                                    ObjMedidor = varObjeto.First();//asignación de objeto local con datos recién ingresados
                                    txtId.Text = ObjMedidor.Id.ToString();//mostrar id del registro creado
                                    res = "Ok";//respuesta positiva
                                }
                                else
                                    res = null;//respuesta negativa si no se realizó correctamente
                            }                                  
                            else
                                res = await manager.UpdateAsync(ObjMedidor);//llamada al método actualizar
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
            CtrlMedidor manager = new CtrlMedidor();
            if (txtId.Text.Length != 0)//validar id no vacío
            {
                var res = await manager.DeleteAsync(ObjMedidor);//llamada a método eliminar
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