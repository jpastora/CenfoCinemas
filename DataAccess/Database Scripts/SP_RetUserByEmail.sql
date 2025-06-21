CREATE PROCEDURE RET_USER_BY_EMAIL_PR
    @P_Email NVARCHAR(30)
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
    WHERE Email = @P_Email;
END
