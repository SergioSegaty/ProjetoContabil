﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Cliente
    {
        public int Id;
        public int IdContabilidade;
        public string Nome;
        public string Cpf;

        public Contabilidade Contabilidade;
    }
}
