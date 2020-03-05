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
    //vista para el ingreso de lecturas de consumo de agua
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagIngresoLectura : ContentPage
    {
        bool opc;
        ClsLectura Obj;
        CtrlPersona ObjCtrlPersona;
        CtrlMedidor ObjCtrlMedidor;
        ClsPersona ObjPersona;
        public PagIngresoLectura(ClsLectura Obj, bool opc)//constructor
        {
            InitializeComponent();
            this.opc = opc;//asignación de variable local
            this.Obj = Obj;//asignación de objeto local
            Obj.Fecha = DateTime.Today;//asignación de fecha actual
            BindingContext = this.Obj;//indica que la vista se relacionará con los valores del objeto
        }
        //manejador del botón guardar
        private async void ButGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(LblNombres.Text) &&
                        PkrNumeroMedidor.SelectedItem != null &&
                        !string.IsNullOrWhiteSpace(txtConsumo.Text)//validación de no nulos
                        )
                {
                    if (txtCedula.Text.Length == 10 &&
                    Obj.Actual > 0)//validación cedula con 10 caracteres
                    {
                        if (txtCedula.TextColor == Color.Green &&
                            txtLecturaActual.TextColor == Color.Green
                            )//Validacíon formato correcto
                        {
                            CtrlLectura manager = new CtrlLectura();//instancia de clase control
                            var res = "";
                            if (opc)
                            {
                                var ObjLecturaInsert = await manager.InsertAsync(Obj);//llamada a método que inserta un nuevo registro
                                if (ObjLecturaInsert != null)
                                {
                                    Obj = ObjLecturaInsert.First();//asignación de objeto local con datos recién ingresados
                                    txtId.Text = Obj.Id.ToString();//mostrar id del registro creado
                                    res = "ok";//respuesta positiva
                                }
                                else
                                    res = null;//respuesta negativa si no se realizó correctamente 
                            }
                            else
                                res = await manager.UpdateAsync(Obj);//llamada al método actualizar
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
            catch (Exception e1)
            {
                await DisplayAlert("Mensaje", e1.Message, "ok");
            }
        }
        //controlador del boton consultar 
        private async void ButConsultaPersona_Clicked(object sender, EventArgs e)
        {
            ObjCtrlPersona = new CtrlPersona();//instancia de clase clspersona
            var ConsPersona = await ObjCtrlPersona.ConsultarCi(txtCedula.Text);//consulta persona por cédula ingresadda
            if(ConsPersona != null)//si se ha encontrado la persona
            {
                ObjPersona = ConsPersona.First();//asignar objeto local con datos consultados
                LblNombres.Text = ObjPersona.Nombres + " " + ObjPersona.Apellidos;
                ObjCtrlMedidor = new CtrlMedidor();
                var ConsMedidor = await ObjCtrlMedidor.ConsultarIdPersona(ObjPersona.Id);//consulta de medidores por id de persona
                if(ConsMedidor != null)//si existen datos
                {
                    PkrNumeroMedidor.ItemsSource = ConsMedidor.ToList();//cargar lista de objeto en selector
                }
            }
        }
        //controlador del selector de medidores
        private async void PkrNumeroMedidor_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClsMedidor ObjMedidor = PkrNumeroMedidor.SelectedItem as ClsMedidor;//asignar nueva instancia de clase medidor según el objeto seleccionado
            ObjMedidor.ObjPersona = ObjPersona;//asignar objeto persona a objeto medidor
            Obj.ObjMedidor = ObjMedidor;//asignar variable local con objeto seleccionado
            CtrlLectura ObjCtrlLectura = new CtrlLectura();//instancia de clase lectura
            var ConsLectura = await ObjCtrlLectura.ConsultarAnterior(ObjMedidor.Id);//consulta de lectura anterior
            if (ConsLectura != null)//si la lectura anterior no es nulo
                if (ConsLectura.Count() != 0)
                {
                    ClsLectura ObjLecturaAnterior = ConsLectura.First();//asignar lectura anterior a objeto lectura anterior
                    Obj.Anterior = ObjLecturaAnterior.Actual;//asignar campo anterior en objeto lectura según lectura del objeto lectura anterior
                    TxtAnterior.Text = Obj.Anterior.ToString();//mostrar lectura anterior en caja de texto
                }
                else
                {
                    Obj.Anterior = 0;//en caso que no haya lectura anterior parte de 0
                    TxtAnterior.Text = "0";
                }
        }
        //manejados de caja de texto lectura actual
        private void txtLecturaActual_TextChanged(object sender, TextChangedEventArgs e)//se activa cuando cambia el texto de la caja
        {
            float.TryParse(txtLecturaActual.Text, out float FLecturaActual);//convertir en número decimal la información ingresada en el texto
            if(FLecturaActual > Obj.Anterior)//si el valor ingresado es mayor a la lectura anterior
            {
                Obj.Calcular();//lama al método calcular de la clase clslectura
                txtConsumo.Text = Obj.Consumo.ToString();//mostrar variable consumo en caja de texto
                txtBasico.Text = Obj.Basico.ToString();//mostrar variable basico en caja de texto
                txtExceso.Text = Obj.Exceso.ToString();//mostrar variable exceso en caja de texto
                TxtTotal.Text = Obj.Total.ToString();//mostrar variable total en caja de texto
            }
        }
    }
}