set nocount on

begin
    declare @Pwd varchar(32) = 'topsecret';
    declare @Length int = len(@Pwd);
    declare @Char char;
    declare @IsLongEnough bit = 1;
    declare @HasUpper bit = 0;
    declare @HasLower bit = 0;
    declare @HasDigit bit = 0;
    
    if @Lenght < 0
    begin
        set @IsLongEnough = 0;
    end;
    
    while @Length > 0 and (@HasUpper & @HasLower & @HasDigit = 0)
    begin
        set @Char = substring(@Pwd, @Length, 1);
        
        if @HasUpper != 1
        begin
            set @HasUpper = charindex(@Char
                                    , 'ABCDEFGHIJKLMNOPQRSTUVWXYZ' collate Latin1_General_CS_AS);
        end;
        
        if @HasLower != 1
        begin
            set @HasLower = charindex(@Char
                                     , 'abcdefghijklmnopqrstuvwxyz' collate Latin1_General_CS_AS);
        end;
        
        if @HasDigit != 1
        begin
            set @HasDigit = charindex(@Char, '1234567890');
        end;
        
        set @Length = @Length - 1;
    end;
    
    select @IsLongEnough & @HasUpper & @HasLower & @HasDigit "IsSecure?";
end;
go
