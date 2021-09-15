<template>
  <div class="card m-1 px-1 py-1 text-white" style="width: 19rem;" data-toggle="modal" :data-target="'#keep-modal-'+ vault.id">
    <h4 class="py-1 px-2">
      {{ vault.name }}
    </h4>
  </div>
</template>

<script>
import { computed } from '@vue/runtime-core'
import { AppState } from '../AppState'

export default {
  setup() {
    return {
      account: computed(() => AppState.account)
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
