using Bogus;
using IniciandoTestes.Contratos;
using IniciandoTestes.Entidades;
using IniciandoTestes.Servicos;
using Moq;
using System;
using Xunit;

namespace IniciandoTestes.Tests
{
    public class ClienteServiceTest
    {
        

        //idade menor que 18
        [Fact]
        public void AddCliente_DeveLancarExcecao_QuandoIdadeMenorQue18()
        {
            // Arrange
            var clienteRepositoryMock = new Mock<IClienteRepository>();
            var sut = new ClienteService(clienteRepositoryMock.Object);

            var cliente = new Cliente
            {
                Id = Guid.NewGuid(),
                Nome = "Cliente Menor",
                Nascimento = DateTime.Now.AddYears(-17) // Menor de 18 anos
            };

            // Act - Assert
            Assert.Throws<Exception>(() => sut.AddClliente(cliente));
        }

        
        //tipo de msn
        [Fact]
        public void AddCliente_DeveLancarArgumentNullException_ComMensagemEsperada_QuandoClienteNulo()
        {
            // Arrange
            var clienteRepositoryMock = new Mock<IClienteRepository>();
            var sut = new ClienteService(clienteRepositoryMock.Object);

            // Act - Assert
            var exception = Assert.Throws<ArgumentNullException>(() => sut.AddClliente(null));
            Assert.Equal("O cliente não pode ser nulo. (Parameter 'cliente')", exception.Message);
        }

        //adicionando cliente válido
        [Fact]
        public void AdicionarCLiente_DeveAdicionarComSucesso_QuandoClienteValido()
        {
            //Arrange
            Mock<IClienteRepository> clienteRepositoryMock = new Mock<IClienteRepository>();
            clienteRepositoryMock.Setup(x => x.GetCliente(It.IsAny<Guid>())).Returns(new Cliente());
            ClienteService sut = new ClienteService(clienteRepositoryMock.Object);
            Faker faker = new Faker();
            Cliente cliente = new Cliente()
            {
                Id = Guid.NewGuid(),
                Nome = faker.Name.FullName(),
                Nascimento = new DateTime(1900, 12, 12)
            };

            //Act

            sut.AddClliente(cliente);

            //Assert

            clienteRepositoryMock.Verify(x => x.GetCliente(It.IsAny<Guid>()), Times.Once());
            clienteRepositoryMock.Verify(x => x.AddCliente(cliente), Times.Once());

        }

        
        //throw exception cliente já existe
        [Fact]
        public void AddCliente_DeveQuebrar_QuandoClienteJaExiste()
        {
            //Arrange
            Faker faker = new Faker();
            Cliente cliente = new Cliente()
            {
                Nome = faker.Name.FullName(),
                Nascimento = new System.DateTime(1900, 12, 12),
                Id = Guid.NewGuid(),
            };

            Mock<IClienteRepository> clienteRepositoryMock = new Mock<IClienteRepository>();
            clienteRepositoryMock.Setup(x => x.GetCliente(It.IsAny<Guid>())).Returns(cliente);

            ClienteService sut = new ClienteService(clienteRepositoryMock.Object);

            //Act - Assert   // x => x.  -- () => 
            Assert.Throws<Exception>(() => sut.AddClliente(cliente));
        }
        //cliente nulo
        [Fact]
        public void AddCliente_DeveLancarExcecao_QuandoClienteNulo()
        {
            // Arrange
            var clienteRepositoryMock = new Mock<IClienteRepository>();
            var sut = new ClienteService(clienteRepositoryMock.Object);

            // Act - Assert
            Assert.Throws<ArgumentNullException>(() => sut.AddClliente(null));
        }
        
        
    }
}