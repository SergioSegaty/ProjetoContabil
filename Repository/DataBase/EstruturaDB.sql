DROP TABLE IF EXISTS contas_receber;
DROP TABLE IF EXISTS contas_pagar;
DROP TABLE IF EXISTS compras;
DROP TABLE IF EXISTS cartoes_credito;
DROP TABLE IF EXISTS clientes;
DROP TABLE IF EXISTS usuarios;
DROP TABLE IF EXISTS categorias;
DROP TABLE IF EXISTS contabilidades;




-- Pais
CREATE TABLE contabilidades(

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
	FOREIGN KEY (id_contabilidade) REFERENCES contabilidades(id)

);

CREATE TABLE clientes (

	id INT PRIMARY KEY IDENTITY(1,1),

	id_contabilidade INT NOT NULL,
	FOREIGN KEY (id_contabilidade) REFERENCES contabilidades(id),

	nome VARCHAR(45),
	cpf VARCHAR(14)
);

CREATE TABLE cartoes_credito(

	id INT PRIMARY KEY IDENTITY(1,1),

	id_cliente INT,
	FOREIGN KEY (id_cliente) REFERENCES clientes(id),

	numero VARCHAR(16),
	data_vencimento DATETIME2,
	cvv VARCHAR(3)
);

CREATE TABLE compras (
	id INT PRIMARY KEY IDENTITY(1,1),

	id_cartao_credito INT,
	FOREIGN KEY (id_cartao_credito) REFERENCES cartoes_credito(id),

	valor DECIMAL(10,2),
	data_compra DATETIME
);

-- Netos

CREATE TABLE contas_pagar(
	id INT PRIMARY KEY IDENTITY(1,1),

	id_cliente INT,
	FOREIGN KEY (id_cliente) REFERENCES clientes(id),

	id_categoria INT,
	FOREIGN KEY (id_categoria) REFERENCES categorias(id),

	nome VARCHAR(45),
	data_vencimento DATETIME2,
	data_pagamento DATETIME2,
	valor DECIMAL(10,2)

);


CREATE TABLE contas_receber(

	id INT PRIMARY KEY IDENTITY(1,1),

	id_cliente INT,
	FOREIGN KEY (id_cliente) REFERENCES clientes(id),

	id_categoria INT,
	FOREIGN KEY (id_categoria) REFERENCES categorias(id),
	
	nome VARCHAR(45),
	data_pagamento DATETIME2,
	valor DECIMAL(8,2)
);

SELECT * FROM contas_pagar

SELECT * FROM categorias


