<template>
  <q-layout view="lHh Lpr lFf">
    <q-header class="bg-white text-blue-grey-8">
      <q-toolbar>
        <q-btn dense flat round icon="menu" @click="left = !left" />

        <q-toolbar-title></q-toolbar-title>
        <q-btn flat round dense icon="person" style="font-size: 20px;">
          <q-menu>
            <div class="row q-pa-lg">
              <div class="column">
                <q-item-label class="text-h6">TEACHER</q-item-label>
                <q-linear-progress
                  class="q-mb-lg"
                  color="grey-4"
                  style="height: 2px"
                />
                <div class="items-center">
                  <div class="q-mb-xs">Teacher Id :</div>
                  <div class="q-mb-xs">Teacher Name :</div>
                </div>
              </div>

              <div class="q-mt-md">
                <q-btn
                  color="primary"
                  label="Sign Out"
                  unelevated
                  size="lg"
                  v-close-popup
                >
                </q-btn>
              </div>
            </div>
          </q-menu>
        </q-btn>
      </q-toolbar>
    </q-header>

    <q-drawer
      v-model="left"
      :breakpoint="500"
      show-if-above
      content-class="bg-white text-teal-10"
    >
      <q-scroll-area class="fit">
        <q-list>
          <q-item style="height: 50px;" class="bg-white text-bold">
            <q-item-section>
              Library Management System
            </q-item-section>
          </q-item>
          <q-item class="justify-center text-center q-mb-md q-pt-md">
            <div>
              <q-avatar size="100px" class="q-mb-md">
                <img src="https://placeimg.com/500/300/nature" alt="" />
              </q-avatar>
              <div>Teacher Name</div>
            </div>
          </q-item>
          <q-item-label header class="text-h6">
            Admin Panel
          </q-item-label>
          <q-item to="/" active-class="q-item-no-link-highlighting">
            <q-item-section avatar>
              <q-icon name="person" />
            </q-item-section>
            <q-item-section>
              <q-item-label class="text-h6">TEACHER</q-item-label>
            </q-item-section>
          </q-item>
          <q-linear-progress color="grey" style="height: 2px" />
          <q-item
            to="/student/details"
            active-class="q-item-no-link-highlighting"
          >
            <q-item-section avatar>
              <q-icon name="person" />
            </q-item-section>
            <q-item-section>
              <q-item-label class="text-h6">STUDENT</q-item-label>
            </q-item-section>
          </q-item>
          <q-linear-progress color="grey" style="height: 2px" />
          <q-item to="/book/details" active-class="q-item-no-link-highlighting">
            <q-item-section avatar>
              <q-icon name="book" />
            </q-item-section>
            <q-item-section>
              <q-item-label class="text-h6">BOOK</q-item-label>
            </q-item-section>
          </q-item>
          <q-linear-progress color="grey" style="height: 2px" />
          <q-item to="/book/request" active-class="q-item-no-link-highlighting">
            <q-item-section avatar>
              <q-icon name="book" />
            </q-item-section>
            <q-item-section>
              <q-item-label class="text-h6">REQUEST BOOK</q-item-label>
            </q-item-section>
          </q-item>
          <q-linear-progress color="grey" style="height: 2px" />
           <q-item to="/search/teacher" active-class="q-item-no-link-highlighting">
            <q-item-section avatar>
              <q-icon name="search" />
            </q-item-section>
            <q-item-section>
              <q-item-label class="text-h6">SEARCH TEACHER</q-item-label>
            </q-item-section>
          </q-item>
        </q-list>
      </q-scroll-area>
    </q-drawer>
    <q-page-container class="bg-grey-2">
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script>
export default {
  name: "MainLayout",

  data() {
    return {
      left: false
    };
  }
};
</script>
