using CrudDapper.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CrudDapper.Services {
	public class ServiceEmployee {

		private readonly IConfiguration _configuration;
		private readonly string cadenaSql;

		public ServiceEmployee(IConfiguration configuration) {
			_configuration = configuration;
			cadenaSql = _configuration.GetConnectionString("CadenaSQL")!;
		}
		/*******************************************************************/
		public async Task<List<Employee>> List() {
			string query = "sp_getEmployees";
			using (var conn = new SqlConnection(cadenaSql)) {
				var list = await conn.QueryAsync<Employee>(query, commandType: CommandType.StoredProcedure);
				return list.ToList();
			}
		}
		/*******************************************************************/
		public async Task<Employee> ListId(int id) {
			string query = "sp_getEmployee";
			var parameters = new DynamicParameters();

			parameters.Add("@idEmployee", id, dbType: DbType.Int32);

			using (var conn = new SqlConnection(cadenaSql)) {
				var employee = await conn.QueryFirstOrDefaultAsync<Employee>(query, parameters, commandType: CommandType.StoredProcedure);
				return employee;
			}
		}
		/*******************************************************************/
		public async Task<string> Create(Employee employee) {
			string query = "sp_createEmployee";
			var parameters = new DynamicParameters();

			parameters.Add("@documentNumber", employee.DocumentNumber, dbType: DbType.String);
			parameters.Add("@fullName", employee.FullName, dbType: DbType.String);
			parameters.Add("@salary", employee.Salary, dbType: DbType.Int32);
			parameters.Add("@msgError", dbType: DbType.String,direction:ParameterDirection.Output, size:100);

			using (var conn = new SqlConnection(cadenaSql)) {
				await conn.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
				return parameters.Get<string>("@msgError");
			}
		}
		/*******************************************************************/
		public async Task<string> Update(Employee employee) {
			string query = "sp_updateEmployee";
			var parameters = new DynamicParameters();

			parameters.Add("@idEmployee", employee.IdEmployee, dbType: DbType.Int32);
			parameters.Add("@documentNumber", employee.DocumentNumber, dbType: DbType.String);
			parameters.Add("@fullName", employee.FullName, dbType: DbType.String);
			parameters.Add("@salary", employee.Salary, dbType: DbType.Int32);
			parameters.Add("@msgError", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);

			using (var conn = new SqlConnection(cadenaSql)) {
				await conn.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
				return parameters.Get<string>("@msgError");
			}
		}
		/*******************************************************************/
		public async Task Remove(int id) {
			string query = "sp_deleteEmployee";
			var parameters = new DynamicParameters();

			parameters.Add("@idEmployee", id, dbType: DbType.Int32);
			
			using (var conn = new SqlConnection(cadenaSql)) {
				await conn.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
			}
		}
		/*******************************************************************/
	}
}
