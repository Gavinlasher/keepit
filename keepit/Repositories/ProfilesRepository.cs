using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepit.Models;

namespace keepit.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Profile GetProfileById(string id)
    {
      string sql = "SELECT * FROM accounts WHERE id = @id";
      return _db.Query<Profile>(sql, new { id }).FirstOrDefault();
    }

    internal List<Keep> GetAllKeeps(string id)
    {
      string sql =
      @"
      SELECT
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON a.id = k.creatorId 
      WHERE k.creatorId = @id
      ";
      return _db.Query<Keep, Account, Keep>(sql, (keep, a) =>
      {
        keep.Creator = a;
        return keep;
      }, new { id }).ToList();
    }

    internal List<Vault> GetAllVaults(string id)
    {
      string sql = "SELECT * FROM vaults WHERE creatorId = @id AND isPrivate = false";

      return _db.Query<Vault>(sql, new { id }).ToList();
    }

    internal Vault GetVaultByProfile(string id)
    {
      string sql = @"
      SELECT * FROM vaults WHERE vaults.creatorId = @id;
      ";
      return _db.Query<Vault>(sql, new { id }).FirstOrDefault();
    }
  }
}