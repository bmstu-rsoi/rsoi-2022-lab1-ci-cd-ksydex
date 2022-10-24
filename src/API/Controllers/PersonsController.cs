using API.Data.Dtos;
using API.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonsController : ControllerBase
{
    private readonly IMapper _mapper;

    public PersonsController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PersonDto>> GetByIdAsync(int id)
    {
        
    }
    
    [HttpGet]
    public async Task<ActionResult<List<PersonDto>>> GetAllAsync()
    {
        return Ok(new List<Person>());
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] PersonDto dto)
    {
        return Created("uri", "");
    }
    
    [HttpPatch("{id:int}")]
    public async Task<ActionResult<PersonDto>> UpdateAsync([FromBody] PersonDto dto, int id)
    {

        return Ok(new PersonDto());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveAsync(int id)
    {
        return Ok();
    }
}