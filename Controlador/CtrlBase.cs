using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppTesisLecturas.Controlador
{
    //clase base de la que herdarán el resto de clases del espacio de nombres Controlador
    public class CtrlBase
    {
        public string Servidor { get; set; }//Propiedad para manejar la ip del servidor
        public CtrlBase()//constructor de la clase
        {
            if (Device.RuntimePlatform == Device.Android)//condicional en caso de plataforma android
                Servidor = "10.0.2.2";//ip servidor local android
            else
                Servidor = "localhost";//ip servidor local otras plataformas
        }
    }
}
