import Vue from 'vue'
import moment from 'moment'

Vue.prototype.$formatDate = function (date,format){
   return moment(date).format(format)
}