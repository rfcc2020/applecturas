using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisLecturas.Modelo
{
    //clase que modela la tabla perfil
    public class ClsPerfil:ClsBase
    {
        public string Nombre { get; set; }
        public ClsPermiso[] ObjPermisos { get; set; }
        public override string ToString()
        {
            return Nombre;
        }
    }
}
