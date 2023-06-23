create database TayUmDoce_BD;
use TayUmDoce_BD;

create table Produto(
id_pro int not null primary key auto_increment,
descricao_pro varchar(300),
peso_pro varchar(100),
valor_pro double,
nome_pro varchar(200),
data_fabricacao_pro date,
quantidade_pro int
);

create table Despesa(
id_des int not null primary key auto_increment,
forma_des varchar(100),
data_hora_des datetime,
valor_des double,
vencimento_des date
);

create table Cliente(
id_cli int not null primary key auto_increment,
nome_cli varchar(100),
cpf_cli varchar(40),
rg_cli varchar(45),
contato_cli varchar(100),
email_cli varchar(150),
bairro_cli varchar(300), 
cidade_cli varchar(300),
complemento_cli varchar(300),
data_nasc_cli date,
rua_cli varchar(200),
numero_cli int,
cep_cli varchar(100)
);

create table Funcionario(
id_fun int not null primary key auto_increment,
cidade_fun varchar(400),
funcao_fun varchar(400),
complemento_fun varchar(400),
cpf_fun varchar(45),
salario_fun double,
rg_fun varchar(45),
nome_fun varchar(400),
contato_fun varchar(50),
bairro_fun varchar(300),
rua_fun varchar(100),
cep_fun varchar(100),
numero_fun int,
data_nasc_fun date
);

create table Caixa(
id_cai int not null primary key auto_increment,
saldo_inicial_cai double,
saldo_final_cai double,
valor_entrada_cai double,
valor_saida_cai double,
data_cai date,
hora_cai time,
id_fun_fk int not null,
foreign key (id_fun_fk) references Funcionario (id_fun)
);

create table Venda(
id_ven int not null primary key auto_increment,
valor_ven double,
forma_pagamento_ven varchar(100),
data_hora_ven date,
desconto_ven varchar(100),
id_cli_fk int not null,
foreign key (id_cli_fk) references Cliente (id_cli),
id_fun_fk int not null,
foreign key (id_fun_fk) references Funcionario (id_fun)
);

create table Recebimento(
id_rec int not null primary key auto_increment,
valor_rec double,
forma_recebimento_rec varchar(100),
quant_parcelas_rec int,
data_rec date,
id_ven_fk int not null,
foreign key (id_ven_fk) references Venda (id_ven),
id_cai_fk int not null,
foreign key (id_cai_fk) references Caixa (id_cai)
);

create table Estoque(
id_est int not null primary key auto_increment,
marca_est varchar(100),
descricao_est varchar(200),
tipo_est varchar(150),
id_pro_fk int not null,
foreign key (id_pro_fk) references Produto (id_pro)
);

create table Fornecedor(
id_for int not null primary key auto_increment,
nome_fantasia_for varchar(400),
nome_fornecedor_for varchar(400),
contato_for varchar(50),
bairro_for varchar(300), 
cidade_for varchar(300),
complemento_for varchar(300),
rua_for varchar(100),
cnpj_for varchar(100),
numero_for int,
id_est_fk int not null,
foreign key (id_est_fk) references Estoque (id_est),
id_fun_fk int not null,
foreign key (id_fun_fk) references Funcionario (id_fun)
);

create table Pagamento(
id_pag int not null primary key auto_increment,
valor_pag double,
data_pag date,
hora_pag time,
parcela_pag int,
id_cai_fk int not null,
foreign key (id_cai_fk) references Caixa (id_cai),
id_des_fk int not null,
foreign key (id_des_fk) references Despesa (id_des)
);

create table Compra(
id_com int not null primary key auto_increment,
valor_com double,
nome_com varchar(100),
data_hora_com datetime,
quantidade_com int,
descricao_com varchar(300),
id_fun_fk int not null,
foreign key (id_fun_fk) references Funcionario (id_fun),
id_des_fk int not null,
foreign key (id_des_fk) references Despesa (id_des),
id_for_fk int not null,
foreign key (id_for_fk) references Fornecedor (id_for)
);

create table Venda_has_Produto(
id_Venda_has_Produto int not null primary key auto_increment,
Venda_id_ven int not null,
foreign key (Venda_id_ven) references Venda (id_ven),
Produto_id_pro int not null,
foreign key (Produto_id_pro) references Produto (id_pro)
);

create table Entrega(
id_ent int not null primary key auto_increment,
bairro_ent varchar(450),
cidade_ent varchar(450),
rua_ent varchar(400),
data_hora_ent datetime,
valor_ent double,
complemento_ent varchar(300),
numero_ent int,
id_fun_fk int not null,
foreign key (id_fun_fk) references Funcionario (id_fun),
Venda_id_ven int not null,
foreign key (Venda_id_ven) references Venda (id_ven)
);

create table Compra_has_Produto(
id_Compra_has_Produto int not null primary key auto_increment,
Compra_id_com int not null,
foreign key (Compra_id_com) references Compra (id_com),
Produto_id_pro int not null,
foreign key (Produto_id_pro) references Produto (id_pro)
);

insert into Funcionario (id_fun, nome_fun) values (1, 'Amanda');

select * from Produto;
select * from Cliente;
select * from Funcionario;
select * from Fornecedor;
select * from Estoque;
select * from Venda;
