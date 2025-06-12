using DataAccess.DAO;

public class Program
{
    public static void Main(string[] args)
    {
        var sqlOperation = new SqlOperation();
        sqlOperation.ProcedureName = "CRE_USER_PR";

        sqlOperation.AddStringParameter("P_UserCode", "testUser");
        sqlOperation.AddStringParameter("P_Name", "Joseph");
        sqlOperation.AddStringParameter("P_Email", "joseph.pastora@gmail.com");
        sqlOperation.AddStringParameter("P_Password", "test1234!");
        sqlOperation.AddStringParameter("P_Status", "AC");
        sqlOperation.AddDateTimeParam("P_BirthDate", DateTime.Now);

        var sqlDao = SqlDao.GetInstance();

        sqlDao.ExecuteProcedure(sqlOperation);
        

        Console.WriteLine("Procedure executed successfully.");

        var sqlOperation2 = new SqlOperation();
        sqlOperation2.ProcedureName = "CRE_MOVIE_PR";
        sqlOperation2.AddStringParameter("P_Title", "Inception");
        sqlOperation2.AddStringParameter("P_Description", "A mind-bending thriller about dreams within dreams.");
        sqlOperation2.AddDateTimeParam("P_ReleaseDate", new DateTime(2010, 7, 16));
        sqlOperation2.AddStringParameter("P_Genre", "Sci-Fi");
        sqlOperation2.AddStringParameter("P_Director", "Christopher Nolan");

        var sqlDao2 = SqlDao.GetInstance();

        sqlDao2.ExecuteProcedure(sqlOperation2);

        Console.WriteLine("Movie created successfully.");

    }

}