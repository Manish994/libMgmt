<template>
  <q-layout>
    <div class="q-pa-md">
      <div class="row">
        <div class="col-md-6">
          <q-btn color="primary" text-color="white" label="Student Information">
          </q-btn>
        </div>
      </div>

      <div class="row q-mt-xl ">
        <div class="col-md-6 q-pr-sm">
          <q-input
            v-model="userName"
            outlined
            label="UserName"
          ></q-input>
        </div>
        <div class="col-md-6 q-pl-sm">
          <q-input v-model="email" outlined label="Email">
          </q-input>
        </div>
      </div>

      <div class="row q-mt-xl ">
        <div class="col-md-6 q-pr-sm">
          <q-input
            v-model="password"
            outlined
            label="Password"
          ></q-input>
        </div>
      </div>
    </div>
  </q-layout>
</template>

<script>
import { LocalStorage } from 'quasar'
export default {
  name:'studentprofile',
  data(){
    return{
      userName:LocalStorage.getItem('Name'),
      email:LocalStorage.getItem('Email'),
      password:LocalStorage.getItem('Password'),
    }
  }
}
</script>