using System;
using System.Collections.Generic;
using keepit.Models;
using keepit.Repositories;

namespace keepit.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository p_repo;

    public ProfilesService(ProfilesRepository p_repo)
    {
      this.p_repo = p_repo;
    }

    internal Profile GetProfileById(string id)
    {
      Profile found = p_repo.GetProfileById(id);
      if (found == null)
      {
        throw new Exception("This is an Invaild profile Id");
      }
      return found;
    }

    internal List<Keep> GetAllKeeps(string id)
    {
      Profile profile = p_repo.GetProfileById(id);
      return p_repo.GetAllKeeps(profile.Id);

    }
  }
}