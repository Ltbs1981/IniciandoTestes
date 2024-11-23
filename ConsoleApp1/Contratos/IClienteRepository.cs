using IniciandoTestes.Entidades;

namespace IniciandoTestes.Contratos
{
    public interface IClienteRepository
    {
        Cliente GetCliente(Guid id);
        Cliente GetCliente (string nome);
        List<Cliente> GetAll();
        void AddCliente(Cliente cliente);

    }
}
