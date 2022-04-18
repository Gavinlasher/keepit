<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-md-12" v-for="vk in vaultKeeps" :key="vk.id">
        <Keeps :keeps="vk" />
      </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { useRoute } from "vue-router"
import { vaultKeepsService } from "../services/VaultKeepsService"
import { AppState } from "../AppState"
export default {
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        await vaultKeepsService.getKeepsByVault(route.params.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, "error message")
      }
    })
    return {
      vaultKeeps: computed(() => AppState.profileVaultKeeps)
    }
  }
}
</script>


<style lang="scss" scoped>
</style>