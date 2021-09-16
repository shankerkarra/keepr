<template>
  <div class="Profile container-fluid">
    <!-- <div class="row masonry"> -->
    <div class="row m-2">
      <div class="col-2 m-2 text-right">
        <img class="rounded" :src="account.picture" alt="user Profile" />
      </div>
      <div class="col-8 justify-content-left">
        <h1>{{ account.name }}</h1>
        <h5>Vaults : {{ profileVaults }}</h5>
        <h5>Keeps  : {{ profileKeeps }}</h5>
        <br>
      </div>
    </div>
    <div class="row">
      <div class="col-2 text-right">
        <h2> Vaults </h2>
      </div>
      <div class="col= text-left" title="Create New Vault">
        <button class="btn btn-warning" :data-target="'#createvault-modal'" data-toggle="modal">
          <i class="fa fa-plus"></i>
        </button>
        <CreateVaultModal :profile="profile" />
      </div>
    </div>
    <div class="row p-2">
      <div class="container card-group">
        <div v-if="loading" class="col-3 text-center">
          <i class="fa fa-spinner fa-spin" aria-hidden="true"></i>
        </div>
        <div v-else class="col">
          <!-- Add Mdi + button -->
          <ProfileVault v-for="v in vaults" :key="v.id" :vault="v" />
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-2 text-right">
        <h2> Keeps </h2>
      </div>
      <div class="col" title="Create New Keep">
        <button class="btn btn-warning" :data-target="'#createkeep-modal'" data-toggle="modal">
          <i class="fa fa-plus"></i>
        </button>
        <CreateKeepModal :profile="profile" />
      </div>
    </div>
    <div class="row m-2">
      <div class="container-fluid">
        <div class="row masonry">
          <div v-if="loading" class="col text-center">
            <i class="fa fa-spinner fa-spin" aria-hidden="true"></i>
          </div>
          <div v-else class="col">
            <ProfileKeep v-for="k in keeps" :key="k.id" :keep="k" />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, ref } from 'vue'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import Pop from '../utils/Notifier'
import { profilesService } from '../services/ProfilesService'

export default {
  name: 'Profile',
  setup() {
    const route = useRoute()
    const loading = ref(true)
    onMounted(async() => {
      try {
        // await keepsService.getAll()
        await profilesService.GetKeepsByProfileId(route.params.id)
        await profilesService.GetVaultsByProfileId(route.params.id)
        loading.value = false
      } catch (error) {
        Pop.Toast(error, 'error')
      }
    })
    return {
      loading,
      account: computed(() => AppState.account),
      profile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      profileVaults: computed(() => AppState.vaults.length),
      profileKeeps: computed(() => AppState.keeps.length)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
.masonry { /* Masonry container */
   -webkit-column-count: 4;
  -moz-column-count:4;
  column-count: 4;
  -webkit-column-gap: 1em;
  -moz-column-gap: 1em;
  column-gap: 1em;
   margin: 1.5em;
    padding: 0;
    -moz-column-gap: 1.5em;
    -webkit-column-gap: 1.5em;
    column-gap: 1.5em;
    font-size: .85em;
}
.btn {
  background-color: rgb(100, 98, 98);
  border: none;
  color: Black;
  padding: 12px 16px;
  font-size: 16px;
  cursor: pointer;
}

/* Darker background on mouse-over */
.btn:hover {
  background-color: white;
}

.card {
    position: relative;
    display: flex;
    flex-direction: column;
    min-width: 0;
    word-wrap: break-word;
    background-color: #fff;
    background-clip: border-box;
    border: 1px solid rgba(0,0,0,.125);
    border-radius: .25rem;
}

.card-body {
    flex: 1 1 auto;
    padding: 1.25rem;
}

.card-title {
    margin-bottom: .75rem;
}

.card-img-top {
    width: 100%;
    border-top-left-radius: calc(.25rem - 1px);
    border-top-right-radius: calc(.25rem - 1px);
}

.text-left {
    text-align: left!important;
}
.pb-2, .py-2 {
    padding-bottom: .5rem!important;
}
.pt-2, .py-2 {
    padding-top: .5rem!important;
}
</style>
