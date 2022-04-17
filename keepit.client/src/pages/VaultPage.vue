<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-md-12">
        <h1></h1>
      </div>
    </div>
  </div>
</template>


<script>
import { onMounted } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { useRoute } from "vue-router"
import { vaultKeepsService } from "../services/VaultKeepsService"
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
    return {}
  }
}
</script>


<style lang="scss" scoped>
</style>