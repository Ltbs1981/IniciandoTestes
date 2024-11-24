using Bogus;
using System.Collections.Generic;
using System;
using Xunit;
using IniciandoTestes.Entidades;
using IniciandoTestes.Servicos;

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
    }
}