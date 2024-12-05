using System;

namespace IniciandoTestes.Entidades
{
    public class Candidato
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int NumeroInscricao { get; set; }
        public DateTime Nascimento { get; set; }
        public string Cpf { get; set; }
        public Escolaridade Escolaridade { get; set; }
        public Concurso Concurso { get; set; }
    }
}
