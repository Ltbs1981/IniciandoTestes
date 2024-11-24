//using IniciandoTestes.Contratos;
//using IniciandoTestes.Entidades;
//using System;
//using System.Collections.Generic;

//namespace IniciandoTestes.Tests.Fakes
//{
//    internal class ClienteRepositoryFake : IClienteRepository
//    {
//        public void AddCliente(Cliente cliente)
//        {

//        }

//        public List<Cliente> GetAll()
//        {
//            return new List<Cliente>();
//        }

//        public Cliente GetCliente(Guid id)
//        {
//            var idFake = new Guid("12ACA6F1-EECF-4CBA-A299-081FFF88EDE5");
//            if (id == idFake)
//                return new Cliente()
//                {
//                    Nome = "Arthur",
//                    Nascimento = new DateTime(2003, 04, 12),
//                    Id = idFake
//                };

//            return null;
//        }

//        public Cliente GetCliente(string nome)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
