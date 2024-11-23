using System;
using IniciandoTestes.Entidades;
namespace IniciandoTestes.Servicos
{
    public class FuncionarioService
    {
        public void AdicionarFuncionario(Funcionario funcionario)
        {
            if (funcionario == null)
            {
                throw new Exception("Funcionario não pode ser nulo");
            }

            if (funcionario.Nome.Length < 3)
            {
                throw new FormatException("Formato incorreto de nome.");
            }

            if (funcionario.Nascimento < DateTime.Now.AddYears(-50))
            {
                throw new Exception("Funcionario muito novo para o cargo");
            }

            switch (funcionario.Senioridade)
            {
                case Senioridade.Junior:
                    {
                        if (funcionario.Salario < 3200 || funcionario.Salario > 5500)
                            throw new Exception("Salario imcompativel com o cargo");
                        break;
                    }
                case Senioridade.Pleno:
                    {
                        if (funcionario.Salario < 5500 || funcionario.Salario > 8000)
                            throw new Exception("Salario imcompativel com o cargo");
                        break;
                    }

                case Senioridade.Senior:
                    {
                        if (funcionario.Salario < 8000)
                            throw new Exception("Salario imcompativel com o cargo");
                        break;
                    }
            }

        }

    }
}
