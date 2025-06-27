function MoviesViewController() {

    this.ViewName = "Movies";
    this.ApiEndpoint = "Movie";

    //Metodo constructor
    this.InitView = function () {

        console.log("Initializing Movies View --> Ok");
        this.LoadTable();

    }

    //Metodo para la carga de la tabla
    this.LoadTable = function () {

        //URL del API a invocar
        //'https://localhost:7265/api/Movie/RetrieveAll'

        var ca = new ControlActions();
        var service = this.ApiEndpoint + "/RetrieveAll";
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

    }

}

$(document).ready(function () {
    var vc = new MoviesViewController();
    vc.InitView();
});