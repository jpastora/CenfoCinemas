using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{

    /*
     * Unico objetos de  acceso a datos que se pueden usar en el proyecto
     * Solo ejecuta Store Procedures 
     * 
     * Esta clase implementa el patron del Singleton
     * para asegurar que solo haya una instancia de este objeto
     * 
     */

    public class SqlDao
    {

        // Paso 1: Crear una instancia privada de la misma clase
        private static SqlDao _instance;

        private string _connectionString;

        // Paso 2: Redefinir el constructor default para que sea privado

        private SqlDao()
        {
            // Aquí puedes inicializar la cadena de conexión
            _connectionString = string.Empty; // Reemplaza con tu cadena de conexión real
        }

        // Paso 3: Crear un método público estático para obtener la instancia

        public static SqlDao GetInstance()
        {
            // Si la instancia no existe, crearla
            if (_instance == null)
            {
                _instance = new SqlDao();
            }
            // Devolver la instancia
            return _instance;

        }

        // Paso 4: Crear un método para ejecutar Store Procedures
        public void ExecuteProcedure(SqlOperation sqlOperation)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(sqlOperation.ProcedureName, conn))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Set de los parametros
                    foreach (var param in sqlOperation.Parameters)
                    {
                        command.Parameters.Add(param);
                    }

                    // Ejecuta el SP
                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        //Metodo para la ejecuacion Store Procedures con retorno de datos
        public List<Dictionary<string, object>> ExecuteQueryProcedure(SqlOperation operation)
        {
            //Conexion a la base de datos y ejecucion del Store Procedure
            //Capturar el resultado en una lista de diccionario
            //Convertirlo en DTOs
            var list = new List<Dictionary<string, object>>();

            return list;

        }
    }
}
