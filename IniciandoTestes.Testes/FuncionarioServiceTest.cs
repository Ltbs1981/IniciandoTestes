using Bogus;
using System.Collections.Generic;
using System;
using Xunit;
using IniciandoTestes.Entidades;
using IniciandoTestes.Servicos;
using Xunit.Sdk;
using IniciandoTestes.Tests.MotherObjects;

namespace IniciandoTestes.Tests
{
    using Bogus;
    using System;
    using System.Collections.Generic;
    using Xunit;

    namespace IniciandoTestes.Tests
    {
        public class FuncionarioServiceTest
        {
            private readonly Faker _faker;

            public FuncionarioServiceTest()
            {
                _faker = new Faker();
            }

            [Theory]
            [MemberData(nameof(GetFuncionariosValidos))]
            public void AdicionarFuncionario_DeveConcluir_QuandoDadosValidos(Funcionario funcionario)
            {
                // Arrange
                var sut = new FuncionarioService();

                // Act & Assert
                sut.AdicionarFuncionario(funcionario);
            }

            public static IEnumerable<object[]> GetFuncionariosValidos()
            {
                yield return new object[] { FuncionarioMother.GetFuncionarioValidoPorSenioridade(Senioridade.Junior) };
                yield return new object[] { FuncionarioMother.GetFuncionarioValidoPorSenioridade(Senioridade.Pleno) };
                yield return new object[] { FuncionarioMother.GetFuncionarioValidoPorSenioridade(Senioridade.Senior) };
            }

            [Theory]
            [MemberData(nameof(GetFuncionariosInvalidos))]
            public void AdicionarFuncionario_DeveLancarExcecao_QuandoDadosInvalidos(Funcionario funcionario, Type excecaoEsperada)
            {
                // Arrange
                var sut = new FuncionarioService();

                // Act & Assert
                Assert.Throws(excecaoEsperada, () => sut.AdicionarFuncionario(funcionario));
            }

            public static IEnumerable<object[]> GetFuncionariosInvalidos()
            {
                yield return new object[] { FuncionarioMother.GetFuncionarioComNomeCurto(), typeof(FormatException) };
                yield return new object[] { FuncionarioMother.GetFuncionarioComNomeInvalido(), typeof(Exception) };
                yield return new object[] { FuncionarioMother.GetFuncionarioComNascimentoInvalido(), typeof(Exception) };

                // Salários inválidos para cada nível de senioridade
                yield return new object[]
                {
                FuncionarioMother.GetFuncionarioComSalarioInvalido(Senioridade.Junior, 3199),
                typeof(Exception)
                };
                yield return new object[]
                {
                FuncionarioMother.GetFuncionarioComSalarioInvalido(Senioridade.Junior, 5501),
                typeof(Exception)
                };
                yield return new object[]
                {
                FuncionarioMother.GetFuncionarioComSalarioInvalido(Senioridade.Pleno, 5499),
                typeof(Exception)
                };
                yield return new object[]
                {
                FuncionarioMother.GetFuncionarioComSalarioInvalido(Senioridade.Pleno, 8001),
                typeof(Exception)
                };
                yield return new object[]
                {
                FuncionarioMother.GetFuncionarioComSalarioInvalido(Senioridade.Senior, 7999),
                typeof(Exception)
                };
                yield return new object[]
                {
                FuncionarioMother.GetFuncionarioComSalarioInvalido(Senioridade.Senior, 20001),
                typeof(Exception)
                };
            }
        }
    }
}