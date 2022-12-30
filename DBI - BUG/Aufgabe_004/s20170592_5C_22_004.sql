set serveroutput on size unlimited;

--s20170592

declare
    l_today_date        date := sysdate;
    l_today_timestamp   timestamp := systimestamp;
    l_today_timetzone   timestamp with time zone := systimestamp;
    l_interval1         interval year (4) to month := '2011-11';
    l_interval2         interval day (2) to second := '15 00:30:44';
begin
    null;
end;
/

begin
    dbms_output.put_line (sysdate);
    dbms_output.put_line (systimestamp);
    dbms_output.put_line (sysdate - systimestamp);
end;
/


--s20170592

begin
    dbms_output.put_line (
        to_char (sysdate));
    dbms_output.put_line (
        to_char (systimestamp));
end;
/


--s20170592

begin
    dbms_output.put_line (
        to_char (sysdate, 
            'day, ddth month yyyy'));
end;
/

--s20170592

begin
    dbms_output.put_line (
        to_char (sysdate, 
            'day, ddth month yyyy', 
            'nls_date_language=spanish'));
end;
/

--s20170592

begin
    dbms_output.put_line (
        to_char (sysdate, 
            'fmday, ddth month yyyy'));
end;
/

--s20170592

begin
    dbms_output.put_line (
        to_char (sysdate, 
            'yyyy-mm-dd hh24:mi:ss'));
end;
/

--s20170592

declare
    l_date date;
begin
    l_date := to_date ('12-jan-2011');
end;
/

--s20170592

declare
    l_date date;
begin
    l_date := to_date ('January 12 2011',
                         'Month DD YYYY');
end;
/

--s20170592

declare
    l_date1 date := sysdate;
    l_date2 date := sysdate + 10;
begin
    dbms_output.put_line (
        l_date2 - l_date1);
    dbms_output.put_line (
        l_date1 - l_date2);
end;
/

--s20170592

create or replace function your_age (
    birthdate_in in date
)
return number
is
begin
    return sysdate - 
        birthdate_in;
end your_age;
/

--s20170592

begin
    dbms_output.put_line (
        add_months (to_date ('31-jan-2011', 
                             'dd-mon-yyyy'), 1));

    dbms_output.put_line (
        add_months (to_date ('27-feb-2011',
                             'dd-mon-yyyy'), -1));

    dbms_output.put_line (
        add_months (to_date ('28-feb-2011',
                             'dd-mon-yyyy'), -1));
end;
/
