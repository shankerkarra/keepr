import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
class KeepsService {
  async getAll() {
    const res = await api.get('api/keeps')
    logger.log(res.data)
    AppState.keeps = res.data
  }

  async create(keep) {
    const res = await api.post('api/keeps', keep)
    await this.getAll()
  }

  async destroy(id) {
    await api.delete('api/keeps/' + id)
    AppState.keeps = AppState.keeps.filter(p => p.id !== id)
  }
}

export const keepsService = new KeepsService()
