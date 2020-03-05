using AppTesisLecturas.Controlador;
using AppTesisLecturas.Interfaces;
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
    //clase que maneja la vista login
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagLogin : ContentPage
    {
        ILoginManager Ilm = null;//instancia de interfaz 
        public PagLogin(ILoginManager Ilm)//constructor recibiendo como parámetro objeto de clase interfaz Iloginmanager
        {
            InitializeComponent();
            this.Ilm = Ilm;//asignar variable local
        }
        public PagLogin()//constructor
        {
            InitializeComponent();
        }
        //manejador evento clic del botón entrar
        private async void ButEntrar_Clicked(object sender, EventArgs e)
        {
            CtrlUsuario ObjCtrlUsuario = new CtrlUsuario();//instancia de controlador
            try
            {
                if (!string.IsNullOrWhiteSpace(TxtEmail.Text))//validar email no nulo
                    if (!string.IsNullOrWhiteSpace(TxtPassword.Text))//validar password no nulo
                        if (TxtEmail.TextColor == Color.Green)//validar email con formato correcto
                            if (TxtPassword.TextColor == Color.Green)//validar password con formato correcto
                            {
                                var ConsUsr = await ObjCtrlUsuario.LoginUsr(TxtEmail.Text, TxtPassword.Text);//invoca al método login del controlador usuario
                                if (ConsUsr.Count() == 1)//si existe un registro que coincide con el email y el password
                                {
                                    ClsUsuario ObjUsuario = ConsUsr.First();//asignar objeto encontrado al objeto local
                                    CtrlPersona ObjCtrlPersona = new CtrlPersona();//instancia de control persona
                                    var ConsPersona = await ObjCtrlPersona.ConsultarId(ObjUsuario.IdPersona);//consulta de persona por id
                                    if (ConsPersona.Count() == 1)//si se encontraron los datos de la persona
                                    {
                                        ObjUsuario.ObjPersona = ConsPersona.First();//asignar objeto encontrado a variable persona de objeto usuario
                                        CtrlPerfil ObjCtrlPerfil = new CtrlPerfil();//instancia de control perfil
                                        var ConsPerfil = await ObjCtrlPerfil.Consultar(ObjUsuario.IdPerfil);//consulta de perfil según id
                                        if (ConsPerfil.Count() == 1)//si los datos del perfil han sido encontrados
                                        {
                                            await DisplayAlert("Mensaje", "Bienvenido", "ok");//mensaje de  bienvenida
                                            ObjUsuario.ObjPerfil = ConsPerfil.First();//asignar objeto encontrado a campo de objeto usuario
                                            App.Current.Properties["name"] = ObjUsuario.ObjPersona.Nombres;//guardar en propiedades de la aplicación el nombre del usuario
                                            App.Current.Properties["IsLoggedIn"] = true;//guardar en propiedades de la aplicación el estado como verdadero
                                            App.Current.Properties["ObjUsuario"] = ObjUsuario;//guardar el objeto usuario en propiedades de la aplicación
                                            Ilm.ShowMainPage();
                                        }
                                    }
                                }
                                else
                                    await DisplayAlert("Mensaje", "Datos no encontrados, vuelva a intentar", "ok");
                            }
                            else
                                await DisplayAlert("Mensaje", "Password con formato incorrecto", "ok");
                        else
                            await DisplayAlert("Mensaje", "Email con formato incorrecto", "ok");
                    else
                        await DisplayAlert("Mensaje", "Debe ingresar el password", "ok");
                else
                    await DisplayAlert("Mensaje", "Debe ingresar el email", "ok");
            }//control de errores
            catch (Exception x) { await DisplayAlert("Mensaje", x.Message, "ok"); }
        }
        //controlador botón cancelar evento clic, para borrar los datos ingresados en el formulario
        private void ButCancelar_Clicked(object sender, EventArgs e)
        {
            TxtEmail.Text = "";
            TxtPassword.Text = "";
        }
    }
}