<template>
  <q-layout class="bg-teal-2">
    <q-page class="row items-center justify-center">
      <div class="col-md-3 col-sm-4 col-xs-12">
        <q-card-section class="bg-teal-1 text-center login-panel">
          <!--------------- Display Successful & Filed Message -------------->
          <div></div>
          <!---------------                       --------------------------->

          <!--------------- Header Section Starts --------------------------->
          <div class="text-weight-medium q-mb-lg" style="font-size: 45px">
            Login In
          </div>
          <p class="font-weight-bold">welcome back</p>
          <!--------------- Header Sections Ends ----------------------------->

          <!---------------  Body Section Starts ------------------------------>
          <div>
            <q-input
              label="Email/Username"
              v-model="loginCredentials.userName"
              filled
              color="bg-teal-1"
              class="q-mt-lg"
            >
              <template v-slot:prepend>
                <q-icon name="email" />
              </template>
            </q-input>
            <q-input
              label="Password"
              type="password"
              v-model="loginCredentials.password"
              filled
              class="q-mt-md"
            >
              <template v-slot:prepend>
                <q-icon name="password" />
              </template>
            </q-input>
            <div class="q-mt-md" style="display: flex">
              <q-btn
                color="bg-teal-1"
                text-color="secondary"
                style="font-size: 15px"
                v-on:click="btnLogin"
                label="Log In"
                class="q-mr-sm"
                push
              />
              <q-btn
                color="bg-teal-1"
                text-color="secondary"
                style="font-size: 15px"
                label="Sign Up"
                class="q-ml-md"
                push
                to="/auth/register"
              />
            </div>
          </div>
          <!---------------  Body Section Ends ------------------------------>
        </q-card-section>
      </div>
    </q-page>
  </q-layout>
</template>
<script>
export default {
  name: "Login",
  data() {
    return {
      loginCredentials: {
        userName: "",
        password: ""
      }
    };
  },
  methods: {
    btnLogin: async function() {
      let vm = this;
      try {
        let response = await vm.$axios.post("login", vm.loginCredentials);
        const token = response.data.token;
        const name = response.data.userName;
        const role = response.data.role;
        const email = response.data.email;
        const password = response.data.password;
        if (response.data.role == "Admin") {
          localStorage.setItem("user-token", token);
          localStorage.setItem("Name", name);
          localStorage.setItem("Role", role);
          localStorage.setItem("Email", email);
          localStorage.setItem("Password", password);
          vm.$router.push("/");
        }

        if (response.data.role == "User") {
          localStorage.setItem("user-token", token);
          localStorage.setItem("Name", name);
          localStorage.setItem("Role", role);
          localStorage.setItem("Email", email);
          localStorage.setItem("Password", password);
          vm.$router.push("/student");
        }
      } catch (error) {}
    }
  }
};
</script>
