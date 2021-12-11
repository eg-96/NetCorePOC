<template>
  <div class='hello'>
    <h1>{{ msg }}</h1>
    <mdb-container>
      <mdb-datatable-2 v-model="data" refresh />
    </mdb-container>
  </div>
</template>

<script>
import { mdbDatatable2, mdbContainer } from 'mdbvue'

export default {
  name: 'WeatherForecast',
  components: {
    mdbDatatable2,
    mdbContainer
  },
  data () {
    return {
      msg: 'Welcome to Weather Forecast App created with Vue.js',
      data: {
        columns: [],
        rows: []
      }
    }
  },
  mounted () {
    let self = this
    fetch('http://localhost:5000/api/weatherforecast', {
      'mode': 'cors',
      'headers': {
        'Access-Control-Allow-Origin': '*'
      }
    })
      .then((res) => res.json())
      .then((res) => {
        self.data = {
          columns: [
            {
              label: 'Date',
              field: 'date',
              sort: true
            },
            {
              label: 'Degrees Celsius',
              field: 'temperatureC',
              sort: true
            },
            {
              label: 'Degrees Fahrenheit',
              field: 'temperatureF',
              sort: true
            },
            {
              label: 'Summary',
              field: 'summaryDesc',
              sort: true
            }
          ],
          rows: res
        }
      })
      .catch(err => console.log(err))
  }
}
</script>

<!-- Add 'scoped' attribute to limit CSS to this component only -->
<style scoped>
h1, h2 {
  font-weight: normal;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
