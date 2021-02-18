
const routes = [
  {
    path: '/',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      { path: '', component: () => import('pages/index.vue'), meta:{ requiresAuthentication: true, hasRole: 'Admin'}},
      { path: 'student/details', component: () => import('pages/studentdetails.vue'), meta:{requiresAuthentication : true, hasRole : 'Admin'} },
      { path: 'book/details', component: () => import('pages/bookdetails.vue') , meta :{requiresAuthentication : true, hasRole : 'Admin'}},
      { path: 'book/request', component: () => import('pages/requestbook.vue'), meta : {requiresAuthentication : true, hasRole : 'Admin'}},
      {path: 'search/teacher', component:() => import ('pages/searchTeacher.vue'), meta : {requiresAuthentication : true, hasRole : 'Admin'}},
      {path:'new/teacher', component:() => import('pages/newTeacher.vue'), meta : {requiresAuthentication : true, hasRole : 'Admin'}},
    ]
  },

  {
    path: '/auth',
    component: () => import('layouts/LoginRegister.vue'),
    children: [
        { path: 'login', component: () => import('pages/login.vue') },
      { path: 'register', component: () => import('pages/register.vue') },
    ]
  },

  {
    path:'/block',
    component:() => import('layouts/block.vue'),
    children:[
      {path:'oops', component: () => import('pages/notAccess.vue'), meta : {requiresAuthentication : true}},
    ]
  },

  {
    path: '/student',
    component: () => import('layouts/studentlayout.vue'),
    meta:{ requiresAuthentication: true },
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
