using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisLecturas.Modelo
{
    //clase base de las que heredarán las demás clases del espacio de nombres modelo
    public abstract class ClsBase
    {
        public int Id { get; set; }//propiedad
        public DateTime FechaIngreso { get; set; }//propiedad
        public DateTime FechaModificacion { get; set; }//propiedad
        public string Estado { get; set; }//propiedad
    }
}
