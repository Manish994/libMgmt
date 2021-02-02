<template>
  <q-layout>
    <q-page class="row items-center">
      <div class="col-md-7 offset-md-2 col-xs-12 q-pl-md q-pr-md">
        <q-card-section class="bg-teal-1">
          <div class="row col-md-6 col-xs-12">
            <q-img src="~/assets/user.png" width="300px" height="300px"></q-img>
            <div class="col-md-6 col-xs-12">
              <div class="q-mt-xl">
                <q-form>
                  <q-input
                    filled
                    v-model="loginDetails.username"
                    label="UserName *"
                    lazy-rules
                    :rules="[
                      val => (val && val.length > 0) || 'Please type UserName'
                    ]"
                  >
                    <template v-slot:append>
                      <q-icon name="person" />
                    </template>
                  </q-input>

                  <q-input
                    filled
                    v-model="loginDetails.password"
                    label="Password *"
                    lazy-rules
                    :rules="[
                      val => (val && val.length > 0) || 'Please type Password'
                    ]"
                  >
                    <template v-slot:append>
                      <q-icon name="password" />
                    </template>
                  </q-input>

                  <div>
                    <q-btn label="Login" type="submit" color="primary"  v-on:click="loginCredentials"/>
                    <q-btn
                      label="Register"
                      type="reset"
                      color="primary"
                      flat
                      class="q-ml-sm"
                      to="/auth/register"
                    />
                  </div>
                </q-form>
              </div>
            </div>
          </div>
        </q-card-section>
      </div>
    </q-page>
  </q-layout>
</template>

<script>
export default {
  name: "login",
  data() {
    return {
      loginDetails: {
        username: "",
        password: ""
      }
    };
  },
  methods: {
    loginCredentials: async function() {
      let vm = this;
      let response = await vm.$axios.post("api/login", vm.loginDetails);
      console.log(response)
    }
  }
};
</script>