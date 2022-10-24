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
    private readonly DbContext _dbContext;

    public PersonsController(IMapper mapper, DbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PersonDto>> GetByIdAsync(int id)
    {
        return Ok(new PersonDto());
    }

    [HttpGet]
    public async Task<ActionResult<List<PersonDto>>> GetAllAsync()
    {
        return Ok(new List<Person>());
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
        return Ok(new PersonDto());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveAsync(int id)
    {
        return Ok();
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