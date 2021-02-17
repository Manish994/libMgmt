<template>
  <q-layout class="bg-teal-2">
    <q-page class="row items-center justify-center" style="height: 5px">
      <div class="col-md-5 col-sm-6 col-xs-12">
        <q-card-section class="bg-teal-1 text-center login-panel">
          <!----------------------------------------------------------------->

          <!--------------- Header Section Starts --------------------------->
          <div class="text-weight-medium q-mb-md" style="font-size: 40px">
            Sign Up
          </div>
          <!--------------- Header Sections Ends ----------------------------->

          <!---------------  Body Section Starts ------------------------------>
          <q-form>
            <q-input
              label="First Name"
              v-model.trim="regCredentials.firstName"
              filled
              color="bg-teal-1"
              :rules="[val => val !== null && val !== '']"
            >
              <template v-slot:append>
                <q-icon name="person" />
              </template>
            </q-input>
            <q-input
              label="Last Name"
              v-model.trim="regCredentials.lastName"
              filled
              color="bg-teal-1"
              class="q-mt-md"
              :rules="[val => val !== null && val !== '']"
            >
              <template v-slot:append>
                <q-icon name="person" />
              </template>
            </q-input>
            <q-input
              label="Email"
              v-model.trim="regCredentials.username"
              filled
              color="bg-teal-1"
              class="q-mt-md"
              :rules="[val => val !== null && val !== '']"
            >
              <template v-slot:append>
                <q-icon name="email" />
              </template>
            </q-input>
            <q-input
              label="Password"
              v-model.trim="regCredentials.password"
              filled
              type="password"
              class="q-mt-md"
              :rules="[val => val !== null && val !== '']"
            >
              <template v-slot:append>
                <q-icon name="password" />
              </template>
            </q-input>
            <q-input
              label="Confirm Password"
              v-model.trim="regCredentials.conPassword"
              filled
              type="password"
              class="q-mt-md"
              :rules="[val => val !== null && val !== '']"
            >
              <template v-slot:append>
                <q-icon name="password" />
              </template>
            </q-input>
            <div class="q-mt-md" style="display: flex">
              <q-btn
                color="bg-teal-1"
                text-color="secondary"
                style="font-size: 15px"
                v-on:click="btnSignUp"
                label="Sign Up"
                class="q-mr-sm"
                push
              />
              <q-btn
                color="bg-teal-1"
                text-color="secondary"
                style="font-size: 15px"
                label="Log In"
                class="q-ml-md"
                push
                to="/auth/login"
              />
            </div>
          </q-form>
          <!---------------  Body Section Ends ------------------------------>
        </q-card-section>
      </div>
    </q-page>
  </q-layout>
</template>
<script>
export default {
  name: "Register",
  data() {
    return {
      regCredentials: {
        firstName: "",
        lastName: "",
        username: "",
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
        if (vm.regCredentials.username) {
          const emailPattern = /^(?=[a-zA-Z0-9@._%+-]{6,254}$)[a-zA-Z0-9._%+-]{1,64}@(?:[a-zA-Z0-9-]{1,63}\.){1,8}[a-zA-Z]{2,63}$/;
          const regEmail =
            emailPattern.test(vm.regCredentials.username) || "Invalid Email";
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
                    "register",
                    vm.regCredentials
                  );
                  vm.$q.notify({
                    message: response.data.message,
                    progress: true,
                    color: "green",
                    position: "top",
                    textColor: "white",
                    timeout: 4000,
                    icon: "email"
                  });
                  vm.regCredentials.firstName = "";
                  vm.regCredentials.lastName = "";
                  vm.regCredentials.username = "";
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
    }
  }
};
</script>
