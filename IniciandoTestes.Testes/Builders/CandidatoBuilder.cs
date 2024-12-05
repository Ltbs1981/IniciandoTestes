using IniciandoTestes.Entidades;
using System;

namespace IniciandoTestes.Tests.Builders
{
    public class CandidatoBuilder
    {
        private readonly Candidato _candidato;

        public CandidatoBuilder()
        {
            _candidato = new Candidato
            {
                Nome = "João da Silva",
                Nascimento = DateTime.Now.AddYears(-25),
                Escolaridade = Escolaridade.Superior,
                Concurso = new Concurso { Id = Guid.NewGuid(), Escolaridade = Escolaridade.Superior }
            };
        }

        public CandidatoBuilder ComNome(string nome)
        {
            _candidato.Nome = nome;
            return this;
        }

        public CandidatoBuilder ComNascimento(DateTime nascimento)
        {
            _candidato.Nascimento = nascimento;
            return this;
        }

        public CandidatoBuilder ComEscolaridade(Escolaridade escolaridade)
        {
            _candidato.Escolaridade = escolaridade;
            return this;
        }

        public CandidatoBuilder ComConcurso(Concurso concurso)
        {
            _candidato.Concurso = concurso;
            return this;
        }

        public Candidato Build()
        {
            return _candidato;
        }
    }
}
