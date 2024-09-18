<script lang="ts" setup>
import type { QTableProps } from 'quasar'

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
    name: 'actions',
    field: 'actions',
    label: 'Actions',
    align: 'left',
  },
]
const requests = useRequestTable(
  params => api.get('/user/requests', { params }).then(r => r.data),
  { search: '' },
)

function onView(id: string) {
  router.push(`/user/requests/${id}`)
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
    <QInput v-model="requests.request.search" label="Search">
      <template #prepend>
        <QIcon>
          <div class="i-hugeicons:search-02" />
        </QIcon>
      </template>
    </QInput>
    <QTable
      v-model:pagination="requests.pagination"
      flat
      :rows="requests.response.rows"
      :columns="columns"
      :loading="requests.loading"
      :filter="requests.request"
      @request="(r) => requests.onRequest(r)"
    >
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
