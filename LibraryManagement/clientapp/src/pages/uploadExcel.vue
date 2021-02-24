<template>
  <div class="q-pa-md">
    <div class="row q-col-gutter-sm q-py-xs">
      <div class="col-xs-12 col-sm-6">
        <q-file filled v-model="file" label="File Upload"> </q-file>
      </div>

      <div class="col-xs-12 col-sm-6">
        <q-btn
          label="Submit"
          color="primary"
          v-on:click="onSubmit"
          icon-right="upload"
          class="q-mb-md q-mr-md"
        >
        </q-btn>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "upoadExcel",
  data() {
    return {
      file: null
    };
  },
  methods: {
    downloadExcelFormat: async function() {
      let vm = this;
      try {
        const method = "GET";
        const url = "DownloadExcelData/ExcelFormat";
        vm.$axios
          .request({
            url,
            method,
            responseType: "blob" //important
          })
          .then(({ data }) => {
            const downloadUrl = window.URL.createObjectURL(new Blob([data]));
            const link = document.createElement("a");
            link.href = downloadUrl;
            link.setAttribute("download", "STUDENT_INFO.xlsx");
            document.body.appendChild(link);
            link.click();
            link.remove();
          });
      } catch (error) {}
    },
    onSubmit: async function() {
      let vm = this;
      try {
        let formData = new FormData();
        formData.append("file", vm.file);
        var response = await vm.$axios.post(`UploadExcelData/All`, formData);
        vm.$q.notify({
          color: "green",
          message: response.data
        });
        vm.file = null;
      } catch (error) {}
    }
  }
};
</script>
