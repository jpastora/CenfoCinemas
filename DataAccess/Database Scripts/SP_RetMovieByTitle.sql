CREATE PROCEDURE RET_MOVIE_BY_TITLE_PR
    @P_Title VARCHAR(35)
AS
BEGIN
    SELECT
        Id,
        Created,
        Updated,
        Title,
        Description,
        ReleaseDate,
        Genre,
        Director
    FROM TBL_Movie
    WHERE Title = @P_Title;
END
