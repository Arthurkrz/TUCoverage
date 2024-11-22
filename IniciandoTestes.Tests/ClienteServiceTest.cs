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
        [Fact]
        public void AdicionarCLiente_DeveAdicionarComSucesso_QuandoClienteValido()
        {
            //Arrange
            Mock<IClienteRepository> clienteRepositoryMock = new Mock<IClienteRepository>();
            clienteRepositoryMock.Setup(x => x.GetCliente(It.IsAny<int>())).Returns(new Cliente());
            ClienteService sut = new ClienteService(clienteRepositoryMock.Object);
            Faker faker = new Faker();
            Cliente cliente = new Cliente()
            {
                Id = faker.Random.Int(1, 100000),
                Nome = faker.Name.FullName(),
                Nascimento = new DateTime(1900, 12, 12)
            };

            //Act

            sut.AddCliente(cliente);

            //Assert

            clienteRepositoryMock.Verify(x => x.GetCliente(It.IsAny<int>()), Times.Once());
            clienteRepositoryMock.Verify(x => x.AddCliente(cliente), Times.Once());

        }

        [Fact]
        public void AddCliente_DeveQuebrar_QuandoClienteJaExiste()
        {
            //Arrange
            Faker faker = new Faker();
            Cliente cliente = new Cliente()
            {
                Nome = faker.Name.FullName(),
                Nascimento = new System.DateTime(1900, 12, 12),
                Id = faker.Random.Int(1, 100000)
            };

            Mock<IClienteRepository> clienteRepositoryMock = new Mock<IClienteRepository>();
            clienteRepositoryMock.Setup(x => x.GetCliente(It.IsAny<int>())).Returns(cliente);

            ClienteService sut = new ClienteService(clienteRepositoryMock.Object);

            // Act & Assert
            Assert.Throws<Exception>(() => sut.AddCliente(cliente));
        }

        [Fact]
        public void AddCliente_DeveEmitirException_QuandoIdIncorreto()
        {
            // Arrange
            Faker faker = new Faker();
            Cliente cliente = new Cliente
            {
                Nome = faker.Name.FullName(),
                Nascimento = new System.DateTime(1900, 12, 12),
                Id = 0
            };

            Mock<IClienteRepository> clienteRepositoryMock = new Mock<IClienteRepository>();
            clienteRepositoryMock.Setup(x => x.GetCliente(cliente.Id)).Returns((Cliente)null);
            
            ClienteService sut = new ClienteService(clienteRepositoryMock.Object);

            // Act & Assert
            var exception = Record.Exception(() => sut.AddCliente(cliente));
            Assert.Null(exception);
        }

        [Fact]
        public void AddCliente_DeveEmitirException_QuandoClienteDeMenor()
        {
            // Arrange
            Faker faker = new Faker();
            Cliente cliente = new Cliente
            {
                Nome = faker.Name.FullName(),
                Nascimento = new System.DateTime(2015, 12, 12),
                Id = faker.Random.Int(1, 100000)
            };

            Mock<IClienteRepository> clienteRepositoryMock = new Mock<IClienteRepository>();
            clienteRepositoryMock.Setup(x => x.GetCliente(cliente.Id)).Returns(cliente);

            ClienteService sut = new ClienteService(clienteRepositoryMock.Object);

            // Act & Assert
            Assert.Throws<Exception>(() => sut.AddCliente(cliente));

        }

        [Fact]
        public void TesteEx()
        {
            Mock<IClienteRepository> mock = new Mock<IClienteRepository>();
            ClienteService clienteService = new ClienteService(mock.Object);

            var result = clienteService.ExemploAtrasadinhoQueNaoAvisaEDepoisEncheOSaco();
            var resultadoEsperado = "Responda a mensagem na proxima vez";
            Assert.NotNull(result);
            Assert.Equal(resultadoEsperado, result);

        }
    }
}
