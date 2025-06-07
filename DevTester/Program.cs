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

    }

}