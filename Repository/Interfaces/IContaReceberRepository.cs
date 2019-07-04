using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    interface IContaReceberRepository
    {
        int Inserir(ContaReceber contaReceber);

        bool Alterar(ContaReceber contaReceber);

        bool Apagar(int id);

        List<ContaReceber> ObterTodos();

        ContaReceber ObterPeloId(int id);
    }
}
