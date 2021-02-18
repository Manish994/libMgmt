<template>
  <q-layout>
    <q-page class="row items-center justify-center">
      <div class="col-md-3 col-sm-4 col-xs-12">
        <q-card-section class="bg-teal-1 text-center login-panel">
          <div class="text-weight-medium q-mb-lg" style="font-size: 45px">
            You Are Not Authorized !!!!
          </div>

          <q-btn
          v-on:click="btnHome"
            color="bg-teal-1"
            text-color="secondary"
            style="font-size: 25px"
            label="BACK HOME"
            class="q-ml-md"
            push
          >
          </q-btn>
        </q-card-section>
      </div>
    </q-page>
  </q-layout>
</template>

<script>
export default {
  name:'notAccess',
  methods:{
    btnHome:async function(){
      let vm=this;
      try {
        vm.$router.push('/student')
      } catch (error) {
        
      }
    }
  }
}
</script>
