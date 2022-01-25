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
                //Creamos un objeto de sql
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                //Asignamos el nombre del servidor
                builder.DataSource = _configuration.GetValue<String>("ServerBD");
                //Asignamos el nombre de usuario a la base
                builder.UserID = _configuration.GetValue<String>("BDUsuario");
                //Asignamos la contraseña 
                builder.Password = _configuration.GetValue<String>("BDContraseña");
                //Asignamos a que base nos vamos a conectar
                builder.InitialCatalog = _configuration.GetValue<String>("BDConexion");

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n"); connection.Open();
                    //String sql = "SELECT * from tlbBitacora";
                    String query = "INSERT INTO tlbBitacora (Nombre,Fecha) VALUES (@Nombre,@FechaRegistro)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        /* using (SqlDataReader reader = command.ExecuteReader()) 
                         { 
                             while (reader.Read()) 
                             { 
                                 Console.WriteLine("{0} {1}", reader.GetInt32(0), reader.GetString(1)); 
                             } 
                         } */

                        command.Parameters.AddWithValue("@Nombre", oModel.Nombre);
                        command.Parameters.AddWithValue("@FechaRegistro", oModel.FechaRegistro);

                        int result = command.ExecuteNonQuery();

                        // Check Error
                        if (result < 0)
                            Console.WriteLine("Error inserting data into Database!");

                    }


                    /*String query = "INSERT INTO dbo.SMS_PW (id,username,password,email) VALUES (@id,@username,@password, @email)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", "abc");
                        command.Parameters.AddWithValue("@username", "abc");
                        command.Parameters.AddWithValue("@password", "abc");
                        command.Parameters.AddWithValue("@email", "abc");



                        connection.Open();
                        int result = command.ExecuteNonQuery();



                        // Check Error
                        if (result < 0)
                            Console.WriteLine("Error inserting data into Database!");
                    }*/



                }
                return Ok(oModel);
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
            
        }

    }
}
