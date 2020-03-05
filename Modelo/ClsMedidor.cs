using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisLecturas.Modelo
{
    //clase que modela la tabla medidor
    public class ClsMedidor:ClsBase
    {
        public string Codigo { get; set; }
        public string Numero { get; set; }
        public string Qr { get; set; }  
        public int IdPersona { get; set; }
        public ClsPersona ObjPersona { get; set; }
        public override string ToString()
        {
            return Numero.ToString();
        }
    }
}
