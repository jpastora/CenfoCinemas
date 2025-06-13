using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DAO;
using DTOs;

namespace DataAccess.CRUD
{
    // Clase abstract que define la estructura de una factoría CRUD
    //Define como se hacen cruds en la arquitectura de la aplicación
    public abstract class CrudFactory
    {

        protected SqlDao _sqlDao;

        //Deinir los metodos que forman parte del contrato
        //C = Create
        //R = Retrieve
        //U = Update
        //D = Delete

        public abstract void Create(BaseDTO baseDTO);
        public abstract void Update(BaseDTO baseDTO);
        public abstract void Delete(BaseDTO baseDTO);

        public abstract T Retrieve<T>();
        public abstract T RetrieveById<T>();
        public abstract List<T> RetrieveAll<T>();

    }
}
