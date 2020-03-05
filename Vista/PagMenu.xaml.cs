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
    //clase que maneja la vista menú
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagMenu : ContentPage
    {
        private ClsUsuario ObjMiUsuario;//variable local usuario autenticado
        public PagMenu()
        {
            InitializeComponent();
            this.ObjMiUsuario = App.Current.Properties["ObjUsuario"] as ClsUsuario;//recuperar objeto guardado en propieades de la aplicación
            TxtUsuario.Text = ObjMiUsuario.ObjPersona.Nombres;//mostrar en caja de texto el nombre de la persona
            TxtPerfil.Text = ObjMiUsuario.ObjPerfil.Nombre;//mostrar el nombre del perfil en una caja de texto
        }
        //controlador del botón cerrar evento clic
        private void ButCerrarSesion_Clicked(object sender, EventArgs e)
        {
            App.Current.Logout();
        }
        //controlador del botón administrar persona evento clic
        private void ButAdminPersona_Clicked(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new PagListaPersona());//mostrar vista listapersona
        }
        //controlador del botón administrar usuario evento clic
        private void ButAdminUsuario_Clicked(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new PagListaUsuario());//mostrar vista listausuario
        }
        //controlador del botón administrar perfil evento clic
        private void ButAdminPerfil_Clicked(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new PagListaPerfil());//mostrar vista listaperfil
        }
        //controlador del botón administrar permiso evento clic
        private void ButAdminPermiso_Clicked(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new PagListaPermiso());//mostrar vista listapermiso
        }
        //controlador del botón administrar medidor evento clic
        private void ButAdminMedidor_Clicked(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new PagListaMedidor());//mostrar vista listamedidor
        }
        //controlador del botón ingresar lectura evento clic
        private void ButIngresarLectura_Clicked(object sender, EventArgs e)
        {
            ClsLectura Obj = new ClsLectura();//nueva instancia clase clslectura
            Obj.FechaIngreso = DateTime.Now;//asigna fecha actual
            Obj.FechaModificacion = DateTime.Now;
            ((NavigationPage)this.Parent).PushAsync(new PagIngresoLectura(Obj, true));//mostrar vista ingresolectura
        }
    }
}