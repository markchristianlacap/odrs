<script lang="ts" setup>
import type { QTableProps } from 'quasar'
import type { RequestStatus } from '~/enums/request-status'
import { requestStatuses } from '~/options/request-statuses'

const router = useRouter()
const columns: QTableProps['columns'] = [
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
    align: 'center',
    sortable: true,
  },

  {
    name: 'actions',
    field: 'actions',
    label: 'Actions',
    align: 'left',
  },
]
const requests = useRequestTable(
  params => api.get('/user/requests', { params }).then(r => r.data),
  { search: '', status: null as RequestStatus | null },
)

function onView(id: string) {
  router.push(`/user/requests/${id}`)
}

function getStatusColor(status: RequestStatus) {
  return requestStatuses.find(s => s.value === status)?.color
}
onMounted(() => requests.submit())
</script>

<template>
  <div class="mx-auto mt-xl container">
    <div class="flex items-center justify-between">
      <div>
        <p class="text-xl font-bold">
          Requested Documents
        </p>
        <p class="text-primary">
          Manage requested documents here.
        </p>
      </div>
    </div>
    <div>
      <QInput v-model="requests.request.search" label="Search">
        <template #prepend>
          <QIcon>
            <div class="i-hugeicons:search-02" />
          </QIcon>
        </template>
      </QInput>
    </div>
    <div class="mt-sm">
      <QBtnGroup flat>
        <QBtn size="sm" label="All" color="primary" :outline="requests.request.status !== null" @click="requests.request.status = null" />
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

    <QTable
      v-model:pagination="requests.pagination"
      flat
      :rows="requests.response.rows"
      :columns="columns"
      :loading="requests.loading"
      :filter="requests.request"
      @request="(r) => requests.onRequest(r)"
    >
      <template #body-cell-status="props">
        <QTd :props="props">
          <QBadge :color="getStatusColor(props.row.status)">
            {{ props.value }}
          </QBadge>
        </QTd>
      </template>
      <template #body-cell-actions="props">
        <QTd :props="props">
          <QBtn size="sm" color="primary" outline @click="onView(props.row.id)">
            View
          </QBtn>
        </QTd>
      </template>
    </QTable>
  </div>
</template>
