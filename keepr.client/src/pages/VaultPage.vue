<template>
  <div class="container-fluid">
    <div class="row m-2">
      <div v-if="loading" class="col text-center">
        <i class="fa fa-spinner fa-spin" aria-hidden="true"></i>
      </div>
      <div v-else class="col">
        <h3>
          {{ activeVault.name }} - {{ activeVault.id }} <h3 />
        </h3>
        <div v-if="keepsbyvault.length != 0">
          <div class="col-2 justify-content-center">
            <h5 class="pt-2 hoverable" @click="destroy($event.target.value)">
              🗑
            </h5>
          </div>
          <div class="row">
            <div v-if="loading" class="col-3 text-center">
              <i class="fa fa-spinner fa-spin" aria-hidden="true"></i>
            </div>
            <div v-else class="col m-3">
              <VaultKeepCard v-for="k in keepsbyvault" :key="k.id" :keep="k" />
            </div>
          </div>
        </div>
        <div v-else>
        <!-- <div class="row m-4">
          <h3>
            No Keeps to display.
          </h3>
        </div> -->
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive, onMounted, ref } from 'vue'
import { AppState } from '../AppState'
import { useRoute, useRouter } from 'vue-router'
import Pop from '../utils/Notifier'
import { vaultsService } from '../services/VaultsService'
import { keepsService } from '../services/KeepsService'

export default {
  name: 'Vault',
  setup() {
    const route = useRoute()
    const router = useRouter()
    const loading = ref(true)
    const emptyVault = ref(true)
    onMounted(async() => {
      try {
        await vaultsService.GetById(route.params.id)
        await keepsService.getKeepsByVaultId(AppState.activeVault.id)
        if (AppState.KeepsByVault === 0) {
          emptyVault.value = false
        }
        if (emptyVault.value) { router.push({ name: 'Home' }) }
        loading.value = false
      } catch (error) {
        Pop.toast(error, 'error')
        router.push({ name: 'Home' })
      }
    })
    const state = reactive({
    })
    return {
      state,
      loading,
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps),
      activeVault: computed(() => AppState.activeVault),
      keepsbyvault: computed(() => AppState.KeepsByVault),

      async destroy() {
        try {
          if (await Pop.confirm('Are you sure you want to Delete?', 'Once Deleted, can be revert back!', 'warning', 'Ok Delete!')) {
            await vaultsService.Delete(AppState.activeVault.id, AppState.account.id)
            Pop.toast('Delorted', 'success')
            router.push({ name: 'Home' })
          }
        } catch (error) {
          Pop.toast(error, 'error')
        }
      }
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
.masonry { /* Masonry container */
   -webkit-column-count: 4;
  -moz-column-count:4;
  column-count: 4;
  -webkit-column-gap: 1em;
  -moz-column-gap: 1em;
  column-gap: 1em;
   margin: 1.5em;
    padding: 0;
    -moz-column-gap: 1.5em;
    -webkit-column-gap: 1.5em;
    column-gap: 1.5em;
    font-size: .85em;
}
.btn {
  background-color: rgb(100, 98, 98);
  border: none;
  color: Black;
  padding: 12px 16px;
  font-size: 16px;
  cursor: pointer;
}

/* Darker background on mouse-over */
.btn:hover {
  background-color: white;
}
</style>
