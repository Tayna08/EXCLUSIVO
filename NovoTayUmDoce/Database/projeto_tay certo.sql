create database Projeto_Tay_bd_v2;
use Projeto_Tay_bd_v2;

create table Endereco(
id_end int primary key auto_increment,
bairro_end varchar(200) not null,
cidade_end varchar(200) not null,
rua_end varchar(200),
complemento_end varchar(200),
numero_end int,
cep_end varchar (300)
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

create table Despesa(
id_des int primary key auto_increment,
nome_des varchar(300),
descricao_des varchar(300),
forma_pag_des varchar(200),
data_des date,
hora_des varchar(10),
valor_des double,
vencimento_des date
);

create table Produto(
id_pro int primary key auto_increment,
nome_pro varchar(200),
peso_pro varchar(100),
tipo_pro varchar(300),
descricao_pro varchar(200),
valor_venda_pro double
);

create table Estoque(
id_est int primary key auto_increment,
quantidade_est int,
validade_est date,
data_fabricacao_est date,
insumos_est varchar (300),
id_pro_fk int,
foreign key (id_pro_fk) references produto(id_pro)
);

create table Pedido(
id_ped int primary key auto_increment,
data_ped date,
hora_ped varchar(100),
forma_recebimento_ped varchar(100),
quantidade_ped int,
total_ped double,
status_ped varchar(100),
id_fun_fk int,
id_cli_fk int,
id_pro_fk int,
foreign key (id_fun_fk) references funcionario (id_fun),
foreign key (id_cli_fk) references cliente (id_cli),
foreign key(id_pro_fk) references produto(id_pro)
);




insert into Endereco values (null, 'Lino Alves Teixeira', 'Médici', 'Somenzari', 'Av.Kubcheck', 3525,'920000');
insert into Endereco values (null, 'Lino Xeigar Maldrim', 'Calcoar', '21323', 'Av.Kubcheck', 312525,'12');
insert into Endereco values (null, 'Alencar Rua dos Afogados', 'Jipa', 'nazari', 'Av.kalbari', 2324,'2312313');

INSERT INTO Cliente VALUES (null, 'Cliente 1', '111.111.111-11', '1990-01-01', '1111-1111', 1);
INSERT INTO Cliente VALUES (null, 'Cliente 2', '222.222.222-22', '1995-02-15', '2222-2222', 1);
INSERT INTO Cliente VALUES (null, 'Cliente 3', '333.333.333-33', '1988-07-20', '3333-3333', 1);

INSERT INTO Funcionario VALUES (null, 'Funcionario 1', '1985-03-10', '444.444.444-44', '4444-4444', 'Cargo 1', 'funcionario1@email.com', 5000.00, 1);
INSERT INTO Funcionario VALUES (null, 'Funcionario 2', '1992-08-25', '555.555.555-55', '5555-5555', 'Cargo 2', 'funcionario2@email.com', 6000.00, 1);
INSERT INTO Funcionario VALUES (null, 'Funcionario 3', '1980-12-05', '666.666.666-66', '6666-6666', 'Cargo 3', 'funcionario3@email.com', 7000.00, 1);

INSERT INTO Produto VALUES (null,'Café Expresso', '250g', 'Bebida Quente', 'Café forte e encorpado', 3.50);
INSERT INTO Produto VALUES (null,'Cappuccino', '300g', 'Bebida Quente', 'Café, leite e espuma de leite', 4.50);
INSERT INTO Produto VALUES (null,'Bolo de Chocolate', '150g', 'Doce', 'Bolo fofinho de chocolate', 5.00);


delimiter $$ 
create procedure Campo_Cliente (nome varchar(300), cpf varchar(50), data_nascimento date, contato varchar(250), endereco_fk int)
begin
declare fkEnd int;
set fkEnd = (select id_end from endereco where (id_end = endereco_fk));

if ((nome <> '') and (cpf <> '') and (contato <> '')) then
	if(fkEnd is not null) then
		insert into Cliente values(null, nome, cpf, data_nascimento, contato, endereco_fk);
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
create procedure Campo_Despesa (nome varchar(300), descricao varchar(300), forma_pag varchar(200), dataDes varchar(25), hora time, valor double, vencimento varchar(25))
begin
if((nome <> '') and (dataDes <> '') and (valor <> '') and (vencimento <> '')) then
	insert into Despesa values (null,nome, descricao, forma_pag, dataDes, hora, valor, vencimento);
	select 'Todos os campos foram preenchidos' as Confirmação;
else
select 'Todos os campos devem ser preenchidos' as Erro;
end if;
end
$$ delimiter ;
call Campo_Despesa ('Agua','','Pix', '2022-02-02','20:00', 1000.50, '2022-05-06');


##################################################################

delimiter $$ 
create procedure Campo_Funcionario (nome varchar(200), data_nascimento varchar(25), cpf varchar(45), contato varchar(200), funcao varchar(200), email varchar(200), salario double, endereco_fk int)
begin
declare fkEnd int;
set fkEnd = (select id_end from endereco where (id_end = fk_End));

if ((nome <> '') and (cpf <> '') and (contato <> '') and (funcao <> '') and (salario <> '')) then
	if(fkEnd is not null) then
		insert into Cliente values(null, nome, cpf, data_nascimento, contato, fk_end);
		select 'Todos os campos foram preenchidos' as Confirmação;
	else
	select 'Essa fk não existe' as Erro;
    end if;
else
select 'Preencha os campos obrigatórios' as Erro;
end if;
end
$$ delimiter ;

###################################################################################
delimiter $$
create procedure Campo_Estoque (nome varchar(100), quantidade int, dataEst date, produto_fk int)
begin
declare fkPro int;
set fkPro = (select id_pro from produto where (id_pro = fk_pro));

if  ((nome <> '') and (quantidade <> '') and (data <> '')) then
	if(fkEst is not null) then
    insert into Estoque values(null, nome,  quantidade, dataEst, fk_pro);
    select 'Todos os campos foram preenchidos' as Confirmação;
    else
    select 'Essa fk não existe' as Erro;
    end if;
else
select 'preencha os campos obrigatórios' as Erro;
end if;
end
$$ delimiter ;

delimiter $$

DELIMITER $$
CREATE PROCEDURE caixa_informativo()
BEGIN

    DECLARE caixa_id int;
    DECLARE valor_entrada double;
    DECLARE valor_saida double;

    SET caixa_id = (SELECT id_cai FROM Caixa WHERE status_cai = "Aberto" ORDER BY data_cai DESC LIMIT 1);

    SET valor_entrada = (SELECT SUM(total_ppro) FROM Pedido WHERE id_cai_fk = caixa_id);
    SET valor_saida = (SELECT SUM(valor_des) FROM Despesa WHERE id_cai_fk = caixa_id);

    IF valor_entrada IS NULL THEN
        SET valor_entrada = 0.0;
    END IF;

    IF valor_saida IS NULL THEN
        SET valor_saida = 0.0;
    END IF;

    SELECT valor_entrada, valor_saida;

END;

$$ DELIMITER ;