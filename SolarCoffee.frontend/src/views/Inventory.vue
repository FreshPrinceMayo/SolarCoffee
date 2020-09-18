<template>
  <div class="inventory-container">
    <h1 id="inventoryTitle">
      Inventory Dashboard
    </h1>
    <hr />

    <div class="inventory-actions">
      <solar-button @click.native="showNewProductModal" id="addNewBtn">
        Add new Item
      </solar-button>

      <solar-button @click.native="showShipmentModal" id="receiveShipmentBtn">
        Receive Shipment
      </solar-button>
    </div>

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

    <new-product-modal v-if="isNewProductVisible" @close="closeModals" />

    <shipment-modal
      v-if="isShipmentVisible"
      :inventory="inventory"
      @close="closeModals"
    />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { IProductInventory } from "@/types/Product";
import SolarButton from "@/components/SolarButton.vue";

@Component({
  name: "Inventory",
  components: { SolarButton }
})
export default class Inventory extends Vue {
  isNewProductVisible = false;
  isShipmentVisible = false;

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

  closeModals() {
    this.isShipmentVisible = false;
    this.isNewProductVisible = false;
  }

  // showNewProductModal() {}
  // receiveShipmentBtn() {}
}
</script>

<style scoped></style>
