<template>
  <div class="customer-container">
    <h1 id="customer-title">
      Customer Dashboard
    </h1>
    <hr />

    <div class="customer-actions">
      <solar-button id="addNewBtn" @click.native="showCustomerModal">
        Add new Customer
      </solar-button>
    </div>

    <table id="customerTable" class="table">
      <tr>
        <th>Customer</th>
        <th>Address</th>
        <th>State</th>
        <th>Since</th>
        <th>Delete</th>
      </tr>
      <tr v-for="customer in customers" :key="customer.id">
        <td>{{ customer.firstName + " " + customer.lastName }}</td>
        <td>
          {{
            customer.primaryAddress.addressLine1 +
              " " +
              customer.primaryAddress.addressLine2
          }}
        </td>
        <td>{{ customer.primaryAddress.State }}</td>
        <td>{{ customer.created }}</td>
        <td>
          <div class="lni lni-cross-circle customer-archive"></div>
        </td>
      </tr>
    </table>

    <customer-modal
      v-if="isCustomerVisible"
      :customer="customer"
      @save:customer="saveNewCustomer"
      @close="closeModals"
      @click="showCustomerModal"
    />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import SolarButton from "@/components/SolarButton.vue";
import { ICustomer, ICustomerAddress } from "@/types/Customer";
import { CustomerService } from "@/services/customer-service";
import CustomerModal from "@/components/modals/CustomerModal.vue";

const customerService = new CustomerService();

@Component({
  name: "Customer",
  components: { SolarButton, CustomerModal }
})
export default class Customer extends Vue {
  isCustomerVisible = false;
  customers: ICustomer[] = [];

  async initialize() {
    this.customers = await customerService.getCustomers();
  }

  async created() {
    await this.initialize();
  }

  async saveNewCustomer(newCustomer: ICustomer) {
    this.isCustomerVisible = false;
    await this.initialize();
  }

  showCustomerModal() {
    this.isCustomerVisible = true;
  }

  closeModals() {
    this.isCustomerVisible = false;
  }
}
</script>

<style lang="scss" scoped>
@import "@/scss/global.scss";

.customer-actions {
  display: flex;
  margin-bottom: 0.8rem;
}

.customer-archive {
  cursor: pointer;
  font-weight: bold;
  font-size: 1.2rem;
  color: $solar-red;
}
</style>
