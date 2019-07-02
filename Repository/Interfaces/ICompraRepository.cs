using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository.Interfaces
{
    interface IComprasRepository
    {
        int Inserir(Compra compra);

        bool Apagar(int id);

        bool Alterar(Compra compra);

        List<Compra> ObterTodos();

        Compra ObterPeloId(int id);
        


    }
}
