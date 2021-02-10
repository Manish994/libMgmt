<template>
  <div class="q-pa-md">
    <q-btn
      label="Request Book"
      color="primary"
      v-on:click="showDialog"
      class="q-mb-md q-mr-md"
    ></q-btn>
    <q-dialog v-model="bookDialog" persistent>
      <q-card style="width: 700px; max-width: 80vw">
        <q-card-section>
          <div class="text-h6">Request Book</div>
        </q-card-section>

        <q-card-section class="q-pt-none">
          <q-select
            filled
            class="q-mb-md"
            v-model="collegeRollN"
            :options="studentInfo"
            option-label="collegeRollNo"
            option-value="id"
            label="Select Roll Number"
            @input="GetValue"
          />
          <q-input
            color="black"
            square
            outlined
            :disable="true"
            label="First Name"
            v-model="requestBook.firstName"
            ><template v-slot:append> <q-icon name="person" /> </template
          ></q-input>
          <q-input
            class="q-mt-md"
            square
            outlined
            :disable="true"
            label="Last Name"
            v-model="requestBook.lastName"
            ><template v-slot:append> <q-icon name="person" /> </template
          ></q-input>
          <q-input
            class="q-mt-md"
            square
            outlined
            :disable="true"
            label="Branch"
            v-model="requestBook.name"
            ><template v-slot:append> <q-icon name="border_color" /> </template
          ></q-input>
          <q-input
            class="q-mt-md"
            square
            outlined
            :disable="true"
            label="E-Mail"
            v-model="requestBook.email"
            ><template v-slot:append> <q-icon name="email" /> </template
          ></q-input>
          <q-select
            filled
            class="q-mt-md"
            v-model="collegeBook"
            :options="getBookss"
            option-label="bookName"
            option-value="id"
            label="Select Book"
            @input="GetValueBook"
          />
          <q-input
            class="q-mt-md"
            square
            outlined
            :disable="true"
            label="Book Id"
            v-model="requestBook.bookId"
            ><template v-slot:append> <q-icon name="person" /> </template
          ></q-input>
          <q-input
            class="q-mt-md"
            square
            outlined
            :disable="true"
            label="Author Name"
            v-model="requestBook.authorName"
            ><template v-slot:append> <q-icon name="person" /> </template
          ></q-input>
          <!---------------------------------    date ----------------------------------->
          <div class="q-pa-md" style="max-width: 300px">
            <q-input filled v-model="date" mask="date" :rules="['date']">
              <template v-slot:append>
                <q-icon name="event" class="cursor-pointer">
                  <q-popup-proxy
                    ref="qDateProxy"
                    transition-show="scale"
                    transition-hide="scale"
                  >
                    <q-date v-model="date">
                      <div class="row items-center justify-end">
                        <q-btn
                          v-close-popup
                          label="Close"
                          color="primary"
                          flat
                        />
                      </div>
                    </q-date>
                  </q-popup-proxy>
                </q-icon>
              </template>
            </q-input>
          </div>
          <!---------------------------------    date ----------------------------------->
        </q-card-section>

        <q-card-actions align="right" class="bg-white text-teal">
          <q-btn flat label="OK" v-on:click="bookRequest" />
          <q-btn flat label="CANCEL" v-close-popup />
        </q-card-actions>
      </q-card>
    </q-dialog>
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
          <td>{{ book.action == false ? "Not Approved" : "Approved" }}</td>
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
      //getRequestInfo()
      RequestInfo: [],

      //getBookInfo()
      getBookss: [],
      collegeBook: [],

      //geStudents()
      studentInfo: [],
      collegeRollN: [],

      bookDialog: false,
      bookDialog: false,

      date: "",
      requestBook: {
        firstName: "",
        lastName: "",
        email: "",
        name: "",
        bookId: "",
        authorName: ""
      }
    };
  },
  methods: {
    //fetch student table. Inside student table exists departmentId.
    getStudents: async function() {
      let vm = this;
      try {
        vm.$q.loading.show();
        const response = await vm.$axios.get("get-AllStudents");
        vm.studentInfo = response.data;
        vm.$q.loading.hide();
      } catch (error) {
        vm.$q.loading.hide();
      }
    },
    //fetch BookDetail table and display in dropdown list
    getBookInfo: async function() {
      let vm = this;
      const response = await vm.$axios.get("Response-All");
      vm.getBookss = response.data;
    },
    //fetch student table with Id and filter student table and display related with Id
    //Also fetch department table where departmentId is stored in student table and display departmentName related with Id.
    GetValue: async function() {
      let vm = this;
      let response = await vm.$axios.get(
        `student/GetStudentByid?id=${vm.collegeRollN.id}`
      );
      vm.requestBook.firstName = response.data.result.firstName;
      vm.requestBook.lastName = response.data.result.lastName;
      vm.requestBook.email = response.data.result.email;
      vm.requestBook.name = response.data.result.department.name;
    },
    //fetch bookDetail table with Id and filter bookDetail table and display related with Id.
    GetValueBook: async function() {
      let vm = this;
      let filterData = await vm.$axios.get(
        `book/BookById?id=${vm.collegeBook.id}`
      );
      vm.requestBook.bookId = filterData.data.result.bookId;
      vm.requestBook.authorName = filterData.data.result.authorName;
    },
    clearForm: async function() {
      let vm = this;
      try {
        vm.collegeRollN.collegeRollNo = "";
        vm.requestBook.firstName = "";
        vm.requestBook.lastName = "";
        vm.requestBook.name = "";
        vm.requestBook.email = "";
        vm.collegeBook.bookName = "";
        vm.requestBook.bookId = "";
        vm.requestBook.authorName = "";
      } catch (error) {}
    },
    //fetch RequestBook table.
    getRequestInfo: async function() {
      let vm = this;
      let response = await vm.$axios.get("Response-Request");
      vm.RequestInfo = response.data;
    },
    //show popup model and
    //fetch CurrentDate from DateController.
    showDialog: async function() {
      let vm = this;
      vm.bookDialog = true;
      await vm.getStudents();
      await vm.getRequestInfo();
      await vm.getBookInfo();
      let response = await vm.$axios.get("Current-Date");
      vm.date = response.data;
    },
    //passing data into BookrequestController.
    bookRequest: async function() {
      let vm = this;
      try {
        let formData = new FormData();
        formData.append("collegeRollNo", vm.collegeRollN.collegeRollNo);
        formData.append("FirstName", vm.requestBook.firstName);
        formData.append("LastName", vm.requestBook.lastName);
        formData.append("Department", vm.requestBook.name);
        formData.append("Email", vm.requestBook.email);
        formData.append("BookName", vm.collegeBook.bookName);
        formData.append("BookId", vm.requestBook.bookId);
        formData.append("AuthorName", vm.requestBook.authorName);
        formData.append("IssueDate", vm.date);
        let response = await vm.$axios.post("Insert-Request", formData);
        vm.$q.notify({
          message: response.data,
          color: "green"
        });
        vm.bookDialog = false;
        vm.clearForm();
        await vm.getRequestInfo();
      } catch (error) {}
    },
    //format Date using moment JS.
    formatDate: function(input) {
      return moment(input).format("yyyy-MM-DD");
    }
  },
  async mounted() {
    let vm = this;
    try {
      //fetch student table and department table
      await vm.getStudents();
      //fetch requestBook table
      await vm.getRequestInfo();
      //fetch bookDetail table
      await vm.getBookInfo();
    } catch (error) {}
  }
};
</script>
