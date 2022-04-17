<template>
  <div class="container">
    <div
      class="bg-img m-2 selectable"
      data-bs-toggle="modal"
      data-bs-target="#active-keep"
      @click="setActive"
    >
      <div class="d-flex align-items-flex-end justify-content-between">
        <h2 class="text-white textsha">
          {{ keeps.name }}
        </h2>
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
</style>