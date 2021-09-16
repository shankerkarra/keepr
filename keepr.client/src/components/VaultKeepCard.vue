<template>
  <div class="card img-fluid m-1 px-1 py-1 Text-white" style="width: 19rem;">
    <img class="cover-img" :src="keep.img" alt="Card image">
    <div class="card-img-overlay">
      <div class="bottom-left">
        <h4 class="py-2 px-2">
          {{ keep.name }}
        </h4>
      </div>
    </div>
    <div class="bottom-right py-3 justify-content-right">
      <img class="rounded-pill" :src="keep.creator.picture" alt="" srcset="" height="40">
    </div>
  </div>
</template>

<script>
import { computed } from '@vue/runtime-core'
import { AppState } from '../AppState'

export default {
  props: {
    keep: {
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
  height: 400px;
  width: 400px;
  object-fit:cover;
  width:100%;
}
a {
  color: inherit;
  text-decoration: inherit;
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
  bottom: 0px;
  right: 8px;
}

</style>
