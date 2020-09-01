using CodeChallenge.Data.Model;

using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeChallenge.Data
{
    public class ZoologicoServicio
    {
        public const int DIAS_MES_ESTANDAR = 30;

        public List<Animal> _animales;

        public ZoologicoServicio()
        {
            _animales = new List<Animal>();
        }

        public List<string> TiposAnimales =>
            new List<string>()
            {
                TipoAnimales.Carnivoro,
                TipoAnimales.Herviboro,
                TipoAnimales.Reptil
            };

        public List<KeyValuePair<string, string>> AgregarAnimal(Animal animal)
        {
            var resultadoValidacion = ValidarIngresoAnimal(animal);
            if (!resultadoValidacion.Any())
            {
                animal.Ingreso = DateTime.Now;
                _animales.Add(animal);
            }

            return resultadoValidacion;
        }

        public List<KeyValuePair<string, string>> ValidarIngresoAnimal(Animal animal)
        {
            var errores = new List<KeyValuePair<string, string>>();

            if (string.IsNullOrEmpty(animal.Tipo))
                errores.Add(new KeyValuePair<string, string>("Tipo", "Debe elegir un Tipo"));

            if (string.IsNullOrEmpty(animal.Especie))
                errores.Add(new KeyValuePair<string, string>("Especie", "Debe ingresar una Especie"));

            if (animal.Edad <= 0)
                errores.Add(new KeyValuePair<string, string>("Edad", "Debe ingresar una Edad"));

            if (string.IsNullOrEmpty(animal.LugarOrigen))
                errores.Add(new KeyValuePair<string, string>("LugarOrigen", "Debe ingresar un Lugar de Origen"));

            if (animal.Peso <= 0)
                errores.Add(new KeyValuePair<string, string>("Peso", "Debe ingresar un Peso"));

            if (animal.Tipo == TipoAnimales.Reptil && animal.CambioPiel <= 0)
                errores.Add(new KeyValuePair<string, string>("CambioPiel", "Debe indicar cada cuantos dias sucede el cambio de piel"));

            if ((animal.Tipo == TipoAnimales.Reptil || animal.Tipo == TipoAnimales.Herviboro) && animal.PorcentajeHierbas <= 0)
                errores.Add(new KeyValuePair<string, string>("PorcentajeHierbas", "Debe indicar el porcentaje del peso en Hierbas"));

            if ((animal.Tipo == TipoAnimales.Reptil || animal.Tipo == TipoAnimales.Carnivoro) && animal.PorcentajeCarne <= 0)
                errores.Add(new KeyValuePair<string, string>("PorcentajeCarne", "Debe indicar el porcentaje del peso en Carne"));

            if (animal.Tipo == TipoAnimales.Herviboro && animal.Kilos <= 0)
                errores.Add(new KeyValuePair<string, string>("Kilos", "Debe indicar los kilos de alimento en Hierbas"));

            return errores;
        }

        public double AdvertirExcesoAlimento(Animal animal)
        {
            var resultado = CalcularAlimentoMensualCarne(animal) + CalcularAlimentoMensualHierbas(animal);
            if (resultado > 1500)
                return resultado;

            return 0;
        }

        public static double CalcularAlimentoMensualCarne(Animal animal)
        {
            var total = 0D;
            if (animal.Tipo == TipoAnimales.Carnivoro)
            {
                total += CalcularAlimentoCarnivoro(animal) * DIAS_MES_ESTANDAR;
            }
            else if (animal.Tipo == TipoAnimales.Reptil)
            {
                var diasReales = DIAS_MES_ESTANDAR - CantidadDiasAlimentoReptil(animal);

                total += CalcularAlimentoReptilCarne(animal) * diasReales;
            }
            return total;
        }

        public static double CalcularAlimentoMensualHierbas(Animal animal)
        {
            var total = 0D;
            if (animal.Tipo == TipoAnimales.Herviboro)
            {
                total += CalcularAlimentoHerviboro(animal) * DIAS_MES_ESTANDAR;
            }
            else if (animal.Tipo == TipoAnimales.Reptil)
            {
                var diasReales = DIAS_MES_ESTANDAR - CantidadDiasAlimentoReptil(animal);

                total += CalcularAlimentoReptilHierbas(animal) * diasReales;
            }
            return total;
        }

        public static double CalcularAlimentoCarnivoro(Animal animal)
        {
            return animal.Tipo == TipoAnimales.Carnivoro ? animal.Peso * animal.PorcentajeCarne : 0;
        }

        public static double CalcularAlimentoHerviboro(Animal animal)
        {
            return animal.Tipo == TipoAnimales.Herviboro ? (2 * animal.Peso) + animal.Kilos : 0;
        }

        public static double CalcularAlimentoReptilCarne(Animal animal)
        {
            return animal.Tipo == TipoAnimales.Reptil ? (animal.Peso * animal.PorcentajeCarne) : 0;
        }

        public static double CalcularAlimentoReptilHierbas(Animal animal)
        {
            return animal.Tipo == TipoAnimales.Reptil ? (animal.Peso * animal.PorcentajeHierbas) : 0;
        }

        private static int CantidadDiasAlimentoReptil(Animal animal)
        {
            const int PERIODO_DIAS_SIN_ALIMENTARSE = 3;

            double cantidadCambiosPielMensuales = DIAS_MES_ESTANDAR / animal.CambioPiel;

            return Convert.ToInt32(Math.Ceiling(cantidadCambiosPielMensuales * PERIODO_DIAS_SIN_ALIMENTARSE));
        }
    }

    public class TipoAnimales
    {
        public static string Carnivoro => "Carnívoro";
        public static string Herviboro => "Herbívoro";
        public static string Reptil => "Reptil";
    }
}
