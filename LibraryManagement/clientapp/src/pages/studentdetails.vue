<template>
  <div class="q-pa-md">
    <q-btn
      label="Add New Student"
      color="primary"
      v-on:click="onAddStudent"
      class="q-mb-md"
    ></q-btn>

    <q-dialog v-model="openStudentAddDialog">
      <q-card style="width: 700px; max-width: 80vw">
        <q-card-section>
          <div class="text-h6">Add New Student</div>
        </q-card-section>

        <q-card-section class="q-pt-none">
          <q-input label="First Name" v-model="addNewStudent.firstName"
            ><template v-slot:append> <q-icon name="person" /> </template
          ></q-input>
          <q-input label="Last Name" v-model="addNewStudent.lastName"
            ><template v-slot:append> <q-icon name="person" /> </template
          ></q-input>
          <q-select
            square
            outlined
            v-model="selectedDepartment"
            :options="allDepartment"
            option-label="name"
            label="Select Department"
          />
         
          <q-input label="E-Mail" v-model="addNewStudent.email"
            ><template v-slot:append> <q-icon name="email" /> </template
          ></q-input>
          <q-input label="Contact Number" v-model="addNewStudent.contactNumber"
            ><template v-slot:append> <q-icon name="phone" /> </template
          ></q-input>
          <q-input label="Address" v-model="addNewStudent.address"
            ><template v-slot:append> <q-icon name="location_city" /> </template
          ></q-input>
        </q-card-section>

        <q-card-actions align="right" class="bg-white text-teal">
          <q-btn flat label="SAVE" v-on:click="saveNewStudent" />
          <q-btn flat label="CANCEL" v-close-popup />
        </q-card-actions>
      </q-card>
    </q-dialog>

    <q-dialog v-model="openStudentEditDialog">
      <q-card style="width: 700px; max-width: 80vw">
        <q-card-section>
          <div class="text-h6">Edit Student</div>
        </q-card-section>

        <q-card-section class="q-pt-none">
          <q-input label="First Name" v-model="addNewStudentNew.firstName"
            ><template v-slot:append> <q-icon name="person" /> </template
          ></q-input>
          <q-input label="Last Name" v-model="addNewStudentNew.lastName"
            ><template v-slot:append> <q-icon name="person" /> </template
          ></q-input>
          <q-input label="Branch" v-model="addNewStudentNew.branch"
            ><template v-slot:append> <q-icon name="border_color" /> </template
          ></q-input>
          <q-input label="E-Mail" v-model="addNewStudentNew.email"
            ><template v-slot:append> <q-icon name="email" /> </template
          ></q-input>
          <q-input
            label="Contact Number"
            v-model="addNewStudentNew.contactNumber"
            ><template v-slot:append> <q-icon name="phone" /> </template
          ></q-input>
          <q-input label="Address" v-model="addNewStudentNew.address"
            ><template v-slot:append> <q-icon name="location_city" /> </template
          ></q-input>
        </q-card-section>

        <q-card-actions align="right" class="bg-white text-teal">
          <q-btn flat label="UPDATE" v-on:click="saveUpdateStudent" />
          <q-btn flat label="CANCEL" v-close-popup />
        </q-card-actions>
      </q-card>
    </q-dialog>
    <q-markup-table>
      <thead>
        <tr>
          <th class="text-left">Student_ID</th>
          <th class="text-left">First_Name</th>
          <th class="text-left">Last_Name</th>
          <th class="text-left">Department</th>
          <th class="text-left">Email</th>
          <th class="text-left">Contact_Number</th>
          <th class="text-left">Address</th>
          <th class="text-left">Action</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="user in students" v-bind:key="user.id">
          <td>{{ user.id }}</td>
          <td>{{ user.firstName }}</td>
          <td>{{ user.lastName }}</td>
          <td>{{ user.department.name }}</td>
          <td>{{ user.email }}</td>
          <td>{{ user.contactNumber }}</td>
          <td>{{ user.address }}</td>
          <td>
            <q-btn
              v-on:click="onUpdate(user.id)"
              color="primary"
              icon-right="edit"
              no-caps
              flat
              dense
            />
            <q-btn
              v-on:click="onRemove(user)"
              color="negative"
              icon-right="delete"
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
  name: "studentdetails",
  data() {
    return {
      students: [],
      allDepartment: [],
      selectedDepartment: null,
      addNewStudent: {
        firstName: "",
        lastName: "",
        branch: "",
        email: "",
        contactNumber: "",
        address: "",
      },
      openStudentEditDialog: false,
      addNewStudentNew: {
        firstName: "",
        lastName: "",
        branch: "",
        email: "",
        contactNumber: "",
        address: "",
      },
      openStudentAddDialog: false,
    };
  },
  methods: {
    getStudents: async function () {
      let vm = this;
      try {
        vm.$q.loading.show();
        const response = await vm.$axios.get("get-AllStudents");
        vm.$q.loading.hide();
        vm.students = response.data;
      } catch (error) {
        vm.$q.loading.hide();
      }
    },
    getAllDepartment: async function () {
      let vm = this;
      try {
        vm.$q.loading.show();
        const response = await vm.$axios.get("All-Department");
        vm.$q.loading.hide();
        vm.allDepartment = response.data;
      } catch (error) {
        vm.$q.loading.hide();
      }
    },
    onUpdate: async function (id) {
      let vm = this;
      let response = await vm.$axios.get(`getStudentsById/` + id);
      this.openStudentEditDialog = true;
      vm.addNewStudentNew = response.data;
    },
    saveUpdateStudent: async function () {
      let vm = this;
      let response = await vm.$axios.post(
        "saveUpdate-StudentDetails",
        vm.addNewStudentNew
      );
      vm.$q.notify({
        message: response.data,
        color: "green",
      });
      vm.openStudentEditDialog = false;
      vm.restetFormUpdate();
      vm.getStudents();
    },
    onRemove: async function (user) {
      let vm = this;
      vm.$q
        .dialog({
          title: "Confim",
          message: "Are you confirm to delete?",
          cancel: true,
          persistent: true,
        })
        .onOk(async () => {
          let response = await vm.$axios.post("del-Student", user);
          await vm.getStudents();
          vm.$q.notify({
            message: response.data,
            color: "red",
          });
        });
    },
    saveNewStudent: async function () {
      let vm = this;
      let response = await vm.$axios.post(
        "insert-newStudent",
        vm.addNewStudent
      );
      vm.$q.notify({
        message: response.data,
        color: "green",
      });
      vm.openStudentAddDialog = false;
      vm.restetForm();
      vm.getStudents();
    },
    onAddStudent: function () {
      this.openStudentAddDialog = true;
    },
    restetForm: function () {
      let vm = this;
      vm.addNewStudent.firstName = "";
      vm.addNewStudent.lastName = "";
      vm.addNewStudent.branch = "";
      vm.addNewStudent.email = "";
      vm.addNewStudent.contactNumber = "";
      vm.addNewStudent.address = "";
    },
    restetFormUpdate: function () {
      let vm = this;
      vm.addNewStudentNew.firstName = "";
      vm.addNewStudentNew.lastName = "";
      vm.addNewStudentNew.branch = "";
      vm.addNewStudentNew.email = "";
      vm.addNewStudentNew.contactNumber = "";
      vm.addNewStudentNew.address = "";
    },
  },
  async mounted() {
    let vm = this;
    try {
      vm.$q.loading.show();
      await vm.getStudents();
      await vm.getAllDepartment();
      vm.$q.loading.hide();
    } catch (error) {
      vm.$q.loading.hide();
    }
  },
};
</script>