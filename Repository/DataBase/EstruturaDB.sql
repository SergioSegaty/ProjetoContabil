DROP TABLE IF EXISTS contabilidades;
DROP TABLE IF EXISTS categorias;
DROP TABLE IF EXISTS usuarios;
DROP TABLE IF EXISTS clientes;
DROP TABLE IF EXISTS cartoes_credito;




-- Pais
CREATE TABLE contalibidades(

	id INT PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(45) NOT NULL
);


CREATE TABLE categorias(

	id INT PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(45) NOT NULL
);


-- Filhos


CREATE TABLE usuarios (
	id INT PRIMARY KEY IDENTITY(1,1),
	login VARCHAR(45) NOT NULL,
	senha VARCHAR(45) NOT NULL,
	data_nascimento DATETIME2,
	
	id_contabilidade INT NOT NULL,
	FOREIGN KEY (id_contabilidade) REFERENCES contabilidade(id)

);

CREATE TABLE clientes (

	id INT PRIMARY KEY IDENTITY(1,1),

	id_contabilidade INT NOT NULL,
	FOREIGN KEY (id_contabilidade) REFERENCES contabilidade(id),

	nome VARCHAR(45),
	cpf VARCHAR(14)
);

CREATE TABLE cartoes_credito(

	id INT PRIMARY KEY IDENTITY(1,1),
);