using Bogus;
using IniciandoTestes.Contratos.Concurso;
using IniciandoTestes.Entidades;
using IniciandoTestes.Servicos.ConcursoService;
using IniciandoTestes.Testes.Builders;
using IniciandoTestes.Tests.Builders;
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
            _mockCandidaturaRepository.Setup(x => x.GetConcurso(candidato.Concurso.Id)).Returns(concurso);
            _mockCandidaturaRepository.Setup(x => x.AdicionaCandidato(candidato)).Returns(123);

            // Act
            var resultado = _candidaturaService.CriarCandidatura(candidato);

            // Assert
            Assert.Equal(123, resultado);
        }

        public static IEnumerable<object[]> GetCandidatosValidos()
        {
            yield return new object[]
            {
                new CandidatoBuilder().ComEscolaridade(Escolaridade.Superior).Build(),
                new ConcursoBuilder().ComEscolaridade(Escolaridade.Superior).Build()
            };
            yield return new object[]
            {
                new CandidatoBuilder().ComEscolaridade(Escolaridade.Medio).Build(),
                new ConcursoBuilder().ComEscolaridade(Escolaridade.Medio).Build()
            };
            yield return new object[]
{
                new CandidatoBuilder().ComEscolaridade(Escolaridade.Fundamental).Build(),
                new ConcursoBuilder().ComEscolaridade(Escolaridade.Fundamental).Build()
};

        }

        [Theory]
        [MemberData(nameof(GetCandidatosInvalidos))]
        public void CriarCandidatura_DeveLancarExcecao_QuandoCandidatoInvalido(Candidato candidato, Type excecaoEsperada)
        {
            // Arrange
            if (candidato != null && candidato.Escolaridade != Escolaridade.Superior)
            {
                var concurso = new ConcursoBuilder().ComEscolaridade(Escolaridade.Superior).Build();
                _mockCandidaturaRepository.Setup(x => x.GetConcurso(candidato.Concurso.Id)).Returns(concurso);
            }

            // Act & Assert
            Assert.Throws(excecaoEsperada, () => _candidaturaService.CriarCandidatura(candidato));
        }

        public static IEnumerable<object[]> GetCandidatosInvalidos()
        {
            yield return new object[] { null, typeof(ArgumentException) };
            yield return new object[] { new CandidatoBuilder().ComNascimento(DateTime.Now.AddYears(-20)).Build(), typeof(ArgumentException) };
            yield return new object[]
            {
                new CandidatoBuilder().ComEscolaridade(Escolaridade.Medio).Build(),
                typeof(Exception)
            };
            yield return new object[]
            {
                new CandidatoBuilder().ComEscolaridade(Escolaridade.Fundamental).Build(),
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
            var candidato = new CandidatoBuilder().ComNascimento(DateTime.Now.AddYears(-20)).Build();

            // Act
            var resultado = _candidaturaService.CandidatoEhValido(candidato);

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public void CandidatoEhValido_DeveRetornarVerdadeiro_QuandoCandidatoEhValido()
        {
            // Arrange
            var candidato = new CandidatoBuilder().Build();

            // Act
            var resultado = _candidaturaService.CandidatoEhValido(candidato);

            // Assert
            Assert.True(resultado);
        }

        [Theory]        [MemberData(nameof(GetCandidatosAptosAoConcurso))]
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
                new CandidatoBuilder().ComEscolaridade(Escolaridade.Superior).Build(),
                new ConcursoBuilder().ComEscolaridade(Escolaridade.Superior).Build()
            };
            yield return new object[]
            {
                new CandidatoBuilder().ComEscolaridade(Escolaridade.Medio).Build(),
                new ConcursoBuilder().ComEscolaridade(Escolaridade.Medio).Build()
            };
            yield return new object[]
            {
                new CandidatoBuilder().ComEscolaridade(Escolaridade.Medio).Build(),
                new ConcursoBuilder().ComEscolaridade(Escolaridade.Fundamental).Build()
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
                new CandidatoBuilder().ComEscolaridade(Escolaridade.Fundamental).Build(),
                new ConcursoBuilder().ComEscolaridade(Escolaridade.Superior).Build()
            };
        }
    }
}
