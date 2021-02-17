
const routes = [
  {
    path: '/',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      { path: '', component: () => import('pages/index.vue') },
      { path: 'student/details', component: () => import('pages/studentdetails.vue') },
      { path: 'book/details', component: () => import('pages/bookdetails.vue') },
      { path: 'book/request', component: () => import('pages/requestbook.vue'), meta:{ requiresAuthentication: true } },
      {path: 'search/teacher', component:() => import ('pages/searchTeacher.vue')},
      {path:'new/teacher', component:() => import('pages/newTeacher.vue')},
    ]
  },

  {
    path: '/',
    component: () => import('layouts/LoginRegister.vue'),
    children: [
        { path: 'auth/login', component: () => import('pages/login.vue') },
      { path: 'auth/login', component: () => import('pages/login.vue') },
      { path: 'auth/register', component: () => import('pages/register.vue') },
    ]
  },

  {
    path: '/test',
    component: () => import('pages/test.vue')
  },

  {
    path: '/student',
    component: () => import('layouts/studentlayout.vue'),
    children: [
      { path: '', component: () => import('pages/studentprofile.vue') },
      { path: 'request/book', component: () => import('pages/studentbookdetails.vue') },
    ]
  },

  // Always leave this as last one,
  // but you can also remove it
  {
    path: '*',
    component: () => import('pages/Error404.vue')
  }
]

export default routes
