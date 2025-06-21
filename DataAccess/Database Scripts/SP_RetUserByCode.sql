CREATE PROCEDURE RET_USER_BY_CODE_PR
    @P_Code NVARCHAR(30)
AS
BEGIN
    SELECT
        Id,
        Created,
        Updated,
        UserCode,
        Name,
        Email,
        Password,
        BirthDate,
        Status
    FROM TBL_User
    WHERE UserCode = @P_Code;
END
