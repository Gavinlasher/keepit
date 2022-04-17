<template>
  <div class="bgbind">
    <img class="img-fluid mt-2" :src="keeps.img" alt="" />
    <div>
      {{ keeps.name }}
    </div>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
export default {
  props: {
    keeps: {
      type: Object,
      required: true
    }
  },
  setup() {
    return {
      // keeps: computed(() => AppState.keeps),
      bgImage: computed(() => `url(${AppState.keeps.img})`)
    }
  }
}
</script>


<style lang="scss" scoped>
.bgbind {
  background-image: v-bind(bgImage);
  background-position: center;
  background-size: cover;
}
</style>