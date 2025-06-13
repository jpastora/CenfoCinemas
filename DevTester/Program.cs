using DataAccess.CRUD;
using DataAccess.DAO;
using DTOs;
using Newtonsoft.Json;

public class Program
{
    public static void Main(string[] args)
    {


        // Menú de creación de usuarios y películas
        Console.WriteLine("Seleccione qué desea crear");
        Console.WriteLine("1. Usuario");
        Console.WriteLine("2. Película");
        Console.WriteLine("3. Salir");

        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                Console.WriteLine("Seleccione la operación a realizar con el usuario");
                Console.WriteLine("1. Crear");
                Console.WriteLine("2. Actualizar");
                Console.WriteLine("3. Listar");
                Console.WriteLine("3. Eliminar");
                Console.WriteLine("4. Salir");

                var userOption = Console.ReadLine(); 

                switch (userOption)
                {
                    case "1":
                        Console.WriteLine("Crear");
                        CreateUser();
                        break;
                    case "2":
                        Console.WriteLine("Actualizar");
                        UpdateUser();
                        break;
                    case "3":
                        Console.WriteLine("Listar");
                        ListUsers();
                        break;
                    case "4":
                        Console.WriteLine("Eliminar");
                        DeleteUser ();
                        break;
                    case "5":
                        Console.WriteLine("Saliendo del menú de usuario...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }
                break;
            case "2":
                CreateMovie();
                break;

            case "3":
                Console.WriteLine("Saliendo...");
                return;

            default:
                Console.WriteLine("Opción no válida. Intente de nuevo.");
                break;
        }


        //Creacion de usuarios

        void CreateUser()
        {
            Console.WriteLine("Creando usuario...");
            Console.Write("Ingrese el codigo de usuario: ");
            var userCode = Console.ReadLine();
            Console.Write("Ingrese el nombre: ");
            var name = Console.ReadLine();
            Console.Write("Ingrese el email: ");
            var email = Console.ReadLine();
            Console.Write("Ingrese la contrasena: ");
            var password = Console.ReadLine();
            Console.Write("Ingrese el estado (AC/IN): ");
            var status = Console.ReadLine();
            Console.Write("Ingrese la fecha de nacimiento (yyyy-mm-dd): ");
            var birthDateInput = Console.ReadLine();
            DateTime birthDate;
            if (!DateTime.TryParse(birthDateInput, out birthDate))
            {
                Console.WriteLine("Fecha invalida. Usando fecha actual.");
                birthDate = DateTime.Now;
            }

            // Creaamos el objeto del usuario apartir de los datos ingresados
            var user = new User()
            {
                UserCode = userCode,
                Name = name,
                Email = email,
                Password = password,
                Status = status,
                BirthDate = birthDate
            };

            var uCrud = new UserCrudFactory();
            uCrud.Create(user);

            Console.WriteLine("Usuario creado exitosamente.");
        }



        //Creacion de peliculas

        void CreateMovie()
        {
            Console.WriteLine("Creando pelicula...");
            Console.Write("Ingrese el titulo: ");
            var title = Console.ReadLine();
            Console.Write("Ingrese la descripcion: ");
            var description = Console.ReadLine();
            Console.Write("Ingrese la fecha de estreno (yyyy-mm-dd): ");
            var releaseDateInput = Console.ReadLine();
            DateTime releaseDate;
            if (!DateTime.TryParse(releaseDateInput, out releaseDate))
            {
                Console.WriteLine("Fecha invalida. Usando fecha actual.");
                releaseDate = DateTime.Now;
            }
            Console.Write("Ingrese el genero: ");
            var genre = Console.ReadLine();
            Console.Write("Ingrese el director: ");
            var director = Console.ReadLine();
            // Crear y ejecutar la operacion SQL
            var sqlOperation = new SqlOperation
            {
                ProcedureName = "CRE_MOVIE_PR"
            };

            sqlOperation.AddStringParameter("P_Title", title);
            sqlOperation.AddStringParameter("P_Description", description);
            sqlOperation.AddDateTimeParam("P_ReleaseDate", releaseDate);
            sqlOperation.AddStringParameter("P_Genre", genre);
            sqlOperation.AddStringParameter("P_Director", director);

            var sqlDao = SqlDao.GetInstance();

            try
            {
                sqlDao.ExecuteProcedure(sqlOperation);
                Console.WriteLine("Pelicula creada exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear pelicula: {ex.Message}");
            }
        }

        // Actualizar usuario por id
        void UpdateUser()
        {
            Console.WriteLine("Actualizando usuario...");

            Console.Write("Ingrese el ID del usuario a actualizar: ");
            var idInput = Console.ReadLine();
            if (!int.TryParse(idInput, out int userId))
            {
                Console.WriteLine("ID inválido. Operación cancelada.");
                return;
            }

            Console.Write("Ingrese el nuevo código de usuario: ");
            var userCode = Console.ReadLine();
            Console.Write("Ingrese el nuevo nombre: ");
            var name = Console.ReadLine();
            Console.Write("Ingrese el nuevo email: ");
            var email = Console.ReadLine();
            Console.Write("Ingrese la nueva contraseña: ");
            var password = Console.ReadLine();
            Console.Write("Ingrese la nueva fecha de nacimiento (yyyy-mm-dd): ");
            var birthDateInput = Console.ReadLine();
            Console.Write("Ingrese el nuevo estado (AC/IN): ");
            var status = Console.ReadLine();

            // Crear y ejecutar la operación SQL
            var sqlOperation = new SqlOperation
            {
                ProcedureName = "UPD_USER_PR"
            };

            sqlOperation.AddIntParam("P_Id", userId);
            sqlOperation.AddStringParameter("P_UserCode", userCode);
            sqlOperation.AddStringParameter("P_Name", name);
            sqlOperation.AddStringParameter("P_Email", email);
            sqlOperation.AddStringParameter("P_Password", password);
            sqlOperation.AddDateTimeParam("P_BirthDate", DateTime.Parse(birthDateInput));
            sqlOperation.AddStringParameter("P_Status", status);
            var sqlDao = SqlDao.GetInstance();

            try
            {
                sqlDao.ExecuteProcedure(sqlOperation);
                Console.WriteLine("Usuario actualizado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar usuario: {ex.Message}");
            }
        }

        // Listar usuarios
        void ListUsers()
        {
            var uCrud = new UserCrudFactory();
            var listUsers = uCrud.RetrieveAll<User>();
            foreach (var user in listUsers)
            {
                Console.WriteLine(JsonConvert.SerializeObject(user));
            }
        }
        
        // Eliminar usuario
        void DeleteUser()
        {
            Console.WriteLine("Eliminando usuario...");
            Console.Write("Ingrese el codigo de usuario a eliminar: ");
            var Id = Console.ReadLine();
            var sqlOperation = new SqlOperation
            {
                ProcedureName = "DEL_USER_PR"
            };
            sqlOperation.AddStringParameter("P_Id", Id);
            var sqlDao = SqlDao.GetInstance();
            try
            {
                sqlDao.ExecuteProcedure(sqlOperation);
                Console.WriteLine("Usuario eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar usuario: {ex.Message}");
            }


            /** var sqlOperation = new SqlOperation();
             sqlOperation.ProcedureName = "SP_CreateUser";

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
             sqlOperation2.ProcedureName = "SP_CreateMovie";
             sqlOperation2.AddStringParameter("P_Title", "Inception");
             sqlOperation2.AddStringParameter("P_Description", "A mind-bending thriller about dreams within dreams.");
             sqlOperation2.AddDateTimeParam("P_ReleaseDate", new DateTime(2010, 7, 16));
             sqlOperation2.AddStringParameter("P_Genre", "Sci-Fi");
             sqlOperation2.AddStringParameter("P_Director", "Christopher Nolan");

             var sqlDao2 = SqlDao.GetInstance();

             sqlDao2.ExecuteProcedure(sqlOperation2);

             Console.WriteLine("Movie created successfully.");

        }*/

        }
    }
}