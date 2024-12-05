using IniciandoTestes.Entidades;
using System;

namespace IniciandoTestes.Contratos.Concurso
{
    public interface ICandidaturaRepository
    {
        Entidades.Concurso GetConcurso(Guid id);

        int AdicionaCandidato(Candidato candidato);
    }
}
