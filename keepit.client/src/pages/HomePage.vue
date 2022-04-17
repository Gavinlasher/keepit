<template>
  <div class="masonry-with-columns">
    <div v-for="k in keeps" :key="k.id">
      <Keeps :keeps="k" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { keepsService } from "../services/KeepsService"
import { AppState } from "../AppState"
export default {
  name: 'Home',
  setup() {
    onMounted(async () => {
      try {
        await keepsService.getAllKeeps()
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, "error")
      }
    })
    return {
      keeps: computed(() => AppState.keeps),
      bgImage: computed(() => `url(${AppState.keeps.img})`)
    }
  }
}
</script>

<style scoped lang="scss">
.masonry-with-columns {
  columns: 6 200px;
  column-gap: 10px;

  div {
    display: inline-block;
    width: 100%;
  }
}
</style>
