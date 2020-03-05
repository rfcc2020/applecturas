using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisLecturas.Modelo
{
    //clase que modela la tabla usuario
    public class ClsUsuario:ClsBase
    {
        public string Password { get; set; }
        public int IdPersona { get; set; }
        public int IdPerfil { get; set; }
        public ClsPersona ObjPersona { get; set; }
        public ClsPerfil ObjPerfil { get; set; }
    }
}
