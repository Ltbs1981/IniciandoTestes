using IniciandoTestes.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IniciandoTestes.Testes.Builders
{
    public class ConcursoBuilder
    {
        private readonly Concurso _concurso;

        public ConcursoBuilder()
        {
            _concurso = new Concurso
            {
                Id = Guid.NewGuid(),
                Escolaridade = Escolaridade.Superior
            };
        }

        public ConcursoBuilder ComEscolaridade(Escolaridade escolaridade)
        {
            _concurso.Escolaridade = escolaridade;
            return this;
        }

        public Concurso Build()
        {
            return _concurso;
        }
    }
}
