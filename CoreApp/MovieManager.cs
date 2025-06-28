using DataAccess.CRUD;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CoreApp
{
    public class MovieManager : BaseManager
    {
        public void Create(Movie movie)
        {

            try
            {
                //Valida que la pelicula no exista
                var mCrud = new MovieCrudFactory();
                var mExist = mCrud.RetrieveByTitle<Movie>(movie);

                if (mExist == null)
                {

                    mCrud.Create(movie);

                    // Consultar todos los usuarios desde la base de datos
                    var uCrud = new UserCrudFactory();
                    var usuarios = uCrud.RetrieveAll<User>();

                    // Inicializar el servicio de correos
                    var emailService = new SendGridService();

 /*                   foreach (var user in usuarios)
                    {
                        emailService.SendNewMovieNotification(
                            toEmail: user.Email,
                            toName: user.Name,
                            movieTitle: movie.Title,
                            genre: movie.Genre,
                            releaseDate: movie.ReleaseDate
                        ).Wait();
                    }*/
                }
                else
                {
                    throw new Exception("La pelicula ya ha sido previamente ingresada.");
                }
            }
            catch (Exception ex)
            {
                ManagerException(ex);
            }





        }

        public List<Movie> RetrieveAll()
        {
                var mCrud = new MovieCrudFactory();
                return mCrud.RetrieveAll<Movie>();
        }

        public Movie RetrieveById(int id)
        {
            var mCrud = new MovieCrudFactory();
            return mCrud.RetrieveById<Movie>(id);
        }

        public void Update(Movie movie)
        {
            var mCrud = new MovieCrudFactory();
            try
            {
                mCrud.Update(movie);
            }
            catch (Exception ex)
            {
                ManagerException(ex);
            }
        }

        public void Delete(Movie movie)
        {
            try
            {
                var mCrud = new MovieCrudFactory();
                mCrud.Delete(movie);
            }
            catch (Exception ex)
            {
                ManagerException(ex);
            }
        }
    }
}