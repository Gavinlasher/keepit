import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
  async getVaults() {
    const res = await api.get("account/vaults")
    logger.log(res.data, 'this is the vaults of this users')
    AppState.userVaults = res.data
  }
}

export const accountService = new AccountService()
