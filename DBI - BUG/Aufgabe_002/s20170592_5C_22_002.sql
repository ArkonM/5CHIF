clear screen;
set serveroutput on size unlimited;


--s20170592

declare
  Pwd varchar(32)       := 'topsecret';
  Length_       int     := length(Pwd);
  Char_         char;
  IsLongEnough  boolean := true;
  HasUpper      boolean := false;
  HasLower      boolean := false;
  HasDigit      boolean := false;
  Secure        boolean := false;
begin

  if Length_ < 8 then
    IsLongEnough := false;
  end if;
  
  while Length_ > 0 and (not HasUpper or 
                         not HasLower or 
                         not HasDigit)
                         
  loop
    
    Char_ := substr(Pwd, Length_, 1);
    
    if not HasUpper then
      HasUpper := case instr('ABCDEFGHIJKLMNOPQRSTUVWXYZ', Char_) 
                  when 0 
                    then false 
                    else true 
                  end;
    end if;
    
    if not HasLower then
      HasLower := case instr('abcdefghijklmnopqrstuvwxyz', Char_)
                  when 0 
                    then false 
                    else true
                  end;
    end if;
    
    if not HasDigit then
      HasDigit := case instr('0123456789', Char_) 
                  when 0 
                    then false 
                    else true 
                  end;
    end if;
    
    Length_ := Length_ - 1;
  
  end loop;
  
  if IsLongEnough and HasUpper and HasLower and HasDigit then
    Secure := true;
  end if;
  
  dbms_output.put_line('IsSecure?');
  dbms_output.put_line(case Secure when false then 0 when true then 1 end);

end;
/


--s20170592

declare
  Pwd varchar(32)       := 'TopSecret2120';
  Length_       int     := length(Pwd);
  Char_         char;
  IsLongEnough  boolean := true;
  HasUpper      boolean := false;
  HasLower      boolean := false;
  HasDigit      boolean := false;
  Secure        boolean := false;
begin

  if Length_ < 8 then
    IsLongEnough := false;
  end if;
  
  while Length_ > 0 and (not HasUpper or 
                         not HasLower or 
                         not HasDigit)
                         
  loop
    
    Char_ := substr(Pwd, Length_, 1);
    
    if not HasUpper then
      HasUpper := case instr('ABCDEFGHIJKLMNOPQRSTUVWXYZ', Char_) 
                  when 0 
                    then false 
                    else true 
                  end;
    end if;
    
    if not HasLower then
      HasLower := case instr('abcdefghijklmnopqrstuvwxyz', Char_)
                  when 0 
                    then false 
                    else true
                  end;
    end if;
    
    if not HasDigit then
      HasDigit := case instr('0123456789', Char_) 
                  when 0 
                    then false 
                    else true 
                  end;
    end if;
    
    Length_ := Length_ - 1;
  
  end loop;
  
  if IsLongEnough and HasUpper and HasLower and HasDigit then
    Secure := true;
  end if;
  
  dbms_output.put_line('IsSecure?');
  dbms_output.put_line(case Secure when false then 0 when true then 1 end);

end;
/


--s20170592

declare
  Pwd varchar(32)       := 's20170592';
  Length_       int     := length(Pwd);
  Char_         char;
  IsLongEnough  boolean := true;
  HasUpper      boolean := false;
  HasLower      boolean := false;
  HasDigit      boolean := false;
  Secure        boolean := false;
begin

  if Length_ < 8 then
    IsLongEnough := false;
  end if;
  
  while Length_ > 0 and (not HasUpper or 
                         not HasLower or 
                         not HasDigit)
                         
  loop
    
    Char_ := substr(Pwd, Length_, 1);
    
    if not HasUpper then
      HasUpper := case instr('ABCDEFGHIJKLMNOPQRSTUVWXYZ', Char_) 
                  when 0 
                    then false 
                    else true 
                  end;
    end if;
    
    if not HasLower then
      HasLower := case instr('abcdefghijklmnopqrstuvwxyz', Char_)
                  when 0 
                    then false 
                    else true
                  end;
    end if;
    
    if not HasDigit then
      HasDigit := case instr('0123456789', Char_) 
                  when 0 
                    then false 
                    else true 
                  end;
    end if;
    
    Length_ := Length_ - 1;
  
  end loop;
  
  if IsLongEnough and HasUpper and HasLower and HasDigit then
    Secure := true;
  end if;
  
  dbms_output.put_line('IsSecure?');
  dbms_output.put_line(case Secure when false then 0 when true then 1 end);

end;
/