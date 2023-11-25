using AngularWebApi.DB.GenericRepository;
using AngularWebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentMasterController : ControllerBase
    {
        private readonly IRepository<Department> _departmentRepository;

        public DepartmentMasterController(IRepository<Department> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        [HttpGet]
        public IActionResult GetDepartments()
        {
            var users = _departmentRepository.GetAll();
            return Ok(users.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartmentAsync(Department department)
        {
            await _departmentRepository.InsertAsync(department);
            return Ok(true);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDepartmentAsync(Department department)
        {
            await _departmentRepository.UpdateAsync(department);
            return Ok(true);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteDepartmentAsync(int id)
        {
            var user = _departmentRepository.Get().Where(x => x.DepartmentId == id).FirstOrDefault();
            if (user != null)
               await _departmentRepository.DeleteAsync(user);
            return Ok(true);
        }
    }
}
