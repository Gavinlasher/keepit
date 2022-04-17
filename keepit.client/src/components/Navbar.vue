<template>
  <div
    class="
      row
      navbar navbar-expand-lg navbar-dark
      bg-custom
      px-3
      justify-content-between
    "
  >
    <nav class="col-md-12 navbar navbar-expand-lg navbar-dark bg-custom px-3">
      <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
        <div class="d-flex flex-column align-items-center">
          <img
            class="logo-color"
            alt="logo"
            src="../assets/img/logo-1.png"
            height="45"
          />
        </div>
      </router-link>

      <form class="form-inline my-2 my-lg-0 centerez">
        <div class="d-flex bg-light">
          <input
            class="form-control mr-sm-2"
            type="search"
            placeholder="Search"
            aria-label="Search"
          />
          <button class="btn btn-light my-2 my-sm-0" type="submit">
            <i class="mdi mdi-magnify rounded"></i>
          </button>
        </div>
      </form>

      <button
        class="navbar-toggler"
        type="button"
        data-bs-toggle="collapse"
        data-bs-target="#navbarText"
        aria-controls="navbarText"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <span class="navbar-toggler-icon" />
      </button>
      <div class="collapse navbar-collapse" id="navbarText">
        <ul class="navbar-nav me-auto">
          <li></li>
        </ul>
        <!-- LOGIN COMPONENT HERE -->
        <Login />
      </div>
    </nav>
  </div>
</template>

<script>
export default {
  setup() {
    return {};
  },
};
</script>

<style scoped>
a:hover {
  text-decoration: none;
}
.nav-link {
  text-transform: uppercase;
}
.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}
.logo-color {
  background-color: grey;
}
.bg-custom {
  background-color: #55efc4;
}
</style>
