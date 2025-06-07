<template>
  <form @submit.prevent="fetch">
    <div class="flex flex-col sm:flex-row sm:space-x-4">
      <select v-model="location" class="border rounded p-2 flex-1">
        <option disabled value="">Select location</option>
        <option v-for="loc in locations" :key="loc">{{ loc }}</option>
      </select>
      <button
        type="submit"
        class="mt-2 sm:mt-0 bg-blue-500 text-white rounded px-4 py-2"
      >
        Get Weather
      </button>
    </div>
  </form>
</template>

<script>
export default {
  name: 'WeatherForm',
  data() {
    return {
      locations: ['Delhi','Moscow','Paris','New York','Sydney','Riyadh'],
      location: ''
    }
  },
  methods: {
    fetch() {
      if (!this.location) {
        return alert('Please select a location');
      }
      this.$emit('fetched', this.location);
    }
  }
}
</script>
