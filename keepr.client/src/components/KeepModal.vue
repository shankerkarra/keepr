<template>
  <!-- Modal -->
  <div class="modal"
       :id="'keep-modal-'+ keep.id"
       tabindex="-1"
       role="dialog"
       aria-labelledby="modelKeepId"
       aria-hidden="true"
  >
    <div class="modal-dialog customclass modal-dialog-centered modal-lg" role="document">
      <div class="modal-content">
        <div class="modal-body p-1 m-1">
          <div class="container">
            <div class="row">
              <div class="col-6 col-md-6">
                <img class="cover-img" :src="keep.img" alt="Card image">
              </div>
              <div class="col-6 col-md-6 order-1">
                <div class="row">
                  <div class="col-3">
                    <i class="mdi mdi-eye" aria-hidden="true">{{ keep.keeps }}</i>
                  </div>
                  <div class="col-3">
                    <i class="mdi mdi:alpha-k-box-outline">{{ keep.views }}</i>
                  </div>
                  <div class="col-3">
                    <i class="mdi mdi:share-variant-outline">{{ keep.shares }}</i>
                  </div>
                  <div class="col-3 justify-content-right">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                      <span aria-hidden="true">&times;</span>
                    </button>
                  </div>
                </div>
                <div class="row">
                  <div class="col-12">
                    <h5 class="modal-title">
                      {{ keep.name }}
                    </h5>
                  </div>
                  <div class="col-12">
                    <h5 class="modal-description">
                      {{ keep.description }}
                    </h5>
                  </div>
                </div>
              </div>
            </div>
            <i class="mdi mdi:delete"></i>
            <div class="bottom-right">
              <img class="rounded-pill" :src="keep.creator.picture" alt="" srcset="" height="40"> {{ keep.creator.name }}
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
import Pop from '../utils/Notifier'

export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const state = reactive({
      newkeep: {
        keepId: props.keep.id
      }
    })
    return {
      state,
      async destroy() {
        try {
          if (await Pop.confirm('Are you sure you want to Delete?', 'Once Deleted, can be revert back!', 'Warning', 'Ok Delete!')) {
            await keepsService.destroy(props.keep.id)
            Pop.toast('Delorted', 'success')
            $('#keepModal').modal('hide')
          }
        } catch (error) {
          Pop.toast(error, 'error')
        }
      }
      // Need to add to VAult
    }
  }
}
</script>

<style lang="scss" scoped>
.customClass{
  width:800px;
  height:600px;// custom width
}
.cover-img {
  height: 400px;
  width: 250px;
  object-fit: cover;
  width:100%;
}
</style>
