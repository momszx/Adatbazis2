create table Users
(
	id int not null ,
	name varchar2(256) not null,
	password varchar2(256) not null,
	CONSTRAINT PK_users PRIMARY KEY (id)
);
create table Logs(
    id int not null ,
    info VARCHAR2(256) not null,
    user_id int not null,
    time TIMESTAMP not null,
    CONSTRAINT PK_logs PRIMARY KEY (id),
    CONSTRAINT FK_Logs_Users_id FOREIGN KEY (user_id)REFERENCES Users(id)
);
create table tulajdonos(
    id int not null ,
    Name VARCHAR2(256),
    CONSTRAINT PK_Tulajdonos PRIMARY KEY (id)
);
create table Auto(
    id int not null ,
    tulajdonos_id int not null,
    rendszam VARCHAR2(256),
    alvazszam VARCHAR2(256),
    CONSTRAINT PK_Autok PRIMARY KEY (id),
    CONSTRAINT FK_Autok_Tulajdonos_id FOREIGN KEY (tulajdonos_id)REFERENCES TULAJDONOS(id)
);
create table tipusok(
    id int not null ,
    auto_id int not null,
    marka VARCHAR2(256),
    tipus VARCHAR2(256),
    CONSTRAINT PK_tipus PRIMARY KEY (id),
    CONSTRAINT FK_Tipus_Auto_id FOREIGN KEY (auto_id)REFERENCES Auto(id)
);
insert into VLNGFW.TULAJDONOS( Name) VALUES ('Béla');
insert into TIPUSOK( auto_id, marka, tipus) VALUES (1,'VW','POLO');

SELECT tulajdonos.id,tulajdonos.Name,Auto.id,Auto.alvazszam,Auto.rendszam,tipusok.id,tipusok.marka,tipusok.tipus from tulajdonos inner join AUTO on tulajdonos.id = Auto.tulajdonos_id inner join TIPUSOK on Auto.id = tipusok.auto_id;
SELECT id,name from Users where name='{0}' and password='{1}';

CREATE Or replace function sf_getTipusokNextId
return Tipusok.id%TYPE
AUTHID  CURRENT_USER
is
    v_next_id Tipusok.id%type;
Begin
    Select
           NVL(max(id)+1,1)
    INTO
        v_next_id
    FROM Tipusok;
    RETURN v_next_id;
end;

CREATE Or REPLACE TRIGGER tipusok_before_insert
    BEFORE INSERT
On Tipusok
fOR EACH ROW
DECLARE
     v_next_id Tipusok.id%TYPE ;
Begin
        v_next_id:=sf_getAutoNextId();
        :new.id := v_next_id;
end;
Select * From vlngfw.tulajdonos;