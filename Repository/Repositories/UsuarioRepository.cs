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
    public class UsuarioRepository : IUsuarioRepository
    {
        public bool Alterar(Usuario usuario)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "UPDATE usuarios SET login = @LOGIN, senha = @SENHA, data_nascimento = @DATA_NASCIMENTO, id_contabilidade = @ID_CONTABILIDADE WHERE id = @ID";
            comando.Parameters.AddWithValue("@LOGIN", usuario.Login);
            comando.Parameters.AddWithValue("@SENHA", usuario.Senha);
            comando.Parameters.AddWithValue("@DATA_NASCIMENTO", usuario.DataNascimento);
            comando.Parameters.AddWithValue("@ID_CONTABILIDADE", usuario.IdContabilidade);
            comando.Parameters.AddWithValue("@ID", usuario.Id);
            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "DELETE FROM usuarios WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;
        }

        public int Inserir(Usuario usuario)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"INSERT INTO usuarios 
            (login, senha, data_nascimento, id_contabilidade)
            OUTPUT INSERTED.ID VALUES 
            (@LOGIN, @SENHA, @DATA_NASCIMENTO, @ID_CONTABILIDADE)";

            comando.Parameters.AddWithValue("@LOGIN", usuario.Login);
            comando.Parameters.AddWithValue("@SENHA", usuario.Senha);
            comando.Parameters.AddWithValue("@DATA_NASCIMENTO", usuario.DataNascimento);
            comando.Parameters.AddWithValue("@ID_CONTABILIDADE", usuario.IdContabilidade);

            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public Usuario ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "SELECT * FROM usuarios WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            if (tabela.Rows.Count == 0)
            {
                return null;
            }
            DataRow linha = tabela.Rows[0];
            Usuario usuario = new Usuario();
            usuario.Login = linha["login"].ToString();
            usuario.Senha = linha["senha"].ToString();
            DateTime dataNascimento = new DateTime();
            dataNascimento = Convert.ToDateTime(linha["data_nascimento"]);
            usuario.DataNascimento = dataNascimento;
            usuario.IdContabilidade = Convert.ToInt32(linha["id_contabilidade"]);
            usuario.Id = Convert.ToInt32(linha["id"]);
            return usuario;
        }

        public List<Usuario> ObterTodos()
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"SELECT
            contabilidades.id AS 'IdContabilidade',
            contabilidades.nome AS 'NomeContabilidade',
            usuarios.id AS 'id',
            usuarios.login AS 'login',
            usuarios.senha AS 'senha',
            usuarios.data_nascimento AS 'data_nascimento' FROM usuarios
            INNER JOIN contabilidades ON (usuarios.id_contabilidade = contabilidades.id)
            ";
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            List<Usuario> usuarios = new List<Usuario>();
            foreach (DataRow linha in tabela.Rows)
            {
                Usuario usuario = new Usuario();
                usuario.Login = linha["login"].ToString();
                usuario.Senha = linha["senha"].ToString();
                usuario.DataNascimento = Convert.ToDateTime(linha["data_nascimento"]);
                usuario.IdContabilidade = Convert.ToInt32(linha["IdContabilidade"]);

                usuario.Id = Convert.ToInt32(linha["id"]);

                usuario.Contabilidade = new Contabilidade();
                usuario.Contabilidade.Nome = linha["NomeContabilidade"].ToString();
                usuario.Contabilidade.Id = Convert.ToInt32(linha["IdContabilidade"]);
                usuarios.Add(usuario);
            }
            return usuarios;
        }
    }
}
