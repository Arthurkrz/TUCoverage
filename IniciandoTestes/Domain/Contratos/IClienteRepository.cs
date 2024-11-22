using IniciandoTestes.Entidades;
using System;
using System.Collections.Generic;

namespace IniciandoTestes.Contratos
{
    public interface IClienteRepository
    {
        Cliente GetCliente(int id);
        Cliente GetCliente(string nome);
        List<Cliente> GetAll();
        void AddCliente(Cliente cliente);

    }
}
