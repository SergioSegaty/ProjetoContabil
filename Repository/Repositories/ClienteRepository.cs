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
    public class ClienteRepository : IClienteRepository
    {
        public bool Alterar(Cliente cliente)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"UPDATE clientes SET
            nome = @NOME,
            cpf = @CPF,
            id_contabilidade = @ID_CONTABILIDADE
            WHERE id = @ID";

            comando.Parameters.AddWithValue("@ID", cliente.Id);
            comando.Parameters.AddWithValue("@NOME", cliente.Nome);
            comando.Parameters.AddWithValue("@CPF", cliente.Cpf);
            comando.Parameters.AddWithValue("@ID_CONTABILIDADE", cliente.IdContabilidade);

            int quantidade = comando.ExecuteNonQuery();

            comando.Connection.Close();

            return quantidade == 1;


        }

        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"DELETE from clientes where id = @ID";

            comando.Parameters.AddWithValue("@ID", id);

            int quantidade = comando.ExecuteNonQuery();

            comando.Connection.Close();

            return quantidade == 1;
        }

        public int Inserir(Cliente cliente)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"INSERT INTO clientes 
            (id_contabilidade, nome, cpf)
            OUTPUT INSERTED.ID
            VALUES
            (@ID_CONTABILIDADE, @NOME, @CPF)";

            comando.Parameters.AddWithValue("@ID_CONTABILIDADE", cliente.IdContabilidade);
            comando.Parameters.AddWithValue("@NOME", cliente.Nome);
            comando.Parameters.AddWithValue("@CPF", cliente.Cpf);

            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;

        }

        public Cliente ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"SELECT * FROM clientes WHERE id = @ID";

            comando.Parameters.AddWithValue("@ID", id);

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());

            if (tabela.Rows.Count == 0)
            {
                return null;
            }
            DataRow linha = tabela.Rows[0];

            Cliente cliente = new Cliente();

            cliente.IdContabilidade = Convert.ToInt32(linha["id_contabilidade"]);
            cliente.Id = Convert.ToInt32(linha["id"]);

            cliente.Cpf = linha["cpf"].ToString();
            cliente.Nome = linha["nome"].ToString();
            comando.Connection.Close();

            return cliente;
        }

        public List<Cliente> ObterTodos()
        {
            SqlCommand comando = Conexao.AbrirConexao();

            comando.CommandText = @"SELECT 
            contabilidades.id AS 'IdContabilidade',
            contabilidades.nome AS 'NomeContabilidade',
            clientes.id AS 'id',
            clientes.nome AS 'nome',
            clientes.cpf AS 'cpf' FROM clientes
            INNER JOIN contabilidades ON (clientes.id_contabilidade = contabilidades.id)           
            ";

            List<Cliente> clientes = new List<Cliente>();
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            foreach (DataRow linha in tabela.Rows)
            {
                Cliente cliente = new Cliente();

                cliente.Cpf = linha["cpf"].ToString();
                cliente.Id = Convert.ToInt32(linha["id"]);
                cliente.Nome = linha["nome"].ToString();
                cliente.IdContabilidade = Convert.ToInt32(linha["IdContabilidade"]);
                cliente.Contabilidade = new Contabilidade();
                cliente.Contabilidade.Id = Convert.ToInt32(linha["IdContabilidade"]);
                cliente.Contabilidade.Nome = linha["NomeContabilidade"].ToString();

                clientes.Add(cliente);
            }

            return clientes;




        }
    }
}
