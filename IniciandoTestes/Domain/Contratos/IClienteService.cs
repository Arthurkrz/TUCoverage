using IniciandoTestes.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace IniciandoTestes.Contratos
{
    internal interface IClienteService
    {
        void AddCliente(Cliente cliente);

        string ExemploAtrasadinhoQueNaoAvisaEDepoisEncheOSaco();
    }
}
