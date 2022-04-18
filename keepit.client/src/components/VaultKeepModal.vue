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
      <div class="col-md-12 align-items-flex-end">
        <div class="d-flex justify-content-between mt-5 text-wrap">
          <h2 class="selectable" v-if="activeKeep.creatorId == account.id">
            <i class="mdi mdi-delete" @click="removeFromVault()"></i>
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
  </div>
</template>


<script>
export default {
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>
</style>