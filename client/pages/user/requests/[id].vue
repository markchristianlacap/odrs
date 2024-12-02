<script setup lang="ts">
import { isAxiosError } from 'axios'
import { RequestStatus } from '~/enums/request-status'
import { requestProcess } from '~/options/request-process'
import { requestStatuses } from '~/options/request-statuses'

const clipboard = useClipboard()
const config = useConfigurations()
const route = useRoute()
const id = computed(() => route.params.id)
const $q = useQuasar()
const request = useRequest(() =>
  api.get(`/user/requests/${id.value}`).then(r => r.data),
)
function getStatusColor(status: RequestStatus) {
  return requestStatuses.find(s => s.value === status)?.color
}
const imagePreview = ref<string | null>(null)
const pictureURL = computed(() => `/api/requests/${id.value}/picture`)
const paymentURL = computed(() => `/api/user/requests/${id.value}/payment`)
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
function forRelease() {
  $q.dialog({
    title: 'For Release Confirmation',
    message: 'This can be undone. Are you sure you want to change the status of this request to for release?',
    prompt: {
      model: '',
      label: 'Enter claim deadline',
      type: 'date',
    },
    ok: {
      label: 'Confirm',
      color: 'positive',
    },
    cancel: {
      label: 'Cancel',
      flat: true,
    },
  }).onOk(async (claimDeadline) => {
    try {
      await api.post(`/user/requests/${id.value}/for-release`, { claimDeadline })
      $q.notify({
        message: 'For release successfully',
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
function release() {
  $q.dialog({
    title: 'Release Confirmation',
    message: 'This can be undone. Are you sure you want to change the status of this request to release?',
    ok: {
      label: 'Confirm',
      color: 'positive',
    },
    cancel: {
      label: 'Cancel',
      flat: true,
    },

  }).onOk(async () => {
    try {
      await api.post(`/user/requests/${id.value}/release-documents`)
      $q.notify({
        message: 'Release successfully',
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
function process() {
  $q.dialog({
    title: 'Process Confirmation',
    message: 'This can be undone. Are you sure you want to process this request?',
    prompt: {
      model: '',
      label: 'Enter Official Receipt Number',
    },
    ok: {
      label: 'Confirm',
      color: 'positive',
    },
    cancel: {
      label: 'Cancel',
      flat: true,
    },
  }).onOk(async (oRNumber) => {
    try {
      await api.post(`/user/requests/${id.value}/process`, { oRNumber })
      $q.notify({
        message: 'Change status successfully',
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
function cancel() {
  $q.dialog({
    title: 'Cancel Confirmation',
    message: 'This can be undone. Are you sure you want to cancel this request?',
    prompt: {
      model: '',
      label: 'Enter reason for cancellation',
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
      await api.post(`/user/requests/${id.value}/cancel`, { remarks })
      $q.notify({
        message: 'Cancelled successfully',
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
function getRequirementURL(requirementId: string) {
  return `/api/requests/${id.value}/requirements/${requirementId}`
}
function copyToClipboard() {
  clipboard.copy(request.response?.referenceNumber)
  $q.notify({
    message: 'Reference Number copied to clipboard',
    type: 'positive',
  })
}
onMounted(() => {
  request.submit()
  config.fetch()
})
</script>

<template>
  <div class="mx-auto mt-xl container">
    <template v-if="request.response ">
      <QStepper v-if="request.response.status !== RequestStatus.Canceled && request.response.status !== RequestStatus.Rejected" v-model="request.response.status" flat>
        <QStep v-for="status in requestProcess" :key="status.value" :title="status.label" :name="status.value" :done="request.response.status >= status.value" :done-color="status.color" />
      </QStepper>

      <QCard flat bordered>
        <QCardSection>
          <div class="flex justify-between gap-sm">
            <div class="mb-xl flex gap-sm text-xl font-bold">
              <p class="text-primary">
                Request #{{ request.response.referenceNumber }}
                <q-btn color="primary" size="sm" flat @click="copyToClipboard">
                  <q-tooltip>
                    Click to copy reference number
                  </q-tooltip>
                  <QIcon>
                    <div class="i-hugeicons:clipboard" />
                  </QIcon>
                  Copy
                </q-btn>
                |
              </p>
              <span
                class="uppercase"
                :class="`text-${getStatusColor(request.response.status)}`"
              >
                {{ request.response.statusDesc }}
              </span>
            </div>
            <div class="space-x-sm">
              <QBtn v-if="request.response.status === RequestStatus.Submitted" color="positive" @click="approve">
                <div class="i-hugeicons:thumbs-up mr-xs text-xl" />
                Approve
              </QBtn>
              <QBtn v-if="request.response.status === RequestStatus.PendingForRelease" color="positive" @click="release">
                <div class="i-hugeicons:new-releases mr-xs text-xl" />
                Release Document
              </QBtn>
              <QBtn v-if="request.response.status === RequestStatus.PaymentSubmitted" color="primary" @click="process">
                <div class="i-hugeicons:thumbs-up mr-xs text-xl" />
                Start Processing
              </QBtn>
              <QBtn v-if="request.response.status === RequestStatus.OnProcess" color="primary" @click="forRelease">
                <div class="i-hugeicons:thumbs-up mr-xs text-xl" />
                Ready for Release
              </QBtn>
              <template v-if="request.response.status !== RequestStatus.Released && request.response.status !== RequestStatus.Canceled && request.response.status !== RequestStatus.Rejected">
                <QBtn v-if="request.response.status <= RequestStatus.Submitted" label="Reject" color="negative" @click="reject" />
                <QBtn v-else label="Cancel" color="negative" @click="cancel" />
              </template>
            </div>
          </div>
          <img :src="pictureURL" alt="" class="mb-xl h-300px cursor-zoom-in" @click="imagePreview = pictureURL">
          <ul class="list-disc pl-xl space-y-2">
            <li>
              {{ request.response.requesterTypeDesc }}
            </li>
            <li>
              <b>
                Who will collect:
              </b>
              <span>
                {{ request.response.collectorTypeDesc }}
              </span>
            </li>
            <li v-if="request.response.representative">
              <b>
                Representative:
              </b>
              <span>
                {{ request.response.representative }}
              </span>
            </li>
            <li>
              <p class="mb-sm font-bold">
                Last Attended
              </p>
              <ol class="list-decimal pl-xl space-y-sm">
                <li> <b>School Year:</b> {{ request.response.lastAttendanceStartYear }} - {{ request.response.lastAttendanceEndYear }}</li>
                <li><b>Semester:</b> {{ request.response.semesterDesc }}</li>
                <li><b>Year Level:</b> {{ request.response.yearLevelDesc || 'N/A' }}</li>
                <li><b>Campus:</b> {{ request.response.campusName }}</li>
                <li><b>Program:</b> {{ request.response.programName }}</li>
                <li><b>Section:</b> {{ request.response.section }}</li>
                <li class="text-red">
                  <b>Claim Deadline:</b> {{ formatDate(request.response.claimDeadline) }}
                </li>
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
          <div class="mt-xl">
            <p class="text-lg font-bold">
              Payment Details
            </p>
            <p>
              <b>Reference Number:</b> {{ request.response.referenceNumber }}
            </p>
            <p>
              <b>Amount:</b> {{ request.response.amount }}
            </p>
            <p>
              <b>GCash Number:</b> {{ config.paymentDetails.accountNumber }}
            </p>
            <p>
              <b>Account Name:</b> {{ config.paymentDetails.accountName }}
            </p>
            <img v-if="request.response.status === RequestStatus.PaymentSubmitted" :src="paymentURL" alt="Request Picture" class="h-300px cursor-zoom-in" @click="imagePreview = paymentURL">
          </div>
          <div v-if="request.response.requirements.length > 0 ">
            <p class="mt-xl text-xl font-bold">
              Requirements
            </p>
            <div class="mt-xl w-full flex flex-wrap items-center justify-between gap-sm">
              <div v-for="requirement in request.response.requirements" :key="requirement.id">
                <p class="uppercase">
                  {{ requirement.typeDesc }}
                </p>
                <img :src="getRequirementURL(requirement.id)" :alt="requirement.typeDesc" class="h-300px cursor-zoom-in" @click="imagePreview = getRequirementURL(requirement.id)">
              </div>
            </div>
          </div>
        </QCardSection>
      </QCard>
      <QCard flat bordered class="mt-xl">
        <QCardSection>
          <p class="text-lg font-bold">
            Requested Documents
          </p>
          <QMarkupTable flat>
            <thead>
              <tr>
                <th class="text-left">
                  Document
                </th>
                <th class="text-left">
                  Purpose
                </th>
                <th class="text-left">
                  Copies
                </th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="document in request.response.documents" :key="document.type" class="text-left">
                <td>
                  {{ document.typeDesc }}
                </td>
                <td>
                  {{ document.purpose }}
                </td>
                <td>
                  {{ document.copies }}
                </td>
              </tr>
            </tbody>
          </QMarkupTable>
        </QCardSection>
      </QCard>

      <QCard flat bordered class="mt-xl">
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
    <QDialog v-if="!!imagePreview" :model-value="!!imagePreview" @update:model-value="imagePreview = null">
      <QCard>
        <QCardSection>
          <img :src="imagePreview" alt="">
        </QCardSection>
      </QCard>
    </QDialog>
  </div>
</template>
