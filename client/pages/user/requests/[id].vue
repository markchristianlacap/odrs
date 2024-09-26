<script setup lang="ts">
import { isAxiosError } from 'axios'
import { RequestStatus } from '~/enums/request-status'

const route = useRoute()
const id = computed(() => route.params.id)
const $q = useQuasar()
const request = useRequest(() =>
  api.get(`/user/requests/${id.value}`).then(r => r.data),
)
function approve() {
  $q.dialog({
    title: 'Approve Confirmation?',
    message: 'Are you sure you want to approve this request? Please enter the amount needed to pay for this request.',
    prompt: {
      model: '',
      type: 'number',
      label: 'Enter amount',
    },
    ok: {
      label: 'Approve',
      color: 'positive',
    },
    cancel: {
      label: 'Cancel',
      flat: true,
    },
  }).onOk(async (amount) => {
    try {
      await api.post(`/user/requests/${id.value}/approve`, { amount })
      $q.notify({
        message: 'Approved successfully',
        type: 'positive',
      })

      request.submit()
    }
    catch (e: any) {
      if (isAxiosError(e)) {
        $q.notify({
          type: 'negative',
          message: e.response?.data?.errors?.amount?.[0] || 'Something went wrong',
        })
      }
    }
  })
}
function reject() {
  $q.dialog({
    title: 'Reject Confirmation',
    message: 'This can be undone. Are you sure you want to reject this request?',
    prompt: {
      model: '',
      label: 'Enter reason for rejection',
    },
    ok: {
      label: 'Confirm',
      color: 'negative',
    },
    cancel: {
      label: 'Cancel',
      flat: true,
    },
  }).onOk(async (remarks) => {
    try {
      await api.post(`/user/requests/${id.value}/reject`, { remarks })
      $q.notify({
        message: 'Reject successfully',
        type: 'positive',
      })
      request.submit()
    }
    catch (e: any) {
      if (isAxiosError(e)) {
        $q.notify({
          type: 'negative',
          message: e.response?.data?.errors?.remarks?.[0] || 'Something went wrong',
        })
      }
    }
  })
}
onMounted(() => request.submit())
</script>

<template>
  <div class="mx-auto mt-xl container">
    <template v-if="request.response">
      <QCard>
        <QCardSection>
          <div class="flex justify-between gap-sm">
            <p class="mb-xl text-xl font-bold">
              Request #{{ request.response.referenceNumber }} | {{ request.response.statusDesc }}
            </p>
            <div v-if="request.response.status === RequestStatus.Submitted" class="space-x-sm">
              <QBtn color="positive" @click="approve">
                <div class="i-hugeicons:thumbs-up mr-xs text-xl" />
                Approve
              </QBtn>
              <QBtn label="Reject" color="negative" @click="reject" />
            </div>
          </div>
          <ul class="list-disc pl-xl space-y-2">
            <li>
              {{ request.response.requesterTypeDesc }}
            </li>
            <li>
              <b> Type of Document: </b>
              <span class="text-lg text-primary font-bold">
                {{ request.response.documentTypeDesc }}
              </span>
            </li>
            <li>
              <p class="mb-sm font-bold">
                Last Attended
              </p>
              <ol class="list-decimal pl-xl space-y-sm">
                <li> <b>School Year:</b> {{ request.response.lastAttendanceStartYear }} - {{ request.response.lastAttendanceEndYear }}</li>
                <li><b>Semester:</b> {{ request.response.semesterDesc }}</li>
                <li><b>Year Level:</b> {{ request.response.yearLevelDesc }}</li>
                <li><b>Campus:</b> {{ request.response.campusName }}</li>
                <li><b>Program:</b> {{ request.response.programName }}</li>
                <li><b>Section:</b> {{ request.response.section }}</li>
              </ol>
            </li>
          </ul>
          <p class="my-sm text-lg">
            Personal Information
          </p>
          <ul class="list-disc pl-xl space-y-sm">
            <li><b>Student Number:</b> {{ request.response.studentNumber }}</li>
            <li>
              <b>Name:</b> {{ request.response.lastName }}
              {{ request.response.middleName }} {{ request.response.firstName }}
              {{ request.response.extensionName }}
            </li>
            <li><b>Birthdate:</b> {{ formatDate(request.response.birthdate) }}</li>
            <li><b>Email:</b> {{ request.response.email }}</li>
            <li><b>Contact Number:</b> {{ request.response.contactNumber }}</li>
            <li><b>Address:</b> {{ request.response.address }}</li>
          </ul>
        </QCardSection>
      </QCard>
      <QCard class="mt-xl">
        <QCardSection>
          <p class="text-lg font-bold">
            Request History
          </p>
          <QMarkupTable flat>
            <thead>
              <tr>
                <th class="text-left">
                  Date
                </th>
                <th class="text-left">
                  Status
                </th>
                <th class="text-left">
                  Remarks
                </th>
                <th class="text-left">
                  Action Taken By
                </th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="history in request.response.histories" :key="history.id" class="text-left">
                <td>
                  {{ formatDate(history.createdAt) }}
                </td>
                <td>
                  {{ history.requestStatusDesc }}
                </td>
                <td>
                  {{ history.remarks }}
                </td>
                <td>
                  {{ history.createdBy }}
                </td>
              </tr>
            </tbody>
          </QMarkupTable>
        </QCardSection>
      </QCard>
    </template>
  </div>
</template>
