<template>
  <div class="createvault-modal">
    <!-- Modal -->
    <div class="modal"
         id="createvault-modal"
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
                <label for="">Title</label>
                <input type="text"
                       class="form-control"
                       aria-describedby="title"
                       placeholder="Title..."
                       v-model="state.newVault.title"
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
import { reactive } from '@vue/reactivity'
import { vaultsService } from '../services/VaultsService'
import { AppState } from '../utils/AppState'
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
        creatorId: AppState.activeProfile,
        isPrivate: false
      }
    })
    return {
      state,
      async createVault() {
        try {
          await vaultsService.create(state.newVault, AppState.activeProfile)
          state.newVault = { creatorId: props.profile.Id, isPrivate: false }
          $('#review-modal').modal('hide')
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
