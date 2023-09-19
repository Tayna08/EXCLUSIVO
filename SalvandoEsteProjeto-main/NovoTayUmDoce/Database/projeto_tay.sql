create database Projeto_Tay_bd;
use Projeto_Tay_bd;

create table Endereco(
id_end int primary key auto_increment,
bairro_end varchar(200),
cidade_end varchar(200),
rua_end varchar(200),
complemento_end varchar(200),
numero_end int
);

create table Cliente(
id_cli int primary key auto_increment,
nome_cli varchar(300),
cpf_cli varchar(50),
data_nascimento_cli date,
contato_cli varchar(250),
id_end_fk int, 
foreign key (id_end_fk) references Endereco(id_end)
);

create table Despesa(
id_des int primary key auto_increment,
forma_pag_des varchar(200),
data_des date,
valor_des double,
vencimento_des date
);

create table Funcionario(
id_fun int primary key auto_increment,
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
id_ped int primary key auto_increment,
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
id_pro int primary key auto_increment,
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
id_est int primary key auto_increment,
nome_est varchar(100),
quantidade_est int,
data_est date,
id_pro_fk int,
foreign key (id_pro_fk) references produto(id_pro)
);

create table Fornecedor(
id_for int primary key auto_increment,
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
id_com int primary key auto_increment,
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
id_ven int primary key auto_increment,
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
id_ent int primary key auto_increment,
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
id_cai int primary key auto_increment,
saldo_inicial_cai double,
saldo_final_cai double,
valor_entrada_cai double,
valor_saida_cai double,
data_cai double,
id_fun_fk int,
foreign key(id_fun_fk) references Funcionario(id_fun)
);

create table Pagamento(
id_pag int primary key auto_increment,
valor_pag double,
data_pag date,
parcela_pag varchar(50),
id_des_fk int,
id_cai_fk int,
foreign key (id_des_fk) references Despesa(id_des),
foreign key (id_cai_fk) references Caixa(id_cai)
);

create table Recebimento(
id_rec int primary key auto_increment,
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
id_comp int primary key auto_increment,
id_com_fk int,
id_pro_fk int,
foreign key (id_com_fk) references Compra(id_com),
foreign key (id_pro_fk) references Produto(id_pro)
);

create table Venda_Produto(
id_venp int primary key auto_increment,
id_ven_fk int,
id_pro_fk int,
foreign key (id_ven_fk) references Venda(id_ven),
foreign key (id_pro_fk) references Produto(id_pro)
);


insert into Endereco values (null, 'Lino Alves Teixeira', 'Médici', 'Somenzari', 'Av.Kubcheck', 3525);



delimiter $$ 
create procedure Campo_Endereco (bairro varchar(200), cidade varchar(200), rua varchar(200), complemento varchar(200), numero int)
begin
if ((bairro <> '') and (cidade <> '') and (rua <> '') and (complemento <> '') and (numero <> '')) then
	insert into Endereco values(null, bairro, cidade, rua, complemento, numero);
	select 'Todos os campos foram preenchidos' as Confirmação;
else
	select 'Todos os campos devem ser preenchidos' as Erro;
end if;
end
$$ delimiter ;

delimiter $$ 
create procedure Campo_Cliente (nome varchar(300), cpf varchar(50), data_nascimento date, contato varchar(250), fk_end int)
begin
declare fkEnde int;
set fkEnd=(select id_end from endereco where (id_end= fk_end));

if ((nome <> '') and (cpf <> '') and (data_nascimento <> '') and (contato <> '') and (fkEnd <> '')) then
	insert into Cliente values(null, nome, cpf, data_nascimento, contato, fkEnd);
	select 'Todos os campos foram preenchidos' as Confirmação;
else
	select 'Todos os campos devem ser preenchidos' as Erro;
end if;
end
$$ delimiter ;

delimiter $$ 
create procedure Campo_Despesa (forma_pag varchar(200), dataDes date, valor double, vencimento date)
begin
if((forma <> ''), (dataDes <> ''), (valor <> ''), (vencimento <> '')) then
	insert into Despesa values (null, forma, dataDes, valor, vencimento);
	select 'Todos os campos foram preenchidos' as Confirmação;
else
select 'Todos os campos devem ser preenchidos' as Erro;
end if;
end
$$ delimiter ;

delimiter $$ 
create procedure Campo_Funcionario (nome varchar(200), data_nascimento date, cpf varchar(45), contato varchar(200), funcao varchar(200), email varchar(200), salario double, fk_end int)
begin
if ((nome <> ''), (data_nascimento <> ''), (cpf <> ''), (contato <> ''), (funcao <> ''), (email <> ''), (salario <> ''), (fk_end)) then

else

end if;
end
$$ delimiter ;