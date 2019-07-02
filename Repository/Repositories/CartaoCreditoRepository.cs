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
    public class CartaoCreditoRepository: ICartaoCreditoRepository
    {
        public bool Alterar(CartaoCredito cartaoCredito)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "UPDATE cartoes_credito SET id_cliente = @ID_CLIENTE, numero = @NUMERO, data_vencimento = @DATA_VENCIMENTO, cvv = @CVV WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID_CLIENTE", cartaoCredito.IdCliente);
            comando.Parameters.AddWithValue("@NUMERO", cartaoCredito.Numero);
            comando.Parameters.AddWithValue("@DATA_VENCIMENTO", cartaoCredito.DataVencimento);
            comando.Parameters.AddWithValue("@CVV", cartaoCredito.Cvv);
            comando.Parameters.AddWithValue("@ID", cartaoCredito.Id);
            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "DELETE FROM cartoes_credito WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;
        }

        public int Inserir(CartaoCredito cartaoCredito)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "INSERT INTO cartoes_credito (id_cliente) OUTPUT INSERTED.ID VALUES (@ID_CLIENTE)";
            comando.Parameters.AddWithValue("@ID_CLIENTE", cartaoCredito.IdCliente);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public CartaoCredito ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "SELECT * FROM cartoes_credito WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            if (tabela.Rows.Count == 0)
            {
                return null;
            }
            DataRow linha = tabela.Rows[0];
            CartaoCredito cartaoCredito = new CartaoCredito();
            cartaoCredito.IdCliente = Convert.ToInt32(linha["id_cliente"]);
            cartaoCredito.Numero = linha["numero"].ToString();
            DateTime dataVencimento = new DateTime();
            cartaoCredito.DataVencimento = Convert.ToDateTime(linha["data_vencimento"]);
            cartaoCredito.DataVencimento = dataVencimento;
            cartaoCredito.Cvv = linha["cvv"].ToString();
            cartaoCredito.Id = Convert.ToInt32(linha["id"]);
            return cartaoCredito;
        }

        public List<CartaoCredito> ObterTodos()
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "SELECT * FROM cartoes_credito";
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            List<CartaoCredito> cartaoCreditos = new List<CartaoCredito>();
            foreach (DataRow linha in tabela.Rows)
            {
                CartaoCredito cartaoCredito = new CartaoCredito()
                {
                    IdCliente = Convert.ToInt32(linha["id_cliente"]),
                    Numero = linha["numero"].ToString(),
                    DataVencimento = Convert.ToDateTime(linha["data_vencimento"]),
                    Cvv = linha["cvv"].ToString(),
                    Id = Convert.ToInt32(linha["id"])
                };
                cartaoCreditos.Add(cartaoCredito);
            }
            return cartaoCreditos;
        }
    }
}
