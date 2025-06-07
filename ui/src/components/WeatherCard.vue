<template>
  <div class="bg-white bg-opacity-80 backdrop-blur-md rounded-2xl p-6 shadow-lg">
    <div class="flex justify-between items-center">
      <div>
        <div class="text-sm text-gray-600">Today</div>
        <div class="text-6xl font-bold">{{ data.temp }}°</div>
        <div class="text-xl capitalize">{{ data.desc }}</div>
        <div class="text-sm text-gray-500 mt-1">
          {{ data.location }} | {{ formattedDate }}
        </div>
      </div>
      <img :src="iconUrl" alt="icon" class="w-20 h-20" />
    </div>
    <div class="mt-6 grid grid-cols-3 sm:grid-cols-6 gap-2">
      <div v-for="hour in hours" :key="hour.time" class="text-center">
        <div class="text-xs text-gray-600">{{ hour.time }}</div>
        <div class="text-sm font-medium">{{ hour.temp }}°</div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ['data'],
  data() {
    return { hours: [] }
  },
  computed: {
    iconUrl() {
      return `https://openweathermap.org/img/wn/${this.data.icon}@2x.png`;
    },
    formattedDate() {
      const d = new Date();
      return d.toLocaleDateString(undefined, { day: 'numeric', month: 'short' });
    }
  },
  mounted() {
    // Simple 6-hour forecast mock
    const now = new Date();
    this.hours = Array.from({ length: 6 }).map((_, i) => {
      const h = new Date(now.getTime() + i * 3600000);
      return { time: `${h.getHours()}:00`, temp: (this.data.temp - i*0.5).toFixed(1) };
    });
  }
}
</script>
