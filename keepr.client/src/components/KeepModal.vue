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
        <div class="modal-body container">
          <div class="row">
            <div class="col-12">
              <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
          </div>
          <div class="row">
            <div class="col-6 col-md-4 col-md-push-8 justify-content-left p-0 !important;">
              <img class="cover-img m-1" :src="keep.img" alt="Card image">
            </div>
            <div class="col-6col-md-8 col-md-pull-4 p-0">
              <div class="row justify-align-content">
                <div class="col-12 p-1 m-1 d-flex justify-content-center">
                  <h6><span class="iconify m-1" data-icon="mdi:eye"></span></h6>
                  &nbsp;<p> {{ keep.keeps }}</p>
                  &nbsp;
                  <h6><span class="iconify m-1" data-icon="mdi:alpha-k-box-outline"></span></h6>
                  &nbsp;<p> {{ keep.views }}</p>
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
                    {{ state.myVaults }}
                  </h6>
                </div>
                <div class="col-12 m-1 justify-content-left">
                  <h6 class="modal-description">
                    {{ keep.description }}
                  </h6>
                </div>
              </div>
              <div class="emptyspace"></div>
              <div class="row m-1 justify-align-left">
                <span v-if="state.user">
                  <div class="col-4 justify-content-start hoverable" v-if="state.user.isAuthenticated && state.account.id == keep.creatorId">
                    <div class="userdisplay">
                      <h5 class="pt-2 hoverable">
                        <!-- Add to Vault -->
                        <!-- DO we need to populate for user selection of Valut? -->
                        <!--  @click="AddtoVault(keep.Id)" -->
                        <form>
                          <select title="Add to Vault" @change.prevent="addToVault($event.target.value)" class="btn btn-primary">
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
                    <div class="col-2 justify-content-center">
                      <h5 class="pt-2 hoverable" @click="destroy($event.target.value)">
                        🗑
                      </h5>
                    </div>
                  </div></span>
              </div>
              <div class="col-6 text-right">
                <div class="bottom-right">
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
    onMounted(async() => {
      try {
        await profilesService.GetVaultsByProfileId(AppState.account?.Id)
        loading.value = false
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    const state = reactive({
      newkeep: {
        keepId: props.keep.id
      },
      user: computed(() => AppState.user),
      activeKeep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account),
      myVaults: computed(() => AppState.vaults),
      newVaultKeep: {}
    })

    return {
      state,
      async destroy() {
        try {
          if (await Pop.confirm('Are you sure you want to Delete?', 'Once Deleted, can be revert back!', 'warning', 'Ok Delete!')) {
            await keepsService.delete(props.keep.id)
            Pop.toast('Delorted', 'success')
            $('#keepModal').modal('hide')
          }
        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      // Need to add to VAult
      async AddtoVault(vault) {
        try {
          state.newVaultkeep = {}
          state.newVaultKeep.keepId = state.activeKeep.id
          state.newVaultKeep.VaultId = vault
          if (state.newVaultKeep.VaultId) {
            await keepsService.addKeeptoVault(state.newVaultKeep)
          }
          $('#keepModal').modal('hide')
        } catch (error) {
          Pop.toast(error, 'error')
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>
// .customClass{
//    width: 400px;
//    height:400px;// custom width
// }
.emptyspace{
  min-height: 15vh;
}
.cover-img {
  object-fit:cover;
  width: 100%;
  height: 100%;
  // max-height: 45vh;
 }
 .hoverable{
  cursor: pointer;
}
 /* Bottom right text */
.bottom-right {
  // position: absolute;
  bottom: 1px;
  // right: 3px;
}
 .modal-dialog{
   min-width: 375px;
   max-width: 650px;
 }

</style>
