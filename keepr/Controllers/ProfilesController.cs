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
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _ps;
    private readonly KeepsService _ks;
    private readonly VaultsService _vs;

    public ProfilesController(ProfilesService ps, KeepsService ks, VaultsService vs)
    {
      _ps = ps;
      _ks = ks;
      _vs = vs;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Profile>> Get()
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_ps.GetOrCreateProfile(userInfo));
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}")]
    // [Authorize]
    public ActionResult<Profile> GetById(string id)
    {
      try
      {
        Profile profile = _ps.GetById(id);
        return Ok(profile);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    // [Authorize]
    public async Task<ActionResult<List<Keep>>> GetKeepsByProfileId(String id)
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
        List<Keep> keeps = _ks.GetKeepsByProfileId(id, userSignIn, userInfo?.Id);
        return Ok(keeps);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}/vaults")]
    // [Authorize]
    public async Task<ActionResult<List<Vault>>> GetVaultsByProfileId(String id)
    {
      try
      {
        // NOTE  Need to look into
        bool userSignIn = false;
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        if (userInfo != null)
        {
          userSignIn = true;
        }
        else { userSignIn = false; }
        // Need to pass on the userSign & userInfo.id
        List<Vault> Vaults = _vs.GetVaultsByProfileId(id, userSignIn, userInfo?.Id);
        return Ok(Vaults);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }
}
