<template>
  <div class="Profile text-center">
    <div class="row">
      <div class="Col">
        <img class="rounded" :src="account.picture" alt="USer Profile" />
      </div>
      <div class="Col">
        <h1>{{ account.name }}</h1>
        <h5>Vaults:{{ state.vaults }}</h5>
        <h5>Keeps: {{ state.keeps.toLength }}</h5>
      </div>
    </div>
    <div div class="row m-2">
      <h2>Vaults:</h2>
      <!-- Add Mdi + button -->
      <!-- <ProfileVault v-for="vault in state.vaults" :key="vault.id" :vault="vault" /> -->
      <!--  vaults v-for-->
    </div>
    <div div class="row m-2">
      <h2>Keeps:</h2>
      <!-- Add Mdi + button -->
      <ProfileKeep v-for="keep in state.keeps" :key="keep.id" :keep="keep" />
      >
    </div>
    <CreateNewVaultModal.vue />
    <CreateNewKeepModal.vue />
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
// import { useRoute } from 'vue-router'

export default {
  name: 'Account',
  setup() {
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
