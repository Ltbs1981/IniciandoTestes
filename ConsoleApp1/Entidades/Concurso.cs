using System;

namespace IniciandoTestes.Entidades
{
    public class Concurso
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public DateTime Data { get; set; }
        public string Local { get; set; }
        public Escolaridade Escolaridade { get; set; }
    }
}
