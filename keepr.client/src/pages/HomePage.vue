<template>
  <div class="container-fluid d-flex flex-wrap">
    <!-- <div class="row"> -->
    <KeepThread :keeps="keeps" :account="account" />
    <!-- </div> -->
  </div>
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core'
import { AppState } from '../AppState'
import Pop from '../utils/Notifier'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'

export default {
  name: 'Home',
  setup() {
    onMounted(async() => {
      try {
        await keepsService.getAll()
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account)

    }
  }
}
</script>

<style scoped lang="scss">
.home{
  text-align: center;
  user-select: none;
  > img{
    height: 200px;
    width: 200px;
  }
}
</style>
