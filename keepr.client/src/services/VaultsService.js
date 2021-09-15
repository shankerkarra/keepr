import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
class VaultsService {
  async GetById(id) {
    const res = await api.get(`api/vaults/${id}`)
    AppState.activeVault = res.data
  }

  async getAll() {
    const res = await api.get('api/vaults')
    logger.log(res.data)
    AppState.keeps = res.data
  }

  async GetVaultsByProfileId(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    AppState.vaults = res.data
  }

  async create(data, userId) {
    const res = await api.post('api/vaults', data)
    AppState.vaults = [res.data, ...AppState.vaults]
    this.GetVaultsByProfileId(userId)
  }

  async Update(data, userId) {
    const res = await api.put(`api/profiles/${id}`, data)
    AppState.vaults = [res.data, ...AppState.vaults]
    this.GetVaultsByProfileId(userId)
  }

  async Delete(id, userId) {
    const res = await api.delete(`api/vaults/${id}`)
    AppState.vaults = AppState.vaults.filter(v => v.id !== res.data.id)
  }
}

export const vaultsService = new VaultsService()
