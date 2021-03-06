using System;
using System.Collections.Generic;
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
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vs;
    private readonly VaultKeepsService _vks;

    public VaultsController(VaultsService vs, VaultKeepsService vks)
    {
      _vs = vs;
      _vks = vks;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Vault>> GetAsync(int id)
    {
      try
      {
        // Check User is Null & if VAult is Private
        // if (userInfo !- null)
        // {
        // }
        bool userSignIn = false;
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        if (userInfo != null)
        {
          userSignIn = true;
        }
        else { userSignIn = false; }
        // Need to pass on the userSign & userInfo.id
        Vault vault = _vs.GetById(id, userSignIn, userInfo?.Id);
        return Ok(vault);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    public async Task<ActionResult<VaultKeepViewModel>> GetKeepsByVaultId(int id)
    {
      try
      {
        bool userSignIn = false;
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        if (userInfo != null)
        {
          userSignIn = true;
        }
        else { userSignIn = false; }
        // Need to pass on the userSign & userInfo.id
        List<VaultKeepViewModel> vkvm = _vks.GetKeepsByVaultId(id, userSignIn, userInfo?.Id);
        return Ok(vkvm);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    // NOTE Need to validate
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault newVault)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newVault.CreatorId = userInfo.Id;
        Vault vault = _vs.Create(newVault);
        return Ok(vault);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Edit([FromBody] Vault updatedVault, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        updatedVault.CreatorId = userInfo.Id;
        updatedVault.Id = id;
        Vault vault = _vs.Edit(updatedVault);
        return Ok(vault);
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
        _vs.Delete(id, userInfo.Id);
        return Ok("DELETED");
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

  }
}