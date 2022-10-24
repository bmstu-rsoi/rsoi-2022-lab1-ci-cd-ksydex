using API.Data;
using API.Data.Dtos;
using API.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _dbContext;

    public PersonsController(IMapper mapper, AppDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PersonDto>> GetByIdAsync(int id)
    {
        var e = await _dbContext.Set<Person>()
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (e == null) return NotFound();
        
        return Ok(_mapper.Map<PersonDto>(e));
    }

    [HttpGet]
    public async Task<ActionResult<List<PersonDto>>> GetAllAsync()
    {
        var lst = await _dbContext.Set<Person>()
            .ToListAsync();
        
        return Ok(_mapper.Map<List<PersonDto>>(lst));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] PersonUpdateDto dto)
    {
        if (new List<object?>
            {
                dto.Name,
                dto.Address,
                dto.Age
            }.Any(x => x == null))
            return BadRequest("All properties should not be empty.");

        var e = new Person();
        MapUpdateDtoToEntity(e, dto);

        await _dbContext.AddAsync(e);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction("GetById", new { id = e.Id }, e);
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult<PersonDto>> UpdateAsync([FromBody] PersonUpdateDto dto, int id)
    {
        var e = await _dbContext.Set<Person>()
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (e == null) return NotFound();

        MapUpdateDtoToEntity(e, dto);

        await _dbContext.SaveChangesAsync();
        
        return Ok(_mapper.Map<PersonDto>(e));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveAsync(int id)
    {
        var e = await _dbContext.Set<Person>()
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (e == null) return NotFound();
        
        return NoContent();
    }

    private void MapUpdateDtoToEntity(Person e, PersonUpdateDto dto)
    {
        if (dto.Name != null)
            e.Name = dto.Name;
        if (dto.Age != null)
            e.Age = dto.Age.Value;
        if (dto.Address != null)
            e.Address = dto.Address;
    }
}