<script lang="ts" setup>
import type { QTableProps } from 'quasar'
import type { RequestStatus } from '~/enums/request-status'
import { requestStatuses } from '~/options/request-statuses'

const columns: QTableProps['columns'] = [
  {
    name: 'createdAt',
    field: 'createdAt',
    label: 'Date Requested',
    align: 'left',
    sortable: true,
    format: val => formatDate(val),
  },
  {
    name: 'referenceNumber',
    field: 'referenceNumber',
    label: 'Reference Number',
    align: 'left',
    sortable: true,
  },
  {
    name: 'studentNumber',
    field: 'studentNumber',
    label: 'Student Number',
    align: 'left',
    sortable: true,
  },
  {
    name: 'lastName',
    field: 'lastName',
    label: 'Name',
    align: 'left',
    format: (val, row) => {
      const mi = row.middleName ? ` ${row.middleName.charAt(0)}.` : ''
      return `${val}${mi} ${row.firstName} ${row.extensionName}`
    },
    sortable: true,
  },
  {
    name: 'email',
    field: 'email',
    label: 'Email',
    align: 'left',
    sortable: true,
  },
  {
    name: 'campusName',
    field: 'campusName',
    label: 'Campus',
    align: 'left',
    sortable: true,
  },
  {
    name: 'programName',
    field: 'programName',
    label: 'Course',
    align: 'left',
    sortable: true,
  },
  {
    name: 'status',
    field: 'statusDesc',
    label: 'Status',
    align: 'left',
    sortable: true,
  },
  {
    name: 'oRNumber',
    field: 'orNumber',
    label: 'OR Number',
    align: 'left',
    sortable: true,
  },
  {
    name: 'dateReleased',
    field: 'dateOfRelease',
    label: 'Date Released',
    align: 'left',
    sortable: true,
    format: val => formatDate(val),
  },
]
const today = new Date().toISOString().split('T')[0]
const requests = useRequest(
  params => api.get('/user/reports', { params }).then(r => r.data),
  { dateFrom: today, dateTo: today, status: null as RequestStatus | null },
)
function getStatusColor(status: RequestStatus) {
  return requestStatuses.find(s => s.value === status)?.color
}
function print() {
  let url = '/user/reports/print'
  url += `?dateFrom=${requests.request.dateFrom}`
  url += `&dateTo=${requests.request.dateTo}`
  url += `&status=${requests.request.status || ''}`
  window.open(url, '_blank')
}
function exportReport() {
  window.open('/api/user/reports/export', '_blank')
}
onMounted(() => requests.submit())
watchDeep(() => requests.request, () => requests.submit())
</script>

<template>
  <div class="mx-auto mt-xl container">
    <div class="flex items-center justify-between">
      <div>
        <p class="text-xl font-bold">
          Requested Reports
        </p>
        <p class="text-primary">
          View and manage your requested reports
        </p>
      </div>
    </div>
    <div>
      <div class="mb-sm flex items-center gap-sm">
        <QInput v-model="requests.request.dateFrom" label="Starting Date" type="date">
          <template #prepend>
            <QIcon>
              <div class="i-hugeicons:calendar-02" />
            </QIcon>
          </template>
        </QInput>
        <QInput v-model="requests.request.dateTo" label="Ending Date" type="date">
          <template #prepend>
            <QIcon>
              <div class="i-hugeicons:calendar-02" />
            </QIcon>
          </template>
        </QInput>
        <QBtn color="primary" @click="print">
          <QIcon left>
            <div class="i-hugeicons:printer" />
          </QIcon>
          Print
        </QBtn>
        <QBtn color="primary" @click="exportReport">
          <QIcon left>
            <div class="i-hugeicons:file-export" />
          </QIcon>
          Export
        </QBtn>
      </div>
      <div>
        <QBtnGroup flat>
          <QBtn
            size="sm"
            label="All"
            color="primary"
            :outline="requests.request.status !== null"
            @click="requests.request.status = null"
          />
          <QBtn
            v-for="status in requestStatuses"
            :key="status.value"
            size="sm"
            :label="status.label"
            :color="status.color"
            :outline="requests.request.status !== status.value"
            @click="requests.request.status = status.value"
          />
        </QBtnGroup>
      </div>
    </div>
    <QTable
      flat
      :rows="requests.response || []"
      :columns="columns"
      :loading="requests.loading"
    >
      <template #body-cell-status="props">
        <QTd :props="props">
          <QBadge :color="getStatusColor(props.row.status)">
            {{ props.value }}
          </QBadge>
        </QTd>
      </template>
    </QTable>
  </div>
</template>
