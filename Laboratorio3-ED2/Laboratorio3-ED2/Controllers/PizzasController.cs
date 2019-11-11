using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Laboratorio3_ED2.Models;
using Laboratorio3_ED2.Services;


namespace Laboratorio3_ED2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController:ControllerBase
    {
        private readonly PizzaService _pizzaService;
        public PizzasController(PizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }

        [System.Web.Http.HttpGet]
        public ActionResult<List<PizzaModel>> Get() =>
            _pizzaService.Get();

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<PizzaModel> Get(string id)
        {
            var pizza = _pizzaService.Get(id);

            if (pizza == null)
            {
                return NotFound();
            }

            return Ok(pizza);
        }

        [HttpPost]
        public ActionResult<PizzaModel> Create(PizzaModel Pizza)
        {
            _pizzaService.Create(Pizza);

            return CreatedAtRoute("GetBook", new { id = Pizza.Id.ToString() }, Pizza);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, PizzaModel NewPizza)
        {
            var pizza = _pizzaService.Get(id);
            if (pizza == null)
            {
                return NotFound();
            }
            NewPizza.Id = id;
            _pizzaService.Update(id, NewPizza);

            return Ok();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var pizza = _pizzaService.Get(id);

            if (pizza == null)
            {
                return NotFound();
            }

            _pizzaService.Remove(pizza.Id);

            return Ok();
        }
        
    }
}
