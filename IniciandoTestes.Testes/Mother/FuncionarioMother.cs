using IniciandoTestes.Entidades;
using Bogus;
using System;

namespace IniciandoTestes.Tests.MotherObjects
{
    internal static class FuncionarioMother
    {
        public static Funcionario GetFuncionarioValidoPorSenioridade(Senioridade senioridade)
        {
            Faker<Funcionario> faker = new Faker<Funcionario>();
            faker.RuleFor(x => x.Nome, f => f.Name.FullName())
                 .RuleFor(x => x.Nascimento, f => f.Date.Past(50, DateTime.Now.AddYears(-21)))
                 .RuleFor(x => x.Senioridade, senioridade);

            switch (senioridade)
            {
                case Senioridade.Junior:
                    faker.RuleFor(x => x.Salario, f => f.Random.Double(3200, 5500));
                    break;
                case Senioridade.Pleno:
                    faker.RuleFor(x => x.Salario, f => f.Random.Double(5500, 8000));
                    break;
                case Senioridade.Senior:
                    faker.RuleFor(x => x.Salario, f => f.Random.Double(8000, 20000));
                    break;
            }

            return faker.Generate();
        }

        public static Funcionario GetFuncionarioComNomeInvalido()
        {
            return new Funcionario
            {
                Nome = "",
                Nascimento = DateTime.Now.AddYears(-30),
                Senioridade = Senioridade.Junior,
                Salario = 4000
            };
        }

        public static Funcionario GetFuncionarioComNascimentoInvalido()
        {
            return new Funcionario
            {
                Nome = "Isabela",
                Nascimento = DateTime.Now.AddYears(-60),
                Senioridade = Senioridade.Junior,
                Salario = 4000
            };
        }

        public static Funcionario GetFuncionarioComSalarioInvalido(Senioridade senioridade, double salario)
        {
            var funcionario = GetFuncionarioValidoPorSenioridade(senioridade);
            funcionario.Salario = salario;
            return funcionario;
        }

        public static Funcionario GetFuncionarioComNomeCurto()
        {
            return new Funcionario
            {
                Nome = "Lu",
                Nascimento = DateTime.Now.AddYears(-30),
                Senioridade = Senioridade.Junior,
                Salario = 4000
            };
        }
    }
}
