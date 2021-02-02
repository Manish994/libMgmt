<template>
  <q-layout>
    <q-page-container>
      <q-page>
        <q-card flat class="bg-white text-black">
          <q-card-section class="fixed-center">
            <div class="text-h4 q-mb-md">Teacher Registration Form</div>
            <q-form>
              <q-input
                filled
                v-model="email"
                label="E-Mail *"
                lazy-rules
                :rules="[val => (val && val.length > 0) || 'Please type Email']"
              >
                <template v-slot:append>
                  <q-icon name="email" />
                </template>
              </q-input>
              <q-input
                filled
                v-model="username"
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
                v-model="password"
                label="Password *"
                lazy-rules
                :rules="[
                  val => (val && val.length > 0) || 'Please type Password'
                ]"
              >
                <template v-slot:append>
                  <q-icon name="password" /> </template
              ></q-input>
              <q-input
                filled
                v-model="repassword"
                label="Renter-Password *"
                lazy-rules
                :rules="[
                  val =>
                    (val && val.length > 0) || 'Please enter password again'
                ]"
              >
                <template v-slot:append>
                  <q-icon name="password" />
                </template>
              </q-input>
              <div class="q-mb-md">
                <q-btn label="Register" type="submit" color="primary" />
                <q-btn
                  label="Login"
                  type="reset"
                  color="primary"
                  flat
                  class="q-ml-sm"
                  to="/auth/login"
                />
              </div>
            </q-form>
          </q-card-section>
        </q-card>
      </q-page>
    </q-page-container>
  </q-layout>
</template>

<script>
export default {
  name: "register",
  data() {
    return {
      email: "",
      username: "",
      password: "",
      repassword: ""
    };
  }
};
</script>