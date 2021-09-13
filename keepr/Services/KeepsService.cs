using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _krepo;

    public KeepsService(KeepsRepository krepo)
    {
      _krepo = krepo;
    }
    internal List<Keep> GetAll()
    {
      return _krepo.GetAll();
    }

    internal Keep GetById(int id)
    {
      Keep found = _krepo.GetById(id);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      found.Views += 1;
      Keep updkeepview = _krepo.AddViewCount(found);
      return found;
    }
    internal Keep Create(Keep newKeep)
    {
      return _krepo.Create(newKeep);
    }


    public Keep EditKeepsCount(Keep keep)
    {
      return _krepo.EditKeepsCount(keep);
    }

    public List<Keep> GetKeepsByProfileId(string profileId)
    {
      List<Keep> keeps = _krepo.GetKeepsByProfileId(profileId);
      return keeps;
    }
    internal Keep Edit(Keep UpdatedKeep)
    {
      Keep original = GetById(UpdatedKeep.Id);
      if (original.CreatorId != UpdatedKeep.CreatorId)
      {
        throw new Exception("You are not the Creator!");
      }
      original.Name = UpdatedKeep.Name ?? original.Name;
      original.Description = UpdatedKeep.Description ?? original.Description;
      original.Img = UpdatedKeep.Img ?? original.Img;
      original.Keeps = UpdatedKeep.Keeps != 0 ? UpdatedKeep.Keeps : original.Keeps;
      original.Shares = UpdatedKeep.Shares != 0 ? UpdatedKeep.Shares : original.Shares;
      original.Views = UpdatedKeep.Views != 0 ? UpdatedKeep.Views : original.Views;
      _krepo.Edit(original);
      return original;
    }

    internal void Delete(int KeepId, string userId)
    {
      Keep toDelete = GetById(KeepId);
      if (toDelete.CreatorId != userId)
      {
        throw new Exception("Thats not your Keep");
      }
      _krepo.Delete(KeepId);
    }
  }
}