using Microsoft.AspNetCore.Mvc;
using RestAPIDemo.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System;
using System.Globalization;

namespace RestAPIDemo.Controllers
{
    //Definimos la ruta de la API
    [Route("[controller]")]
    [ApiController]
    public class LogZohoPruebasController : Controller
    {
        //Definimos una variable para obtener los valores del archivo "appsettings.json"
        private IConfiguration _configuration;
        //Definimos la varaible donde almaceneremos el resultado del select
        private String consulta;
        public LogZohoPruebasController(IConfiguration iConfiguration)
        {
            _configuration = iConfiguration;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Método Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SabanaLog>>> GetTodoItems()
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
                    Console.WriteLine("=========================================\n");
                    //Escibimos los query que vamos a necesitar, en el primer parentesís lleva el nombre del campo en la bd y en el segundo es el valor de la variable                   
                    String query = "Select top 5 * From LogPruebas Order By IdEvento Desc";
                    //Ejecutamos la query
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                consulta += String.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}\n",
                                    reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6], reader[7], reader[8], reader[9],
                                    reader[10], reader[11], reader[12], reader[13], reader[14], reader[15], reader[16], reader[17], reader[18], reader[19]);
                            }
                        }
                    }
                }
                Console.WriteLine(consulta);
                //Regresamos lo que nos llegó               
                return Ok(consulta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Método POST
        [HttpPost]
        public async Task<ActionResult<SabanaModel>> CreateTodoItem(List<SabanaLog> oModelList)
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
                    Console.WriteLine("=========================================\n");
                    connection.Open();
                    //Escibimos los query que vamos a necesitar, en el primer parentesís lleva el nombre del campo en la bd y en el segundo es el valor de la variable                    
                    String query = "INSERT INTO LogPruebas (IDLinea,Evento,EventoAnterior,EstatusProceso,QuienPagaTiempo,RolOrigen,RolDestino,Etapa,Fase,Fecha,SecuenciaGeneral,SecuenciaDJ,SecuenciaBC,SecuenciaPLD,SecuenciaSPyPyTC,SecuenciaMA,SecuenciaRC,SecuenciaMC,UsuarioCreator) VALUES (@IDLinea,@Evento,@EventoAnterior,@EstatusProceso,@QuienPagaTiempo,@RolOrigen,@RolDestino,@Etapa,@Fase,@Fecha,@SecuenciaGeneral,@SecuenciaDJ,@SecuenciaBC,@SecuenciaPLD,@SecuenciaSPyPyTC,@SecuenciaMA,@SecuenciaRC,@SecuenciaMC,@UsuarioCreator)";
                    //Para cada objeto ejecutamos la query
                    foreach (SabanaLog oModel in oModelList)
                    {
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
                            command.Parameters.AddWithValue("@Fecha", ConvertirFecha(oModel.Fecha));
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
                            {
                                //Console.WriteLine("Error inserting data into Database!");
                                return BadRequest("Error al insertar en base de datos");
                            }
                        }
                    }
                }
                //Regresamos lo que nos llegó
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        public static DateTime ConvertirFecha(string fecha)
        {
            DateTime dateValue;
            if (DateTime.TryParseExact(fecha, "dd-MM-yy HH:mm:ss",
                                      CultureInfo.InvariantCulture,
                                      DateTimeStyles.None,
                                      out dateValue))
            {
                return dateValue;
            }
            else
            {
                return DateTime.MinValue;
            }
        }

    }
}
