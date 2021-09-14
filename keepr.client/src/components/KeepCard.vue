<template>
  <div class="card img-fluid m-1 px-1 py-1 bg-black text-white rounded" style="width: 19rem;" data-toggle="modal" :data-target="'#keep-modal-'+keep.id">
    <img class="cover-img" :src="keep.img" alt="Card image">
    <div class="card-img-overlay">
      <div class="bottom-left">
        <h4 class="py-2 px-3">
          {{ keep.name }}
        </h4>
      </div>
    </div>
    <div class="top-right">
      <router-link router-link :to="{ name: 'Account', params: {id: keep.creator.id } }" @click.stop="" class="creator p-3 align-self-end">
        <img class="rounded-pill" :src="keep.creator.picture" alt="" srcset="" height="40">
      </router-link>
    </div>
  </div>
  <KeepModal v-if="keep.id" :keep="keep" />
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
  height: 200px;
  width: 250px;
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
  bottom: 8px;
  left: 16px;
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
  bottom: 8px;
  right: 16px;
}

</style>
