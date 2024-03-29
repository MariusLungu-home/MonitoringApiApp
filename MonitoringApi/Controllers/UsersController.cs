﻿using Microsoft.AspNetCore.Mvc;
using MonitoringApi.Models;

namespace MonitoringApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    ILogger<UsersController> _logger;
    public UsersController(ILogger<UsersController> logger)
    {
        _logger = logger;
    }
    // GET: api/<UsersController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        throw new Exception("Something bad happened here!");
    }

    // GET api/<UsersController>/5
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        if (id < 0 || id > 100)
        {
            _logger.LogWarning("The given Id if {Id} is invalid", id);
            return BadRequest("The index was out of range.");
        }

        _logger.LogInformation(@"The api\Users\{id} was called", id);
        return Ok($"Value: {id}");
    }

    // POST api/<UsersController>
    [HttpPost]
    public IActionResult Post([FromBody] UserModel value)
    {
        if (ModelState.IsValid)
        {

            _logger.LogInformation("The given model is valid");
            return Ok();
        }
        else
        {
            _logger.LogWarning("The given model is invalid");
            return BadRequest(ModelState);
        }
    }

    // PUT api/<UsersController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<UsersController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
