using CodeChallenge.Data;
using CodeChallenge.Data.Model;

using NUnit.Framework;

using System.Collections.Generic;
using System.Linq;

namespace CodeChallengeTest
{
    [TestFixture]
    public class Tests
    {
        private List<Animal> _animales;

        [SetUp]
        public void Setup()
        {
            _animales = new List<Animal>();
        }

        [Test]
        public void CalcularAlimentoSinAnimales()
        {
            var result = _animales.Sum(a => ZoologicoServicio.CalcularAlimentoCarnivoro(a) +
                                            ZoologicoServicio.CalcularAlimentoHerviboro(a) +
                                            ZoologicoServicio.CalcularAlimentoReptilCarne(a) +
                                            ZoologicoServicio.CalcularAlimentoReptilHierbas(a));
            Assert.AreEqual(result, 0);
        }

        [Test]
        public void CalcularAlimentoSoloCarnivoros()
        {
            _animales.AddRange(MockFactoryCarnivoros());
            var result = _animales.Sum(a => ZoologicoServicio.CalcularAlimentoCarnivoro(a));
            Assert.AreEqual(result, 22.5);
        }

        [Test]
        public void CalcularAlimentoMensualSoloCarnivoros()
        {
            _animales.AddRange(MockFactoryCarnivoros());
            var result = _animales.Sum(a => ZoologicoServicio.CalcularAlimentoMensualCarne(a));
            Assert.AreEqual(result, 675);
        }

        [Test]
        public void CalcularAlimentoReptilHierbas()
        {
            _animales.AddRange(MockFactoryReptiles());
            var result = _animales.Sum(a => ZoologicoServicio.CalcularAlimentoReptilCarne(a) +
                                            ZoologicoServicio.CalcularAlimentoReptilHierbas(a));
            Assert.AreEqual(result, 99.5d);
        }

        [Test]
        public void CalcularAlimentoSoloHerviboros()
        {
            _animales.AddRange(MockFactoryHerivboros());
            var result = _animales.Sum(a => ZoologicoServicio.CalcularAlimentoHerviboro(a));
            Assert.AreEqual(result, 185);
        }

        [Test]
        public void CalcularAlimentoMensualSoloHerviboros()
        {
            _animales.AddRange(MockFactoryHerivboros());
            var result = _animales.Sum(a => ZoologicoServicio.CalcularAlimentoMensualHierbas(a));
            Assert.AreEqual(result, 5550);
        }

        [Test]
        public void CalcularAlimentoTodos()
        {
            _animales.AddRange(MockFactoryTodos());
            var result = _animales.Sum(a => ZoologicoServicio.CalcularAlimentoCarnivoro(a) +
                                            ZoologicoServicio.CalcularAlimentoHerviboro(a) +
                                            ZoologicoServicio.CalcularAlimentoReptilCarne(a) +
                                            ZoologicoServicio.CalcularAlimentoReptilHierbas(a));

            Assert.AreEqual(result,307);
        }

        #region Mock Factory
        private List<Animal> MockFactoryCarnivoros()
        {
            return new List<Animal>() {
                new Animal{
                    Tipo = TipoAnimales.Carnivoro,
                    Peso = 100,
                    PorcentajeCarne = 0.05
                },
                new Animal{
                    Tipo = TipoAnimales.Carnivoro,
                    Peso = 80,
                    PorcentajeCarne = 0.1
                },
                new Animal{
                    Tipo = TipoAnimales.Carnivoro,
                    Peso = 95,
                    PorcentajeCarne = 0.1
                }
            };
        }

        private List<Animal> MockFactoryReptiles()
        {
            return new List<Animal>() {
                new Animal{
                    Tipo = TipoAnimales.Reptil,
                    Peso = 100,
                    PorcentajeCarne = 0.05,
                    PorcentajeHierbas = 0.07,
                },
                new Animal{
                    Tipo = TipoAnimales.Reptil,
                    Peso = 80,
                    PorcentajeCarne = 0.1,
                    PorcentajeHierbas = 0.4,
                },
                new Animal{
                    Tipo = TipoAnimales.Reptil,
                    Peso = 95,
                    PorcentajeCarne = 0.2,
                    PorcentajeHierbas = 0.3
                }
            };
        }

        private List<Animal> MockFactoryHerivboros()
        {
            return new List<Animal>() {
                new Animal{
                    Tipo = TipoAnimales.Herviboro,
                    Peso = 30,
                    Kilos = 10
                },
                new Animal{
                    Tipo = TipoAnimales.Herviboro,
                    Peso = 50,
                    Kilos = 15
                }
            };
        }

        private List<Animal> MockFactoryTodos()
        {
            return new List<Animal>() {
                /*carniboros*/
                new Animal{
                    Tipo = TipoAnimales.Carnivoro,
                    Peso = 100,
                    PorcentajeCarne = 0.05
                },
                new Animal{
                    Tipo = TipoAnimales.Carnivoro,
                    Peso = 80,
                    PorcentajeCarne = 0.1
                },
                new Animal{
                    Tipo = TipoAnimales.Carnivoro,
                    Peso = 95,
                    PorcentajeCarne = 0.1
                },
                /*herviboros*/
                new Animal{
                    Tipo = TipoAnimales.Herviboro,
                    Peso = 30,
                    Kilos = 10
                },
                new Animal{
                    Tipo = TipoAnimales.Herviboro,
                    Peso = 50,
                    Kilos = 15
                },
                /*reptiles*/
                new Animal{
                    Tipo = TipoAnimales.Reptil,
                    Peso = 100,
                    PorcentajeCarne = 0.05,
                    PorcentajeHierbas = 0.07,
                },
                new Animal{
                    Tipo = TipoAnimales.Reptil,
                    Peso = 80,
                    PorcentajeCarne = 0.1,
                    PorcentajeHierbas = 0.4,
                },
                new Animal{
                    Tipo = TipoAnimales.Reptil,
                    Peso = 95,
                    PorcentajeCarne = 0.2,
                    PorcentajeHierbas = 0.3
                }
            };
        }
        #endregion
    }
}