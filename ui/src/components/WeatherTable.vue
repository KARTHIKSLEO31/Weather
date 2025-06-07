<template>
  <div>
    <div class="flex flex-col sm:flex-row sm:space-x-4 mb-4">
      <select v-model="filter.location" class="border rounded p-2 mb-2 sm:mb-0">
        <option value="">All Locations</option>
        <option v-for="loc in locations" :key="loc">{{ loc }}</option>
      </select>
      <input type="date" v-model="filter.from" class="border rounded p-2" />
      <input type="date" v-model="filter.to" class="border rounded p-2" />
      <button
        @click="apply"
        class="bg-green-500 text-white rounded px-4 py-2"
      >Filter</button>
    </div>
    <table class="min-w-full bg-white bg-opacity-80 backdrop-blur-md rounded-2xl">
      <thead>
        <tr class="bg-gray-200">
          <th class="p-2">Time</th><th class="p-2">Location</th>
          <th class="p-2">Temp</th><th class="p-2">Desc</th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="rec in filtered"
          :key="rec.fetchedAt"
          class="hover:bg-gray-100"
        >
          <td class="p-2">{{ rec.fetchedAt }}</td>
          <td class="p-2">{{ rec.location }}</td>
          <td class="p-2">{{ rec.temp }}°</td>
          <td class="p-2 capitalize">{{ rec.desc }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
export default {
  props: ['records'],
  data() {
    return {
      filter: { location: '', from: '', to: '' },
      locations: ['Delhi','Moscow','Paris','New York','Sydney','Riyadh'],
      filtered: []
    }
  },
  methods: {
    apply() {
      let res = [...this.records];
      if (this.filter.location) {
        res = res.filter(r => r.location === this.filter.location);
      }
      if (this.filter.from && this.filter.to) {
        const from = new Date(this.filter.from);
        const to = new Date(this.filter.to);
        if ((to - from)/86400000 > 30) {
          return alert('Max range 30 days');
        }
        res = res.filter(r => {
          const d = new Date(r.fetchedAt);
          return d >= from && d <= to;
        });
      }
      this.filtered = res;
    }
  },
  watch: {
    records() { this.filtered = this.records; }
  },
  mounted() {
    this.filtered = this.records;
  }
}
</script>
