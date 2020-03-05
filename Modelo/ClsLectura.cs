using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisLecturas.Modelo
{
    //clase que modela la tabla lectura
    public class ClsLectura:ClsBase
    {
        private float CantidadConsumo = 5;
        private float ValorConsumo = 4;
        private float VaLorExceso = 5;
        public DateTime Fecha { get; set; }
        public float Anterior { get; set; }
        public float Actual { get; set; }
        public float Consumo { get; set; }
        public float Basico { get; set; }
        public float Exceso { get; set; }
        public float Total { get; set; }
        public string Observacion { get; set; }
        public ClsMedidor ObjMedidor { get; set; }
        public ClsDetalleLectura[] ObjDetalle { get; set; }
        public void Calcular()//método para calcular consumo, exceso y valores según la lectura anterior y la lectura actual
        {
            Consumo = Actual - Anterior;
            Basico = ValorConsumo;
            if (Consumo > CantidadConsumo)
            {                
                Exceso = (Consumo - CantidadConsumo) * VaLorExceso;
            }
            else
            {
                Exceso = 0; 
            }
            Total = Basico + Exceso;
        }
    }
}
