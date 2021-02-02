<template>
  <div class="q-pa-md">
    <div class="q-gutter-md" style="max-width: 300px">
      <q-file filled bottom-slots v-model="model" label="label" counter>
        <template v-slot:prepend>
          <q-icon name="image_upload" v-on:click.stop></q-icon>
        </template>
        <template v-slot:append>
          <q-icon
            name="close"
            v-on:click.stop="model = null"
            class="cursor-pointer"
          ></q-icon>
        </template>
        <templete v-slot:hint>
          Field hind
        </templete>
      </q-file>

      <q-btn v-on:click="uploadFile">
        Upload
      </q-btn>
    </div>
  </div>
</template>
<script>
export default {
  name: "impageUpload",
  data() {
    return {
      model: ""
    };
  },
  methods: {
    uploadFile: function() {
      let vm = this;
      let reponse = vm.$axios.post("uploadFile", vm.model);
    }
  }
};
</script>