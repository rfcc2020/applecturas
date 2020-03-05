using AppTesisLecturas.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppTesisLecturas.Vista
{
    //clase para manejar la vista login como una vista modal
    public class LoginModalPage:CarouselPage
    {
        ContentPage Login;//variable local tipo ContenPage(página de contenido)
        public LoginModalPage(ILoginManager ObjIlm)//constructor
        {
            Login = new PagLogin(ObjIlm);//instanciación de un objeto de la clase PagLogin del espacio de nombres vista
            this.Children.Add(Login);//se añade a la pila de vistas de la aplicación la vista login
            MessagingCenter.Subscribe<ContentPage>(this, "Login", (sender) =>
              {
                  this.SelectedItem = Login;//para mostrar la vista login
              });
        }
    }
}
