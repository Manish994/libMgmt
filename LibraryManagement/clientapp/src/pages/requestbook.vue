<template>
  <div class="q-pa-md">
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
          <td><q-btn
                v-on:click="btnApproved(book.id)"
                color="primary"
                icon-right="Ok"
                no-caps
                flat
                dense
              />
              <q-btn
                v-on:click="btnCancel(book.id)"
                color="negative"
                icon-right="cancel"
                no-caps
                flat
                dense
              />d</td>
        </tr>
      </tbody>
    </q-markup-table>
  </div>
</template>
<script>
import moment from 'moment';
export default {
  name: "studentbookdetails",
  data() {
    return {
      RequestInfo: [],
    };
  },
  methods: {
    getRequestInfo: async function() {
      let vm = this;
      let response = await vm.$axios.get("Response-Request");
      vm.RequestInfo = response.data;
    },
    btnApproved : async function(id){
      alert(id);
    },
    btnCancel:async function(id){
      alert(id);
    },
    formatDate: function (input){
      return moment(input).format("yyyy-MM-DD")
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
