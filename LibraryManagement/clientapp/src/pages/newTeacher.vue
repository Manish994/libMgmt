<template>
  <q-layout>
    <div class="q-pa-md">
      <div class="row">
        <div class="col-md-6">
          <q-btn color="primary" text-color="white" label="ADD NEW TEACHER">
          </q-btn>
        </div>
      </div>

      <div class="row q-mt-xl ">
        <div class="col-md-6 q-pr-sm">
          <q-input
            v-model="regCredentials.userName"
            outlined
            label="UserName"
          ></q-input>
        </div>
        <div class="col-md-6 q-pl-sm">
          <q-input v-model="regCredentials.email" outlined label="Email">
          </q-input>
        </div>
      </div>

      <div class="row q-mt-xl ">
        <div class="col-md-6 q-pr-sm">
          <q-input
            v-model="regCredentials.password"
            outlined
            label="Password"
          ></q-input>
        </div>
        <div class="col-md-6 q-pl-sm">
          <q-input
            v-model="regCredentials.conPassword"
            outlined
            label="Confirm Password"
          >
          </q-input>
        </div>
      </div>

      <div class="row q-mt-xl ">
        <div class="q-pr-sm">
          <q-btn
            color="secondary"
            push
            text-color="white"
            label="SUBMIT"
            v-on:click="btnSignUp"
          >
          </q-btn>
        </div>
        <div class="q-pr-sm">
          <q-btn
            color="negative"
            push
            text-color="white"
            label="CANCEL"
            v-on:click="btnCancel"
          >
          </q-btn>
        </div>
      </div>
    </div>
  </q-layout>
</template>
<script>
export default {
  name: "newTeacher",
  data() {
    return {
      regCredentials: {
        userName: "",
        email: "",
        password: "",
        conPassword: ""
      }
    };
  },

  methods: {
    btnSignUp: async function() {
      let vm = this;
      try {
        //check email is valid or not
        if (vm.regCredentials.email) {
          const emailPattern = /^(?=[a-zA-Z0-9@._%+-]{6,254}$)[a-zA-Z0-9._%+-]{1,64}@(?:[a-zA-Z0-9-]{1,63}\.){1,8}[a-zA-Z]{2,63}$/;
          const regEmail =
            emailPattern.test(vm.regCredentials.email) || "Invalid Email";
          if (regEmail == true) {
            //check password match or not
            if (vm.regCredentials.password) {
              if (vm.regCredentials.conPassword) {
                if (
                  vm.regCredentials.password !== vm.regCredentials.conPassword
                ) {
                  vm.$q.notify({
                    message: "Passwords do not match.",
                    progress: true,
                    color: "red",
                    position: "top",
                    textColor: "white",
                    timeout: 4000,
                    icon: "password"
                  });
                } else {
                  //pass data into controller
                  let response = await vm.$axios.post(
                    "register-admin",
                    vm.regCredentials
                  );
                  console.log(response.data.message);
                  vm.$q.notify({
                    message: response.data.message,
                    color: "green"
                  });
                  vm.regCredentials.userName = "";
                  vm.regCredentials.email = "";
                  vm.regCredentials.password = "";
                  vm.regCredentials.conPassword = "";
                }
              } else {
                vm.$q.notify({
                  message: "Confirm Password cannot be blank",
                  progress: true,
                  color: "red",
                  position: "top",
                  textColor: "white",
                  timeout: 4000,
                  icon: "email"
                });
              }
            } else {
              vm.$q.notify({
                message: "Password cannot be blank",
                progress: true,
                color: "red",
                position: "top",
                textColor: "white",
                timeout: 4000,
                icon: "email"
              });
            }
          } else {
            vm.$q.notify({
              message: regEmail,
              progress: true,
              color: "red",
              position: "top",
              textColor: "white",
              timeout: 4000,
              icon: "email"
            });
          }
        } else {
          vm.$q.notify({
            message: "Email cannot be blank",
            progress: true,
            color: "red",
            position: "top",
            textColor: "white",
            timeout: 4000,
            icon: "email"
          });
        }
      } catch (error) {}
    },

    btnCancel: async function() {
      let vm = this;
      try {
        vm.regCredentials.userName = "";
        vm.regCredentials.email = "";
        vm.regCredentials.password = "";
        vm.regCredentials.conPassword = "";
      } catch (error) {}
    }
  }
};
</script>
