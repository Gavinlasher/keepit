using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepit.Models;
using keepit.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepit.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vks;

    public VaultKeepsController(VaultKeepsService vks)
    {
      _vks = vks;
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep vaultKeepData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        vaultKeepData.CreatorId = userInfo.Id;
        VaultKeep vaultkeep = _vks.Create(vaultKeepData);
        return Ok(vaultkeep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}/keeps")]


    public ActionResult<VaultKeep> GetById(int id)
    {
      try
      {
        VaultKeep vaultkeep = _vks.GetById(id);
        return Ok(vaultkeep);

      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Remove(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _vks.Remove(id, userInfo.Id);
        return Ok("Deleted the VaultKeep");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}