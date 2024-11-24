using Bogus;
using System.Collections.Generic;
using System;
using Xunit;
using IniciandoTestes.Entidades;
using IniciandoTestes.Servicos;
using Xunit.Sdk;

namespace IniciandoTestes.Tests
{
    public class FuncionarioServiceTest
    {
        private readonly Faker _faker;

        public FuncionarioServiceTest()
        {
            _faker = new Faker();
        }

        //teste de sucesso com dados ok

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
            var faker = new Faker();

            // Funcionários com salários válidos para cada nível de senioridade
            yield return new object[]
            {
            new Funcionario
            {
                Nome = faker.Name.FullName(),
                Nascimento = faker.Date.Between(DateTime.Now.AddDays(-21), DateTime.Now.AddDays(-50)),
                Senioridade = Senioridade.Junior,
                Salario = faker.Random.Double(3200, 5500)
            }
            };

            yield return new object[]
            {
            new Funcionario
            {
                Nome = faker.Name.FullName(),
                Nascimento = faker.Date.Between(DateTime.Now.AddDays(-21), DateTime.Now.AddDays(-50)),
                Senioridade = Senioridade.Pleno,
                Salario = faker.Random.Double(5500, 8000)
            }
            };

            yield return new object[]
            {
            new Funcionario
            {
                Nome = faker.Name.FullName(),
                Nascimento = faker.Date.Between(DateTime.Now.AddDays(-21), DateTime.Now.AddDays(-50)),
                Senioridade = Senioridade.Senior,
                Salario = faker.Random.Double(8000, 20000)
            }
            };
        }

        //lançando excepitions
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
            var faker = new Faker();

                        // Nome inválido (curto)
            yield return new object[]
            {
        new Funcionario
        {
            Nome = "Jo",
            Nascimento = faker.Date.Past(30, DateTime.Now.AddYears(-21)),
            Senioridade = Senioridade.Junior,
            Salario = 4000
        },
        typeof(FormatException)
            };

            // nome nulo
            yield return new object[]
            {
    new Funcionario
    {
        Nome = null, // Nome nulo
        Nascimento = faker.Date.Between(DateTime.Now.AddYears(-50), DateTime.Now.AddYears(-21)),
        Senioridade = Senioridade.Junior,
        Salario = 4000
    },
    typeof(Exception) // Exceção esperada
            };

            // Idade inválida
            yield return new object[]
           {
        new Funcionario
        {
            Nome = faker.Name.FullName(),
            Nascimento = faker.Date.Past(60, DateTime.Now.AddYears(-60)),
            Senioridade = Senioridade.Pleno,
            Salario = 6000
        },
        typeof(Exception)
            };
            // Salário fora do intervalo para cada senioridade
            yield return new object[]
            {
            new Funcionario
            {
                Nome = faker.Name.FullName(),
                Nascimento = faker.Date.Between(DateTime.Now.AddDays(-21), DateTime.Now.AddDays(-50)),
                Senioridade = Senioridade.Junior,
                Salario = faker.Random.Double(0, 3199) // Salário inválido para Junior
            },
            typeof(Exception)
            };

            yield return new object[]
{
        new Funcionario
        {
            Nome = faker.Name.FullName(),
            Nascimento = faker.Date.Past(30, DateTime.Now.AddYears(-21)),
            Senioridade = Senioridade.Junior,
            Salario = faker.Random.Double(5501, 10000) // Acima do limite
        },
        typeof(Exception)
};

            yield return new object[]
            {
            new Funcionario
            {
                Nome = faker.Name.FullName(),
                Nascimento = faker.Date.Between(DateTime.Now.AddDays(-21), DateTime.Now.AddDays(-50)),
                Senioridade = Senioridade.Pleno,
                Salario = faker.Random.Double(0, 5499) // Salário inválido para Pleno
            },
            typeof(Exception)
            };

            yield return new object[]
{
        new Funcionario
        {
            Nome = faker.Name.FullName(),
            Nascimento = faker.Date.Past(30, DateTime.Now.AddYears(-21)),
            Senioridade = Senioridade.Pleno,
            Salario = faker.Random.Double(8001, 15000) // Acima do limite
        },
        typeof(Exception)
};

            yield return new object[]
            {
            new Funcionario
            {
                Nome = faker.Name.FullName(),
                Nascimento = faker.Date.Between(DateTime.Now.AddDays(-21), DateTime.Now.AddDays(-50)),
                Senioridade = Senioridade.Senior,
                Salario = faker.Random.Double(0, 7999) // Salário inválido para Senior
            },
            typeof(Exception)
            };
        }
    }
}