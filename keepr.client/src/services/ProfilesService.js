import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
class ProfilesService {
  //  Do we need to write for Profile GET

  async GetById(id) {
    const res = await api.get(`api/profiles/${id}`)
    AppState.activeProfile = res.data
  }

  async GetKeepsByProfileId(id) {
    const res = await api.get(`api/profiles/${id}/keeps`)
    AppState.keeps = res.data
    // logger.log(res)
  }

  async GetVaultsByProfileId(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    AppState.vaults = res.data
    logger.log(res)
  }

  async Create(data, userId) {
    const res = await api.post('api/vaults', data)
    AppState.vaults = [res.data, ...AppState.vaults]
    this.GetVaultsByProfileId(userId)
  }

  async Update(data, id) {
    // eslint-disable-next-line no-template-curly-in-string
    const res = await api.put(`api/vaults/${id}`, data)
    AppState.vaults = [res.data, ...AppState.vaults]
    this.GetVaultsByProfileId(id)
  }

  async Delete(id, userId) {
    const res = await api.delete(`api/vaults/${id}`)
    AppState.vaults = AppState.vaults.filter(v => v.id !== res.data.id)
  }
}

export const profilesService = new ProfilesService()
