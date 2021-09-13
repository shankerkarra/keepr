<template>
  <div class="keep-modal">
    <!-- Modal -->
    <div class="modal"
         id="keep-modal"
         tabindex="-1"
         role="dialog"
         aria-labelledby="modelkeep"
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
                <label for="">Title</label>
                <input type="text"
                       class="form-control"
                       aria-describedby="title"
                       placeholder="Title..."
                       v-model="state.newkeep.title"
                >
              </div>
              <div class="form-group">
                <label for="body">Image Url</label>
                <input type="text"
                       class="form-control"
                       aria-describedby="ImgUrl"
                       placeholder="Url..."
                       v-model="state.newkeep.img"
                >
              </div>
              <div class="form-group">
                <label for="body">Description</label>
                <input type="textarea"
                       class="form-control"
                       aria-describedby="Keep Description"
                       placeholder="Keep Description..."
                       v-model="state.newkeep.img"
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
import { reactive } from '@vue/reactivity'
import { keepsService } from '../services/KeepsService'
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
      newkeep: {
        creatorId: props.profile.Id
      }
    })
    return {
      state,
      async createkeep() {
        try {
          await keepsService.create(state.newkeep)
          state.newkeep = { creatorId: props.profile.Id }
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
