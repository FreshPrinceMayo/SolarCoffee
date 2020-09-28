<template>
  <div>
    <h1 id="invoice-title">
      Create Invoice
    </h1>
    <hr />

    <div class="invoice-step" v-if="invoiceStep === 1">
      <h2>Step 1: Select Customer</h2>
      <div v-if="customers.length" class="invoice-step-detail">
        <label for="customer">Customer:</label>
        <select
          v-model="selectedCustomerId"
          class="invoice-customers"
          id="customer"
        >
          <option disabled value="">Please select a Customer</option>
          <option v-for="c in customers" :value="c.id" :key="c.id">{{
            c.firstName + " " + c.lastName
          }}</option>
        </select>
      </div>
    </div>

    <div class="invoice-step" v-if="invoiceStep === 2">
      <p>setp 2</p>
    </div>

    <div class="invoice-step" v-if="invoiceStep === 3">
      <p>step 3</p>
    </div>
    <hr />
    <div class="invoice-step-actions">
      <solar-button @button:click="prev" :disabled="!canGoPrev">
        Previous
      </solar-button>
      <solar-button @button:click="next" :disabled="!canGoNext">
        Next
      </solar-button>
      <solar-button @button:click="statOver">Start Over</solar-button>
    </div>
  </div>
</template>

<script lang="ts">
import { CustomerService } from "@/services/customer-service";
import { InventoryService } from "@/services/inventory-service";
import { InvoiceService } from "@/services/invoice-service";
import { ICustomer } from "@/types/Customer";
import { IInvoice, ILineItem } from "@/types/Invoice";
import { IProductInventory } from "@/types/Product";
import { Component, Vue } from "vue-property-decorator";
import SolarButton from "@/components/SolarButton.vue";

const customerService = new CustomerService();
const inventoryService = new InventoryService();
const invoiceService = new InvoiceService();

@Component({
  name: "Create Invoice",
  components: {
    SolarButton
  }
})
export default class CreateInvoice extends Vue {
  invoiceStep = 1;
  invoice: IInvoice = {
    createdDate: new Date(),
    customerId: 0,
    lineItems: [],
    updatedDate: new Date()
  };
  customers: ICustomer[] = [];
  selectedCustomerId = 0;
  inventory: IProductInventory[] = [];
  lineItems: ILineItem[] = [];
  newItem: ILineItem = { product: undefined, quantity: 0 };

  async initialize(): Promise<void> {
    customerService.getCustomers().then(res => (this.customers = res));
    inventoryService.getInventory().then(res => (this.inventory = res));
  }

  async created() {
    this.initialize();
  }
  prev() {
    if (this.invoiceStep === 1) {
      return;
    }
    this.invoiceStep -= 1;
  }

  next() {
    if (this.invoiceStep === 3) {
      return;
    }
    this.invoiceStep += 1;
  }

  get canGoNext() {
    if (this.invoiceStep === 1) {
      return this.selectedCustomerId !== 0;
    }

    if (this.invoiceStep === 2) {
      return true;
    }

    if (this.invoiceStep === 3) {
      return false;
    }

    return false;
  }

  canGoPrev() {}

  startOver() {}
}
</script>

<style scoped lang="scss">
@import "@/scss/global.scss";
.invoice-steps-actions {
  display: flex;
  width: 100%;
}
.invoice-step {
}
.invoice-step-detail {
  margin: 1.2rem;
}
.invoice-order-list {
  margin-top: 1.2rem;
  padding: 0.8rem;
  .line-item {
    display: flex;
    border-bottom: 1px dashed #ccc;
    padding: 0.8rem;
  }
  .item-col {
    flex-grow: 1;
  }
}
.invoice-item-actions {
  display: flex;
}
.price-pre-tax {
  font-weight: bold;
}
.price-final {
  font-weight: bold;
  color: $solar-green;
}
.due {
  font-weight: bold;
}
.invoice-header {
  width: 100%;
  margin-bottom: 1.2rem;
}
.invoice-logo {
  padding-top: 1.4rem;
  text-align: center;
  img {
    width: 280px;
  }
}
</style>