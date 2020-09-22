<template>
  <div class="inventory-container">
    <h1 id="inventoryTitle">
      Inventory Dashboard
    </h1>
    <hr />

    <div class="inventory-actions">
      <solar-button @click.native="showProductModal" id="addNewBtn">
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

    <product-modal
      v-if="isProductVisible"
      @save:product="saveNewProduct"
      @close="closeModals"
      @click="showProductModal"
    />

    <shipment-modal
      v-if="isShipmentVisible"
      :inventory="inventory"
      @save:shipment="saveNewShipment"
      @close="closeModals"
      @click="showShipmentModal"
    />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { IProduct, IProductInventory } from "@/types/Product";
import SolarButton from "@/components/SolarButton.vue";
import ProductModal from "@/components/modals/ProductModal.vue";
import ShipmentModal from "@/components/modals/ShipmentModal.vue";
import { IShipment } from "@/types/Shipment";
import { InventoryService } from "@/services/inventory-service";

const inventoryService = new InventoryService();

@Component({
  name: "Inventory",
  components: { SolarButton, ProductModal, ShipmentModal }
})
export default class Inventory extends Vue {
  isProductVisible = false;
  isShipmentVisible = false;

  inventory: IProductInventory[] = [];

  closeModals() {
    this.isShipmentVisible = false;
    this.isProductVisible = false;
  }

  showProductModal() {
    this.isShipmentVisible = false;
    this.isProductVisible = true;
  }

  showShipmentModal() {
    this.isProductVisible = false;
    this.isShipmentVisible = true;
  }
  saveNewProduct(product: IProduct) {
    console.log("SaveNewProduct");
    console.log(product);
    this.closeModals();
  }

  async saveNewShipment(shipment: IShipment) {
    await inventoryService.updateInventoryQuantity(shipment);
    this.closeModals();
    await this.initialize();
  }

  async initialize() {
    this.inventory = await inventoryService.getInventory();
  }

  async created() {
    await this.initialize();
  }
}
</script>

<style scoped></style>
