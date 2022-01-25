using Microsoft.AspNetCore.Mvc;
using RestAPIDemo.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace RestAPIDemo.Controllers
{
    //Definimos la ruta de la API
    [Route("[controller]")]
    [ApiController]
    public class ZohoController : Controller
    {
        //Definimos una variable para obtener los valores del archivo "appsettings.json"
        private IConfiguration _configuration;
        public ZohoController(IConfiguration iConfiguration) 
        { 
            _configuration = iConfiguration;
        }
        public IActionResult Index()
        {
            return View();
        }
        //Método POST
        [HttpPost]
        public async Task<ActionResult<SabanaModel>> CreateTodoItem(SabanaModel oModel)
        {
            try
            {
                //Creamos un objeto de SqlConnectionStringBuilder
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                //Asignamos el nombre del servidor
                builder.DataSource = _configuration.GetValue<String>("ServerBD");
                //Asignamos el nombre de usuario a la base
                builder.UserID = _configuration.GetValue<String>("BDUsuario");
                //Asignamos la contraseña 
                builder.Password = _configuration.GetValue<String>("BDContrasena");
                //Asignamos a que base nos vamos a conectar
                builder.InitialCatalog = _configuration.GetValue<String>("BDConexion");
                //Creamos un objeto de SqlConnection
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    //Escribimos en consola
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n"); connection.Open();
                    //Escibimos los query que vamos a necesitar
                    //String sql = "SELECT * from tlbBitacora";
                    String query = "INSERT INTO tlbBitacora (Nombre,Fecha) VALUES (@Nombre,@FechaRegistro)";
                    //Ejecutamos la query
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        //Hacemos la relación entre los datos mandadados y los que se almacenarán en la base
                        command.Parameters.AddWithValue("@Nombre", oModel.Nombre);
                        command.Parameters.AddWithValue("@FechaRegistro", oModel.FechaRegistro);
                        //Revisamos que no haya error
                        int result = command.ExecuteNonQuery();
                        // Check Error
                        if (result < 0)
                            Console.WriteLine("Error inserting data into Database!");
                    }
                }
                //Regresamos lo que nos llegó
                return Ok(oModel);
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
            
        }

    }
}
