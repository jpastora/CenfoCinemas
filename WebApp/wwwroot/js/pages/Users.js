// JS que maneja todo el comportamiento de la página de usuarios

//definir una clase JS, usando prototype, para manejar la página de usuarios

function UsersViewController() {

    this.ViewName = "Users";
    this.ApiEndpoint = "User";

    //Metodo constructor
    this.InitView = function () {

        console.log("Initializing Users View --> Ok");
        this.LoadTable();

    }

    //Metodo para la carga de la tabla
    this.LoadTable = function () {

        //URL del API a invocar
        //'https://localhost:7265/api/User/RetrieveAll'

        var ca = new ControlActions();
        var service = this.ApiEndpoint + "/RetrieveAll";
        var urlService = ca.GetUrlApiService(service);

        /*  {
    "userCode": "testUser",
    "name": "Jose",
    "email": "test@test1.com",
    "password": "test1234!",
    "birthDate": "1995-03-15T00:00:00",
    "status": "AC",
    "id": 4,
    "created": "2025-06-13T02:35:18.02",
    "updated": "0001-01-01T00:00:00"
  }

                <th>ID</th>
                <th>User Code</th>
                <th>Name</th>
                <th>Email</th>
                <th>Birth Date</th>
                <th>Status</th>

  */

        var columns = [];
        columns[0] = { 'data': 'id' }
        columns[1] = { 'data': 'userCode' }
        columns[2] = { 'data': 'name' }
        columns[3] = { 'data': 'email' }
        columns[4] = { 'data': 'birthDate' }
        columns[5] = { 'data': 'status' }



        // Invocamos a datatables para convertir la tabla HTML en una tabla dinámica
        $('#tblUsers').dataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            columns: columns
        });

    }

}

$(document).ready(function () {
    var vc = new UsersViewController();
    vc.InitView();
});