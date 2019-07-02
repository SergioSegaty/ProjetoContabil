using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    interface IContaPagarRepository
    {
        int Inserir(ContaPagar contaPagar);

        List<ContaPagar> ObterTodos();

        bool Alterar(ContaPagar contaPagar);

        ContaPagar ObterPeloId(int id);

        bool Apagar(int id);
    }
}
