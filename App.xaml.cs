using AppTesisLecturas.Interfaces;
using AppTesisLecturas.Vista;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTesisLecturas
{
    //clase que representa la aplicación

    public partial class App : Application,ILoginManager
    {
        static ILoginManager ObjLoginManager;//objeto de la interfaz Iloginmanager
        public static App Current;//variable local
        public static int Valor;//variable pública
        public App()//constructor
        {
            InitializeComponent();//inicializa elemento visuales
            Current = this;//asigna aplicación a la variable
            var isLoggedIn = Properties.ContainsKey("IsLoggedIn") ? (bool)Properties["IsLoggedIn"] : false;//compara si hay usuario autenticado
            if (isLoggedIn)
                MainPage = new NavigationPage(new PagMenu());//si el usuario ya se ha autenticado muestra el menú principal
            else
                MainPage = new LoginModalPage(this);//si el usuario aún no se ha autenticado muestra la interfaz login
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        //método que muestra la página principal
        public void ShowMainPage()
        {
            MainPage = new NavigationPage(new PagMenu());
        }
        //método que desautentica a un usaurio y luego muestra la interfaz login
        public void Logout()
        {
            Properties["IsLoggedIn"] = false;
            MainPage = new LoginModalPage(this);
        }
        //método para cerrar la aplicación
        public void Salir()
        {
            this.Quit();
        }
    }
}
