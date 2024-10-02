using Host.Auxiliary;
using Host.Dtos;
using Host.Sql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : Controller
    {
        private readonly IConfigurationHelper _config;
        private readonly SqlHandler _handler;

        public TodoController(IConfigurationHelper config)
        {
            _config = config;

            _handler = new SqlHandler(_config.GetSqlServerName, "TodoApi");
            _handler.EnsureDatabaseExists();
            _handler.EnsureTableExists("Todos");
        }

        [HttpPost]
        public IActionResult Post([FromBody] ITodo todo)
        {
            if (todo == null)
            {
                return BadRequest("Product data is required");
            }

            using (SqlConnection connection = new SqlConnection(_handler.GenerateConnectionString()))
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

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters.ToArray());

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            return Ok("Product created successfully.");
        }
    }
}
