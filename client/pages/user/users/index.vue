<script lang="ts" setup>
import type { QTableProps } from 'quasar'

const dialog = ref(false)
const currentId = ref<string | null>(null)
const form = ref({
  firstName: '',
  lastName: '',
  middleName: null,
  extensionName: null,
  email: '',
  contactNumber: '',
  birthdate: null,
  address: '',
  password: '',
  confirmPassword: '',
})
const columns: QTableProps['columns'] = [
  {
    label: 'First Name',
    field: 'firstName',
    name: 'firstName',
    align: 'left',
    sortable: true,
  },
  {
    label: 'Middle Name',
    field: 'middleName',
    name: 'middleName',
    align: 'left',
    sortable: true,
  },
  {
    label: 'Last Name',
    field: 'lastName',
    name: 'lastName',
    align: 'left',
    sortable: true,
  },
  {
    label: 'Extension Name',
    field: 'extensionName',
    name: 'extensionName',
    align: 'left',
    sortable: true,
  },
  {
    label: 'Email',
    field: 'email',
    name: 'email',
    align: 'left',
    sortable: true,
  },
  {
    label: 'Contact Number',
    field: 'contactNumber',
    name: 'contactNumber',
    align: 'left',
    sortable: true,
  },
  {
    label: 'Birthdate',
    field: 'birthdate',
    name: 'birthdate',
    align: 'left',
    sortable: true,
  },
  {
    label: 'Address',
    field: 'address',
    name: 'address',
    align: 'left',
  },
  {
    label: 'Actions',
    field: 'actions',
    name: 'actions',
    align: 'left',
  },
]
const requests = useRequestTable(
  params => api.get('/user/users', { params }).then(r => r.data),
  { search: '' },
)
function onEdit(row: any) {
  form.value = row
  dialog.value = true
  currentId.value = row.id
}
onMounted(() => requests.submit())
</script>

<template>
  <div class="mx-auto mt-xl container">
    <div class="flex items-center justify-between">
      <div>
        <p class="text-xl font-bold">
          User Accounts
        </p>
        <p class="text-primary">
          Manage user accounts here.
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
        <template #append>
          <QBtn label="Add User" color="primary" @click="dialog = true" />
        </template>
      </QInput>
    </div>

    <QTable
      v-model:pagination="requests.pagination"
      flat
      :rows="requests.response.rows"
      :loading="requests.loading"
      :filter="requests.request"
      :columns="columns"
      @request="(r) => requests.onRequest(r)"
    >
      <template #body-cell-actions="props">
        <QTd :props="props">
          <QBtn size="sm" color="primary" outline @click="onEdit(props.row)">
            Edit
          </QBtn>
        </QTd>
      </template>
    </QTable>
    <UsersDialogForm
      v-model:dialog="dialog"
      :form="form"
      :current-id="currentId"
      @success="requests.submit"
    />
  </div>
</template>
