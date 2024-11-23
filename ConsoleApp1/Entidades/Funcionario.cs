namespace IniciandoTestes.Entidades
{
    public class Funcionario
    {
        public string Nome { get; set; }
        public DateTime Nascimento { get; set; }
        public double Salario { get; set; }

        public Senioridade Senioridade { get; set; }

    }
}
