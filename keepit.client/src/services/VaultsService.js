import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class VaultsService {
  async createVault(body) {
    const res = await api.post("api/vaults", body)
    logger.log(res.data)
    AppState.profileVaults = [...AppState.profileVaults, res.data]
  }
  async getProfileVaults(id) {
    const res = await api.get("api/profiles/" + id + "/vaults")
    logger.log(res.data)
    AppState.activeVaults = res.data
  }
  async getVault(id) {
    const res = await api.get("api/vaults/" + id)
    logger.log("this is the vault page and this is the vault", res.data)
    AppState.vaultpage = res.data
  }
  async deleteVault(id) {
    const res = await api.delete("api/vaults/" + id)
    logger.log(res.data)
    AppState.profileVaults = AppState.profileVaults.filter(v => v.id != id)
  }
}
export const vaultsService = new VaultsService();