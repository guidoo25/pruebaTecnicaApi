using database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace pruebaTecnicaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly CineContext _context;

        public ClienteController(CineContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<CustomerEntity>> PostCustomer(CustomerDto customerDto)
        {
            var customer = new CustomerEntity
            {
                DocumentNumber = customerDto.DocumentNumber,
                Name = customerDto.Name,
                Lastname = customerDto.Lastname,
                Age = customerDto.Age,
                PhoneNumber = customerDto.PhoneNumber,
                Email = customerDto.Email
            };

            _context.CustomerEntities.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerEntity>> GetCustomer(int id)
        {
            var customer = await _context.CustomerEntities.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }
    }
}
