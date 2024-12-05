using IniciandoTestes.Entidades;
using IniciandoTestes.Contratos.Concurso;

namespace IniciandoTestes.Contratos.Concurso
{
    internal interface ICandidaturaService
    {
        int CriarCandidatura(Candidato candidato);
    }
}
