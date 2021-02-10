<template>
   <q-layout>
      <q-page>
      <q-file multiple v-model="files"></q-file>
      <q-btn label="Submit" @click="handleSubmit" color="primary"></q-btn>
   </q-page>
   </q-layout>
</template>

<script>
import axios from 'axios'
export default {
   data(){
      return{
         files: null
      }
   },
   methods:{
      handleSubmit: function () {
         let formData = new FormData();
         // let theFiles = this.files;
         // theFiles.forEach(function(item, index){
         //    formData.append("file" + index, theFiles[index]);
         // })

         for(var i= 0; i < this.files.length; i++){
            let file = this.files[i];
            formData.append("files[" + i + "]", file);
         }
         formData.append("test", "TEST");
         // axios({
         //    url:'https://localhost:44387/test',
         //    method:'post',
         //    data: formData,
         //    headers:{
         //       'Content-Type':'multipart/form-data'
         //    }
         // })
         // axios.post('https://localhost:44387/api/test', formData, {
         //    headers: {
         //       'Content-Type': 'multipart/form-data'
         //    }
         // }).then(function(res){
         //    console.log(res)
         // });
         
         this.$axios.post('api/test', formData);
      }
   }
}
</script>