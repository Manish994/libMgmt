<template>
  <div class="q-pa-md">
    <div class="row q-col-gutter-sm q-py-xs">
      <div class="col-xs-12 col-sm-6">
        <q-btn
          label="Add New Student"
          color="primary"
          v-on:click="onAddStudent"
          class="q-mb-md"
        ></q-btn>
      </div>
      <div class="col-xs-12 col-sm-6">
        <q-btn
          label="Download"
          color="primary"
          v-on:click="onDownLoadExcel"
          icon-right="download"
          class="q-mb-md q-mr-md"
        >
        </q-btn>
      </div>
    </div>

    <q-dialog v-model="openStudentAddDialog">
      <q-card style="width: 700px; max-width: 80vw">
        <q-card-section>
          <div class="text-h6">Add New Student</div>
        </q-card-section>

        <q-card-section class="q-pt-none">
          <q-input label="Roll Number" v-model="addNewStudent.collegeRollNo"
            ><template v-slot:append> <q-icon name="person" /> </template
          ></q-input>
          <q-input label="First Name" v-model="addNewStudent.firstName"
            ><template v-slot:append> <q-icon name="person" /> </template
          ></q-input>
          <q-input label="Last Name" v-model="addNewStudent.lastName"
            ><template v-slot:append> <q-icon name="person" /> </template
          ></q-input>
          <q-select
            filled
            clearable
            v-model="addNewStudent.stdDepartment"
            :options="studentDepInfo"
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
          <q-btn push label="SAVE" v-on:click="saveNewStudent" />
          <q-btn push label="CANCEL" v-close-popup />
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
          <q-select
            clearable
            filled
            v-model="addNewStudentNew.department"
            :options="studentDepInfo"
            option-label="name"
            label="Select Department"
            option-value="id"
          />
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
          <td>{{ user.collegeRollNo }}</td>
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
      studentDepInfo: [],
      saveAll: null,
      addNewStudent: {
        collegeRollNo: "",
        firstName: "",
        lastName: "",
        stdDepartment: "",
        email: "",
        contactNumber: "",
        address: ""
      },
      openStudentEditDialog: false,
      addNewStudentNew: {
        firstName: "",
        lastName: "",
        department: null,
        email: "",
        contactNumber: "",
        address: ""
      },
      openStudentAddDialog: false
    };
  },
  methods: {
    getStudents: async function() {
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
    getAllDepartment: async function() {
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
    onUpdate: async function(id) {
      let vm = this;
      let response = await vm.$axios.get(`getStudentsById/` + id);
      vm.getStudentDepartment();
      this.openStudentEditDialog = true;
      vm.addNewStudentNew = response.data;
    },
    saveUpdateStudent: async function() {
      let vm = this;
      let response = await vm.$axios.post(
        "saveUpdate-StudentDetails",
        vm.addNewStudentNew
      );
      vm.$q.notify({
        message: response.data,
        color: "green"
      });
      vm.openStudentEditDialog = false;
      vm.restetFormUpdate();
      vm.getStudents();
    },
    onRemove: async function(user) {
      let vm = this;
      vm.$q
        .dialog({
          title: "Confim",
          message: "Are you confirm to delete?",
          cancel: true,
          persistent: true
        })
        .onOk(async () => {
          let response = await vm.$axios.post("del-Student", user);
          await vm.getStudents();
          vm.$q.notify({
            message: response.data,
            color: "red"
          });
        });
    },
    saveNewStudent: async function() {
      let vm = this;
      vm.addNewStudent.departmentId = vm.addNewStudent.stdDepartment.id;
      let response = await vm.$axios.post(
        "insert-newStudent",
        vm.addNewStudent
      );
      vm.$q.notify({
        message: response.data,
        color: "green"
      });
      vm.openStudentAddDialog = false;
      vm.restetForm();
      vm.getStudents();
    },
    onAddStudent: function() {
      let vm = this;
      this.openStudentAddDialog = true;
      vm.getStudentDepartment();
    },
    restetForm: function() {
      let vm = this;
      vm.addNewStudent.collegeRollNo = "";
      vm.addNewStudent.firstName = "";
      vm.addNewStudent.lastName = "";
      vm.addNewStudent.stdDepartment = "";
      vm.addNewStudent.email = "";
      vm.addNewStudent.contactNumber = "";
      vm.addNewStudent.address = "";
    },
    restetFormUpdate: function() {
      let vm = this;
      vm.addNewStudentNew.firstName = "";
      vm.addNewStudentNew.lastName = "";
      vm.addNewStudentNew.branch = "";
      vm.addNewStudentNew.email = "";
      vm.addNewStudentNew.contactNumber = "";
      vm.addNewStudentNew.address = "";
    },
    getStudentDepartment: async function() {
      let vm = this;
      try {
        vm.$q.loading.show();
        const response = await vm.$axios.get("department");
        vm.studentDepInfo = response.data;
        vm.$q.loading.hide();
      } catch (error) {
        vm.$q.loading.hide();
      }
    },
    onDownLoadExcel: async function() {
      let vm = this;
      const method = "GET";
      const url = `exportDataToExcel/GetAll`;
      this.$axios
        .request({
          url,
          method,
          responseType: "blob" //important
        })
        .then(({ data }) => {
          const downloadUrl = window.URL.createObjectURL(new Blob([data]));
          const link = document.createElement("a");
          link.href = downloadUrl;
          link.setAttribute("download", "List Of Students.xlsx"); //any other extension
          document.body.appendChild(link);
          link.click();
          link.remove();
        });
    }
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
  }
};
</script>
