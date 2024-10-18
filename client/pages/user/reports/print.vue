<script lang="ts" setup>
definePageMeta({ layout: 'empty' })
const today = new Date().toISOString().split('T')[0]
const route = useRoute()
const requests = useRequest(
  params => api.get('/user/reports', { params }).then(r => r.data),
  { dateFrom: route.query.dateFrom || today, dateTo: route.query.dateTo || today, status: route.query.status },
)
function formatName(row: any) {
  return `${row.firstName} ${row.middleName} ${row.lastName}`
}
onMounted(async () => {
  await requests.submit()
  window.print()
})
watchDeep(() => requests.request, () => requests.submit())
</script>

<template>
  <QMarkupTable
    flat
    :loading="requests.loading"
  >
    <thead>
      <tr>
        <th class="text-left">
          Reference Number
        </th>
        <th class="text-left">
          Name
        </th>
        <th class="text-left">
          Type of Documents
        </th>
        <th class="text-left">
          Campus
        </th>
        <th class="text-left">
          Course
        </th>
        <th class="text-left">
          Purpose
        </th>
        <th class="text-left">
          Price
        </th>
        <th class="text-left">
          OR Number
        </th>
        <th class="text-left">
          Date of Release
        </th>
        <th>Action Taken By</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="row in requests.response" :key="row.referenceNumber">
        <td>{{ row.referenceNumber }}</td>
        <td>{{ formatName(row) }}</td>
        <td>{{ row.documentTypesDesc }}</td>
        <td>{{ row.campusName }}</td>
        <td>{{ row.programName }}</td>
        <td>{{ row.purpose }}</td>
        <td>{{ row.amount }}</td>
        <td>{{ row.orNumber }}</td>
        <td>{{ row.dateOfRelease }}</td>
        <td>{{ row.actionTakenBy }}</td>
      </tr>
    </tbody>
  </QMarkupTable>
</template>

<style lang="css" scoped>
@page {
  size: 25cm 35.7cm;
  margin: 5mm 5mm 5mm 5mm; /* change the margins as you want them to be. */
}
</style>
