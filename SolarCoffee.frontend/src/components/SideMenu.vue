<template>
  <div class="side-menu-container">
    <router-link to="/">
      <img id="logo" alt="logo" src="../assets/images/logo.png" />
    </router-link>

    <h1>Management Portal</h1>
    <solar-button
      id="menuInventory"
      @button:click="goToRoute('/inventory')"
      is-full-width
    >
      Inventory
    </solar-button>
    <solar-button
      id="menuCustomer"
      @button:click="goToRoute('/customer')"
      is-full-width
    >
      Manage Customers
    </solar-button>
    <solar-button
      id="menuInvoice"
      @button:click="goToRoute('/invoice/new')"
      is-full-width
    >
      New Invoice
    </solar-button>
    <solar-button
      id="menuOrders"
      @button:click="goToRoute('/orders')"
      is-full-width
    >
      Orders
    </solar-button>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import SolarButton from "@/components/SolarButton.vue";
@Component({
  name: "SideMenu",
  components: { SolarButton }
})
export default class SideMenu extends Vue {
  goToRoute(route: string) {
    this.$router.push(route);
  }
}
</script>

<style scoped lang="scss">
@import "@/scss/global.scss";

side-menu-container {
  background-color: #fcfcfc;
  height: 100vh;
  width: $menu-width;
  display: flex;
  flex-direction: column;
  padding: 0.8rem;
  box-shadow: 0 1px 1px rgba(0, 0, 0, 0.12), 0 1px 1px rgba(0, 0, 0, 0.24);
  box-sizing: border-box;
}

#logo {
  width: 100%;
}

h1 {
  font-size: 1.2rem;
  margin: 1rem 0;
}
</style>
