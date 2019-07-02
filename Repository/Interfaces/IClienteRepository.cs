using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository.Interfaces
{
    interface IClienteRepository
    {
        int Inserir(Cliente cliente);

        bool Apagar(int id);

        bool Alterar(Cliente cliente);

        List<Cliente> ObterTodos();

        Cliente ObterPeloId(int id);
    }
}
