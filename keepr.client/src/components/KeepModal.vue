<template>
  <!-- Modal -->
  <div class="modal"
       :id="'keep-modal-'+ keep.id"
       tabindex="-1"
       role="dialog"
       aria-labelledby="modelKeepId"
       aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
      <div class="modal-content">
        <div class="modal-body">
          <div class="container">
            <div class="row">
              <div class="col-12">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
            </div>
            <div class="row">
              <div class="col-12 col-md d-flex align-items-center justify-content-left">
                <img class="cover-img" :src="keep.img" alt="Card image">
              </div>
              <!-- <div class="row justify-align-content">
                <div class="col-12 d-flex justify-content-start">
                  &nbsp;
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
              </div> -->
              <div class="col-12 col-md-4 m-2 position-relative">
                <div class="row justify-align-content">
                  <div class="col-12 d-flex justify-content-start">
                  &nbsp;
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
                <!--     <div class="row justify-content-between">
              <div class="col-12 justify-content-right">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                      <span aria-hidden="true">&times;</span>
                    </button>
                  </div>
                </div> -->
                <!-- <div class="row justify-align-content">
                  <div class="col-3 d-flex justify-content-start">
                    <h6><span class="iconify m-1" data-icon="mdi:eye"></span></h6>
                   &nbsp;<p> {{ keep.keeps }}</p>
                  </div>
                  <div class="col-3  d-flex justify-content-center">
                    <h6><span class="iconify m-1" data-icon="mdi:alpha-k-box-outline"></span></h6>
                    &nbsp;<p> {{ keep.views }}</p>
                  </div>
                  <div class="col-3 d-flex justify-content-end">
                    <h6><span class="iconify m-1" data-icon="mdi:share-variant-outline"></span></h6>
                    &nbsp;<p> {{ keep.shares }}</p>
                  </div>
                </div> -->
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

                <div class="col-12 col-md-4 m-2 position-relative">
                  <div class="row justify-align-content">
                    <div class="col-12 d-flex justify-content-start">
                  &nbsp;
                      <h5> Add to Vault</h5>
                      &nbsp;
                  &nbsp;
                      <h5 class="pt-3 hoverable" @click="destory()">
                        🗑
                      </h5>
                      <!-- <h6><span class="iconify m-1" data-icon="mdi:alpha-k-box-outline"></span></h6> -->
                      &nbsp;
                      <!-- <p> {{ keep.views }}</p> -->
                  &nbsp;
                      <h6 class="user" style="justify-self: end;">
                        <img class="rounded-pill" :src="keep.creator.picture" alt="" srcset="" height="40"> {{ keep.creator.name }}
                      </h6>
                      &nbsp;
                  &nbsp;
                    </div>
                  </div>
                <!-- <div class="row justify-align-content">
                  <div class="col-4 d-flex justify-content-start">
                    <h5> Add to Vault</h5>
                  </div>
                  <div class="col-2  d-flex justify-content-center">
                    <h5 class="pt-3 hoverable" @click="destory()">
                      🗑
                    </h5>
                  </div>
                  <div class="col-6 d-flex justify-content-end">
                    <div class="bottom-right">
                      <img class="rounded-pill" :src="keep.creator.picture" alt="" srcset="" height="40"> {{ keep.creator.name }}
                    </div>
                  </div>
                </div> -->
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
  width:600px;
  height:600px;// custom width
}
.emptyspace{
  min-height: 15vh;
}
.cover-img {
  object-fit:cover;
  width: 80%;
  max-height: 45vh;
 }
 /* Bottom right text */
.bottom-right {
  position: absolute;
  bottom: 1px;
  right: 1px;
}
</style>
