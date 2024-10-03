using Dto;
using Host.Auxiliary;
using Host.Sql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : Controller
    {
        private readonly IConfigurationHelper _config;
        private readonly SqlHandler _handler;
        private readonly string CONNECTION_STRING;

        private const string TABLE_NAME = "Todos";

        public TodoController(IConfigurationHelper config)
        {
            _config = config;

            _handler = new SqlHandler(_config.GetSqlServerName, "TodoApi");
            _handler.EnsureDatabaseExists();
            _handler.EnsureTableExists(TABLE_NAME, true);
            CONNECTION_STRING = _handler.GenerateConnectionString();
        }

        [HttpGet]
        public ActionResult<IEnumerable<ITodo>> Get()
        {
            var products = new List<ITodo>();

            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                string query = $"SELECT * FROM {TABLE_NAME};";

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    connection.Open();

                    IDataReader reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            products.Add(new TodoDto
                            {
                                Id = reader.GetInt32(0),
                                CreatedDate = reader.GetDateTime(1),
                                DueDate = reader.GetDateTime(2),
                                Description = reader.GetString(3),
                                Creator = reader.GetString(4),
                                Alert = reader.GetBoolean(5),
                                Extra1 = reader.GetString(6),
                                Extra2 = reader.GetString(7),
                                Extra3 = reader.GetString(8),
                                Extra4 = reader.GetString(9),
                                Extra5 = reader.GetString(10),
                            });
                        }
                    }
                    finally
                    {
                        reader?.Dispose();
                    }
                }
            }
            return Ok(products);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ITodo todo)
        {
            if (todo == null)
            {
                return BadRequest("Product data is required");
            }

            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                var insertCommand = "INSERT INTO Products (";
                var valuesClause = "VALUES (";

                var parameters = new List<SqlParameter>();
                foreach (PropertyInfo prop in todo.GetType().GetProperties())
                {
                    if (prop.GetValue(todo) != null)
                    {
                        insertCommand += $"[{prop.Name}], ";
                        valuesClause += $"@{prop.Name}, ";

                        parameters.Add(new SqlParameter($"@{prop.Name}", prop.GetValue(todo)));
                    }
                }

                insertCommand = insertCommand.TrimEnd(',', ' ') + ") ";
                valuesClause = valuesClause.TrimEnd(',', ' ') + ");";

                string query = insertCommand + valuesClause;

                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                }
            }

            return Ok("Product created successfully.");
        }
    }
}
