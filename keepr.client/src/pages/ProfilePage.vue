<template>
  <div class="Profile container-fluid">
    <div class="row masonry">
      <div class="row">
        <img class="rounded" :src="account.picture" alt="user Profile" />
      </div>
      <div class="col m-2">
        <h1>{{ account.name }}</h1>
        <h5>Vaults : {{ profileVaults }}</h5>
        <h5>Keeps  : {{ profileKeeps }}</h5>
        <br>
      </div>
    </div>
    <h2> Vaults </h2>
    <div class="col" title="Create New Vault">
      <button class="btn btn-warning" :data-target="'#createvault-modal'" data-toggle="modal">
        <i class="fa fa-plus"></i>
      </button>
      <CreateVaultModal :profile="profile" />
    </div>
    <!-- <h2> Vaults </h2>
    <div class="col-4 offset-3" title="Edit Comment">
      <button class="btn btn-warning" :data-target="'#createvault-modal'" data-toggle="modal">
        <i class="fa fa-plus"> </i>
      </button>
      <CreateVaultModal :profile="profile" />
    </div> -->
    <!-- <div class="vault">
      <h2>
        Vaults<button class="btn" data-backdrop="static" data-keyboard="false" data-toggle="modal" :data-target="'#createvault-modal'">
          <i class="fa fa-plus"> </i>
          <CreateVaultModal :profile="profile" />
        </button>
      </h2>
    </div> -->
    <!-- <div class="container-fluid"> -->
    <div class="row">
      <div v-if="loading" class="col-3 text-center">
        <i class="fa fa-spinner fa-spin" aria-hidden="true"></i>
      </div>
      <div v-else class="col-3">
        <!-- Add Mdi + button -->
        <ProfileVault v-for="v in vaults" :key="v.id" :vault="v" />
      </div>
    </div>
    <h2> Keeps </h2>
    <div class="col" title="Create New Keep">
      <button class="btn btn-warning" :data-target="'#createkeep-modal'" data-toggle="modal">
        <i class="fa fa-plus"></i>
      </button>
      <CreateKeepModal :profile="profile" />
    </div>
    <!-- <h2>
      Keeps<button class="btn">
        <i class="fa fa-plus"></i>
      </button>
    </h2> -->
    <div class="row m-2">
      <div v-if="loading" class="col text-center">
        <i class="fa fa-spinner fa-spin" aria-hidden="true"></i>
      </div>
      <div v-else class="col">
        <h2>Keeps:</h2>
        Add Mdi + button
        <ProfileKeep v-for="k in keeps" :key="k.id" :keep="k" />
      </div>
    </div>
    <!-- <NewModal v-if="vaults.id" :vaults="vault" /> -->
    <!-- <CreateKeepModal v-if="keep.id" :keep="keep" /> -->
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
</style>
