using Model;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.DataBase;
using System.Data.SqlClient;
using System.Data;

namespace Repository.Repositories
{
    class CategoriaRepository : ICategoriaRepository

    {
        public int Inserir(Categoria categoria)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"INSERT INTO categorias
            (nome)
            VALUES
            (@NOME)";

            comando.Parameters.AddWithValue("@NOME", categoria.Nome);

            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public bool Alterar(Categoria categoria)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"UPDATE categorias SET 
            nome = @NOME
            WHERE id = @ID";
            comando.Parameters.AddWithValue("@NOME", categoria.Nome);
            comando.Parameters.AddWithValue("@ID", categoria.Id);

            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();

            return quantidade == 1;

            
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"DELETE FROM categorias WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);

            int quantidade = comando.ExecuteNonQuery();

            comando.Connection.Close();
            return quantidade == 1;
        }

        public Categoria ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"SELECT * FROM categorias";
            comando.Parameters.AddWithValue("@ID", id);

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());

            Categoria categoria = new Categoria();
            if (tabela.Rows.Count == 0)
            {
                return null;
            }
            DataRow linha = tabela.Rows[0];
            categoria.Nome = linha["nome"].ToString();
            categoria.Id = Convert.ToInt32(linha["id"]);

            return categoria;

            
        }

        public List<Categoria> ObterTodos()
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"SELECT * FROM categorias";

            List<Categoria> categorias = new List<Categoria>();

            DataTable tabela = new DataTable();
            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                DataRow linha = tabela.Rows[i];
                Categoria categoria = new Categoria();

                categoria.Id = Convert.ToInt32(linha["id"]);
                categoria.Nome = linha["nome"].ToString();
                categorias.Add(categoria);
            }

            return categorias;

        }
    }
}
