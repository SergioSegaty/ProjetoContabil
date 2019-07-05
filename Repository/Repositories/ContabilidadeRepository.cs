using Model;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.DataBase;

namespace Repository.Repositories
{
    public class ContabilidadeRepository : IContabilidadeRepository
    {
        public bool Alterar(Contabilidade contabilidade)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "UPDATE contabilidades SET nome = @NOME WHERE id = @ID";
            comando.Parameters.AddWithValue("@NOME", contabilidade.Nome);
            comando.Parameters.AddWithValue("ID", contabilidade.Id);
            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "DELETE FROM contabilidades WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;
        }

        public int Inserir(Contabilidade contabilidade)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "INSERT INTO contabilidades (nome) OUTPUT INSERTED.ID VALUES (@NOME)";
            comando.Parameters.AddWithValue("@NOME", contabilidade.Nome);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public Contabilidade ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "SELECT * FROM contabilidades WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            if (tabela.Rows.Count == 0)
            {
                return null;
            }
            DataRow linha = tabela.Rows[0];
            Contabilidade contabilidade = new Contabilidade();
            contabilidade.Id = Convert.ToInt32(linha["id"]);
            contabilidade.Nome = linha["nome"].ToString();
            return contabilidade;
        }

        public List<Contabilidade> ObterTodos()
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "SELECT * FROM contabilidades";
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            List<Contabilidade> contabilidades = new List<Contabilidade>();
            foreach(DataRow linha in tabela.Rows)
            {
                Contabilidade contabilidade = new Contabilidade();

                contabilidade.Id = Convert.ToInt32(linha["id"]);
                contabilidade.Nome = linha["nome"].ToString();
                
                contabilidades.Add(contabilidade);
            }
            return contabilidades;

        }

    }
}
