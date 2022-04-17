<template>
  <div class="row">
    <div class="col-md-6">
      <img class="img-fluid rounded modalpic" :src="activeKeep.img" alt="" />
    </div>
    <div class="col-md-6">
      <div class="d-flex justify-content-between p-3">
        <h3>
          <i class="mdi mdi-eye">{{ activeKeep.views }}</i>
        </h3>
        <h3 class="K">
          K <span>{{ activeKeep.kept }}</span>
        </h3>
        <h3>
          <i class="mdi mdi-eye">{{ random }}</i>
        </h3>
      </div>
      <h1 class="text-dark text-center">
        {{ activeKeep.name }}
      </h1>
      <p class="text-dark text-center mt-3">
        {{ activeKeep.description }}
      </p>
      <div class="d-flex justify-content-between">
        <button class="btn btn-outline-success">ADD TO VAULT</button>
        <h2 class="selectable" v-if="activeKeep.creatorId == account.id">
          <i class="mdi mdi-delete" @click="removeKeep(activeKeep.id)"></i>
        </h2>
        <div
          class="d-flex align-items-center selectable"
          title="Go to Profile Page"
          @click="goTo(activeKeep.creatorId)"
        >
          <img
            class="img-fluid rounded pp"
            :src="activeKeep.creator?.picture"
            alt=""
          />
          <h5 class="text-dark ms-2">
            {{ activeKeep.creator?.name }}
          </h5>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { keepsService } from "../services/KeepsService"
import { Modal } from "bootstrap"
import { useRouter } from "vue-router"
export default {
  setup() {
    const router = useRouter()
    return {
      async removeKeep(id) {
        try {
          await keepsService.removeKeep(id)
          Modal.getOrCreateInstance(document.getElementById("active-keep")).hide()
        } catch (error) {
          logger.log(error)
          Pop.toast(error.message, "error message")
        }
      },
      async goTo(id) {
        Modal.getOrCreateInstance(document.getElementById('active-keep')).hide()
        router.push({ name: "Profile", params: { id } })
      },
      activeKeep: computed(() => AppState.activeKeep),
      random: computed(() => Math.floor(Math.random() * 445)),
      account: computed(() => AppState.account)
    }
  }
}
</script>


<style lang="scss" scoped>
.modalpic {
  height: 80vh;
  width: 100%;
  object-fit: cover;
  position: center;
}
i {
  color: green;
}
.pp {
  height: 7vh;
  width: 7vh;
}
.K {
  color: green;
}
</style>