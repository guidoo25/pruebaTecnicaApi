using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using database;
using pruebaTecnicaApi.Services;
using System;

[Route("api/[controller]")]
[ApiController]
public class CarteleraController : ControllerBase
{
    private readonly CineContext _context;
    private readonly BillboardServices _billboardServices;

    public CarteleraController(CineContext context, BillboardServices billboardServices)
    {
        _context = context;
        _billboardServices = billboardServices;
    }

    // GET: api/Billboards
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BillboardEntity>>> GetBillboards()
    {
        try
        {
            return await _context.BillboardEntities.ToListAsync();
        }
        catch (Exception ex)
        {
    
            return CustomExceptionHandler.HandleException<IEnumerable<BillboardEntity>>(ex);
        }
    }

    // GET: api/Billboards/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BillboardEntity>> GetBillboard(int id)
    {
        try
        {
            var billboard = await _context.BillboardEntities.FindAsync(id);

            if (billboard == null)
            {
                return NotFound();
            }

            return billboard;
        }
        catch (Exception ex)
        {

            return CustomExceptionHandler.HandleException<BillboardEntity>(ex);
        }
    }

    // PUT: api/Billboards/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBillboard(int id, BillboardEntity billboard)
    {
        try
        {
            if (id != billboard.Id)
            {
                return BadRequest();
            }

            _context.Entry(billboard).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {

            return CustomExceptionHandler.HandleException(ex);
        }
    }

    /*
    "Date": "2024-01-31T00:00:00",
    "StartTime": "18:00:00",
    "EndTime": "20:00:00",
    "MovieId": 2,
    "RoomId": 1
} */
    [HttpPost]
    public async Task<ActionResult<BillboardEntity>> PostBillboard(BillboardDto billboardDto)
    {
        try
        {
            var movie = await _context.MovieEntities.FindAsync(billboardDto.MovieId);
            if (movie == null)
            {
                return BadRequest(new { message = "no se encontro el  movie id." });
            }

            var room = await _context.RoomEntities.FindAsync(billboardDto.RoomId);
            if (room == null)
            {
                return BadRequest(new { message = "no se encontro room id." });
            }

            var billboard = new BillboardEntity
            {
                Date = billboardDto.Date,
                StartTime = billboardDto.StartTime,
                EndTime = billboardDto.EndTime,
                MovieId = billboardDto.MovieId,
                RoomId = billboardDto.RoomId
            };

            _context.BillboardEntities.Add(billboard);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBillboard), new { id = billboard.Id }, billboard);
        }
        catch (Exception ex)
        {
            return CustomExceptionHandler.HandleException<BillboardEntity>(ex);
        }
    }

    [HttpPut("cancel/{billboardId}")]
    public async Task<IActionResult> CancelBillboardAndAllBookings(int billboardId)
    {
        try
        {
            await _billboardServices.CancelBillboardAndAllBookings(billboardId);
            return Ok();
        }
        catch (Exception ex)
        {
            // Handle exception using custom exception handler
            return CustomExceptionHandler.HandleException(ex);
        }
    }

    private bool BillboardExists(int id)
    {
        return _context.BillboardEntities.Any(e => e.Id == id);
    }
}

public static class CustomExceptionHandler
{
    public static ActionResult<T> HandleException<T>(Exception ex)
    {


        return new BadRequestObjectResult(new { message = "An error occurred." });
    }

    public static IActionResult HandleException(Exception ex)
    {


        return new BadRequestObjectResult(new { message = "An error occurred." });
    }
}