using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [ApiController]
  [Route("/api/[controller]")]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vks;

    public VaultKeepsController(VaultKeepsService vks)
    {
      _vks = vks;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Vaultkeep>> GetAsync(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Vaultkeep vaultkeep = _vks.GetById(id);
        // Check User is Null & if VAult is Private
        return Ok(vaultkeep);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }


    }


    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vaultkeep>> Create([FromBody] Vaultkeep newVaultkeep)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newVaultkeep.CreatorId = userInfo.Id;
        Vaultkeep vaultkeep = _vks.Create(newVaultkeep);
        return Ok(vaultkeep);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<String>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _vks.Delete(id, userInfo.Id);
        return Ok("DELETED");
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }



  }
}