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

        public void AddCliente(Cliente cliente)
        {
            if (DateTime.Now.AddYears(-18) < cliente.Nascimento)
            {
                throw new Exception("Cliente de menor");
            }

            var clienteBd = _clienteRepository.GetCliente(cliente.Id);

            if (clienteBd?.Id == cliente.Id)
            {              
                    throw new Exception("Cliente já existe no banco de dados " +
                                        "ou o ID inserido não corresponde a " +
                                        "nenhum cliente na base de dados.");
            }

            _clienteRepository.AddCliente(cliente);
        }

        public string ExemploAtrasadinhoQueNaoAvisaEDepoisEncheOSaco()
        {
            return "Responda a mensagem na proxima vez";
        }
    }
}
