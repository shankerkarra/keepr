<template>
  <div class="card img-fluid m-1 px-1 py-1 bg-black text-white" style="width: 19rem;" data-toggle="modal" :data-target="'#keep-modal-'+vault.id">
    <h4 class="py-2 px-3">
      {{ vault.name }}
    </h4>
  </div>
</template>

<script>
import { computed } from '@vue/runtime-core'
import { AppState } from '../AppState'

export default {
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    return {
      account: computed(() => AppState.account)
    }
  }
}
</script>

<style lang="scss" scoped>
.cover-img {
  height: 200px;
  width: 250px;
  object-fit:cover;
  width:100%;
}
a {
  color: inherit;
  text-decoration: inherit;
}
.cardopacity {
  background-color: rgba(245, 245, 245, 1);
  opacity: .4;
}
.creator{
  height: 3em;
}

.container {
  position: relative;
  text-align: center;
  color: white;
}

/* Bottom left text */
.bottom-left {
  position: absolute;
  bottom: 2px;
  left: 2px;
}

/* Top left text */
.top-left {
  position: absolute;
  top: 8px;
  left: 16px;
}

/* Top right text */
.top-right {
  position: absolute;
  top: 8px;
  right: 16px;
}

/* Bottom right text */
.bottom-right {
  position: absolute;
  bottom: 1px;
  right: 1px;
}

</style>
