using System;
using System.Collections.Generic;
using keepit.Models;
using keepit.Services;
using Microsoft.AspNetCore.Mvc;

namespace keepit.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _ps;

    public ProfilesController(ProfilesService ps)
    {
      _ps = ps;
    }
    [HttpGet("{id}")]
    public ActionResult<Profile> GetByProfileById(string id)
    {
      try
      {
        Profile profile = _ps.GetProfileById(id);
        return Ok(profile);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}/keeps")]
    public ActionResult<List<Keep>> GetAllKeeps(string id)
    {
      try
      {
        List<Keep> keeps = _ps.GetAllKeeps(id);
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}