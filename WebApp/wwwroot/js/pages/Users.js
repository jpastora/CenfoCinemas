// JS que maneja todo el comportamiento de la página de usuarios

//definir una clase JS, usando prototype, para manejar la página de usuarios

function UsersViewController() {

    this.ViewName = "Users";
    this.ApiEndpointName = "User";

    //Metodo constructor
    this.InitView = function () {

        console.log("Initializing Users View --> Ok");
        this.LoadTable();

        //Asociar el evento de click al botón
        $('#btnCreate').click(function () {
            var vc = new UsersViewController();
            vc.Create();
        });
        $('#btnUpdate').click(function () {
            var vc = new UsersViewController();
            vc.Update();
        });
        $('#btnDelete').click(function () {
            var vc = new UsersViewController();
            vc.Delete();
        });

    }

    //Metodo para la carga de la tabla
    this.LoadTable = function () {


        //URL del API a invocar
        //'https://localhost:7265/api/User/RetrieveAll'

        var ca = new ControlActions();
        var service = this.ApiEndpointName + "/RetrieveAll";
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

        //Asignar eventos de carga de datos o binding segun el clic en la tabla
        $('#tblUsers tbody').on('click', 'tr', function () {
            //extraemos la fila
            var row = $(this).closest('tr');

            //extraemos el dto de la fila
            //esto nos devuelve el json de la fila seleccionada por el usaurio
            //Segun la data devuelta por el Apiº
            var userDto = $('#tblUsers').DataTable().row(row).data();

            //Binding con el form
            $('#txtId').val(userDto.id);
            $('#txtUserCode').val(userDto.userCode);
            $('#txtName').val(userDto.name);
            $('#txtEmail').val(userDto.email);
            $('#txtStatus').val(userDto.status);

            //fecha tiene formato

            var onlyDate = userDto.birthDate.split('T');
            $('#txtBirthDate').val(onlyDate[0]);
        });
    }
    this.Create = function () {

        var userDto = {};

        //Atributos con valores por defecto, que son controlados por el API
        userDto.id = 0; //Id por defecto
        userDto.created = "2000-01-01";
        userDto.updated = "2025-01-25";

        // Valores capturasdos en pantalla
        userDto.userCode = $('#txtUserCode').val();
        userDto.name = $('#txtName').val();
        userDto.email = $('#txtEmail').val();
        userDto.birthDate = $('#txtBirthDate').val();
        userDto.status = $('#txtStatus').val();
        userDto.password = $('#txtPassword').val();

        //Enviar la data al API
        var ca = new ControlActions();
        var urlService = this.ApiEndpointName + "/Create";

        ca.PostToAPI(urlService, userDto, function (){

            //refrescar la tabla
            $('#tblUsers').DataTable().ajax.reload();
        });
    }

    this.Update = function () {

        var userDto = {};

        //Atributos con valores por defecto, que son controlados por el API
        userDto.id = $('#txtId').val();
        userDto.created = "2000-01-01";
        userDto.updated = "2025-01-25";

        // Valores capturados en pantalla
        userDto.userCode = $('#txtUserCode').val();
        userDto.name = $('#txtName').val();
        userDto.email = $('#txtEmail').val();
        userDto.birthDate = $('#txtBirthDate').val();
        userDto.status = $('#txtStatus').val();
        userDto.password = $('#txtPassword').val();

        //Enviar la data al API

        var ca = new ControlActions();
        var urlService = this.ApiEndpointName + "/Update";

        ca.PutToAPI(urlService, userDto, function (){
            $('#tblUsers').DataTable().ajax.reload();
        });
    }   

    this.Delete = function () {

        var userDto = {};

        //Atributos con valores por defecto, que son controlados por el API
        userDto.id = $('#txtId').val();
        userDto.created = "2000-01-01";
        userDto.updated = "2025-01-25";

        // Valores capturados en pantalla
        userDto.userCode = $('#txtUserCode').val();
        userDto.name = $('#txtName').val();
        userDto.email = $('#txtEmail').val();
        userDto.birthDate = $('#txtBirthDate').val();
        userDto.status = $('#txtStatus').val();
        userDto.password = $('#txtPassword').val();

        //Enviar la data al API

        var ca = new ControlActions();
        var urlService = this.ApiEndpointName + "/Delete";

        ca.DeleteToAPI(urlService, userDto, function (){
            $('#tblUsers').DataTable().ajax.reload();
        });
    } 

}

$(document).ready(function () {
    var vc = new UsersViewController();
    vc.InitView();
});