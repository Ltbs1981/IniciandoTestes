using Bogus;
using IniciandoTestes.Contratos.Concurso;
using IniciandoTestes.Entidades;
using IniciandoTestes.Servicos.ConcursoService;
using IniciandoTestes.Tests.MotherObjects;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace IniciandoTestes.Tests
{
    public class CandidaturaServiceTestes
    {
        private readonly Faker _faker;
        private readonly Mock<ICandidaturaRepository> _mockCandidaturaRepository;
        private readonly CandidaturaService _candidaturaService;

        public CandidaturaServiceTestes()
        {
            _faker = new Faker();
            _mockCandidaturaRepository = new Mock<ICandidaturaRepository>();
            _candidaturaService = new CandidaturaService(_mockCandidaturaRepository.Object);
        }

        [Theory]
        [MemberData(nameof(GetCandidatosValidos))]
        public void CriarCandidatura_DeveRetornarMatricula_QuandoCandidatoValido(Candidato candidato, Concurso concurso)
        {
            // Arrange
            _mockCandidaturaRepository.Setup(r => r.GetConcurso(candidato.Concurso.Id)).Returns(concurso);
            _mockCandidaturaRepository.Setup(r => r.AdicionaCandidato(candidato)).Returns(123);

            // Act
            var resultado = _candidaturaService.CriarCandidatura(candidato);

            // Assert
            Assert.Equal(123, resultado);
        }

        public static IEnumerable<object[]> GetCandidatosValidos()
        {
            yield return new object[]
            {
                CandidaturaMotherObject.GetCandidatoValidoPorEscolaridade(Escolaridade.Superior),
                CandidaturaMotherObject.GetConcursoPorEscolaridade(Escolaridade.Superior)
            };
            yield return new object[]
            {
                CandidaturaMotherObject.GetCandidatoValidoPorEscolaridade(Escolaridade.Medio),
                CandidaturaMotherObject.GetConcursoPorEscolaridade(Escolaridade.Medio)
            };
        }

        [Theory]
        [MemberData(nameof(GetCandidatosInvalidos))]
        public void CriarCandidatura_DeveLancarExcecao_QuandoCandidatoInvalido(Candidato candidato, Type excecaoEsperada)
        {
            // Arrange
            if (candidato != null && candidato.Escolaridade != Escolaridade.Superior)
            {
                var concurso = CandidaturaMotherObject.GetConcursoPorEscolaridade(Escolaridade.Superior);
                _mockCandidaturaRepository.Setup(r => r.GetConcurso(candidato.Concurso.Id)).Returns(concurso);
            }

            // Act & Assert
            Assert.Throws(excecaoEsperada, () => _candidaturaService.CriarCandidatura(candidato));
        }

        public static IEnumerable<object[]> GetCandidatosInvalidos()
        {
            yield return new object[] { null, typeof(ArgumentException) };
            yield return new object[] { CandidaturaMotherObject.GetCandidatoInvalido(), typeof(ArgumentException) };
            yield return new object[]
            {
                CandidaturaMotherObject.GetCandidatoValidoPorEscolaridade(Escolaridade.Medio),
                typeof(Exception)
            };
            yield return new object[]
            {
                CandidaturaMotherObject.GetCandidatoValidoPorEscolaridade(Escolaridade.Fundamental),
                typeof(Exception)
            };
        }

        [Fact]
        public void CandidatoEhValido_DeveRetornarFalso_QuandoCandidatoEhNulo()
        {
            // Arrange
            Candidato candidato = null;

            // Act
            var resultado = _candidaturaService.CandidatoEhValido(candidato);

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public void CandidatoEhValido_DeveRetornarFalso_QuandoIdadeMenorQue21Anos()
        {
            // Arrange
            var candidato = CandidaturaMotherObject.GetCandidatoValidoPorEscolaridade(Escolaridade.Medio);
            candidato.Nascimento = DateTime.Now.AddYears(-20);

            // Act
            var resultado = _candidaturaService.CandidatoEhValido(candidato);

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public void CandidatoEhValido_DeveRetornarVerdadeiro_QuandoCandidatoEhValido()
        {
            // Arrange
            var candidato = CandidaturaMotherObject.GetCandidatoValidoPorEscolaridade(Escolaridade.Superior);

            // Act
            var resultado = _candidaturaService.CandidatoEhValido(candidato);

            // Assert
            Assert.True(resultado);
        }

        [Theory]
        [MemberData(nameof(GetCandidatosAptosAoConcurso))]
        public void CandidatoAptoAoConcurso_DeveRetornarVerdadeiro_QuandoCandidatoApto(Candidato candidato, Concurso concurso)
        {
            // Act
            var resultado = _candidaturaService.CandidatoAptoAoConcurso(candidato, concurso);

            // Assert
            Assert.True(resultado);
        }

        public static IEnumerable<object[]> GetCandidatosAptosAoConcurso()
        {
            yield return new object[]
            {
                CandidaturaMotherObject.GetCandidatoValidoPorEscolaridade(Escolaridade.Superior),
                CandidaturaMotherObject.GetConcursoPorEscolaridade(Escolaridade.Superior)
            };
            yield return new object[]
            {
                CandidaturaMotherObject.GetCandidatoValidoPorEscolaridade(Escolaridade.Medio),
                CandidaturaMotherObject.GetConcursoPorEscolaridade(Escolaridade.Medio)
            };
            yield return new object[]
            {
                CandidaturaMotherObject.GetCandidatoValidoPorEscolaridade(Escolaridade.Medio),
                CandidaturaMotherObject.GetConcursoPorEscolaridade(Escolaridade.Fundamental)
            };
        }

        [Theory]
        [MemberData(nameof(GetCandidatosInaptosAoConcurso))]
        public void CandidatoAptoAoConcurso_DeveRetornarFalso_QuandoCandidatoInapto(Candidato candidato, Concurso concurso)
        {
            // Act
            var resultado = _candidaturaService.CandidatoAptoAoConcurso(candidato, concurso);

            // Assert
            Assert.False(resultado);
        }

        public static IEnumerable<object[]> GetCandidatosInaptosAoConcurso()
        {
            yield return new object[]
            {
                CandidaturaMotherObject.GetCandidatoValidoPorEscolaridade(Escolaridade.Fundamental),
                CandidaturaMotherObject.GetConcursoPorEscolaridade(Escolaridade.Superior)
            };
        }
    }
}
