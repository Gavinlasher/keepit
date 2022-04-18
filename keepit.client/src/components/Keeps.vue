<template>
  <div class="container">
    <div
      class="m-2 selectable"
      data-bs-toggle="modal"
      data-bs-target="#active-keep"
      @click="setActive"
    >
      <img class="img-fluid rounded" :src="keeps.img" alt="" />
      <div class="d-flex align-items-flex-end moveup">
        <h3 class="text-white me-4">
          {{ keeps.name }}
        </h3>
        <img
          class="img-fluid pp p-1 mt-1"
          :src="keeps.creator.picture"
          alt=""
        />
      </div>
    </div>
  </div>
  <Modal id="active-keep">
    <template #body><ActiveKeep /></template>
  </Modal>
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { keepsService } from "../services/KeepsService"
import { vaultsService } from "../services/VaultsService"
export default {
  props: {
    keeps: {
      type: Object,
      required: true
    }
  },
  setup(props) {

    return {
      async setActive() {
        try {
          AppState.activeKeep = props.keeps
          await keepsService.getActiveKeep(props.keeps.id)
          await vaultsService.getProfileVaults(props.keeps.creatorId)
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message)
        }
      },
      bgImage: computed(() => `url(${props.keeps.img})`)
    }
  }
}
</script>


<style lang="scss" scoped>
.bg-img {
  background-image: v-bind(bgImage);
  background-position: center;
  background-size: cover;
}
.textsha {
  text-shadow: 2px 4px 3px rbga (0, 0, 0, 0.3);
}
.pp {
  height: 5vh;
  width: 5vh;
  border-radius: 50%;
}
.moveup {
  transform: translate(5px, -60px);
}
h3 {
  text-shadow: 2px 2px black;
}
</style>