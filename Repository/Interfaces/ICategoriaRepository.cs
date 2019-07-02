using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository.Interfaces
{
    interface iCategoriaRepository
    {
        int Inserir(Categoria categoria);

        bool Alterar(Categoria categoria);

        bool Apagar(int id);

        List<Categoria> ObterTodos();

        Categoria ObterPeloId(int id);

    }
}
