import Vue from "vue";
import VueRouter, { RouteConfig } from "vue-router";
import Inventory from "../views/Inventory.vue";
import Customer from "../views/Customer.vue";
import CreateInvoice from "../views/CreateInvoice.vue";

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
 {
   path: '/',
   name:'home',
   component: Inventory
 },
 {
  path: '/inventory',
  name:'inventory',
  component: Inventory
},
{
  path: '/customer',
  name:'customer',
  component: Customer
},
{
  path: '/invoice/new',
  name:'newInvoice',
  component: CreateInvoice
}

];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

export default router;
