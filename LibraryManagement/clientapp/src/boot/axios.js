import Vue from 'vue'
import axios from 'axios'
import { LocalStorage } from 'quasar'


Vue.prototype.$axios = axios.create({
    baseURL: 'https://localhost:44387/',
    headers:{
        Authorization:"Bearer" + " " + LocalStorage.getItem('user-token') 
    }
})

