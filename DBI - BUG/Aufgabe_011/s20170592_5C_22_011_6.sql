clear screen
--s20170592

drop table Produkt;
drop table Produkt_Neu;

create table Produkt (
  ID   number primary key
, Name varchar (64)
);


create table Produkt_Neu (
  ID   number primary key
, Name varchar (64)
);

insert into Produkt
    (ID, Name)
          select 4711, 'Pizza Funghi' from dual
union all select 4712, 'Pizza Quattro Stagione' from dual
union all select 4713, 'Pizza Vegetale' from dual
;
/

insert into Produkt_Neu
    (ID, Name)
          select 4711, 'Pizza Funghi'           from dual
union all select 4712, 'Pizza Quattro Stagione' from dual
union all select 4713, 'Pizza Vegetale'         from dual
union all select 4714, 'Pizza Hawaii'           from dual
;
/


--s20170592
--Aufagbe 6
merge into Produkt P1
using 
(select ID, Name
  from Produkt_Neu) P2
on (P1.ID = P2.ID)
when matched then
      update set P1.Name = P2.Name
when not matched then
      insert (P1.ID, P1.Name) values (P2.ID, P2.Name)
;
/


select *
  from produkt
;
/
