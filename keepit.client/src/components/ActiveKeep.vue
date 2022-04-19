<template>
  <div class="row">
    <div class="col-md-6">
      <img class="img-fluid rounded modalpic" :src="activeKeep.img" alt="" />
    </div>
    <div class="col-md-6">
      <div class="col-md-12">
        <div class="d-flex justify-content-between p-3">
          <h3>
            <i class="mdi mdi-eye">{{ activeKeep.views }}</i>
          </h3>
          <h3 class="K">
            K <span>{{ activeKeep.kept }}</span>
          </h3>
          <h3>
            <i class="mdi mdi-share">{{ random }}</i>
          </h3>
        </div>
      </div>
      <div class="col-md-12">
        <h1 class="text-dark text-center">
          {{ activeKeep.name }}
        </h1>
        <p class="text-dark text-center mt-3">
          {{ activeKeep.description }}
        </p>
      </div>
      <div class="col-md-12">
        <div class="d-flex bottom-left justify-content-between mt-5 text-wrap">
          <select
            v-if="route != 'Vault' && account.id != null"
            name=""
            id=""
            v-model="vaultId"
            class="text-dark me-5"
          >
            <option
              class="text-dark"
              :value="pv.id"
              v-for="pv in userVaults"
              :key="pv.id"
            >
              {{ pv.name }}
            </option>
          </select>
          <button
            v-if="route != 'Vault' && account.id != null"
            class="btn btn-success"
            @click="addToVaultKeep"
          >
            +
          </button>
          <button
            v-if="route == 'Vault'"
            class="btn btn-outline-primary"
            @click="removeKeepFromVault(activeKeep.vaultKeepId)"
          >
            Remove keep from Vault
          </button>
          <h2
            class="selectable me-5"
            v-if="activeKeep.creatorId == account.id && route != 'Vault'"
          >
            <i class="mdi mdi-delete" @click="removeKeep(activeKeep.id)"></i>
          </h2>
          <div
            class="d-flex align-items-center selectable ms-5"
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
  </div>
</template>


<script>
import { computed, ref } from "@vue/reactivity"
import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { keepsService } from "../services/KeepsService"
import { Modal } from "bootstrap"
import { useRoute, useRouter } from "vue-router"
import { vaultKeepsService } from "../services/VaultKeepsService"
import { onMounted } from "@vue/runtime-core"
export default {
  // props: {
  //   keeps: {
  //     type: Object,
  //     required: true
  //   }
  // },
  setup(props) {
    const route = useRoute()
    const router = useRouter()
    const vaultId = ref({})

    return {
      vaultId,
      async removeKeep(id) {
        try {
          if (await Pop.confirm()) {
            await keepsService.removeKeep(id)

            Modal.getOrCreateInstance(document.getElementById("active-keep")).hide()
          }
        } catch (error) {
          logger.log(error)
          Pop.toast(error.message, "error message")
        }
      },
      async goTo(id) {
        Modal.getOrCreateInstance(document.getElementById('active-keep')).hide()
        router.push({ name: "Profile", params: { id } })
      },
      async addToVaultKeep(id) {
        try {
          let vaultKeep = {
            vaultId: vaultId.value,
            keepId: AppState.activeKeep.id
          }
          await vaultKeepsService.addToVaultKeep(vaultKeep)

        } catch (error) {
          logger.error(error)
          Pop.toast(error.message)
        }
      },
      async removeKeepFromVault(id) {
        try {

          await vaultKeepsService.removeKeep(id)

        } catch (error) {
          logger.error(error)
          Pop.toast(error.message)
        }
      },
      async getVaultKeeps() {
        try {
          if (route.name == 'Vault') {
            await vaultKeepsService.getKeepsByVault(route.params.id)
          }
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message)
        }
      },
      // newKeep: computed(() => props.keeps.id),
      profileVaults: computed(() => AppState.activeVaults),
      activeKeep: computed(() => AppState.activeKeep),
      random: computed(() => Math.floor(Math.random() * 445)),
      account: computed(() => AppState.account),
      route: computed(() => route.name),
      activeVaultKeep: computed(() => AppState.profileVaultKeeps),
      // curkeep: computed(() => props.keeps.vaultKeepId)
      userVaults: computed(() => AppState.userVaults)


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
.bottom-left {
  position: absolute;
  bottom: 8px;
  left: 50%;
}
</style>