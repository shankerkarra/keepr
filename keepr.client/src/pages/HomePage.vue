<template>
  <div class="home flex-grow-1 d-flex flex-column align-items-center justify-content-center">
    <div class="row">
      <KeepThread :keeps="keeps" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core'
import { AppState } from '../AppState'
import Pop from '../utils/Notifier'
import { keepsService } from '../services/KeepsService'

export default {
  name: 'Home',
  setup() {
    onMounted(async() => {
      try {
        await keepsService.getAll()
      } catch (error) {
        Pop.Toast(error, 'error')
      }
    })
    return {
      keeps: computed(() => AppState.keeps)
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
