<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-md-12 text-dark">
        <div class="d-flex justify-content-between p-3">
          <h1>{{ currVault.name }}</h1>
          <button
            v-if="account.id == currVault.creatorId"
            class="btn btn-outline-danger"
            @click="deleteVault"
          >
            Delete Vault
          </button>
        </div>
      </div>
      <h3 class="ms-4">
        Kept: <span>{{ vaultKeeps.length }}</span>
      </h3>
    </div>
    <div class="masonary-with-colums">
      <div v-for="vk in vaultKeeps" :key="vk.id">
        <Keeps :keeps="vk" class="selectable" />
      </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { useRoute, useRouter } from "vue-router"
import { vaultKeepsService } from "../services/VaultKeepsService"
import { AppState } from "../AppState"
import { vaultsService } from "../services/VaultsService"
import Keeps from "../components/Keeps.vue"
export default {

  setup() {
    const route = useRoute()
    const router = useRouter()
    onMounted(async () => {
      try {
        await vaultsService.getVault(route.params.id)
        await vaultKeepsService.getKeepsByVault(route.params.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, "error message")
      }
    })
    return {
      vaultKeeps: computed(() => AppState.profileVaultKeeps),
      currVault: computed(() => AppState.vaultpage),
      account: computed(() => AppState.account),
      async deleteVault(id) {
        try {
          if (await Pop.confirm()) {

            await vaultsService.deleteVault(route.params.id)
            router.push({ name: "Profile", params: AppState.account.id })
          }
        } catch (error) {
          logger.log(error)
          Pop.toast(error.message, "error message")
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.masonary-with-colums {
  columns: 4 200px;
  column-gap: 2rem;
  div {
    display: inline-block;
    width: 100%;
  }
}
</style>