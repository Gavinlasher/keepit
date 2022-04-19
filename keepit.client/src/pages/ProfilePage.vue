<template>
  <div class="container fluid">
    <div class="row">
      <div class="col-md-12">
        <div class="d-flex">
          <img
            class="img-fluid pp p-3 mt-4"
            :src="activeProfile.picture"
            alt=""
          />
          <div>
            <h1 class="mt-5">
              {{ activeProfile.name }}
            </h1>
            <h3>
              Vaults: <span>{{ activeVaults.length }}</span>
            </h3>
            <h3>
              Keeps: <span>{{ activeKeeps.length }}</span>
            </h3>
          </div>
        </div>
      </div>
    </div>
    <div class="row">
      <h2 class="mt-5">
        Vaults
        <span v-if="account.id == activeProfile.id"
          ><i
            class="mdi mdi-plus selectable"
            title="Add a Vault"
            data-bs-toggle="modal"
            data-bs-target="#createVault"
          ></i
        ></span>
      </h2>
      <div class="col-md-3 p-4" v-for="v in activeVaults" :key="v.id">
        <div
          class="bg-img rounded shadow selectable"
          @click="goToVaultPage(v.id)"
        >
          <h3 class="text-light">
            {{ v.name }}
          </h3>
        </div>
      </div>
    </div>
    <div class="row">
      <h2 class="mt-5">
        Keeps
        <span v-if="account.id == activeProfile.id"
          ><i
            class="mdi mdi-plus selectable"
            title="Add a Keeps"
            data-bs-toggle="modal"
            data-bs-target="#createKeep"
          ></i
        ></span>
      </h2>
    </div>
    <div class="masonary-with-colums">
      <div v-for="k in activeKeeps" :key="k.id">
        <Keeps :keeps="k" />
        <!-- <img class="img-fluid p-2 rounded" :src="k.img" alt="" />
        <h3 class="moveup text-light">{{ k.name }}</h3> -->
      </div>
    </div>
  </div>
  <Modal id="createKeep">
    <template #body><KeepForm /></template>
  </Modal>
  <Modal id="createVault">
    <template #body><VaultForm /></template>
  </Modal>
</template>


<script>
import { computed, onMounted } from "@vue/runtime-core"
import { useRoute, useRouter } from "vue-router"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { profilesService } from "../services/ProfilesService"
import { AppState } from "../AppState"
export default {
  setup() {
    const route = useRoute()
    const router = useRouter()
    onMounted(async () => {

      try {
        await profilesService.getProfile(route.params.id)
        await profilesService.getVaults(route.params.id)
        await profilesService.getKeeps(route.params.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, "error message")
      }

    })
    return {
      goToVaultPage(id) {
        router.push({ name: "Vault", params: { id } })
      },
      activeProfile: computed(() => AppState.activeProfile),
      activeVaults: computed(() => AppState.profileVaults),
      activeKeeps: computed(() => AppState.profileKeeps),
      account: computed(() => AppState.account)
    }
  }
}
</script>


<style lang="scss" scoped>
.pp {
  height: 30vh;
  width: 30vh;
  object-fit: cover;
  border-radius: 25px;
}
i {
  color: green;
}
.bg-img {
  background-image: url("https://images.unsplash.com/photo-1582139329536-e7284fece509?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1180&q=80");
  background-position: center;
  background-size: cover;
  height: 40vh;
  width: 40vh;
}
.masonary-with-colums {
  columns: 6 200px;
  column-gap: 2rem;
  div {
    display: inline-block;
    width: 100%;
  }
}
.moveup {
  transform: translate(14px, -45px);
}
h3 {
  text-shadow: 2px 2px black;
}
</style>