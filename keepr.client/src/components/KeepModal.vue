<template>
  <!-- Modal -->
  <div class="modal"
       :id="'keep-modal-'+ keep.id"
       tabindex="-1"
       role="dialog"
       aria-labelledby="modelKeepId"
       aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered modal-lg customClass" role="document">
      <div class="modal-content">
        <div class="modal-body container-fluid">
          <div class="row">
            <div class="col-12">
              <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
          </div>
          <div class="row">
            <div class="col-md-6 justify-content-center primary p-0">
              <img class="cover-img m-1 p-0" :src="keep.img" alt="Card image">
            </div>
            <div class="col-md-6 secondary p-0" v-if="state.activeKeep.id != null">
              <div class="row justify-align-content">
                <div class="col-12 p-1 m-1 d-flex justify-content-center">
                  <h6><span class="iconify m-1" data-icon="mdi:eye"></span></h6>
                  &nbsp;<p> {{ keep.views }}</p>
                  &nbsp;
                  <h6><span class="iconify m-1" data-icon="mdi:alpha-k-box-outline"></span></h6>
                  &nbsp;<p> {{ keep.keeps }}</p>
                  &nbsp;
                  <h6><span class="iconify m-1" data-icon="mdi:share-variant-outline"></span></h6>
                  &nbsp;<p> {{ keep.shares }}</p>
                  &nbsp;
                </div>
              </div>
              <div class="row justify-content-between">
                <div class="col-12 m-1 justify-align-text">
                  <h6 class="modal-title">
                    {{ keep.name }}
                  </h6>
                </div>
                <div class="col-12 m-1 justify-content-left">
                  <h6 class="modal-description">
                    {{ keep.description }}
                  </h6>
                </div>
              </div>
              <div class="emptyspace"></div>
              <div class="row m-1 justify-align-right">
                <span v-if="state.user">
                  <!-- <div class="col-3 justify-content-start hoverable"> -->
                  <!-- v-if="state.user.isAuthenticated && state.account.id == keep.creatorId"> -->
                  <div class="userdisplay">
                    <div class="col-2 col-md-2">
                      <h5 class="hoverable bottom-row m-0">
                        <form>
                          <select title="Add to Vault" @change.prevent="addToVault($event.target.value)" class="btn btn-primary" style="width:140px">
                            <option value="" selected disabled hidden>
                              Add to Vault
                            </option>
                            <option v-for="vault in state.myVaults" :value="vault.id" :key="vault.id">
                              {{ vault.name }}
                            </option>
                          </select>
                        </form>
                      </h5>
                    </div>
                    <div class="col-2 bottom-row-trash justify-content-right" v-if="state.user.isAuthenticated && state.account.id == keep.creatorId">
                      <h5 class="hoverable" @click="destroy($event.target.value)">
                        🗑
                      </h5>
                    </div>
                  </div></span>
                <!-- </div> -->
                <div class="col-6 offset-8 bottom-right">
                  <img class="rounded-pill" :src="keep.creator.picture" alt="" srcset="" height="40"> {{ keep.creator.name }}
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { keepsService } from '../services/KeepsService'
import $ from 'jquery'
import { computed, onMounted, ref } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { profilesService } from '../services/ProfilesService'
import { useRoute } from 'vue-router'
import Pop from '../utils/Notifier'

export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const route = useRoute()
    const loading = ref(true)
    // onMounted(async() => {
    //   try {
    //        loading.value = false
    //   } catch (error) {
    //     Pop.toast(error, 'error')
    //   }
    // })
    const state = reactive({
      newkeep: {
        keepId: props.keep.id
      },
      user: computed(() => AppState.user),
      activeKeep: computed(() => AppState.activeKeep),
      // keep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account),
      myVaults: computed(() => AppState.myVaults),
      newVaultKeep: {}
    })

    return {
      state,
      async destroy() {
        try {
          if (await Pop.confirm('Are you sure you want to Delete?', 'Once Deleted, can be revert back!', 'warning', 'Ok Delete!')) {
            await keepsService.delete(props.keep.id)
            Pop.toast('Delorted', 'success')
            $('#keep-modal-' + props.keep.id).modal('toggle')
          }
        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      async fetchkeep() {
        await keepsService.GetById(props.keep.id)
      }, // Need to add to VAult
      async addToVault(vault) {
        try {
          state.newVaultkeep = {}
          state.newVaultKeep.keepId = props.keep.id
          state.newVaultKeep.VaultId = vault
          // state.newVaultKeep.keepId = AppState.account.id
          if (state.newVaultKeep.VaultId) {
            await keepsService.addKeeptoVault(state.newVaultKeep)
            state.activeKeep.keeps++
            // TODO props.keep.keeps++
            Pop.toast('Keep added  to your Vault', 'success')
          }
          $('#keep-modal-' + props.keep.id).modal('toggle')
          // $('#keepModal').modal('toggle')
        } catch (error) {
          Pop.toast(error, 'error')
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.emptyspace{
  min-height: 18vh;
}
.cover-img {
  object-fit:inherit;
  width: 300px;
  height: 300px;
  // max-height: 45vh;
 }
 .hoverable{
  cursor: pointer;
}
 /* Bottom right text */
.bottom-right {
  position: absolute;
  bottom: 25px;
  right: 1px;
}
.bottom-row {
   bottom: 25px;
  left:-5px;
}
.bottom-row-trash {
   bottom: 25px;
   left: 150px;
}
 .modal-dialog{
   min-width: 450px;
   max-width: 750px;
 }

// @media all and (max-width: 420px) {
//   primary, secondary {
//     float: none;
//     width: auto;
//   }
// }

 </style>
