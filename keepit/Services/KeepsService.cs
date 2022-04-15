using System;
using System.Collections.Generic;
using keepit.Models;
using keepit.Repositories;

namespace keepit.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository ks_repo;

    public KeepsService(KeepsRepository ks_repo)
    {
      this.ks_repo = ks_repo;
    }

    internal Keep Create(Keep keepData)
    {
      return ks_repo.Create(keepData);
    }

    internal List<Keep> GetAll()
    {
      return ks_repo.GetAll();
    }

    internal Keep GetById(int id)
    {
      Keep found = ks_repo.GetById(id);
      if (found == null)
      {
        throw new Exception("Invaild Keep ID ");
      }
      return found;
    }

    internal Keep Edit(Keep updates, Account userInfo)
    {
      Keep og = ks_repo.GetById(updates.Id);
      if (og.CreatorId != userInfo.Id)
      {
        throw new Exception("This is not your keep to change");
      }
      og.Name = updates.Name ?? og.Name;
      og.Img = updates.Img ?? og.Img;
      og.Description = updates.Description ?? og.Description;
      og.Views = updates.Views;
      og.Kept = updates.Kept;
      ks_repo.Edit(og);
      return og;

    }

    internal string Remove(int id, Account userInfo)
    {
      Keep keep = ks_repo.GetById(id);
      if (keep.CreatorId != userInfo.Id)
      {
        throw new Exception("Not Your keep!!");
      }
      return ks_repo.Remove(id);
    }
  }
}