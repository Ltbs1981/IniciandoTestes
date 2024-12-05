using IniciandoTestes.Contratos.Concurso;
using IniciandoTestes.Entidades;
using System;

namespace IniciandoTestes.Servicos.ConcursoService
{
    public class CandidaturaService : ICandidaturaService
    {
        private readonly ICandidaturaRepository _candidaturaRepository;

        public CandidaturaService(ICandidaturaRepository candidaturaRepository)
        {
            _candidaturaRepository = candidaturaRepository;
        }

        public int CriarCandidatura(Candidato candidato)
        {
            if (!CandidatoEhValido(candidato))
            {
                throw new ArgumentException("Candidato invalido para concursos");
            }

            Concurso concursoBd = _candidaturaRepository.GetConcurso(candidato.Concurso.Id);

            if (!CandidatoAptoAoConcurso(candidato, concursoBd))
            {
                throw new Exception("Candidato inapto a esse concurso");
            }

            int matricula = _candidaturaRepository.AdicionaCandidato(candidato);

            return matricula;
        }

        public bool CandidatoAptoAoConcurso(Candidato candidato, Concurso concurso)
        {
            if (candidato.Escolaridade == Escolaridade.Superior)
                return true;
            else if (candidato.Escolaridade == Escolaridade.Medio &&
                    concurso.Escolaridade != Escolaridade.Superior)
                return true;
            else if (candidato.Escolaridade == concurso.Escolaridade)
                return true;
            else
                return false;
        }

        public bool CandidatoEhValido(Candidato candidato)
        {
            if (candidato == null)
                return false;

            if (candidato.Nascimento > DateTime.Now.AddYears(-21))
            {
                return false;
            }

            return true;
        }
    }
}
