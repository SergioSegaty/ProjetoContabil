using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ContaPagar
    {
        public int Id;
        public int IdCliente;
        public int IdCategoria;
        public string Nome;
        public DateTime DataVencimento;
        public DateTime DataPagamento;
        public decimal Valor;

        public Categoria Categoria;
        public Cliente Cliente;
    }
}
