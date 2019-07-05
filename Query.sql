SELECT
            contabilidades.id AS 'IdContabilidade',
            contabilidades.nome AS 'NomeContabilidade',
            usuarios.id AS 'id',
            usuarios.login AS 'login',
            usuarios.senha AS 'senha',
            usuarios.data_nascimento AS 'data_nascimento' FROM usuarios
            INNER JOIN contabilidades ON (usuarios.id_contabilidade = contabilidades.id);