<template>
  <div class="card m-1 px-1 py-1" style="width: 19rem;">
    <h4 class="py-1 px-2">
      {{ vault.name }}
    </h4>
  </div>
</template>

<script>
import { computed, onMounted, ref } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { profilesService } from '../services/ProfilesService'
import { useRoute } from 'vue-router'
import Pop from '../utils/Notifier'

export default {
  setup() {
    const route = useRoute()
    const loading = ref(true)
    onMounted(async() => {
      try {
        // await keepsService.getAll()
        await profilesService.GetVaultsByProfileId(route.params.id)
        loading.value = false
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      account: computed(() => AppState.account)
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
