function MoviesViewController() {

    this.ViewName = "Movies";
    this.ApiEndpointName = "Movie";

    //Metodo constructor
    this.InitView = function () {

        console.log("Initializing Movies View --> Ok");
        this.LoadTable();

        //Asociar el evento de click al botón
        $('#btnCreate').click(function () {
            var vc = new MoviesViewController();
            vc.Create();
        });
        $('#btnUpdate').click(function () {
            var vc = new MoviesViewController();
            vc.Update();
        });
        $('#btnDelete').click(function () {
            var vc = new MoviesViewController();
            vc.Delete();
        });
        $('#btnRetrieveById').click(function () {
            var vc = new MoviesViewController();
            vc.RetrieveById();
        });
    }

    //Metodo para la carga de la tabla
    this.LoadTable = function () {

        //URL del API a invocar
        //'https://localhost:7265/api/Movie/RetrieveAll'

        var ca = new ControlActions();
        var service = this.ApiEndpointName + "/RetrieveAll";
        var urlService = ca.GetUrlApiService(service);

        /*  {
  {
    "title": "Inception",
    "description": "A mind-bending thriller about dreams within dreams.",
    "releaseDate": "2010-07-16T00:00:00",
    "genre": "Sci-Fi",
    "director": "Christopher Nolan",
    "id": 1,
    "created": "2025-06-12T15:13:29.097",
    "updated": "0001-01-01T00:00:00"
  }

                    <tr>
                        <th>Id</th>
                        <th>Title</th>
                        <th>Description</th>
                        <th>Release Date</th>
                        <th>Genre</th>
                        <th>Director</th>
                    </tr>
  */

        var columns = [];
        columns[0] = { 'data': 'id' }
        columns[1] = { 'data': 'title' }
        columns[2] = { 'data': 'description' }
        columns[3] = { 'data': 'releaseDate' }
        columns[4] = { 'data': 'genre' }
        columns[5] = { 'data': 'director' }



        // Invocamos a datatables para convertir la tabla HTML en una tabla dinámica
        $('#tblMovies').dataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            columns: columns
        });

        // Asignar el evento de click a las filas de la tabla
        $('#tblMovies tbody').on('click', 'tr', function () {

            //Obtenemos el objeto de la fila seleccionada
            var movieDto = $('#tblMovies').DataTable().row(this).data();


            //Asignamos los valores a los campos del formulario
            $('#txtId').val(movieDto.id);
            $('#txtTitle').val(movieDto.title);
            $('#txtDescription').val(movieDto.description);
            $('#txtGenre').val(movieDto.genre);
            $('#txtDirector').val(movieDto.director);

            // Convertimos la fecha a un formato legible
            var onlyDate = movieDto.releaseDate.split("T");
            $('#txtReleaseDate').val(onlyDate[0]); // Formato YYYY-MM-DD
        });
    }

    this.RetrieveById = function () {

        var id = $('#txtId').val();

        var ca = new ControlActions();
        var service = this.ApiEndpointName + '/RetrieveById?id=' + encodeURIComponent(id);

        ca.GetToApi(service, function (result) {
            var movie = result;

            // Asignar los valores al formulario
            $('#txtTitle').val(movie.title);
            $('#txtDescription').val(movie.description);
            $('#txtGenre').val(movie.genre);
            $('#txtDirector').val(movie.director);
            // Convertir la fecha a un formato legible
            var onlyDate = movie.releaseDate.split("T");
            $('#txtReleaseDate').val(onlyDate[0]); 
        });
    };  

    this.Create = function () {

        var movieDto = {};

        // Atributos con valores por defecto, que son controlados por el API
        movieDto.id = 0; // Id por defecto
        movieDto.created = "2025-01-01"; // Fecha de creación por defecto 
        movieDto.updated = "0001-01-01"; // Fecha de actualización por defecto

        //Valores capturados en pantalla
        movieDto.title = $('#txtTitle').val();
        movieDto.description = $('#txtDescription').val();
        movieDto.releaseDate = $('#txtReleaseDate').val();
        movieDto.genre = $('#txtGenre').val();
        movieDto.director = $('#txtDirector').val();


        var ca = new ControlActions();
        var urlService = this.ApiEndpointName + "/Create";


        ca.PostToAPI(urlService, movieDto, function (){

            $('#tblMovies').DataTable().ajax.reload();
        });
    }

    this.Update = function () {
        var movieDto = {};
        // Atributos con valores por defecto, que son controlados por el API
        movieDto.id = $('#txtId').val(); // Id capturado del formulario
        movieDto.created = "2025-01-01"; // Fecha de creación por defecto 
        movieDto.updated = "0001-01-01"; // Fecha de actualización por defecto
        //Valores capturados en pantalla
        movieDto.title = $('#txtTitle').val();
        movieDto.description = $('#txtDescription').val();
        movieDto.releaseDate = $('#txtReleaseDate').val();
        movieDto.genre = $('#txtGenre').val();
        movieDto.director = $('#txtDirector').val();


        var ca = new ControlActions();
        var urlService = this.ApiEndpointName + "/Update";
        ca.PutToAPI(urlService, movieDto, function () {
            $('#tblMovies').DataTable().ajax.reload();
        });
    }

    this.Delete = function () {
        var movieDto = {};
        // Atributos con valores por defecto, que son controlados por el API
        movieDto.id = $('#txtId').val(); // Id capturado del formulario
        movieDto.created = "2025-01-01"; // Fecha de creación por defecto 
        movieDto.updated = "0001-01-01"; // Fecha de actualización por defecto

        //Valores capturados en pantalla
        movieDto.title = $('#txtTitle').val();
        movieDto.description = $('#txtDescription').val();
        movieDto.releaseDate = $('#txtReleaseDate').val();
        movieDto.genre = $('#txtGenre').val();
        movieDto.director = $('#txtDirector').val();


        var ca = new ControlActions();
        var urlService = this.ApiEndpointName + "/Delete";
        ca.DeleteToAPI(urlService, movieDto, function (){
            $('#tblMovies').DataTable().ajax.reload();
        });
    }

}

$(document).ready(function () {
    var vc = new MoviesViewController();
    vc.InitView();
});