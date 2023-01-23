clear screen;
set serveroutput on size unlimited;
--s20170592
declare
    type list_of_names_t is table of varchar2 (100);

    happyfamily list_of_names_t := list_of_names_t ();
    children    list_of_names_t := list_of_names_t ();
    parents     list_of_names_t := list_of_names_t ();
begin
    happyfamily.extend (4);
    happyfamily (1) := 'Veva';
    happyfamily (2) := 'Chris';
    happyfamily (3) := 'Eli';
    happyfamily (4) := 'Steven';
    
    children.extend;
    children (children.last) := ‘Chris’;
    children.extend;
    children (children.last) := ‘Eli’;
    parents := happyfamily multiset except children;

    for l_row in 1 .. parents.count
    loop
        dbms_output.put_line (parents (l_row));
    end loop;
end;
/


--s20170592
create or replace procedure show_contents (
    names_in in DBMS_UTILITY.maxname_array
    )
is
begin
    for indx in names_in.first .. names_in.last
    loop
        dbms_output.put_line (names_in (indx));
    end loop;
end;
/


--s20170592
create or replace procedure show_contents (
    names_in in DBMS_UTILITY.maxname_array)
is
    l_index   pls_integer := names_in.first;
begin
    while (l_index is not null)
    loop
        dbms_output.put_line (names_in (l_index));
        l_index := names_in.next (l_index);
    end loop;
end;
/


--s20170592
create or replace procedure show_contents (
    names_in in DBMS_UTILITY.maxname_array)
is
    l_index   pls_integer := names_in.last;
begin
    while (l_index is not null)
    loop
        dbms_output.put_line (names_in (l_index));
        l_index := names_in.prior (l_index);
    end loop;
end;
/


clear screen;
----s20170592 c)
declare
	l_names dbms_utility.maxname_array;
begin
	l_names (1) := 'Strawberry';
	l_names (10) := 'Blackberry';
	l_names (2) := 'Raspberry';
declare
	indx pls_integer := l_names.first;
begin
	while (indx IS NOT NULL)
	loop
		dbms_output.put_line (l_names (indx));
		indx := l_names.next (indx);
	end loop;
	end;
end;
/