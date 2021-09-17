<template>
  <div class="createkeepmodal">
    <!-- Modal -->
    <div class="modal"
         :id="'createkeep-modal'"
         tabindex="-1"
         role="dialog"
         aria-labelledby="modelcreatekeep"
         aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h4 class="modal-title">
              New Keep
            </h4>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <form @submit.prevent="createkeep">
              <div class="form-group">
                <label for="">Name</label>
                <input type="text"
                       class="form-control"
                       aria-describedby="name"
                       placeholder="Name should be minimum 3 char...."
                       v-model="state.newKeep.name"
                >
              </div>
              <div class="form-group">
                <label for="body">Description</label>
                <input type="textarea"
                       class="form-control"
                       aria-describedby="Description"
                       placeholder="Description..."
                       v-model="state.newKeep.description"
                >
              </div>
              <div class="form-group">
                <label for="body">ImgUrl</label>
                <input type="text"
                       class="form-control"
                       aria-describedby="Img Url"
                       placeholder="Img Url..."
                       v-model="state.newKeep.img"
                >
              </div>
              <div class="form-group">
                <label for="body">Tags</label>
                <input type="text"
                       class="form-control"
                       aria-describedby="Tags"
                       placeholder="Tags..."
                >
                <label for="body1">* Seperate tags with a comma</label>
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
import { keepsService } from '../services/KeepsService'
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
  setup() {
    const state = reactive({
      newKeep: { creatorId: AppState.account.id }
    })
    return {
      state,
      account: computed(() => AppState.account),
      async createkeep() {
        try {
          state.newKeep.creatorId = AppState.account.id
          await keepsService.create(state.newKeep, AppState.account.id)
          $('#createkeep-modal').modal('hide')
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
