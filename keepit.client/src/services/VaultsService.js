import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class VaultsService {
  async createVault(body) {
    const res = await api.post("api/vaults", body)
    logger.log(res.data)
    AppState.profileVaults = [...AppState.profileVaults, res.data]
  }
}
export const vaultsService = new VaultsService();