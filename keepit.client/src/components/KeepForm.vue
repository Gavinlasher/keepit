<template>
  <form
    class="row bg-light justify-content-center text-dark"
    @submit.prevent="createKeep"
  >
    <h2>Create Keep</h2>
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
    <div class="col-md-12">
      <label for="" class="form-label">Image: </label>
      <input
        type="text"
        v-model="editable.img"
        required
        class="form-control"
        aria-describedby="helpId"
        placeholder="Image Url Here..."
      />
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
import { keepsService } from "../services/KeepsService"
export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async createKeep() {
        try {
          await keepsService.createKeep(editable.value)
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, "error message")
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>