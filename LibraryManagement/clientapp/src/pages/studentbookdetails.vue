<template>
    <div class="q-pa-md">
         <q-markup-table>
      <thead>
        <tr>
          <th class="text-left">Student_ID</th>
          <th class="text-left">First_Name</th>
          <th class="text-left">Last_Name</th>
          <th class="text-left">Branch</th>
          <th class="text-left">Book_ID</th>
          <th class="text-left">Book_Name</th>
          <th class="text-left">Author_Name</th>
          <th class="text-left">Issue_Date</th>
          <th class="text-left">Due_Date</th>
          <th class="text-left">Action</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="book in getbookss" v-bind:key="book.id">
          <td>{{ book.Id }}</td>
          <td>{{ book.BookId }}</td>
          <td>{{ book.AuthorName }}</td>
          <td>{{ book.BookName }}</td>
        </tr>
      </tbody>
    </q-markup-table>
    </div>
</template>
<script>
export default {
    name:'studentbookdetails',
    data(){
        return{
            getbookss:[],
        }
    },
    methods:{
        getStudents: async function() {
      let vm = this;
    //   vm.$q.loading.show();
      const response = await vm.$axios.get("GetDetailStudent");
    //   vm.$q.loading.hide();
      vm.students = response.data;
    },

    },
     async mounted() {
    let vm = this;
    // vm.$q.loading.show();
    await vm.getStudents();
    // vm.$q.loading.hide();
  },
}
</script>