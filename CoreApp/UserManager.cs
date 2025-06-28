using DataAccess.CRUD;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class UserManager : BaseManager
    {
        /*
         * Metodo para la creacion de un usuario
         * Valida que el usuario sea mayor de 18 años
         * Valida que el usuario este disponible
         * Validad que el correo no este registrado
         * Envia un correo electronico de bienvenida
         */
        public void Create(User user)
        {

            try
            {
                //Validar la edad
                if (IsOver18(user))
                {
                    var uCrud = new UserCrudFactory();

                    //Consultamos en la base de datos si existe un usuario con ese codigo
                    var uExist = uCrud.RetrieveByUserCode<User>(user);

                    if (uExist == null)
                    {

                        if (uExist == null)
                        {
                            //Consultamos si en la bd existe un usuario con ese mail
                            uExist = uCrud.RetrieveByUserEmail<User>(user);

                            if(uExist == null)
                                uCrud.Create(user);

                           // var emailService = new SendGridService();
                           // emailService.SendWelcomeEmail(user.Email, user.Name).Wait();

                        }
                        else
                        {
                            throw new Exception("Este correo ya se encuentra registrado.");
                        }
                    }
                    else
                    {
                        throw new Exception("Codigo de usuario no disponible.");
                    }

                        uCrud.Create(user);
                }
                else
                {
                    throw new Exception("Usuario no cumple con la edad");
                }
               
            }
            catch (Exception ex)
            {
                {
                    ManagerException(ex);
                }

            }
        }

        public List<User> RetrieveAll()
        {
            var uCrud = new UserCrudFactory();
            return uCrud.RetrieveAll<User>();
        }

        public User RetrieveById(int id)
        {
            var uCrud = new UserCrudFactory();
            return uCrud.RetrieveById<User>(id);
        }

        public User RetrieveByUserCode(User user)
        {
            var uCrud = new UserCrudFactory();
            return uCrud.RetrieveByUserCode<User>(user);
        }

        public User RetrieveByUserEmail(User user)
        {
            var uCrud = new UserCrudFactory();
            return uCrud.RetrieveByUserEmail<User>(user);
        }

        public void Update(User user)
        {
            try
            {
                var uCrud = new UserCrudFactory();
                uCrud.Update(user);
            }
            catch (Exception ex)
            {
                ManagerException(ex);
            }
        }

        public void Delete(User user)
        {
            try
            {
                var uCrud = new UserCrudFactory();
                uCrud.Delete(user);
            }
            catch (Exception ex)
            {
                ManagerException(ex);
            }
        }


        private bool IsOver18(User user)
        {

            var currentDate = DateTime.Now;
            int age = currentDate.Year - user.BirthDate.Year;

            if (user.BirthDate > currentDate.AddYears(-age).Date)
            {
                age--;
            }
            return age >= 18;
        }
    }
}
