using Bogus;
using System.Collections.Generic;
using System;
using Xunit;
using IniciandoTestes.Entidades;
using IniciandoTestes.Servicos;

namespace IniciandoTestes.Tests
{
    public class FuncionarioServiceTest
    {
        private readonly Faker _faker;

        public FuncionarioServiceTest()
        {
            _faker = new Faker();
        }


        [Theory]
        [MemberData(nameof(GetFuncionariosData))]
        public void AdicionarFuncionario_DeveConcluir_QuandoDadosValidos(Funcionario funcionario)
        {
            //Arrange
            FuncionarioService sut = new FuncionarioService();

            //Act & Assert
            sut.AdicionarFuncionario(funcionario);
        }

        public static IEnumerable<object[]> GetFuncionariosData()
        {
            var faker = new Faker();

            yield return new object[]
            {
                new Funcionario()
                {
                    Nome = faker.Name.FullName(),
                    Nascimento = faker.Date.Between(DateTime.Now.AddDays(-21), DateTime.Now.AddDays(-50)),
                    Senioridade = Senioridade.Junior,
                    Salario = faker.Random.Double(3300,5400)
                },
                2,
                "Sucesso"
            };

            yield return new object[] 
            { 
                new Funcionario()
                {
                    Nome = faker.Name.FullName(),
                    Nascimento = faker.Date.Between(DateTime.Now.AddDays(-21), DateTime.Now.AddDays(-50)),
                    Senioridade = Senioridade.Pleno,
                    Salario = faker.Random.Double(5600,7000)
                },
                5,
                "Falha"
            };

            yield return new object[] 
            { 
                new Funcionario()
                {
                    Nome = faker.Name.FullName(),
                    Nascimento = faker.Date.Between(DateTime.Now.AddDays(-21), DateTime.Now.AddDays(-50)),
                    Senioridade = Senioridade.Senior,
                    Salario = faker.Random.Double(9000,20000)
                }, 
                10, 
                "Uhuuul"
            };

        }

        public static IEnumerable<object[]> GetFuncionariosSalariosInvalidos()
        {
            var faker = new Faker();

            yield return new object[]
            {
                new Funcionario
                {
                    Nome = faker.Name.FullName(),
                    Nascimento = faker.Date.Between(DateTime.Now.AddDays(-21), DateTime.Now.AddDays(-50)),
                    Senioridade = Senioridade.Junior,
                    Salario = 3000
                }
            };

            yield return new object[]
{
                new Funcionario
                {
                    Nome = faker.Name.FullName(),
                    Nascimento = faker.Date.Between(DateTime.Now.AddDays(-21), DateTime.Now.AddDays(-50)),
                    Senioridade = Senioridade.Junior,
                    Salario = 6000
                }
            };

            yield return new object[]
            {
                new Funcionario
                {
                    Nome = faker.Name.FullName(),
                    Nascimento = faker.Date.Between(DateTime.Now.AddDays(-21), DateTime.Now.AddDays(-50)),
                    Senioridade = Senioridade.Pleno,
                    Salario = 5000
                }
            };

            yield return new object[]
            {
                new Funcionario
                {
                    Nome = faker.Name.FullName(),
                    Nascimento = faker.Date.Between(DateTime.Now.AddDays(-21), DateTime.Now.AddDays(-50)),
                    Senioridade = Senioridade.Pleno,
                    Salario = 9000
                }
            };

            yield return new object[]
            {
                new Funcionario
                {
                    Nome = faker.Name.FullName(),
                    Nascimento = faker.Date.Between(DateTime.Now.AddDays(-21), DateTime.Now.AddDays(-50)),
                    Senioridade = Senioridade.Senior,
                    Salario = 7000
                }
            };

        }

        [Fact]
        public void AdicionarFuncionario_DeveEmitirException_QuandoFuncionarioNulo()
        {
            // Arrange
            Funcionario funcionario = null;
            FuncionarioService sut = new FuncionarioService();

            // Act & Assert
            Assert.Throws<Exception>(() => sut.AdicionarFuncionario(funcionario));

        }

        [Fact]
        public void AdicionarFuncionario_DeveEmitirException_QuandoNomeCurto()
        {
            // Arrange
            var faker = new Faker();
            Funcionario funcionario = new Funcionario
            {
                Nome = "a",
                Nascimento = faker.Date.Between(DateTime.Now.AddYears(-50), DateTime.Now.AddYears(-70)),
                Senioridade = Senioridade.Senior,
                Salario = faker.Random.Int(8000, 20000)
            };

            FuncionarioService sut = new FuncionarioService();

            // Act & Assert
            Assert.Throws<FormatException>(() => sut.AdicionarFuncionario(funcionario));

        }

        [Fact]
        public void AdicionarFuncionario_DeveEmitirException_QuandoNascimentoInvalido()
        {
            // Arrange
            var faker = new Faker();
            Funcionario funcionario = new Funcionario
            {
                Nome = faker.Name.FullName(),
                Nascimento = DateTime.Now.AddYears(-51),
                Senioridade = Senioridade.Senior,
                Salario = faker.Random.Int(8000, 20000)
            };

            FuncionarioService sut = new FuncionarioService();

            // Act & Assert
            Assert.Throws<Exception>(() => sut.AdicionarFuncionario(funcionario));

        }

        [Theory]
        [MemberData(nameof(GetFuncionariosSalariosInvalidos))]
        public void AdicionarFuncionario_DeveEmitirException_QuandoSalarioInvalido(Funcionario funcionario)
        {
            // Arrange
            FuncionarioService sut = new FuncionarioService();

            // Act & Assert
            Assert.Throws<Exception>(() => sut.AdicionarFuncionario(funcionario));

        }
    }
}