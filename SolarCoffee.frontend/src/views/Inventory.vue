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
        <td
          v-bind:class="
            `${applyColor(item.quantityOnHand, item.idealQuantity)}`
          "
        >
          {{ item.quantityOnHand }}
        </td>
        <td>{{ item.product.price | price }}</td>
        <td>
          <span v-if="item.product.isTaxable">
            Yes
          </span>
          <span v-else>
            No
          </span>
        </td>
        <td>
          <div
            class="lni lni-cross-circle product-archive"
            @click="archiveProduct(item.product.id)"
          ></div>
        </td>
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
import { ProductService } from "@/services/product-service";

const inventoryService = new InventoryService();
const productService = new ProductService();

@Component({
  name: "Inventory",
  components: { SolarButton, ProductModal, ShipmentModal }
})
export default class Inventory extends Vue {
  isProductVisible = false;
  isShipmentVisible = false;

  inventory: IProductInventory[] = [];

  async archiveProduct(productId: number) {
    await productService.archive(productId);
    await this.initialize();
  }

  async saveNewProduct(newProduct: IProduct) {
    await productService.save(newProduct);
    this.closeModals();
    await this.initialize();
  }

  closeModals() {
    this.isShipmentVisible = false;
    this.isProductVisible = false;
  }

  showProductModal() {
    this.isShipmentVisible = false;
    this.isProductVisible = true;
  }

  applyColor(current: number, target: number) {
    if (current <= 0) {
      return "red";
    }
    if (Math.abs(target - current) > 8) {
      return "yellow";
    }

    return "green";
  }

  showShipmentModal() {
    this.isProductVisible = false;
    this.isShipmentVisible = true;
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

<style scoped lang="scss">
@import "@/scss/global.scss";

.green {
  font-weight: bold;
  color: $solar-green;
}
.yellow {
  font-weight: bold;
  color: $solar-yellow;
}
.red {
  font-weight: bold;
  color: $solar-red;
}
.inventory-actions {
  display: flex;
  margin-bottom: 0.8rem;
}
.product-archive {
  cursor: pointer;
  font-weight: bold;
  font-size: 1.2rem;
  color: $solar-red;
}
</style>
