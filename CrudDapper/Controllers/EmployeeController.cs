using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using CrudDapper.Services;
using CrudDapper.Models;

namespace CrudDapper.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : ControllerBase {
		private readonly ServiceEmployee _serviceEmployee;
		public EmployeeController(ServiceEmployee serviceEmployee) {
			_serviceEmployee = serviceEmployee;
		}

		[HttpGet]
		[Route("List")]
		public async Task<ActionResult<List<Employee>>> GetEmployees() {
			return Ok(await _serviceEmployee.List());
		}

		[HttpGet]
		[Route("ListId/{id}")]
		public async Task<ActionResult<List<Employee>>> GetEmployeeId(int id) {

			var employee = await _serviceEmployee.ListId(id);

			if (employee == null)
				return NotFound("Not found employee");
			else
				return Ok(employee);
		}

		[HttpPost]
		[Route("Create")]
		public async Task<ActionResult> SaveEmployee(Employee employee) {

			var response = await _serviceEmployee.Create(employee);

			if (response != "")
				return BadRequest(response);
			else
				return Ok("Registered employee");
		}

		[HttpPut]
		[Route("Edit")]
		public async Task<ActionResult> EditEmployee(Employee employee) {

			var response = await _serviceEmployee.Update(employee);

			if (response != "")
				return BadRequest(response);
			else
				return Ok("Edited employee");
		}

		[HttpDelete]
		[Route("Delete")]
		public async Task<ActionResult> DeleteEmployee(int id) {

			await _serviceEmployee.Remove(id);
			return Ok("Deleted employee");
			
		}

	}
}
