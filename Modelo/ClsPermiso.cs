using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisLecturas.Modelo
{
    //clase que modela la tabla permiso
    public class ClsPermiso:ClsBase
    {
        public string Nombre { get; set; }
        public bool Asignado { get; set; }
    }
}
