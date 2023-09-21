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
data_cai ,
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
call Campo_Endereco('Ernandes Gonçalves', 'Médici' , 'Ji-Paraná', 'Avenida', 5281);
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
call Campo_Cliente ('Kaick Damas dos Santos', '022.420.402-51', '2023-01-01', '69992096461', 1);

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
call Campo_Despesa ('Crédito', '2022-02-02', 1000.50, '2022-05-06');


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
call Campo_Funcionario ('Gilmar', '2022-01-01', '02242040251', '69992096461', 'Gerente', 'tayna@gmail.com', 2150.40, 1);

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
            select 'O cadastro foi realizado com sucesso' as Confirmação;
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
call Campo_Pedido ('2022-03-03', 32, 5.50, 'Pix', 'Brigadeiro', 1, 1);

###############################################################

delimiter $$
create procedure Campo_Produto (controle int, peso double, valor double, nome varchar(150), quantidade int, dataPro varchar(25), descricao varchar(200), pedido_fk int)
begin

declare fkPed int;
set fkPed = (select id_ped from Pedido where (id_ped = pedido_fk));

if((nome <> '') and (quantidade <> '') and (controle <> '') and (dataPro <> '') and (peso <> '')) then 
	if(fkPed <> '') or (fkPed is not null) then 
		insert into Produto values (null, controle, peso, valor, nome, quantidade, dataPro, descricao, pedido_fk);
        select 'cadastro realizado com sucesso' as Confirmação;
	else 
    select 'essa fk não existe' as Erro;
    end if;
else
select 'Os campos obrigatórios devem ser preenchidos' as Erro;
end if;
end;
 $$ delimiter ;
 call Campo_Produto(004, 21.100, 5.5, 'Bolo', 3, '2022-01-01', 'Bolo com cobertura de brigadeiro', 1);
  
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
call Campo_Estoque('Leite Condensado', 10, '2023-01-01', 1);

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
call Campo_Fornecedor('Maria Helena Industria LTDA', 'Roberta', '69992096461', '001.457.144215/0004', 'Maria Helena', 1, 1);

########################################################

delimiter $$
create procedure Campo_Compra(valor double, nome varchar(200), dataCom varchar(25), quantidade int, descricao varchar(300), funcionario_fk int, despesa_fk int, fornecedor_fk int)
begin 
declare fkFun int;
declare fkDes int;
declare fkFor int;
set fkFun = (select id_fun from Funcionario where (id_fun = funcionario_fk));
set fkDes = (select id_des from Despesa where (id_des = despesa_fk));
set fkFor = (select id_for from Fornecedor where (id_for = fornecedor_fk));

if((valor <> '') and (dataCom <> '') and (descricao <> '')) then
	if(fkFun is not null) then
		if(fkDes is not null) then
			if(fkFor is not null) then 
				insert into Compra values(null, valor, nome, dataCom, quantidade, descricao, funcionario_fk, despesa_fk, fornecedor_fk);
                select 'O cadastro foi realizado com sucesso' as Confirmação;
            else
            select 'A fk de fornecedor não existe' as Erro;
            end if;
		else
		select 'A fk de despesa não existe' as Erro;
        end if;
    else
    select 'A fk de funcionario não existe' as Erro;
    end if;
else
select 'Os campos obrigatórios precisam ser preenchidos' as Erro;
end if;
end;
$$ delimiter ;
call Campo_Compra(100.50, 'Caixas de leite', '2022-01-01', 30, 'Caixas de leite contendo 2 litros cada um', 1, 1, 1);

#####################################################

delimiter $$
create procedure Campo_Venda(valor double, pagamento varchar(150), dataVen varchar(25), desconto varchar(25), cliente_fk int, funcionario_fk int)
begin
declare fkCli int;
declare fkFun int;
set fkCli = (select id_cli from Cliente where (id_cli = cliente_fk));
set fkFun = (select id_fun from Funcionario where (id_fun = funcionario_fk));

if((valor <> '') and (pagamento <> '') and (dataVen <> '')) then
	if(fkCli is not null) then
		if(fkFun is not null) then 
			 insert into Venda values (null, valor, pagamento, dataVen, desconto, cliente_fk, funcionario_fk);
             select 'O cadastro foi realizado com sucesso' as Confirmação;
        else
        select 'A fk de funcionario não existe' as Erro;
        end if;
	else
    select 'A fk de cliente não existe' as Erro;
    end if;
else
select 'Os campos obrigatórios precisam estar preenchidos' as Erro;
end if;
end;
$$ delimiter ;
call Campo_Venda(60.60, 'Pix', '2022-01-01', '20%', 1, 1);

######################################################

delimiter $$
create procedure Campo_Entrega (controle_numero int, entregador varchar(200), dataEnt varchar(100), valor double, venda_fk int, endereco_fk int, funcionario_fk int)
begin
declare fkVen int;
declare fkEnd int;
declare fkFun int;
set fkVen = (select id_ven from venda where (id_ven = venda_fk));
set fkEnd = (select id_end from endereco where (id_end = endereco_fk));
set fkFun = (select id_fun from funcionario where (id_fun = funcionario_fk));

if((entregador <>'') and (dataEnt <>'') and (valor <>'')) then
	if(fkVen is not null) then 
		if(fkEnd is not null) then 
			if(fkFun is not null) then 
				insert entrega values (null, controle_numero, entregador, dataEnt, valor, venda_fk, endereco_fk, funcionario_fk);
				select 'O cadastro foi realizado com sucesso' as Confirmação;
			else
			select 'A fk de funcionario não existe' as Erro;
			end if;
		else
		select 'A fk de endereço não existe' as Erro;
		end if;
    else
    select 'A fk de venda não existe' as Erro;
    end if;
else
select 'preencha os campos obrigatórios' as Erro;
end if;
end
$$ delimiter ;
call Campo_Entrega(003, 'Roberto', '2022-01-01', 100.50, 1, 1, 1);

####################################################

delimiter $$
create procedure Campo_Caixa(saldo_inicial double, saldo_final double, valor_entrada double, valor_saida double, dataCai varchar(100), funcionario_fk int)
begin
declare fkFun int;
set fkFun = (select id_fun from funcionario where (id_fun = funcionario_fk));

if((saldo_inicial <> '') and (valor_entrada <> '') and (dataCai <> '')) then
	if(fkFun is not null) then
		insert into caixa values (null, saldo_inicial, saldo_final, valor_entrada, valor_saida, dataCai, funcionario_fk);
		select 'Todos os campos foram preenchidos' as Confirmação;
    else
    select 'Essa fk não existe' as Erro;
    end if;
else
select 'preencha os campos obrigatórios' as Erro;
end if;
end
$$ delimiter ;
call Campo_Caixa(1000, 5000, 500, 502, '2022-01-01', 1);

######################################################

delimiter $$
create procedure Campo_Pagamento (valor double, dataPag varchar(25), parcela varchar(100), despesa_fk int, caixa_fk int) 
begin
declare fkDes int;
declare fkCai int;
set fkDes = (select id_des from despesa where (id_des = despesa_fk));
set fkCai = (select id_cai from caixa where (id_cai = caixa_fk));

if((valor <> '') and (dataPag <> '') and (parcela <> '')) then
	if((fkDes is not null) or (fkDes <> ''))then
		if((fkCai is not null) or (fkCai <> ''))then
			insert into pagamento values(null, valor, dataPag, parcela, despesa_fk, caixa_fk);
            select 'Todos os campos foram preenchidos' as Confirmação;
		else
        select 'A fk de Caixa não existe' as Erro;
        end if;
	else
    select 'A fk de Despesa não existe' as Erro;
	end if;
else
select 'Preencha os Campos obrigatórios' as Erro;
end if;
end;
$$ delimiter ;
call Campo_Pagamento (100.05, '2022-01-01', '8', 1, 1);

#######################################################

delimiter $$
create procedure Campo_Recebimento (valor double, forma_recebimento varchar(200), quant_parcelas varchar(200), data_recebimento varchar(25), venda_fk int, caixa_fk int)
begin
declare fkVen int;
declare fkCai int;
set fkVen = (select id_ven from Venda where (id_ven = venda_fk));
set fkCai = (select id_cai from Caixa where (id_cai = caixa_fk));

if((valor <> '') and (forma_recebimento <> '') and (quant_parcelas <> '') and (data_recebimento <> '')) then
	if(fkVen <> '') or (fkVen is not null) then
		if(fkCai <> '') or (fkCai is not null) then
		insert into Recebimento values(null, valor, forma_recebimento, quant_parcelas, data_recebimento, venda_fk, caixa_fk);
		select 'Recebimento realizado com sucesso' as Erro;
       
       else
        select 'A fk de caixa não existe' as Erro;
	   end if;
	else
	select 'A fk de venda não existe' as Erro;
	end if;
else
select 'Os campos obrigatórios não foram preenchidos' as Erro;
end if;
end;

$$ delimiter ;
call Campo_Recebimento (5000.00, 'Pix', 10, '2022-01-01', 1, 1);

#########################################

delimiter $$ 
create procedure Campo_Compra_Produto (compra_fk int, produto_fk int)
begin
declare fkCom int;
declare fkPro int;
set fkCom = (select id_com from Compra where (id_com = compra_fk));
set fkPro = (select id_pro from Produto where (id_pro = produto_fk));

if((fkCom is not null) or (fkCom <> '')) then
	if((fkPro is not null) or (fkPro <> ''))then
		insert Compra_Produto values(null, compra_fk, produto_fk);
		select 'Todos os campos foram preenchidos' as Confirmação;
    else 
    select 'A fk de produto não existe' as Erro;
    end if;
else
select 'A fk de compra não existe' as Erro;
end if;
end
$$ delimiter ;
call Campo_Compra_Produto(2,2);

###################################################

delimiter $$
create procedure Campo_Venda_Produto (venda_fk int, produto_fk int)
begin
declare fkVen int;
declare fkPro int;

set fkVen = (select id_ven from Venda where (id_ven = venda_fk));
set fkPro = (select id_pro from Produto where (id_pro = produto_fk));

if((fkVen is not null) or (fkVen <> '')) then
	if((fkPro is not null) or (fkPro <> '')) then
		insert into Venda_Produto values(null, venda_fk, produto_fk);
        select 'Todos os campos foram preenchidos' as Confirmação;
	else 
	select 'A fk de Produto não existe' as Erro;
	end if;
else
select 'A fk de Venda não existe' as Erro;
end if;
end;
$$ delimiter ;
call Campo_Venda_Produto(1,3);