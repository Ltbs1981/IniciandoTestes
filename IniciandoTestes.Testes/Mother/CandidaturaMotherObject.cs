using Bogus;
using IniciandoTestes.Entidades;
using System;

namespace IniciandoTestes.Tests.MotherObjects
{
    internal static class CandidaturaMotherObject
    {
        public static Candidato GetCandidatoValidoPorEscolaridade(Escolaridade escolaridade)
        {
            Faker<Candidato> faker = new Faker<Candidato>();
            faker.RuleFor(x => x.Nome, f => f.Name.FullName())
                 .RuleFor(x => x.Nascimento, f => f.Date.Past(50, DateTime.Now.AddYears(-21)))
                 .RuleFor(x => x.Escolaridade, escolaridade)
                 .RuleFor(x => x.Concurso, f => new Concurso { Id = Guid.NewGuid(), Escolaridade = escolaridade });

            return faker.Generate();
        }

        public static Candidato GetCandidatoInvalido()
        {
            return new Candidato
            {
                Nome = "João da Silva",
                Nascimento = DateTime.Now.AddYears(-20), // Muito jovem
                Escolaridade = Escolaridade.Medio,
                Concurso = new Concurso { Id = Guid.NewGuid(), Escolaridade = Escolaridade.Superior }
            };
        }

        public static Concurso GetConcursoPorEscolaridade(Escolaridade escolaridade)
        {
            return new Concurso
            {
                Id = Guid.NewGuid(),
                Escolaridade = escolaridade
            };
        }
    }
}
