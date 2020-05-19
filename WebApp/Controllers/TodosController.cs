using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Repository;

namespace WebApp.Controllers
{
    [Authorize]
    [Route("todos")]
    public class TodosController : ControllerBase
    {
        private readonly IRepository _repository;

        public TodosController(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public async Task<ActionResult<IList<Todo>>> GetAll()
        {
            var todos = await _repository.GetAllAsync();
            return new JsonResult(todos);
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> Add([FromBody]Todo model)
        {
            await _repository.AddTodoAsync(model);
            return new JsonResult(model);
        }
    }
}