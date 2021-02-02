<template>
  <q-page padding class="items-center-justify-center bg-grey-3">
    <q-card class="bg-white q-pa-sm">
      <div class="q-pa-md">
        <q-form class="q-gutter-xs" v-for="book in student" v-bind:key="book.id">
          <q-input
            label="First Name"
            :disable="true"
            lazy-rules
            color="teal"
            v-model="book.firstName"
            :rules="[
              val => (val !== null && val !== '') || 'Please Provide First Name'
            ]"
          >
            <template v-slot:append>
              <q-icon name="person" />
            </template>
          </q-input>

          <q-input
            label="Last Name"
            :disable="true"
            lazy-rules
            color="teal"
            v-model="book.lastName"
            :rules="[
              val => (val !== null && val !== '') || 'Please Provide Last Name'
            ]"
          >
            <template v-slot:append>
              <q-icon name="person" />
            </template>
          </q-input>

          <q-input
            label="Branch"
            :disable="true"
            lazy-rules
            color="teal"
            v-model="book.branch"
            :rules="[
              val => (val !== null && val !== '') || 'Please Provide Branch'
            ]"
          >
            <template v-slot:append>
              <q-icon name="border_color" />
            </template>
          </q-input>

          <q-input
            label="E-Mail"
            :disable="true"
            lazy-rules
            color="teal"
            v-model="book.email"
            :rules="[
              val =>
                (val !== null && val !== '') || 'Please Provide Valid E-Mail'
            ]"
          >
            <template v-slot:append>
              <q-icon name="email" />
            </template>
          </q-input>

          <q-input
            label="Contact Number"
            lazy-rules
            :disable="true"
            color="teal"
            v-model="book.contactNumber"
            :rules="[
              val =>
                (val !== null && val !== '') || 'Please Provide Contact Number'
            ]"
          >
            <template v-slot:append>
              <q-icon name="phone" />
            </template>
          </q-input>

          <q-input
            label="Address"
            :disable="true"
            lazy-rules
            color="teal"
            v-model="book.address"
            :rules="[
              val => (val !== null && val !== '') || 'Please Provide Address'
            ]"
          >
            <template v-slot:append>
              <q-icon name="location_city" />
            </template>
          </q-input>

          <div>
            <q-btn
              unelevated
              class="q-mr-lg"
              icon="update"
              label="Update Profile"
              style="height: 50px"
              color="teal-6"
              v-on:click="updateProfile"
            />
            <q-btn
              unelevated
              icon="update"
              label="Update Password"
              style="height: 50px"
              color="teal-6"
              v-on:click="updatePassword"
            />
          </div>
        </q-form>
      </div>
    </q-card>
  </q-page>
</template>
 
<script>
export default {
  data() {
    return {
    //   detail: {
    //     firstName: "",
    //     lastName: "",
    //     branch: "",
    //     email: "",
    //     contactNumber: "",
    //     address: ""
    //   },
      student:[],
    };
  },

  methods: {
    updateProfile: async function() {
      let vm = this;
      let response = await vm.$axios.post("create-studentDetails", vm.detail);
      vm.$router.push("list");
      vm.$q.notify({
        message: response.data,
        color: "green"
      });
    },
    updatePassword:async function(){},
    getStudents: async function() {
      let vm = this;
      vm.$q.loading.show();
      const response = await vm.$axios.get("get-DetailStudent");
      console.log(response);
      vm.$q.loading.hide();
      vm.student = response.data;
    },
  },
   async mounted() {
    let vm = this;
    vm.$q.loading.show();
    await vm.getStudents();
    vm.$q.loading.hide();
  },
};
</script>
 
<style scoped>
</style>