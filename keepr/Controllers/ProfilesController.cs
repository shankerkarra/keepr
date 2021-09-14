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
    [Authorize]
    public ActionResult<Profile> GetById(string Id)
    {
      try
      {
        Profile profile = _ps.GetById(Id);
        return Ok(profile);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    [Authorize]
    public async Task<ActionResult<List<Keep>>> GetKeepsByProfileId(String Id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        // Need to validate the parameter ID to UserInfo.Id
        List<Keep> keeps = _ks.GetKeepsByProfileId(Id);
        return Ok(keeps);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}/vaults")]
    [Authorize]
    public async Task<ActionResult<List<Vault>>> GetVaultsByProfileId(String Id)
    {
      try
      {
        // NOTE  Need to look into
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<Vault> Vaults = _vs.GetVaultsByProfileId(Id);
        return Ok(Vaults);

      }
      catch (Exception)
      {
        throw new Exception();
        // return BadRequest(err.Message);
      }
    }
  }
}