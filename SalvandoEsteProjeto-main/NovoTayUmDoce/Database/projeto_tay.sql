create database Projeto_Tay_bd;
use Projeto_Tay_bd;

create table Cliente(
id_cli int primary key not null,
nome_cli varchar(300),
cpf_cli varchar(50),
data_nascimento_cli date,
contato_cli varchar(250)
);

create table Despesa(
id_des int primary key not null,
forma_pag_des varchar(200),
data_des date,
valor_des double,
vencimento_des date
);

create table Endereco(
id_end int primary key not null,
bairro_end varchar(200),
cidade_end varchar(200),
rua_end varchar(200),
complemento_end varchar(200),
numero_end int
);

create table Funcionario(
id_fun int primary key not null,
nome_fun varchar(200),
data_nascimento_fun date,
cpf_fun varchar(100),
contato_fun varchar(200),
funcao_fun varchar(200),
email_fun varchar(200),
salario_fun double,
id_end_fk int,
foreign key(id_end_fk) references Endereco (id_end)
);

create table Pedido(
id_ped int primary key not null,
data_ped date,
quantidade_ped int,
valor_ped double,
forma_pagamento varchar(100),
tipo_doce_ped varchar(100),
id_fun_fk int,
id_cli_fk int,
foreign key (id_fun_fk) references funcionario (id_fun),
foreign key (id_cli_fk) references cliente (id_cli)
);

create table Produto(
id_pro int primary key not null,
controle_numero_pro int,
peso_pro varchar(100),
valor_pro double,
nome_pro varchar(200),
quantidade_pro int,
data_fabricacao_pro date,
descricao_pro varchar(200),
id_ped_fk int,
foreign key (id_ped_fk) references Pedido (id_ped)
);

create table Estoque(
id_est int primary key not null,
nome_est varchar(100),
quantidade_est int,
data_est date,
id_pro_fk int,
foreign key (id_pro_fk) references produto(id_pro)
);

create table Fornecedor(
id_for int primary key not null,
nome_fantasia_for varchar(200),
nome_representante_for varchar(200),
contato_for varchar(200),
cnpj_for varchar(200),
razao_social_for varchar(200),
id_end_fk int,
id_est_fk int,
foreign key (id_end_fk) references Endereco (id_end),
foreign key (id_est_fk) references Estoque (id_est)
);

create table Compra(
id_com int primary key not null,
valor_com double,
nome_com varchar(200),
data_com date,
quantidade_com int,
descricao_com varchar(200),
id_fun_fk int,
id_des_fk int, 
id_for_fk int,
foreign key (id_fun_fk) references Funcionario (id_fun),
foreign key (id_des_fk) references Despesa (id_des),
foreign key (id_for_fk) references Fornecedor (id_for)
);

create table Venda(
id_ven int primary key not null,
valor_ven double,
forma_pagamento_ven varchar(200),
data_ven date,
desconto_ven varchar(200),
id_cli_fk int,
id_fun_fk int,
foreign key (id_cli_fk) references Cliente(id_cli),
foreign key (id_fun_fk) references Funcionario(id_fun)
);

create table Entrega(
id_ent int primary key not null,
controle_numero_ent int,
entregador_ent varchar(200),
data_ent date,
valor_ent double,
id_ven_fk int,
id_end_fk int,
id_fun_fk int,
foreign key(id_ven_fk) references Venda(id_ven),
foreign key(id_end_fk) references Endereco(id_end),
foreign key(id_fun_fk) references Funcionario(id_fun)
);

create table Caixa(
id_cai int primary key not null,
saldo_inicial_cai double,
saldo_final_cai double,
valor_entrada_cai double,
valor_saida_cai double,
data_cai double,
id_fun_fk int,
foreign key(id_fun_fk) references Funcionario(id_fun)
);

create table Pagamento(
id_pag int primary key not null,
valor_pag double,
data_pag date,
parcela_pag varchar(50),
id_des_fk int,
id_cai_fk int,
foreign key (id_des_fk) references Despesa(id_des),
foreign key (id_cai_fk) references Caixa(id_cai)
);

create table Recebimento(
id_rec int primary key not null,
valor_rec double,
forma_recebimento_rec varchar(200),
quant_parcelas_rec int,
data_rec date,
id_ven_fk int,
id_cai_fk int,
foreign key (id_ven_fk) references Venda(id_ven),
foreign key (id_cai_fk) references Caixa(id_cai)
);

create table Compra_Produto(
id_comp int primary key not null,
id_com_fk int,
id_pro_fk int,
foreign key (id_com_fk) references Compra(id_com),
foreign key (id_pro_fk) references Produto(id_pro)
);

create table Venda_Produto(
id_venp int primary key not null,
id_ven_fk int,
id_pro_fk int,
foreign key (id_ven_fk) references Venda(id_ven),
foreign key (id_pro_fk) references Produto(id_pro)
);