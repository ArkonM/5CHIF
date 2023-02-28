clear screen
set serveroutput on size unlimited
set feedback off
  
  
/*  
drop table SupplierParts;
drop table Parts;
drop table Suppliers;

create table Suppliers(
  SupplierID       char(2)      not null primary key
, SupplierName     char(6)
, SupplierCity     char(6)
, SupplierDiscount number(5, 2) not null 
);

create table Parts(
  PartID    char(2)      not null primary key
, PartName  char(8)      not null
, PartColor char(5)      not null
, PartPrice number(8, 2) not null
, PartCity  char(6)      not null  
);

create table SupplierParts(
  SupplierID char(2)   not null references Suppliers(SupplierID)
, PartID     char(2)   not null references Parts(PartID)
, Amount     number(4) not null
, primary key (SupplierID, PartID)
);


clear screen
set feedback off

delete from SupplierParts;
delete from Suppliers;
delete from Parts;

insert into Suppliers 
                (SupplierID, SupplierName, SupplierCity, SupplierDiscount)
        select  'L1'      , 'Schmid'    , 'London'    ,               20 from dual
  union select  'L2'      , 'Jonas'     , 'Paris'     ,               10 from dual
  union select  'L3'      , 'Berger'    , 'Paris'     ,               30 from dual
  union select  'L4'      , 'Klein'     , 'London'    ,               20 from dual
  union select  'L5'      , 'Adam'      , 'Athen'     ,               30 from dual
;

insert into Parts
                (PartID, PartName  , PartColor, PartPrice, PartCity)
        select  'T1'  , 'Mutter'  , 'rot'    ,        12, 'London' from dual
  union select  'T2'  , 'Bolzen'  , 'gelb'   ,        17, 'Paris'  from dual
  union select  'T3'  , 'Schraube', 'blau'   ,        17, 'Rom'    from dual
  union select  'T4'  , 'Schraube', 'rot'    ,        14, 'London' from dual
  union select  'T5'  , 'Welle'   , 'blau'   ,        12, 'Paris'  from dual
  union select  'T6'  , 'Zahnrad' , 'rot'    ,        19, 'London' from dual
;

insert into SupplierParts
                (SupplierID, PartID, Amount)
        select  'L1'      , 'T1'  ,    300 from dual
  union select  'L1'      , 'T2'  ,    200 from dual
  union select  'L1'      , 'T3'  ,    400 from dual
  union select  'L1'      , 'T4'  ,    200 from dual
  union select  'L1'      , 'T5'  ,    100 from dual
  union select  'L1'      , 'T6'  ,    100 from dual
  union select  'L2'      , 'T1'  ,    300 from dual
  union select  'L2'      , 'T2'  ,    400 from dual
  union select  'L3'      , 'T2'  ,    200 from dual
  union select  'L4'      , 'T2'  ,    200 from dual
  union select  'L4'      , 'T4'  ,    300 from dual
  union select  'L4'      , 'T5'  ,    400 from dual
;

commit;
*/
/*
--S20170592
--Aufgabe 1
create or replace trigger parts_aft_ins_upd_row
after insert or update
on parts
for each row
begin
  if inserting then
    dbms_output.put_line('Yikes! Insert on table parts detected.');
  else
    dbms_output.put_line('Hey, update on table parts detected.');
  end if;
end;
/
show errors

insert into Parts
                (PartID, PartName  , PartColor, PartPrice, PartCity)
        select  'T7'  , 'Mutter'  , 'blau'    ,        14, 'Paris' from dual
;

update Parts
   set PartPrice = 20
 where PartID = 'T1'
;

drop trigger parts_aft_ins_upd_row;

delete 
  from Parts
 where PartID = 'T7'
;

update Parts
   set PartPrice = 12
 where PartID = 'T1'
;



--s20170592
--Aufgabe 2
create or replace trigger parts_aft_upd_row
after update of PartColor
on parts
for each row
begin
    dbms_output.put_line('Attention, part has changed color!');
end;
/
show errors

update Parts
   set PartColor = 'gruen'
 where PartID = 'T1'
;

drop trigger parts_aft_upd_row;


update Parts
   set PartColor = 'rot'
 where PartID = 'T1'
;



--s20170592
--Aufgabe 3
create or replace trigger parts_aft_upd_color
after update of PartColor
on parts
for each row
begin
    dbms_output.put_line('Attention, part has changed color from ' ||
                         trim(:old.PartColor) || ' to ' ||
                         trim(:new.PartColor) || '!');
end;
/
show errors

update Parts
   set PartColor = 'gruen'
 where PartID = 'T1'
;

drop trigger parts_aft_upd_color;


update Parts
   set PartColor = 'rot'
 where PartID = 'T1'
;
*/

--s20170592
--Aufgabe 4
create or replace trigger suppliers_aft_ins_upd_del
after insert or update OR delete
on suppliers
for each row
begin
  if inserting then
    dbms_output.put_line('Triggered by INSERT');
    dbms_output.put_line('SupplierID: NEW: ' || trim(:new.SupplierID));
    dbms_output.put_line('SupplierName: NEW: ' || trim(:new.SupplierName));
    dbms_output.put_line('SupplierCity: NEW: ' || trim(:new.SupplierCity));
    dbms_output.put_line('SupplierDiscount: NEW: ' || trim(:new.SupplierDiscount));
    
  elsif updating then
    dbms_output.put_line('Triggered by UPDATE');
    dbms_output.put_line('Changed SupplierID to: NEW: ' || trim(:new.SupplierID) || ' from OLD: ' || trim(:old.SupplierID));
    dbms_output.put_line('Changed SupplierName to: NEW: ' || trim(:new.SupplierName) || ' from OLD: ' || trim(:old.SupplierName));
    dbms_output.put_line('Changed SupplierCity to: NEW: ' || trim(:new.SupplierCity) || ' from OLD: ' || trim(:old.SupplierCity));
    dbms_output.put_line('Changed SupplierDiscount to: NEW: ' || trim(:new.SupplierDiscount) || ' from OLD: ' || trim(:old.SupplierDiscount));
  else
    dbms_output.put_line('Triggered by DELETE');
    dbms_output.put_line('SupplierID: OLD: ' || trim(:old.SupplierID));
    dbms_output.put_line('SupplierName: OLD: ' || trim(:old.SupplierName));
    dbms_output.put_line('SupplierCity: OLD: ' || trim(:old.SupplierCity));
    dbms_output.put_line('SupplierDiscount: OLD: ' || trim(:old.SupplierDiscount));
  end if;
end;
/
show errors

insert into Suppliers
                (SupplierID, SupplierName, SupplierCity, SupplierDiscount)
        select  'L6'       , 'Armin'     , 'Stadt' ,    30  from dual
;

update Suppliers
   set SupplierName = 'Hans'
 where SupplierID = 'L1'
;

delete 
  from Suppliers
 where SupplierID = 'L6'
;

rollback;

drop trigger suppliers_aft_ins_upd_del;


