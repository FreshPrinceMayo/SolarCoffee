<template>
  <div class="home">
    <HelloWorld
      msg="Welcome to my application"
      customMessage="my custom text"
    />
  </div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from "@/components/HelloWorld.vue";

export default {
  name: "Home",
  components: {
    HelloWorld
  }
};
</script>
