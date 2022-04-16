using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepit.Models;

namespace keepit.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal VaultKeep Create(VaultKeep vaultKeepData)
    {
      string sql = @"
     INSERT INTO vaultkeeps
     (vaultId, keepId, creatorId)
     VALUES
     (@VaultId,@KeepId, @CreatorId);
     SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, vaultKeepData);
      vaultKeepData.Id = id;
      return vaultKeepData;
    }

    internal VaultKeep GetById(int id)
    {
      string sql = @"
      SELECT 
        vk.*,
        a.* 
      FROM vaultkeeps vk
      JOIN accounts a ON vk.creatorId = a.id
      WHERE vk.id = @id;
      ";
      return _db.Query<VaultKeep, Account, VaultKeep>(sql, (vk, a) =>
      {

        vk.Creator = a;
        return vk;
      }, new { id }).FirstOrDefault();
    }

    internal void Remove(int id)
    {
      string sql = "DELETE FROM vaultkeeps WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }

    internal List<KeepsViewModal> GetKeepsByVaultId(int VaultId)
    {
      string sql = @"
      SELECT 
        k.*,
        vk.id AS VaultKeepId,
        a.*
      FROM vaultkeeps vk
      JOIN keeps k ON k.id = vk.keepId
      JOIN accounts a ON a.id = vk.creatorId
      WHERE vk.vaultId = @VaultId
      ";
      return _db.Query<KeepsViewModal, Account, KeepsViewModal>(sql, (keepsview, a) =>
      {
        keepsview.Creator = a;
        return keepsview;
      }, new { VaultId }).ToList();
    }
  }
}