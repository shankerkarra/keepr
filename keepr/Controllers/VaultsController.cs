using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
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
        Vault vault = _vs.GetById(id, userSignIn, userInfo.Id);
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
        List<VaultKeepViewModel> vkvm = _vks.GetKeepsByVaultId(id, userSignIn, userInfo.Id);
        return Ok(vkvm);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }
}