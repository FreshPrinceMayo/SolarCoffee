<template>
  <div class="inventory-container">
    <h1 id="inventoryTitle">
      Inventory Dashboard
    </h1>
    <hr />

    <table id="inventoryTable" class="table">
      <tr>
        <th>Item</th>
        <th>Quantity On-hand</th>
        <th>Unit Price</th>
        <th>Taxable</th>
        <th>Delete</th>
      </tr>
      <tr v-for="item in inventory" :key="item.id">
        <td>{{ item.product.name }}</td>
        <td>{{ item.quantityOnHand }}</td>
        <td>{{ item.product.price | price }}</td>
        <td>
          <span v-if="item.product.isTaxable">
            Yes
          </span>
          <span v-else>
            No
          </span>
        </td>
        <td><div>x</div></td>
      </tr>
    </table>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { IProductInventory } from "@/types/Product";

@Component({
  name: "Inventory",
  components: {}
})
export default class Inventory extends Vue {
  inventory: IProductInventory[] = [
    {
      id: 1,
      quantityOnHand: 100,
      idealQuantity: 100,
      product: {
        id: 1,
        name: "Some product",
        description: "Good stuff",
        price: 100,
        createdDate: new Date(),
        updatedDate: new Date(),
        isTaxable: false,
        isArchived: false
      }
    },
    {
      id: 2,
      quantityOnHand: 800,
      idealQuantity: 800,
      product: {
        id: 1,
        name: "Old product",
        description: "Bad stuff",
        price: 999,
        createdDate: new Date(),
        updatedDate: new Date(),
        isTaxable: true,
        isArchived: false
      }
    }
  ];
}
</script>

<style scoped></style>
