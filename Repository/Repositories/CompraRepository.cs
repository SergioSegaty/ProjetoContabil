using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository.DataBase;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class CompraRepository : ICompraRepository
    {
        public bool Alterar(Compra compra)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"UPDATE compras SET
            id_cartao_credito =  @ID_CARTAO_CREDITO
            valor = @VALOR,
            data_compra = @DATA_COMPRA
            WHERE id = @ID";

            comando.Parameters.AddWithValue("@ID_CARTAO_CREDITO", compra.IdCartao);
            comando.Parameters.AddWithValue("@VALOR", compra.Valor);
            comando.Parameters.AddWithValue("@DATA_COMPRA", compra.DataCompra);
            comando.Parameters.AddWithValue("@ID", compra.Id);

            int quantidade = comando.ExecuteNonQuery();

            comando.Connection.Close();
            return quantidade == 1;


        }

        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"DELETE compra where id = @ID";

            comando.Parameters.AddWithValue("@ID", id);

            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;

        }

        public int Inserir(Compra compra)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"INSERT INTO compras
            (id_cartao_credito, valor, data_compra)
            OUTPUT INSERTED.ID
            VALUES
            (@ID_CARTAO_CREDITO, @VALOR, @DATA_COMPRA)";

            comando.Parameters.AddWithValue("@ID_CARTAO_CREDITO", compra.IdCartao);
            comando.Parameters.AddWithValue("@VALOR", compra.Valor);
            comando.Parameters.AddWithValue("@DATA_COMPRA", compra.DataCompra);

            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public Compra ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"SELECT * FROM compras WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);

            DataTable tabela = new DataTable();
            if (tabela.Rows.Count == 0)
            {
                return null;
            }
            DataRow linha = tabela.Rows[0];
            Compra compra = new Compra();
            compra.IdCartao = Convert.ToInt32(linha["id_cartao_credito"]);
            compra.Valor = Convert.ToDecimal(linha["valor"]);
            compra.DataCompra = Convert.ToDateTime(linha["data_compra"]);
            compra.Id = Convert.ToInt32(linha["id"]);

            return compra;
        }

        public List<Compra> ObterTodos()
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"SELECT 
            cartoes_credito.id AS 'IdCartaoCredito',
            cartoes_credito.numero AS 'NumeroCartaoCredito',
            cartoes_credito.data_vencimento AS 'VencimentoCartao',
            compras.id AS 'id',
            compras.valor AS 'valor',
            compras.data_compra AS 'data_compra' FROM compras
            INNER JOIN cartoes_credito ON (compras.id_cartao_credito = cartoes_credito.id)";


            List<Compra> compras = new List<Compra>();
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());

            foreach (DataRow linha in tabela.Rows)
            {
                Compra compra = new Compra();              

                compra.Id = Convert.ToInt32(linha["id"]);
                compra.Valor = Convert.ToDecimal(linha["valor"]);
                compra.DataCompra = Convert.ToDateTime(linha["data_compra"]);
                compra.IdCartao = Convert.ToInt32(linha["id_cartao_credito"]);
                compra.CartaoCredito = new CartaoCredito();
                compra.CartaoCredito.Id = Convert.ToInt32(linha["IdCartaoCredito"]);
                compra.CartaoCredito.Numero = linha["NumeroCartaoCredito"].ToString();

                compras.Add(compra);


            }

            return compras;
        }
    }
}
