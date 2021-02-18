import Vue from 'vue'
import axios from 'axios'


Vue.prototype.$axios = axios.create({
    baseURL: 'https://localhost:44387/'
})

