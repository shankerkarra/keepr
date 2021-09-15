<template>
  <div class="Profile text-center">
    <div class="row">
      <div class="Col">
        <img class="rounded" :src="account.picture" alt="user Profile" />
      </div>
      <div class="Col">
        <h1>{{ account.name }}</h1>
        <h2>{{ state.vaults.name }}</h2>
        <h5>Keeps: {{ state.keeps.Length }}</h5>
        <br>
      </div>
    </div>
    <h5 class="pt-2 hoverable" @change="destroy($event.target.value)">
      🗑
    </h5>
    <div div class="row m-2">
      <h5 class="pt-2 hoverable" @change="destroy($event.target.value)">
        🗑
      </h5>
    </div>
    <div div class="row m-2">
      <h2>Keeps:</h2>
      <!-- Add Mdi + button -->
      <ProfileKeep v-for="keep in state.keeps" :key="keep.id" :keep="keep" />
      >
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import Pop from '../utils/Notifier'
import { vaultsService } from '../services/VaultsService'

export default {
  name: 'Account',
  setup() {
    onMounted(async() => {
      try {
        await keepsService.getAll()
        await vaultsService.getAll()
      } catch (error) {
        Pop.Toast(error, 'error')
      }
    })
    // const route = useRoute()
    const state = reactive({
      profile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults)
    })
    return {
      state,
      account: computed(() => AppState.account)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
