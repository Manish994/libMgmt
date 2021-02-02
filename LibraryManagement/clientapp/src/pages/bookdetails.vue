<template>
  <div class="q-pa-md">
    <q-btn
      label="Add New Book"
      color="primary"
      v-on:click="onAddBook"
      class="q-mb-md q-mr-md"
    ></q-btn>
    <q-dialog v-model="openAddBookDialog">
      <q-card style="width: 650px; max-width: 80vw">
        <q-card-section>
          <div class="text-h6">Add New Book</div>
        </q-card-section>

        <q-card-section class="q-pt-none">
          <q-input
            label="Book ID"
            v-model="insertNewBooks.bookId"
            lazy-rules
            color="teal"
            :rules="[
              val =>
                (val !== null && val !== '') || 'Please Provide Serial Number'
            ]"
            ><template v-slot:append> <q-icon name="border_color" /> </template
          ></q-input>
          <q-input
            label="Author Name"
            v-model="insertNewBooks.authorName"
            lazy-rules
            color="teal"
            :rules="[
              val =>
                (val !== null && val !== '') || 'Please Provide Author Name'
            ]"
            ><template v-slot:append> <q-icon name="border_color" /> </template
          ></q-input>
          <q-input
            label="Book Name "
            v-model="insertNewBooks.bookName"
            lazy-rules
            color="teal"
            :rules="[
              val => (val !== null && val !== '') || 'Please Provide Book '
            ]"
            ><template v-slot:append> <q-icon name="book" /> </template
          ></q-input>
        </q-card-section>

        <q-card-actions align="right" class="bg-white text-teal">
          <q-btn flat label="SAVE" v-on:click="saveNewBook" />
          <q-btn flat label="CANCEL" v-close-popup />
        </q-card-actions>
      </q-card>
    </q-dialog>

    <q-dialog v-model="openBookEditDialog">
      <q-card style="width: 6500px; max-width: 80vw">
        <q-card-section>
          <div class="text-h6">Edit Book</div>
        </q-card-section>

        <q-card-section class="q-pt-none">
          <q-input
            label="Book ID"
            v-model="insertNewBooksNew.bookId"
            lazy-rules
            color="teal"
            :rules="[
              val =>
                (val !== null && val !== '') || 'Please Provide Serial Number'
            ]"
            ><template v-slot:append> <q-icon name="border_color" /> </template
          ></q-input>
          <q-input
            label="Author Name"
            v-model="insertNewBooksNew.authorName"
            lazy-rules
            color="teal"
            :rules="[
              val =>
                (val !== null && val !== '') || 'Please Provide Author Name'
            ]"
            ><template v-slot:append> <q-icon name="border_color" /> </template
          ></q-input>
          <q-input
            label="Book Name "
            v-model="insertNewBooksNew.bookName"
            lazy-rules
            color="teal"
            :rules="[
              val => (val !== null && val !== '') || 'Please Provide Book '
            ]"
            ><template v-slot:append> <q-icon name="book" /> </template
          ></q-input>
        </q-card-section>

        <q-card-actions align="right" class="bg-white text-teal">
          <q-btn flat label="UPDATE" v-on:click="updateBook" />
          <q-btn flat label="CANCEL" v-close-popup />
        </q-card-actions>
      </q-card>
    </q-dialog>
    <q-markup-table>
      <thead>
        <tr>
          <th class="text-left">Book_ID</th>
          <th class="text-left">Author_Name</th>
          <th class="text-left">Book_Name</th>
          <th class="text-left">Action</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="book in booksDetails" v-bind:key="book.id">
          <td>{{ book.bookId }}</td>
          <td>{{ book.authorName }}</td>
          <td>{{ book.bookName }}</td>
          <td>
            <q-btn
              v-on:click="onUpdate(book.id)"
              color="primary"
              icon="edit"
              no-caps
              flat
              dense
            />
            <q-btn
              v-on:click="onRemove(book)"
              color="negative"
              icon="delete"
              no-caps
              flat
              dense
            />
          </td>
        </tr>
      </tbody>
    </q-markup-table>
  </div>
</template>

<script>
export default {
  name: "bookdetails",
  data() {
    return {
      booksDetails: [],
      openAddBookDialog: false,
      insertNewBooks: {
        bookId: "",
        authorName: "",
        bookName: ""
      },
      insertNewBooksNew: {
        bookId: "",
        authorName: "",
        bookName: ""
      },
      openBookEditDialog: false
    };
  },
  methods: {
    getBooks: async function() {
      let vm = this;
      vm.$q.loading.show();
      const response = await vm.$axios.get("getallbooks");
      vm.$q.loading.hide();
      vm.booksDetails = response.data;
    },
    onAddBook: async function() {
      this.openAddBookDialog = true;
    },
    addNewBook: async function() {},
    saveNewBook: async function() {
      let vm = this;
      let response = await vm.$axios.post("create-newBooks", vm.insertNewBooks);
      vm.$q.notify({
        message: response.data,
        color: "green"
      });
      vm.openAddBookDialog = false;
      vm.restetForm();
      vm.getBooks();
    },
    onUpdate: async function(id) {
      let vm = this;
      let response = await vm.$axios.get(`books/` + id);
      this.openBookEditDialog = true;
      vm.insertNewBooksNew = response.data;
    },
    onRemove: async function(book) {
      let vm = this;
      vm.$q
        .dialog({
          title: "Confim",
          message: "Are you confirm to delete?",
          cancel: true,
          persistent: true
        })
        .onOk(async () => {
          let response = await vm.$axios.post("del-Book", book);
          await vm.getBooks();
          vm.$q.notify({
            message: response.data,
            color: "red"
          });
        });
    },
    restetForm: function() {
      let vm = this;
      vm.insertNewBooks.bookId = "";
      vm.insertNewBooks.authorName = "";
      vm.insertNewBooks.bookName = "";
    },
    restetFormUpdate: function() {
      let vm = this;
      vm.insertNewBooksNew.bookId = "";
      vm.insertNewBooksNew.authorName = "";
      vm.insertNewBooksNew.bookName = "";
    },
    updateBook: async function() {
      let vm = this;
      let response = await vm.$axios.post(
        "saveUpdate-BookDetails",
        vm.insertNewBooksNew
      );
      vm.$q.notify({
        message: response.data,
        color: "green"
      });
      vm.openBookEditDialog = false;
      vm.restetFormUpdate();
      this.getBooks();
    }
  },
  async mounted() {
    let vm = this;
    vm.$q.loading.show();
    await vm.getBooks();
    vm.$q.loading.hide();
  }
};
</script>