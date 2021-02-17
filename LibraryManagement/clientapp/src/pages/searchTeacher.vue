<template>
  <q-layout>
    <div class="q-pa-md" style="max-width:600px">
      <div class="row">
        <div class="col-sm-6">
          <q-select
            v-model="search"
            :options="options"
            option-label="teacherId"
            @filter="filterFn"
            use-input
            hide-selected
            fill-input
            outlined
            clearable
            placeholder="Search By Id"
          >
            <template v-slot:append>
              <q-icon name="search"></q-icon>
            </template>
          </q-select>
        </div>
        <div class="col-md-6 q-pa-sm">
          <q-btn
            color="primary"
            text-color="white"
            label="SEARCH"
            v-on:click="submit"
          ></q-btn>
        </div>
      </div>

      <div class="row q-mt-xl ">
        <div class="col-md-6 q-pr-sm">
          <q-input
            v-model="infoTeachers.firstName"
            outlined
            label="First Name"
          ></q-input>
        </div>
        <div class="col-md-6 q-pl-sm">
          <q-input v-model="infoTeachers.lastName" outlined label="Last Name">
          </q-input>
        </div>
      </div>

      <div class="row q-mt-xl">
        <div class="col-md-6 q-pr-sm">
          <q-input
          v-model="infoTeachers.department.name"
            outlined
            label="Department"
          >
          </q-input>
        </div>
        <div class="col-md-6 q-pl-sm">
          <q-input
            v-model="infoTeachers.email"
            outlined
            label="Email"
          ></q-input>
        </div>
      </div>

      <div class="row q-mt-xl">
        <div class="col-md-6 q-pr-sm">
          <q-input
            v-model="infoTeachers.contactNumber"
            outlined
            label="Contact Number"
          >
          </q-input>
        </div>
      </div>
    </div>
  </q-layout>
</template>
<script>
export default {
  name: "searchTeacher",
  data() {
    return {
      search: [],
      filterTeacher: [],
      options:this.filterTeacher,
      infoTeachers: {
        firstName: "",
        lastName: "",
        department:{
          name:""
        },
        email: "",
        contactNumber: ""
      }
    };
  },
  methods: {
    teachersId: async function() {
      let vm = this;
      try {
        vm.$q.loading.show();
        let response = await vm.$axios.get("fetch-teachersId");
        vm.filterTeacher = response.data;
        vm.$q.loading.hide();
      } catch (error) {
        vm.$q.loading.hide();
      }
    },

    filterFn(val, update,abort) {
       
      if (val.length < 1) {
        abort()
        return
      }
      update(() => {
        const needle = val.toLowerCase();
        this.options = this.filterTeacher.filter(
          v => v.teacherId.toLowerCase().indexOf(needle) > -1
        );
      });
    },
    submit: async function() {
      let vm = this;
      try {
        let response = await vm.$axios.get(
          `Search-TeacherWithId/` + vm.search.teacherId
        );
        vm.infoTeachers = response.data;
      } catch (error) {}
    }
  },
  async mounted() {
    let vm = this;
    try {
      vm.$q.loading.show();
      await vm.teachersId();
      vm.$q.loading.hide();
    } catch (error) {
      vm.$q.loading.hide();
    }
  }
};
</script>
