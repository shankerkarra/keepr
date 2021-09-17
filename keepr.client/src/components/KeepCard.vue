<template>
  <div class="card m-1 px-1 py-1 text-white" style="width: 19rem;" data-toggle="modal" :data-target="'#keep-modal-'+keep.id" @click="fetchkeep()">
    <img class="cover-img" :src="keep.img" alt="Card image">
    <div class="card-img-overlay">
      <div class="bottom-left">
        <h4 class="py-1 px-2">
          {{ keep.name }}
        </h4>
      </div>
    </div>
    <div class="bottom-right py-2 justify-content-right">
      <router-link router-link :to="{ name: 'Profile', params: {id: keep.creator.id } }" @click.stop="" class="creator p-3 align-self-end">
        <img class="rounded-pill" :src="keep.creator.picture" alt="" srcset="" height="40">
      </router-link>
    </div>
  </div>
  <KeepModal v-if="keep.id" :keep="keep" />
</template>

<script>
import { computed } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'

export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    return {
      account: computed(() => AppState.account),
      async fetchkeep() {
        await keepsService.GetById(props.keep)
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.cover-img {
  // min-height: 200px;
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
  right: 0px;
}

</style>
