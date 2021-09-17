<template>
  <div class="createvaultmodal">
    <!-- Modal -->
    <div class="modal"
         :id="'createvault-modal'"
         tabindex="-1"
         role="dialog"
         aria-labelledby="modelcreatevault"
         aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h4 class="modal-title">
              New Vault
            </h4>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <form @submit.prevent="createVault">
              <div class="form-group">
                <label for="">Name</label>
                <input type="text"
                       class="form-control"
                       aria-describedby="name"
                       placeholder="Name should be minimum 3 char...."
                       v-model="state.newVault.name"
                >
              </div>
              <div class="form-group">
                <label for="body">Description</label>
                <input type="text"
                       class="form-control"
                       aria-describedby="Description"
                       placeholder="Description..."
                       v-model="state.newVault.description"
                >
              </div>
              <div class="form-group">
                <label for="body">Image Url</label>
                <input type="text"
                       class="form-control"
                       aria-describedby="ImgUrl"
                       placeholder="Url..."
                       v-model="state.newVault.img"
                >
              </div>
              <div class="form-group">
                <label for="isPrivate" class="form-label">Private ?</label>
                <input class="form-checkbox" type="checkbox" v-model="state.newVault.isPrivate">
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">
                  Close
                </button>
                <button type="submit" class="btn btn-primary">
                  Create
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from '@vue/reactivity'
import { vaultsService } from '../services/VaultsService'
import { AppState } from '../AppState'
import $ from 'jquery'
import Pop from '../utils/Notifier'

export default {
  props: {
    profile: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const state = reactive({
      newVault: {
        creatorId: AppState.account.id,
        isPrivate: false
      }
    })
    return {
      state,
      account: computed(() => AppState.account),
      async createVault() {
        try {
          state.newVault.creatorId = AppState.account.id
          await vaultsService.create(state.newVault, AppState.account.id)
          state.newVault = { creatorId: props.profile.Id, isPrivate: false }
          $('#createvault-modal').modal('hide')
        } catch (error) {
          Pop.toast(error, 'error')
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
