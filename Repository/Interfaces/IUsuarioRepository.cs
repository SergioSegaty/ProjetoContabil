﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    interface IUsuarioRepository
    {
        int Inserir(Usuario usuario);

        List<Usuario> ObterTodos();

        bool Alterar(Usuario usuario);

        Usuario ObterPeloId(int id);

        bool Apagar(int id);
    }
}
