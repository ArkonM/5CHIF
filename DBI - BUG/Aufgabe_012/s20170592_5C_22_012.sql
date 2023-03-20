--s20170592
--Aufgabe 012
clear screen

drop table Ort_Produkt_Monat_Verkauf;

create table Ort_Produkt_Monat_Verkauf(
  Ort varchar (16)
, Produkt varchar (32)
, Monat varchar (8)
, Anzahl number
, primary key (Ort, Produkt, Monat)
);


insert into Ort_Produkt_Monat_Verkauf
    (Ort, Produkt, Monat, Anzahl)
          select 'Stuttgart','Pizza Funghi'  , '2006-1', 155 from dual
union all select 'Stuttgart','Pizza Vegetale', '2006-1', 133 from dual
union all select 'Stuttgart','Pizza Hawaii'  , '2006-1', 89  from dual
union all select 'Stuttgart','Pizza Funghi'  , '2006-2', 141 from dual
union all select 'Stuttgart','Pizza Vegetale', '2006-2', 112 from dual
union all select 'Stuttgart','Pizza Hawaii'  , '2006-2', 95  from dual
union all select 'Frankfurt','Pizza Funghi'  , '2006-1', 77  from dual
union all select 'Frankfurt','Pizza Vegetale', '2006-1', 93  from dual
union all select 'Frankfurt','Pizza Hawaii'  , '2006-1', 102 from dual
union all select 'Frankfurt','Pizza Funghi'  , '2006-2', 144 from dual
union all select 'Frankfurt','Pizza Vegetale', '2006-2', 178 from dual
union all select 'Frankfurt','Pizza Hawaii'  , '2006-2', 177 from dual
;
/

clear screen

--s20170592
--Übung b
select Monat, Produkt, sum(Anzahl) as Anzahl
  from Ort_Produkt_Monat_Verkauf
 group by Monat, Produkt
;


--s20170592
--Übung c
select decode(grouping(Monat),1, 'Alle Monate', Monat) as Monat
      ,decode(grouping(Produkt),1, 'Alle Produkte', Produkt) as Produkt
      ,sum(Anzahl) as Anzahl
  from Ort_Produkt_Monat_Verkauf
 group by rollup(Monat, Produkt)
;


--clear screen
--s20170592
--Übung d
select decode(Produkt, 'Pizza Hawaii', 'Pizza mit Ananas', Produkt) as Produkt
  from Ort_Produkt_Monat_Verkauf
;

select case
       when grouping(Monat) = 1 then 'Alle Monate' 
       else Monat end as Monat
      ,case
       when grouping(Produkt) = 1 then 'Alle Produkte'
       else Produkt end as Produkt
      ,sum(Anzahl) as Anzahl
  from Ort_Produkt_Monat_Verkauf
 group by rollup(Monat, Produkt)
;


clear screen
--s20170592
--Übung e
--Grouping(Tabelle_x) gibt zurück ob die Tabelle_x opfer eines groupings ist, 
--das bedeutet dass eine oder mehrere Tabellen in der Abfrage gruppiert werden und
--in der entsprechenden Tabelle_x mehr als nur ein Datensatz angezeigt werden müsste.

select decode(grouping(Monat),1, 'Alle Monate', Monat) as Monat
      ,decode(grouping(Produkt),1, 'Alle Produkte', Produkt) as Produkt
      ,sum(Anzahl) as Anzahl
      ,grouping(Produkt) as "Groupingstatus Produkt"
  from Ort_Produkt_Monat_Verkauf
 group by rollup(Monat, Produkt)
;



clear screen

--s20170592
--Übung f
select Monat, Produkt, sum(Anzahl) as Anzahl
  from Ort_Produkt_Monat_Verkauf
 group by rollup(Monat, Produkt)
;

--Die Datensätze in denen mehr als nur ein Wert angezeigt werden müsste, sind leer bzw. null


clear screen

--s20170592
--Übung g
select decode(grouping(Monat), 1, 'Alle Monate', Monat) as Monat
     , decode(grouping(Produkt), 1, 'Alle Produkte', Produkt) as Produkt
     , sum(Anzahl) as Anzahl
  from Ort_Produkt_Monat_Verkauf
 group by cube(Monat, Produkt)
;


--s20170592
--Übung h
--ROLL-UP in kombination mit group by gruppiert die angegebenen Tabellenspalten
--Diese Gruppierung verläuft so, dass zuerst nach keinem gruppiert wird und dann
--von links nach rechts der group by Klausel immer eine Spalte mehr zu der gruppierung
--hinzugefügt wird

--CUBE funktioniert ähnlich nur dass CUBE nach allen Möglichkeiten der gegebenen Spalten
--gruppiert wird. Bei verwendung von CUBE wird 2^n-1 oft gruppiert, wobei n die Anzahl der
--gegebenen Spalten in der group by Cube Klausel ist


clear screen
--s20170592
--Übung i
select Monat, Produkt, sum(Anzahl) as Anzahl
  from Ort_Produkt_Monat_Verkauf
 where Monat = '2006-1'
 group by Monat, Produkt


union all

select Monat
     , 'Alle Produkte'
     , sum(Anzahl) as Anzahl
  from Ort_Produkt_Monat_Verkauf
 where Monat = '2006-1'
 group by Monat
 
 union all
 
 select Monat, Produkt, sum(Anzahl) as Anzahl
  from Ort_Produkt_Monat_Verkauf
 where Monat = '2006-2'
 group by Monat, Produkt


union all

select Monat
     , 'Alle Produkte'
     , sum(Anzahl) as Anzahl
  from Ort_Produkt_Monat_Verkauf
 where Monat = '2006-2'
 group by Monat

union all

select 'Alle Monate'
     , 'Alle Produkte'
     , sum(Anzahl) as Anzahl
  from Ort_Produkt_Monat_Verkauf
;
/


select 'Alle Monate'
     , 'Alle Produkte'
     , sum(Anzahl) as Anzahl
  from Ort_Produkt_Monat_Verkauf

union all

select 'Alle Monate'
     , Produkt
     , sum(Anzahl) as Anzahl
  from Ort_Produkt_Monat_Verkauf
 where Monat = '2006-1'
 group by Produkt
 
union all

select Monat
     , 'Alle Produkte'
     , sum(Anzahl) as Anzahl
  from Ort_Produkt_Monat_Verkauf
 where Monat = '2006-1'
 group by Monat
 
union all

select Monat, Produkt, sum(Anzahl) as Anzahl
  from Ort_Produkt_Monat_Verkauf
 where Monat = '2006-1'
 group by Monat, Produkt

union all

select Monat
     , 'Alle Produkte'
     , sum(Anzahl) as Anzahl
  from Ort_Produkt_Monat_Verkauf
 where Monat = '2006-2'
 group by Monat
 
union all
 
select Monat, Produkt, sum(Anzahl) as Anzahl
  from Ort_Produkt_Monat_Verkauf
 where Monat = '2006-2'
 group by Monat, Produkt
;
/



