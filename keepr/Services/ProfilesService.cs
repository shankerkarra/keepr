using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _prepo;
    private readonly KeepsRepository _krepo;

    private readonly VaultsRepository _vrepo;

    public ProfilesService(ProfilesRepository prepo, KeepsRepository krepo, VaultsRepository vrepo)
    {
      _prepo = prepo;
      _krepo = krepo;
      _vrepo = vrepo;
    }

    internal string GetProfileEmailById(string id)
    {
      return _prepo.GetById(id).Email;
    }
    internal Profile GetProfileByEmail(string email)
    {
      return _prepo.GetByEmail(email);
    }
    internal Profile GetOrCreateProfile(Profile userInfo)
    {
      Profile profile = _prepo.GetById(userInfo.Id);
      if (profile == null)
      {
        return _prepo.Create(userInfo);
      }
      return profile;
    }

    public Profile GetById(string Id)
    {
      Profile profile = _prepo.GetById(Id);
      if (profile == null)
      {
        throw new Exception("Invalid Id");
      }
      return profile;
    }
    public List<Keep> GetKeepsByProfileId(String ProfileId)
    {
      List<Keep> keeps = _krepo.GetKeepsByProfileId(ProfileId);
      return keeps;
    }
    public List<Vault> GetVaultByProfileId(String ProfileId)
    {
      List<Vault> vaults = _vrepo.GetVaultsByProfileId(ProfileId);
      return vaults;
    }

    internal Profile Edit(Profile editData, string userEmail)
    {
      Profile original = GetProfileByEmail(userEmail);
      original.Name = editData.Name.Length > 0 ? editData.Name : original.Name;
      original.Picture = editData.Picture.Length > 0 ? editData.Picture : original.Picture;
      return _prepo.Edit(original);
    }

  }

}

