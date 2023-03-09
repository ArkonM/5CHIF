--s20170592
--Übung 010

clear screen;

drop table Verkaufszahl;
drop table Produkt;
drop table Zeit;
drop table Ort;


create table Ort (
  Filiale varchar (64) primary key
, Stadt   varchar (64)
, Region  varchar (64)
, Land    varchar (64)
);


create table Zeit (
  Tag     date primary key
, Woche   varchar (64)
, Monat   varchar (64)
, Quartal varchar (64)
, Jahr    varchar (64)
);


create table Produkt (
  Produkt       varchar (64) primary key
, Marke         varchar (64)
, Hersteller    varchar (64)
, Produktgruppe varchar (64)
);


create table Verkaufszahl (
  Filiale varchar (64) references Ort
, Produkt varchar (64) references Produkt
, Tag     date         references Zeit
, Anzahl  integer
, primary key(Filiale, Tag, Produkt)
);

insert into Ort 
    (Filiale, Stadt, Region, Land)
          select 'Hamburg'    ,'Hamburg'  ,'Nord','D' from dual
union all select 'Leipzig'    ,'Leipzig'  ,'Ost' ,'D' from dual
union all select 'Stuttgart'  ,'Stuttgart','Süd' ,'D' from dual
union all select 'Bremen-Nord','Bremen'   ,'Nord','D' from dual
union all select 'Bremen-Süd' ,'Bremen'   ,'Nord','D' from dual
union all select 'München'    ,'München'  ,'Süd' ,'D' from dual
;


insert into Zeit
    (Tag, Woche, Monat, Quartal, Jahr)
          select TO_DATE('05/01/2006', 'dd/mm/yyyy'), '2006 - 1',  '2006 - 1', '2006 - Q1', '2006' from dual
union all select TO_DATE('12/01/2006', 'dd/mm/yyyy'), '2006 - 2',  '2006 - 1', '2006 - Q1', '2006' from dual
union all select TO_DATE('13/02/2006', 'dd/mm/yyyy'), '2006 - 7',  '2006 - 2', '2006 - Q1', '2006' from dual
union all select TO_DATE('23/02/2006', 'dd/mm/yyyy'), '2006 - 8',  '2006 - 2', '2006 - Q1', '2006' from dual
union all select TO_DATE('04/03/2006', 'dd/mm/yyyy'), '2006 - 9',  '2006 - 3', '2006 - Q1', '2006' from dual
union all select TO_DATE('07/04/2006', 'dd/mm/yyyy'), '2006 - 14', '2006 - 4', '2006 - Q2', '2006' from dual
union all select TO_DATE('25/04/2006', 'dd/mm/yyyy'), '2006 - 17', '2006 - 4', '2006 - Q2', '2006' from dual
;
/


insert into Produkt 
    (Produkt, Marke, Hersteller, Produktgruppe)
          select 'Pizza Funghi'   ,'Gourmet-Pizza' ,'Frost GmbH' ,'Tiefkühlkost' from dual
union all select 'Pizza Hawaii'   ,'Gourmet-Pizza' ,'Frost GmbH' ,'Tiefkühlkost' from dual
union all select 'Pizza Napoli'   ,'Pizza'         ,'TK-Pizza AG','Tiefkühlkost' from dual
union all select 'Pizza Vegetale' ,'GoodandCheap'  ,'Frost GmbH' ,'Tiefkühlkost' from dual
union all select 'Pizza Calzione' ,'Pizza'         ,'TK-Pizza AG','Tiefkühlkost' from dual
;
/


insert into Verkaufszahl
    (Filiale, Produkt, Tag, Anzahl)
          select 'Hamburg'    ,'Pizza Funghi'  ,TO_DATE('05/01/2006', 'dd/mm/yyyy') ,78 from dual
union all select 'Hamburg'    ,'Pizza Funghi'  ,TO_DATE('12/01/2006', 'dd/mm/yyyy') ,67 from dual
union all select 'Leipzig'    ,'Pizza Hawaii'  ,TO_DATE('12/01/2006', 'dd/mm/yyyy') ,42 from dual
union all select 'München'    ,'Pizza Calzione',TO_DATE('13/02/2006', 'dd/mm/yyyy') ,53 from dual
union all select 'Stuttgart'  ,'Pizza Napoli'  ,TO_DATE('23/02/2006', 'dd/mm/yyyy') ,23 from dual
union all select 'Bremen-Nord','Pizza Funghi'  ,TO_DATE('04/03/2006', 'dd/mm/yyyy') ,69 from dual
union all select 'Bremen-Süd' ,'Pizza Vegetale',TO_DATE('07/04/2006', 'dd/mm/yyyy') ,45 from dual
union all select 'Stuttgart'  ,'Pizza Hawaii'  ,TO_DATE('25/04/2006', 'dd/mm/yyyy') ,92 from dual
;
/



--s20170592
--Aufgabe 4
select O.Region, Z.Jahr, P.Hersteller
  from Verkaufszahl V
  join Ort O on V.Filiale = O.Filiale
  join Zeit Z on V.Tag = Z.Tag
  join Produkt P on V.Produkt = P.Produkt
 where O.Region = 'Nord'
   and Z.Jahr = '2006'
 group by O.Region, Z.Jahr, P.Hersteller
;
/


clear screen
create materialized view Region_Marke_Jahr
as
select O.Region, P.Marke, Z.Jahr, sum(V.Anzahl) as Anzahl
  from Verkaufszahl V 
  join Ort O on V.Filiale = O.Filiale
  join Zeit Z on V.Tag = Z.Tag
  join Produkt P on V.Produkt = P.Produkt
 group by O.Region, P.Marke, Z.Jahr
;
/


--s20170592
--Aufgabe 6
select RMJ.Region, RMJ.Jahr, P.Hersteller
  from Region_Marke_Jahr RMJ
  join Produkt P on RMJ.Marke = P.Marke
 where RMJ.Region = 'Nord'
   and RMJ.Jahr = '2006'
 group by RMJ.Region, RMJ.Jahr, P.Hersteller
;
/




drop materialized view Region_Marke_Jahr;
/




