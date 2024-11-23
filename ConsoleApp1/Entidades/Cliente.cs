using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IniciandoTestes.Entidades
{
    public class Cliente
    {
        public Guid Id { get; set; }
    public string Nome { get; set; }
        public DateTime Nascimento { get; set; }
    }
}
