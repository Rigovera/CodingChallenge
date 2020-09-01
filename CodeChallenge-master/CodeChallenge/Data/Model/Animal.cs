using System;

namespace CodeChallenge.Data.Model
{
    public class Animal
    {
        public DateTime Ingreso { get; set; }
        public string Tipo { get; set; }
        public string Especie { get; set; }
        public int Edad { get; set; }
        public string LugarOrigen { get; set; }
        public double Peso { get; set; }
        public double PorcentajeHierbas { get; set; }
        public double PorcentajeCarne { get; set; }
        public double Kilos { get; set; }
        public int CambioPiel { get; set; }
        public double AlimentoMensualCarne => ZoologicoServicio.CalcularAlimentoMensualCarne(this);
        public double AlimentoMensualHierbas => ZoologicoServicio.CalcularAlimentoMensualHierbas(this);
    }
}