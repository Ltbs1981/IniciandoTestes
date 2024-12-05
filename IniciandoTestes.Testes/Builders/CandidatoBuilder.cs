using Bogus;
using IniciandoTestes.Entidades;
using System;

namespace IniciandoTestes.Tests.Builders
{
    public class CandidatoBuilder
    {
        private readonly Candidato _candidato;
        private readonly Faker _faker;

        public CandidatoBuilder()
        {
            _faker = new Faker();
            _candidato = new Candidato
            {
                Nome = _faker.Name.FullName(),
                Nascimento = _faker.Date.Past(50, DateTime.Now.AddYears(-25)),
                Escolaridade = Escolaridade.Superior,
                Concurso = new Concurso { Id = Guid.NewGuid(), Escolaridade = Escolaridade.Superior }
            };
        }

        public CandidatoBuilder ComNome(string nome = null)
        {
            _candidato.Nome = nome ?? _faker.Name.FullName();
            return this;
        }

        public CandidatoBuilder ComNascimento(DateTime nascimento = default)
        {
            _candidato.Nascimento = nascimento == default ? _faker.Date.Past(50, DateTime.Now.AddYears(-25)) : nascimento;
            return this;
        }

        public CandidatoBuilder ComEscolaridade(Escolaridade escolaridade)
        {
            _candidato.Escolaridade = escolaridade;
            return this;
        }

        public CandidatoBuilder ComConcurso(Concurso concurso = null)
        {
            _candidato.Concurso = concurso ?? new Concurso { Id = Guid.NewGuid(), Escolaridade = Escolaridade.Superior };
            return this;
        }

        public Candidato Build()
        {
            return _candidato;
        }
    }
}
