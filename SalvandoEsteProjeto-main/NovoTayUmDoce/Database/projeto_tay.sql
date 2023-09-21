create database Projeto_Tay_bd;
use Projeto_Tay_bd;

create table Endereco(
id_end int primary key auto_increment,
bairro_end varchar(200) not null,
cidade_end varchar(200) not null,
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
if ((bairro <> '' ) and (cidade <> '') and (rua <> '') and (numero <> '')) then
	insert into Endereco values(null, bairro, cidade, rua, complemento, numero);
	select 'Os campos obrigatórios foram preenchidos' as Confirmação;
else
	select 'Os campos obrigatórios devem ser preenchidos' as Erro;
end if;
end;
$$ delimiter ;
call Campo_Endereco('Ernandes Gonçalves', 'Médici' , 'Ji-Paraná', 'Avenida', 2431);
select * from Endereco;

############################################################

delimiter $$ 
create procedure Campo_Cliente (nome varchar(300), cpf varchar(50), data_nascimento date, contato varchar(250), fk_end int)
begin
declare fkEnd int;
set fkEnd = (select id_end from endereco where (id_end = fk_end));

if ((nome <> '') and (cpf <> '') and (contato <> '')) then
	if(fkEnd is not null) then
		insert into Cliente values(null, nome, cpf, data_nascimento, contato, fk_end);
		select 'Todos os campos foram preenchidos' as Confirmação;
	else
	select 'Essa fk não existe' as Erro;
    end if;
else
select 'Preencha os campos obrigatórios' as Erro;
end if;
end;
$$ delimiter ;
call Campo_Cliente ('Samara Hespanhol', '022.420.402-51', '2023-01-01', '69992096461', 1);

###############################################################

delimiter $$ 
create procedure Campo_Despesa (forma_pag varchar(200), dataDes varchar(25), valor double, vencimento varchar(25))
begin
if((forma_pag <> '') and (dataDes <> '') and (valor <> '') and (vencimento <> '')) then
	insert into Despesa values (null, forma_pag, dataDes, valor, vencimento);
	select 'Todos os campos foram preenchidos' as Confirmação;
else
select 'Todos os campos devem ser preenchidos' as Erro;
end if;
end
$$ delimiter ;
call Campo_Despesa ('Pix', '2022-02-02', 1000.50, '2022-05-06');


##################################################################

delimiter $$ 
create procedure Campo_Funcionario (nome varchar(200), data_nascimento varchar(25), cpf varchar(45), contato varchar(200), funcao varchar(200), email varchar(200), salario double, endereco_fk int)
begin
declare endfk int;
set endfk = (select id_end from endereco where (id_end = endereco_fk));

if ((nome <> '') and (cpf <> '') and (contato <> '') and (funcao <> '') and (salario <> '')) then
	if(endfk is not null) then
		insert into Funcionario values(null, nome, data_nascimento, cpf, contato, funcao, email, salario, endereco_fk);
		select 'Todos os campos foram preenchidos' as Confirmação;
	else
	select 'Essa fk não existe' as Erro;
    end if;
else
select 'Preencha os campos obrigatórios' as Erro;
end if;
end;
$$ delimiter ;
call Campo_Funcionario ('Tayná', '2022-01-01', '02242040251', '69992096461', 'Gerente', 'tayna@gmail.com', 2150.40, 1);

#####################################################################

delimiter $$
create procedure Campo_Pedido (dataP varchar(25), quantidade int, valor double, pagamento varchar(100), tipo varchar(150), funcionario_fk int, cliente_fk int)
begin 
declare fun_fk int;
declare cli_fk int;

set cli_fk = (select id_cli from cliente where (id_cli = cliente_fk));
set fun_fk = (select id_fun from funcionario where (id_fun = funcionario_fk));

if((dataP <> '') and (valor <> '')) then
	if(funcionario_fk <> '') or (funcionario_fk is not null) then
		if(cliente_fk <> '') or (cliente_fk is not null) then 
			insert into Pedido values (null, dataP, quantidade, valor, pagamento, tipo, funcionario_fk, cliente_fk);
            select 'O cadastro foi realizado com sucesso' as Erro;
        else
        select 'A fk de cliente não existe' as Erro;
        end if;
    
    else
    select 'A fk do funcionario não existe' as Erro;
    end if;

else
select 'Os campos obrigatórios não foram preenchidos' as Erro;
end if;
end;
$$ delimiter ;
call Campo_Pedido ('2022-01-01', 32, 5.50, 'Pix', 'Brigadeiro', 1, 1);

###############################################################

delimiter $$
create procedure Campo_Produto (controle int, peso double, valor double, nome varchar(150), quantidade int, dataPro varchar(25), descricao varchar(200), pedido_fk int)
begin

declare fkPed int;
set fkPed = (select id_ped from Pedido where (id_ped = pedido_fk));

if((nome <> '') and (quantidade <> '') and (controle <> '') and (dataPro <> '') and (peso <> '')) then 
	if(fkPed <> '') or (fkPed is not null) then 
		insert into Produto values (null, controle, peso, valor, nome, quantidade, dataPro, descricao, pedido_fk);
        select 'cadastro realizado com sucesso' as Erro;
	else 
    select 'essa fk não existe' as Erro;
    end if;
else
select 'Os campos obrigatórios devem ser preenchidos' as Erro;
end if;
end;
 $$ delimiter ;
 call Campo_Produto(001, 21.100, 5.5, 'Bolo', 3, '2022-01-01', 'Bolo com cobertura de brigadeiro', 1);
  
##############################################################

delimiter $$
create procedure Campo_Estoque (nome varchar(100), quantidade int, dataEst varchar(25), produto_fk int)
begin
declare fkPro int;
set fkPro = (select id_pro from produto where (id_pro = produto_fk));

if((nome <> '') and (quantidade <> '') and (dataEst <> '')) then
	if(fkPro is not null) or (fkPro <> '')then
    insert into Estoque values(null, nome,  quantidade, dataEst, produto_fk);
    select 'Todos os campos foram preenchidos' as Confirmação;
    
    else
    select 'Essa fk não existe' as Erro;
    end if;
else
select 'preencha os campos obrigatórios' as Erro;
end if;
end
$$ delimiter ;
call Campo_Estoque('Leite Condensado', 2, '2022-01-01', 1);

delimiter $$ 
create procedure Campo_Fornecedor (nome_fantasia varchar(200), nome_representante varchar(200), contato varchar(200), cnpj varchar(200), 
razao_social varchar(200), endereco_fk int, estoque_fk int)
begin
declare fkEnd int;
declare fkEst int;
set fkEnd = (select id_end from endereco where (id_end = endereco_fk));
set fkEst = (select id_est from estoque where (id_est = estoque_fk));

if((nome_fantasia <>'') and (nome_representante <>'') and (contato <>'') and (cnpj <> '')) then 
	if((fkEnd is not null) or (fkEnd <> '')) then
		if((fkEst is not null) or (fkEst <> '')) then 
			insert fornecedor values(null, nome_fantasia, nome_representante, contato, cnpj, razao_social, endereco_fk, estoque_fk);
			select 'Todos os campos foram preenchidos' as Confirmação;
		else
		select 'A fk de estoque não existe' as Erro;
		end if;
	else
    select 'A fk de endereco não existe' as Erro;
    end if;
else
select 'preencha os campos obrigatórios' as Erro;
end if;
end;
$$ delimiter ;
call Campo_Fornecedor('Maria Helena Industria LTDA', 'Silvania Ribeiro', '69992096461', '001.457

