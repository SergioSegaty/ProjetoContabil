using Model;
using Repository.DataBase;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ContaPagarRepository : IContaPagarRepository
    {
        public bool Alterar(ContaPagar contaPagar)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "UPDATE ContaPagar SET id_cliente = @ID_CLIENTE WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID_CLIENTE", contaPagar.IdCliente);
            comando.Parameters.AddWithValue("@ID_CATEGORIA", contaPagar.IdCategoria);
            comando.Parameters.AddWithValue("@NOME", contaPagar.Nome);
            comando.Parameters.AddWithValue("@DATA_VENCIMENTO", contaPagar.DataVencimento);
            comando.Parameters.AddWithValue("@DATA_PAGAMENTO", contaPagar.DataPagamento);
            comando.Parameters.AddWithValue("@VALOR", contaPagar.Valor);
            comando.Parameters.AddWithValue("@ID", contaPagar.Id);
            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "DELETE FROM contas_pagar WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;
        }

        public int Inserir(ContaPagar contaPagar)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "INSERT INT contas_pagar (id_cliente) OUTPUT INSERTED.ID VALUES (@ID_CLIENTE)";
            comando.Parameters.AddWithValue("@ID_CLIENTE", contaPagar.IdCliente);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public ContaPagar ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "SELECT * FROM contas_pagar WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            if (tabela.Rows.Count == 0)
            {
                return null;
            }
            DataRow linha = tabela.Rows[0];
            ContaPagar contaPagar = new ContaPagar();
            contaPagar.IdCliente = Convert.ToInt32(linha["id_cliente"]);
            contaPagar.IdCategoria = Convert.ToInt32(linha["id_categoria"]);
            contaPagar.Nome = linha["nome"].ToString();
            DateTime DataVencimento = new DateTime();
            contaPagar.DataVencimento = Convert.ToDateTime(linha["data_vencimento"]);
            contaPagar.DataVencimento = DataVencimento;
            DateTime DataPagamento = new DateTime();
            contaPagar.DataPagamento = Convert.ToDateTime(linha["data_pagamento"]);
            contaPagar.DataPagamento = DataPagamento;
            contaPagar.Valor = Convert.ToDecimal(linha["valor"]);
            contaPagar.Id = Convert.ToInt32(linha["id"]);
            return contaPagar;

        }

        public List<ContaPagar> ObterTodos()
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"SELECT
            clientes.id AS 'IdCliente',
            clientes.nome AS 'NomeCliente',
            clientes.cpf AS 'CpfCliente',
            categorias.id AS 'IdCategoria',
            categorias.nome AS 'NomeCategoria',
            contas_pagar.id AS 'id',
            contas_pagar.nome AS 'nome',
            contas_pagar.data_vencimento AS 'data_vencimento',
            contas_pagar.data_pagamento AS 'data_pagamento',
            contas_pagar.valor AS 'valor' FROM contas_pagar
            INNER JOIN clientes ON (contas_pagar.id_cliente = clientes.id)
            INNER JOIN categorias ON (contas_pagar.id_categoria = categorias.id)";

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            List<ContaPagar> contasPagar = new List<ContaPagar>();
            comando.Connection.Close();

            foreach (DataRow linha in tabela.Rows)
            {
                ContaPagar contaPagar = new ContaPagar();

                contaPagar.Id = Convert.ToInt32(linha["id"]);
                contaPagar.IdCliente = Convert.ToInt32(linha["idCliente"]);
                contaPagar.IdCategoria = Convert.ToInt32(linha["idCategoria"]);
                contaPagar.Nome = linha["nome"].ToString();
                contaPagar.DataVencimento = Convert.ToDateTime(linha["data_vencimento"]);
                contaPagar.DataPagamento = Convert.ToDateTime(linha["data_pagamento"]);
                contaPagar.Valor = Convert.ToDecimal(linha["valor"]);

                contaPagar.Categoria = new Categoria();
                contaPagar.Cliente = new Cliente();

                contaPagar.Categoria.Nome = linha["NomeCategoria"].ToString();
                contaPagar.Categoria.Id = Convert.ToInt32(linha["idCategoria"]);

                contaPagar.Cliente.Id = Convert.ToInt32(linha["IdCliente"]);
                contaPagar.Cliente.Nome = linha["NomeCliente"].ToString();
                contaPagar.Cliente.Cpf = linha["CpfCliente"].ToString();

                contasPagar.Add(contaPagar);
            }

            return contasPagar;
        }
    }
}

