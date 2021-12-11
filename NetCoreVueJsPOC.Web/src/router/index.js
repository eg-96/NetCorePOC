import Vue from 'vue'
import Router from 'vue-router'
import WeatherForecast from '@/components/WeatherForecast'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'WeatherForecast',
      component: WeatherForecast
    }
  ]
})
