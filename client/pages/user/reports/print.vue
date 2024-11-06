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
const dateTime = new Date()
onMounted(async () => {
  await requests.submit()
  window.print()
})
watchDeep(() => requests.request, () => requests.submit())
</script>

<template>
  <div>
    <div>
      <p class="mb-xl text-2xl text-primary font-bold">
        Online Document Request System Report
      </p>
    </div>
    <table>
      <thead>
        <tr class="*:border *:px-1 *:py-2 *:text-nowrap">
          <th class="text-left">
            Reference Number
          </th>
          <th class="text-left">
            Document
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
            Status
          </th>
          <th class="text-left">
            Date of Release
          </th>
          <th>Action Taken By</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="row in requests.response" :key="row.referenceNumber" class="*:border *:px-1 *:py-2 *:text-nowrap">
          <td>{{ row.referenceNumber }}</td>
          <td>{{ row.document }}</td>
          <td>{{ formatName(row) }}</td>
          <td>{{ row.documentTypesDesc }}</td>
          <td>{{ row.campusName }}</td>
          <td>{{ row.programName }}</td>
          <td>{{ row.purpose }}</td>
          <td>{{ row.amount }}</td>
          <td>{{ row.orNumber }}</td>
          <td>{{ row.statusDesc }}</td>
          <td>{{ row.dateOfRelease }}</td>
          <td>{{ row.actionTakenBy }}</td>
        </tr>
      </tbody>
    </table>
    <p class="mt-xl">
      Printed on {{ dateTime }}
    </p>
  </div>
</template>

  <style>
    /* Force landscape orientation for printing */
@media print {
  @page {
    size: A4 landscape; /* A4 paper in landscape mode */
    margin: 10mm; /* Adjust margins if needed */
  }

  body {
    font-family: Arial, sans-serif;
    padding: 20px;
    background-color: white;
    font-size: 12px;
  }

  .no-print {
    display: none; /* Hide elements you don't want to print */
  }
}
</style>
