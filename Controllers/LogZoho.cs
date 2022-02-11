using Microsoft.AspNetCore.Mvc;
using RestAPIDemo.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace RestAPIDemo.Controllers
{
    //Definimos la ruta de la API
    [Route("[controller]")]
    [ApiController]

    public class LogZoho : Controller
    {
        //Definimos una variable para obtener los valores del archivo "appsettings.json"
        private IConfiguration _configuration;
        public LogZoho(IConfiguration iConfiguration)
        {
            _configuration = iConfiguration;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Método POST
        [HttpPost]
        public async Task<ActionResult<SabanaModel>> CreateTodoItem(SabanaLog oModel)
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
                    //Escibimos los query que vamos a necesitar, en el primer parentesís lleva el nombre del campo en la bd y en el segundo es el valor de la variable
                    //String sql = "SELECT * from tlbBitacora";
                    //String query = "INSERT INTO tlbBitacora (Nombre,Fecha) VALUES (@Nombre,@FechaRegistro)";
                    String query = "INSERT INTO LogPrincipal (IDLinea,Evento,EventoAnterior,EstatusProceso,QuienPagaTiempo,RolOrigen,RolDestino,Etapa,Fase,Fecha,SecuenciaGeneral,SecuenciaDJ,SecuenciaBC,SecuenciaPLD,SecuenciaSPyPyTC,SecuenciaMA,SecuenciaRC,SecuenciaMC,UsuarioCreator) VALUES (@IDLinea,@Evento,@EventoAnterior,@EstatusProceso,@QuienPagaTiempo,@RolOrigen,@RolDestino,@Etapa,@Fase,@Fecha,@SecuenciaGeneral,@SecuenciaDJ,@SecuenciaBC,@SecuenciaPLD,@SecuenciaSPyPyTC,@SecuenciaMA,@SecuenciaRC,@SecuenciaMC,@UsuarioCreator)";
                    //Ejecutamos la query
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        //Hacemos la relación entre los datos mandadados y los que se almacenarán en la base
                        command.Parameters.AddWithValue("@IDLinea", oModel.IDLinea);
                        command.Parameters.AddWithValue("@Evento", oModel.Evento);
                        command.Parameters.AddWithValue("@EventoAnterior", oModel.EventoAnterior);
                        command.Parameters.AddWithValue("@EstatusProceso", oModel.EstatusProceso);
                        command.Parameters.AddWithValue("@QuienPagaTiempo", oModel.QuienPagaTiempo);
                        command.Parameters.AddWithValue("@RolOrigen", oModel.RolOrigen);
                        command.Parameters.AddWithValue("@RolDestino", oModel.RolDestino);
                        command.Parameters.AddWithValue("@Etapa", oModel.Etapa);
                        command.Parameters.AddWithValue("@Fase", oModel.Fase);
                        command.Parameters.AddWithValue("@Fecha", oModel.Fecha);
                        command.Parameters.AddWithValue("@SecuenciaGeneral", oModel.SecuenciaGeneral);
                        command.Parameters.AddWithValue("@SecuenciaDJ", oModel.SecuenciaDJ);
                        command.Parameters.AddWithValue("@SecuenciaBC", oModel.SecuenciaBC);
                        command.Parameters.AddWithValue("@SecuenciaPLD", oModel.SecuenciaPLD);
                        command.Parameters.AddWithValue("@SecuenciaSPyPyTC", oModel.SecuenciaSPyPyTC);
                        command.Parameters.AddWithValue("@SecuenciaMA", oModel.SecuenciaMA);
                        command.Parameters.AddWithValue("@SecuenciaRC", oModel.SecuenciaRC);
                        command.Parameters.AddWithValue("@SecuenciaMC", oModel.SecuenciaMC);
                        command.Parameters.AddWithValue("@UsuarioCreator", oModel.UsuarioCreator);
                            
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
