using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisLecturas.Modelo
{
    //clase que modela la tabla persona
    public class ClsPersona:ClsBase
    {
        public string Cedula { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}
