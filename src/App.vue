<template>
  <div class="p-4 max-w-2xl mx-auto">
    <WeatherForm @fetched="onFetched" />
    <WeatherCard v-if="latest" :data="latest" />
    <WeatherTable v-if="records.length" :records="records" />
  </div>
</template>

<script>
import axios from 'axios';
import WeatherForm from './components/WeatherForm.vue';
import WeatherCard from './components/WeatherCard.vue';
import WeatherTable from './components/WeatherTable.vue';

export default {
  components: { WeatherForm, WeatherCard, WeatherTable },
  data() {
    return { latest: null, records: [] }
  },
  methods: {
    async onFetched(location) {
      const base = import.meta.env.VITE_API_BASE_URL;
      try {
        const { data } = await axios.get(`${base}/weather/current/${location}`);
        this.latest = { ...data, location };
        this.records.unshift({ ...data, fetchedAt: new Date().toISOString(), location });
      } catch {
        alert('Error fetching weather');
      }
    }
  }
}
</script>
