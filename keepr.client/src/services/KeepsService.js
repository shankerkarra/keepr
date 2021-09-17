import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
class KeepsService {
  async getAll() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data
  }

  async GetById(keep) {
    const res = await api.get(`api/keeps/${keep.id}`)
    keep.views += 1
    AppState.activeKeep = keep
  }

  async create(keep, userId) {
    const res = await api.post('api/keeps', keep)
    AppState.vaults = [res.data, ...AppState.vaults]
    this.GetKeepsByProfileId(userId)
  }

  async Update(data, userId) {
    // eslint-disable-next-line no-template-curly-in-string
    const res = await api.put('api/keeps/${id}', data)
    AppState.keeps = [res.data, ...AppState.vaults]
    this.GetKeepsByProfileId(userId)
  }

  async delete(id) {
    await api.delete('api/keeps/' + id)
    AppState.keeps = AppState.keeps.filter(p => p.id !== id)
  }

  // NOTE Cross check whether this is the right place for the below

  async getKeepsByVaultId(id) {
    const res = await api.get(`api/vaults/${id}/keeps`)
    AppState.KeepsByVault = res.data
    // logger.log(AppState.KeepsByVault)
  }

  async GetKeepsByProfileId(id) {
    const res = await api.get(`api/profiles/${id}/keeps`)
    AppState.myKeeps = res.data
  }

  async addKeeptoVault(data) {
    await api.post('api/vaultKeeps', data)
    data.keeps++
  }

  async deleteVaultKeep(id, vaultId) {
    await api.delete(`api/vaultkeeps/${id}`)
    this.getKeepsByVaultId(vaultId)
  }
}

export const keepsService = new KeepsService()
