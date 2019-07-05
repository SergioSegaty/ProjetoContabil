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
    class ContaReceberRepository : IContaReceberRepository

    {
        public bool Alterar(ContaReceber contaReceber)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"UPDATE contas_receber SET
            id_cliente = @ID_CLIENTE,
            id_categoria = @ID_CATEGORIA,
            nome = @NOME,
            data_pagamento = @DATA_PAGAMENTO,
            valor = @VALOR
            WHERE id = @ID";

            comando.Parameters.AddWithValue("@ID_CLIENTE", contaReceber.IdCliente);
            comando.Parameters.AddWithValue("@ID_CATEGORIA", contaReceber.IdCategoria);
            comando.Parameters.AddWithValue("@NOME", contaReceber.Nome);
            comando.Parameters.AddWithValue("@DATA_PAGAMENTO", contaReceber.DataPagamento);
            comando.Parameters.AddWithValue("@VALOR", contaReceber.Valor);
            comando.Parameters.AddWithValue("@ID", contaReceber.Id);

            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;
            
            
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"DELETE from contas_pagar WHERE id = @ID";

            comando.Parameters.AddWithValue("@ID", id);

            int quantidade = comando.ExecuteNonQuery();

            comando.Connection.Close();

            return quantidade == 1;
        }

        public int Inserir(ContaReceber contaReceber)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"INSERT INTO contas_receber
            (nome, id_cliente, id_categoria, valor, data_pagamento)
            OUTPUT INSERTED.ID
            VALUES
            (@NOME, @ID_CLIENTE, @ID_CATEGORIA, @VALOR, @DATA_PAGAMENTO)";

            comando.Parameters.AddWithValue("@NOME", contaReceber.Nome);
            comando.Parameters.AddWithValue("@ID_CLIENTE", contaReceber.IdCliente);
            comando.Parameters.AddWithValue("@ID_CATEGORIA", contaReceber.IdCategoria);
            comando.Parameters.AddWithValue("@VALOR", contaReceber.Valor);
            comando.Parameters.AddWithValue("@DATA_PAGAMENTO", contaReceber.DataPagamento);

            int id = Convert.ToInt32(comando.ExecuteScalar());

            comando.Connection.Close();
            return id;

        }

        public ContaReceber ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"SELECT * FROM contas_receber WHERE id = @ID";

            comando.Parameters.AddWithValue("@ID", id);

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());

            if (tabela.Rows.Count == 0)
            {
                return null;    
            }

            DataRow linha = tabela.Rows[0];

            ContaReceber contaReceber = new ContaReceber();
            contaReceber.Id = Convert.ToInt32(linha["id"]);
            contaReceber.IdCategoria = Convert.ToInt32(linha["id_categoria"]);
            contaReceber.IdCliente = Convert.ToInt32(linha["id_cliente"]);
            contaReceber.Nome = linha["nome"].ToString();
            contaReceber.DataPagamento = Convert.ToDateTime(linha["data_pagamento"]);
            contaReceber.Valor = Convert.ToDecimal(linha["valor"]);

            return contaReceber;
        }

        public List<ContaReceber> ObterTodos()
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"SELECT 
            clientes.id AS 'IdCliente',
            clientes.nome AS 'NomeCliente',
            clientes.cpf AS 'CpfCliente',
            categorias.id AS 'IdCategoria',
            categorias.nome AS 'NomeCategoria',
            contas_receber.id AS 'id',
            contas_receber.nome AS 'nome',
            contas_receber.data_pagamento AS 'data_pagamento',
            contas_receber.valor AS 'valor' FROM  contas_receber
            INNER JOIN clientes ON (contas_receber.id_cliente = clientes.id)
            INNER JOIN categorias ON (contas_receber.id_categoria = categorias.id)
            ";

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            List<ContaReceber> listaDeContas = new List<ContaReceber>();
            comando.Connection.Close();

            foreach (DataRow linha in tabela.Rows)
            {
                ContaReceber contaReceber = new ContaReceber();

                contaReceber.Nome = linha["nome"].ToString();
                contaReceber.Id = Convert.ToInt32(linha["id"]);
                contaReceber.Valor = Convert.ToDecimal(linha["valor"]);
                contaReceber.DataPagamento = Convert.ToDateTime(linha["valor"]);
                contaReceber.IdCliente = Convert.ToInt32(linha["IdCliente"]);
                contaReceber.IdCategoria = Convert.ToInt32(linha["IdCategoria"]);

                contaReceber.Categoria = new Categoria();
                contaReceber.Cliente = new Cliente();

                contaReceber.Categoria.Nome = linha["NomeCategoria"].ToString();
                contaReceber.Categoria.Id = Convert.ToInt32(linha["IdCategoria"]);

                contaReceber.Cliente.Id = Convert.ToInt32(linha["IdCliente"]);
                contaReceber.Cliente.Nome = linha["NomeCliente"].ToString();
                contaReceber.Cliente.Cpf = linha["CpfCliente"].ToString();

                listaDeContas.Add(contaReceber);

            }

            return listaDeContas;
        }
    }
}
