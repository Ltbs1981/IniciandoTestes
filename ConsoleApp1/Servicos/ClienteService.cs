using IniciandoTestes.Contratos;
using IniciandoTestes.Entidades;
using System;

namespace IniciandoTestes.Servicos
{
    public class ClienteService : IClienteService
    {

        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public void AddClliente(Cliente cliente)
        {
            //if (string.IsNullOrWhiteSpace(cliente.Nome))
            //{
            //    throw new ArgumentException("O nome do cliente não pode ser vazio ou nulo.", nameof(cliente.Nome));
            //}

            if (cliente == null)
            {
                throw new ArgumentNullException(nameof(cliente), "O cliente não pode ser nulo.");
            }

            if (DateTime.Now.AddYears(-18) < cliente.Nascimento)
            {
                throw new Exception("Cliente de menor");
            }

            var clienteBd = _clienteRepository.GetCliente(cliente.Id);

            if (clienteBd?.Id == cliente.Id)
            {
                throw new Exception("Cliente já existe no banco de dados.");
            }

            _clienteRepository.AddCliente(cliente);
        }

        //    public string ExemploString()
        //    {
        //        return "Retorna a string";
        //    }
    }
}
