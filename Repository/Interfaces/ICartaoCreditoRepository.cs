using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    interface ICartaoCreditoRepository
    {
        int Inserir(CartaoCredito cartaoCredito);

        List<CartaoCredito> ObterTodos();

        bool Alterar(CartaoCredito cartaoCredito);

         CartaoCredito ObterPeloId(int id);

        bool Apagar(int id);
    }
}
