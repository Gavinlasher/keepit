<template>
  <form
    class="row bg-light justify-content-center text-dark"
    @submit.prevent="createVault"
  >
    <h2>Create Vault</h2>
    <div class="col-md-12">
      <label for="" class="form-label">Name: </label>
      <input
        type="text"
        v-model="editable.name"
        required
        class="form-control"
        aria-describedby="helpId"
        placeholder="Name of keep..."
      />
    </div>
    <div class="col-md-12">
      <label for="" class="form-label">Description: </label>
      <input
        type="text"
        v-model="editable.description"
        required
        class="form-control"
        aria-describedby="helpId"
        placeholder="Description..."
      />
    </div>
    <div class="col-md-12 mt-3">
      <label for="" id="isPrivate">Is this a private vault?</label>
      <input class="ms-3" type="checkbox" v-model="editable.isPrivate" />
    </div>
    <div class="d-flex justify-content-end">
      <button class="btn btn-success">Create</button>
    </div>
  </form>
</template>


<script>
import { ref } from "@vue/reactivity"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { vaultsService } from "../services/VaultsService"
export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async createVault() {
        try {
          await vaultsService.createVault(editable.value)
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, " error message")
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>