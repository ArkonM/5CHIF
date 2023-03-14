
clear screen;

drop table Zeit;

create table Zeit (
  Tag     date primary key
, Woche   varchar (64)
, Monat   varchar (64)
, Quartal varchar (64)
, Jahr    varchar (64)
, B1      number
, B2      number
, B3      number
, B4      number
, B5      number
, B6      number
, B7      number
, B8      number
, B9      number
, B10     number
, B11     number
, B12     number
);


insert into Zeit
    (Tag, Woche, Monat, Quartal, Jahr, B1, B2, B3, B4, B5, B6, B7, B8, B9, B10, B11, B12)
          select TO_DATE('05/12/2006', 'dd/mm/yyyy'), '2006 - 49', '2006 - 12', '2006 - Q4', '2006', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 from dual
union all select TO_DATE('12/01/2006', 'dd/mm/yyyy'), '2006 - 2' , '2006 - 1' , '2006 - Q1', '2006', 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 from dual
union all select TO_DATE('23/03/2006', 'dd/mm/yyyy'), '2006 - 12', '2006 - 3' , '2006 - Q1', '2006', 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 from dual
union all select TO_DATE('04/05/2006', 'dd/mm/yyyy'), '2006 - 18', '2006 - 5' , '2006 - Q2', '2006', 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 from dual
union all select TO_DATE('07/01/2006', 'dd/mm/yyyy'), '2006 - 1' , '2006 - 1' , '2006 - Q1', '2006', 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 from dual
union all select TO_DATE('13/07/2006', 'dd/mm/yyyy'), '2006 - 28', '2006 - 7' , '2006 - Q3', '2006', 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1 from dual
union all select TO_DATE('07/08/2006', 'dd/mm/yyyy'), '2006 - 32', '2006 - 8' , '2006 - Q3', '2006', 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1 from dual
union all select TO_DATE('13/05/2006', 'dd/mm/yyyy'), '2006 - 19', '2006 - 5' , '2006 - Q2', '2006', 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 from dual
;
/

clear screen;
--s20170592
--Aufgabe d)
select *
  from Zeit
  where B1 = 0 and B5 = 1
;

clear screen;
select *
  from Zeit
 where B6 = 1
 ;
 
 clear screen;
 select *
   from Zeit
 where B7 = 0
 ;
 
 clear screen;
 select *
   from Zeit
 where B5 = 1 and B4 = 0
;