<template>
  <q-layout>
    <q-page-container>
      <router-view></router-view>
    </q-page-container>
    <div class="q-pa-md">
      <q-btn
        label="Add New Teacher"
        color="primary"
        v-on:click="onAddTeacher"
        class="q-mb-md q-mr-md"
      ></q-btn>

      <q-dialog v-model="openTeacherAddDialog">
        <q-card style="width: 700px; max-width: 80vw">
          <q-card-section>
            <div class="text-h6">Add New Teacher</div>
          </q-card-section>

          <q-card-section class="q-pt-none">
             <q-input
              label="Teacher ID"
              v-model="addNewTeacher.teacherId"
              lazy-rules
              color="teal"
              :rules="[
                val =>
                  (val !== null && val !== '') || 'Please Provide Teacher Id'
              ]"
              ><template v-slot:append> <q-icon name="person" /> </template
            ></q-input>

            <q-input
              label="First_Name"
              v-model="addNewTeacher.firstName"
              lazy-rules
              color="teal"
              :rules="[
                val =>
                  (val !== null && val !== '') || 'Please Provide First Name'
              ]"
              ><template v-slot:append> <q-icon name="person" /> </template
            ></q-input>
            <q-input
              label="Last_Name"
              v-model="addNewTeacher.lastName"
              lazy-rules
              color="teal"
              :rules="[
                val =>
                  (val !== null && val !== '') || 'Please Provide Last Name'
              ]"
              ><template v-slot:append> <q-icon name="person" /> </template
            ></q-input>

            <!-- <q-input> -->
            <q-select
              square
              outlined
              v-model="selectedDepartment"
              :options="teacherDepInfo"
              option-label="name"
              label="Select Department"
            />
            <q-input
              label="E-Mail"
              v-model="addNewTeacher.email"
              lazy-rules
              color="teal"
              :rules="[
                val =>
                  (val !== null && val !== '') ||
                  'Please Provide Valid Email-Address'
              ]"
              ><template v-slot:append> <q-icon name="email" /> </template
            ></q-input>
            <q-input
              label="Contact_Number"
              v-model="addNewTeacher.contactNumber"
              lazy-rules
              color="teal"
              :rules="[
                val =>
                  (val !== null && val !== '') || 'Please Provide Phone Number'
              ]"
              ><template v-slot:append> <q-icon name="phone" /> </template
            ></q-input>

            <q-file  filled v-model="file" label="File Upload"> </q-file>

             <q-input
              label="Citizenship NO"
              v-model="addNewTeacher.citizenshipNo"
              lazy-rules
              color="teal"
              :rules="[
                val =>
                  (val !== null && val !== '') || 'Please Provide Phone Number'
              ]"
              ><template v-slot:append> <q-icon name="phone" /> </template
            ></q-input>

          </q-card-section>

          <q-card-actions align="right" class="bg-white text-teal">
            <q-btn push label="SAVE" v-on:click="saveNewTeacher" />
            <q-btn push label="CANCEL" color="negative" v-close-popup />
          </q-card-actions>
        </q-card>
      </q-dialog>

      <q-dialog v-model="openEditTeacherDialog">
        <q-card style="width: 700px; max-width: 80vw">
          <q-card-section>
            <div class="text-h6">Edit Teacher</div>
          </q-card-section>

          <q-card-section class="q-pt-none">
             <q-input
              label="Teacher ID"
              v-model="addNewTeacherNew.teacherId"
              lazy-rules
              color="teal"
              :rules="[
                val =>
                  (val !== null && val !== '') || 'Please Provide First Name'
              ]"
              ><template v-slot:append> <q-icon name="person" /> </template
            ></q-input>

            <q-input
              label="First_Name"
              v-model="addNewTeacherNew.firstName"
              lazy-rules
              color="teal"
              :rules="[
                val =>
                  (val !== null && val !== '') || 'Please Provide First Name'
              ]"
              ><template v-slot:append> <q-icon name="person" /> </template
            ></q-input>
            <q-input
              label="Last_Name"
              v-model="addNewTeacherNew.lastName"
              lazy-rules
              color="teal"
              :rules="[
                val =>
                  (val !== null && val !== '') || 'Please Provide Last Name'
              ]"
              ><template v-slot:append> <q-icon name="person" /> </template
            ></q-input>
            <q-select
              square
              outlined
              v-model="addNewTeacherNew.department"
              :options="teacherDepInfo"
              option-label="name"
              label="Select Department"
              option-value="id"
            />
            <q-input
              label="E-Mail"
              v-model="addNewTeacherNew.email"
              lazy-rules
              color="teal"
              :rules="[
                val =>
                  (val !== null && val !== '') ||
                  'Please Provide Valid Email-Address'
              ]"
              ><template v-slot:append> <q-icon name="email" /> </template
            ></q-input>
            <q-input
              label="Contact_Number"
              v-model="addNewTeacherNew.contactNumber"
              lazy-rules
              color="teal"
              :rules="[
                val =>
                  (val !== null && val !== '') || 'Please Provide Phone Number'
              ]"
              ><template v-slot:append> <q-icon name="phone" /> </template
            ></q-input>
          </q-card-section>

          <q-card-actions align="right" class="bg-white text-teal">
            <q-btn flat label="UPDATE" v-on:click="saveUpdateTeacher" />
            <q-btn flat label="CANCEL" v-close-popup />
          </q-card-actions>
        </q-card>
      </q-dialog>
      <q-markup-table style="width:100%" class="table table-bordered">
        <thead>
          <tr>
            <th class="text-left">Teacher_ID</th>
            <th class="text-left">First_Name</th>
            <th class="text-left">Last_Name</th>
            <th class="text-left">Department</th>
            <th class="text-left">Email</th>
            <th class="text-left">Contact_Number</th>
            <th class="text-left">Citizenshio NO</th>
            <th class="text-left">Action</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="user in teachersinfo" v-bind:key="user.id">
            <td>{{ user.teacherId}}</td>
            <td>{{ user.firstName }}</td>
            <td>{{ user.lastName }}</td>
            <td>{{ user.department.name }}</td>
            <td>{{ user.email }}</td>
            <td>{{ user.contactNumber }}</td>
            <td>{{ user.citizenshipNo }}</td>
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
  </q-layout>
</template>

<script>
export default {
  name: "studentdetails",
  data() {
    return {
      teachersinfo: [],
      teacherDepInfo: [],
      selectedDepartment: null,

      openTeacherAddDialog: false,
      file: null,
      addNewTeacher: {
        teacherId:"",
        firstName: "",
        lastName: "",
        branch: "",
        email: "",
        contactNumber: "",
        citizenshipNo:"",
      },
      addNewTeacherNew: {
        teacherId:"",
        firstName: "",
        lastName: "",
        department: null,
        email: "",
        contactNumber: ""
      },
      openEditTeacherDialog: false
    };
  },
  methods: {
    onUpdate: async function(id) {
      let vm = this;
      let response = await vm.$axios.get(`teachers/` + id);
      this.openEditTeacherDialog = true;
      vm.addNewTeacherNew = response.data;
    },
    saveUpdateTeacher: async function() {
      let vm = this;
      let response = await vm.$axios.post(
        "saveUpdate-TeacherDetails",
        vm.addNewTeacherNew
      );
      vm.$q.notify({
        message: response.data,
        color: "green"
      });
      vm.restetFormUpdate();
      vm.openEditTeacherDialog = false;
      this.getTeachers();
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
          let response = await vm.$axios.post("del-Teacher", user);
          await vm.getTeachers();
          vm.$q.notify({
            message: response.data,
            color: "red"
          });
        });
    },
    saveNewTeacher: async function() {
      let vm = this;
      let formData = new FormData();
      formData.append("files", vm.file);
       formData.append("TeacherId", vm.addNewTeacher.teacherId);
      formData.append("FirstName", vm.addNewTeacher.firstName);
      formData.append("LastName", vm.addNewTeacher.lastName);
      formData.append("Id", vm.selectedDepartment.id);
      formData.append("Email", vm.addNewTeacher.email);
      formData.append("Contact", vm.addNewTeacher.contactNumber);
      formData.append("CitizenshipNo", vm.addNewTeacher.citizenshipNo);

      let response = await vm.$axios.post("insert-newteacher", formData);
      // vm.$axios({
      //   method: 'post',
      //   url: 'insert-newteacher',
      //   formData,
      //   headers:{'Content-Type': 'multipart/form-data'}
      // }).then(function() {

      // });

      vm.$q.notify({
        message: response.data,
        color: "green"
      });
      vm.openTeacherAddDialog = false;
      vm.restetForm();
      vm.getTeachers();
    },
    onAddTeacher: async function() {
      this.openTeacherAddDialog = true;
    },
    restetForm: function() {
      let vm = this;
      vm.addNewTeacher.firstName = "";
      vm.addNewTeacher.lastName = "";
      vm.selectedDepartment = null;
      vm.addNewTeacher.email = "";
      vm.addNewTeacher.contactNumber = "";
      vm.file = null;
      vm.addNewTeacher.citizenshipNo = "";
    },
    restetFormUpdate: function() {
      let vm = this;
      vm.addNewTeacherNew.firstName = "";
      vm.addNewTeacherNew.lastName = "";
      vm.addNewTeacherNew.branch = "";
      vm.addNewTeacherNew.email = "";
      vm.addNewTeacherNew.contactNumber = "";
      vm.addNewTeacherNew.address = "";
    },
    getTeachers: async function() {
      let vm = this;
      try {
        vm.$q.loading.show();
        const response = await vm.$axios.get("get-Teachers");
        vm.$q.loading.hide();
        vm.teachersinfo = response.data;
      } catch (error) {
        vm.$q.loading.hide();
      }
    },

    getTeacherDepartment: async function() {
      let vm = this;
      try {
        vm.$q.loading.show();
        const response = await vm.$axios.get("department");
        vm.teacherDepInfo = response.data;
        vm.$q.loading.hide();
      } catch (error) {
        vm.$q.loading.hide();
      }
    }
  },
  async mounted() {
    let vm = this;
    vm.$q.loading.show();
    await vm.getTeachers();
    await vm.getTeacherDepartment();
    vm.$q.loading.hide();
  }
};
</script>
