import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class KeepsService {
  async getAllKeeps() {
    const res = await api.get("api/keeps")
    logger.log(res.data)
    AppState.keeps = res.data
  }
  async getActiveKeep(id) {
    const res = await api.get('api/keeps/' + id)
    logger.log(res.data)
    AppState.activeKeep = res.data
  }
  async removeKeep(id) {
    const res = await api.delete("api/keeps/" + id)
    logger.log(res.data)
    AppState.keeps = AppState.keeps.filter(k => k.id != id)
  }
  async createKeep(body) {
    const res = await api.post("api/keeps", body)
    logger.log(res.data)
    AppState.keeps = [...AppState.keeps, res.data]
  }
}
export const keepsService = new KeepsService();