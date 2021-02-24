<template>
  <div class="q-pa-md">
    <div class="row q-col-gutter-sm q-py-xs">
      <div class="col-xs-12 col-sm-6">
        <q-btn
          label="Download"
          color="primary"
          v-on:click="downloadExcel"
          icon-right="download"
          class="q-mb-md q-mr-md"
        >
        </q-btn>
      </div>
    </div>
    <q-markup-table>
      <thead>
        <tr>
          <th class="text-left">Student ID</th>
          <th class="text-left">First Name</th>
          <th class="text-left">Last Name</th>
          <th class="text-left">Branch</th>
          <th class="text-left">email</th>
          <th class="text-left">Book ID</th>
          <th class="text-left">Book Name</th>
          <th class="text-left">Author Name</th>
          <th class="text-left">Issue Date</th>
          <th class="text-left">Due Date</th>
          <th class="text-left">Action</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="book in RequestInfo" v-bind:key="book.id">
          <td>{{ book.collegeRollNo }}</td>
          <td>{{ book.firstName }}</td>
          <td>{{ book.lastName }}</td>
          <td>{{ book.departmentName }}</td>
          <td>{{ book.email }}</td>
          <td>{{ book.bookId }}</td>
          <td>{{ book.bookName }}</td>
          <td>{{ book.bookAuthor }}</td>
          <td>{{ formatDate(book.issueDate) }}</td>
          <td>{{ formatDate(book.dueDate) }}</td>
          <td>
            <q-btn
              v-on:click="btnApproved(book)"
              color="primary"
              icon-right="check_circle"
              no-caps
              flat
              dense
              v-if="book.action == null || book.action == 1"
            />
            <q-btn
              v-on:click="btnCancel(book)"
              color="negative"
              icon-right="cancel"
              no-caps
              flat
              dense
              v-if="book.action == null || book.action == 0"
            />
          </td>
        </tr>
      </tbody>
    </q-markup-table>
  </div>
</template>
<script>
import moment from "moment";
export default {
  name: "studentbookdetails",
  data() {
    return {
      RequestInfo: []
    };
  },
  methods: {
    getRequestInfo: async function() {
      let vm = this;
      let response = await vm.$axios.get("Response-Request");
      vm.RequestInfo = response.data;
    },
    downloadExcel: async function() {
      let vm = this;
      try {
       const method = 'GET';
      const url = 'bookRequest/pullAllExcel';
      vm.$axios
          .request({
              url, method,
              responseType: 'blob', //important
          })
          .then(({ data }) => {
              const downloadUrl = window.URL.createObjectURL(new Blob([data]));
              const link = document.createElement('a');
              link.href = downloadUrl;
              link.setAttribute("download", "List Of Students.xlsx");
              document.body.appendChild(link);
              link.click();
              link.remove();
          });
      } catch (error) {}
    },
    btnApproved: async function(book) {
      let vm = this;
      try {
        let response = await vm.$axios.post("Approve-Request", book);
        vm.$q.notify({
          message: response.data,
          color: "green"
        });
        vm.getRequestInfo();
      } catch (error) {}
    },
    btnCancel: async function(book) {
      let vm = this;
      try {
        let response = await vm.$axios.post("Cancel-Request", book);
        vm.$q.notify({
          message: response.data,
          color: "negative"
        });
        vm.getRequestInfo();
      } catch (error) {}
    },
    formatDate: function(input) {
      return moment(input).format("yyyy-MM-DD");
    }
  },
  async mounted() {
    let vm = this;
    try {
      await vm.getRequestInfo();
    } catch (error) {}
  }
};
</script>
