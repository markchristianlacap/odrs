<script setup lang="ts">
import { isAxiosError } from 'axios'
import { RequestStatus } from '~/enums/request-status'

const payment = useForm({
  payment: null as File | null,
})
const route = useRoute()
const referenceNumber = route.params.referenceNumber as string
const status = ref<200 | 404 >(200)
const request = useRequest(() =>
  api.get(`/requests/${referenceNumber}`).then(r => r.data),
)
function uploadPayment() {
  payment.submit(async (fields) => {
    const formData = new FormData()
    formData.append('payment', fields.payment as File)
    await api.post(`/requests/${request.response?.id}/payment`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    })
    request.submit()
  })
}
const pictureURL = computed(() => `/api/requests/${request.response?.id}/picture`)
onMounted(async () => {
  try {
    await request.submit()
  }
  catch (e) {
    if (isAxiosError(e)) {
      if (e.response?.status === 404) {
        status.value = 404
      }
    }
  }
})
</script>

<template>
  <div v-if="status === 404" class="mx-auto container">
    <p class="mt-xl text-xl font-bold">
      Request Not Found | <span class="text-red">404</span>
    </p>
    <p class="text-gray-7">
      Request with reference number <b> "{{ referenceNumber }} "</b> not found.
    </p>
    <NuxtLink to="/" class="mt-xl flex text-primary font-bold">
      <div class="i-hugeicons:arrow-left-02 mr-1 text-xl" />
      Back to Home
    </NuxtLink>
  </div>
  <div v-else-if="request.response && status === 200" class="mx-auto container">
    <div class="flex justify-between">
      <div>
        <p class="mt-xl text-xl font-bold">
          Check Request Status |
          <span
            :class="{
              'text-negative': request.response.status === RequestStatus.Rejected,
              'text-primary': request.response.status !== RequestStatus.Rejected,
            }"
          > {{ request.response.statusDesc }} </span>
        </p>
        <p class="text-primary">
          <b>Reference Number:</b>
          {{ request.response.referenceNumber }}
        </p>
        <div v-if="request.response.status === RequestStatus.PendingForPickup" class="mt-xl">
          <p class="text-lg ">
            Your request is ready for pickup. Please bring the needed documents.
          </p>
          <p>
            <b>Requirements:</b>
          </p>
          <p>
            For representatives of owners of documents
          </p>
          <ul>
            <li>
              a. For immediate family (parents, siblings, spouse, children)
            </li>
            <li>
              ✓ Authorization letter (1 copy)
            </li>
            <li>
              ✓Photocopy of valid ID of the owner of document/s (1 copy)
            </li>
            <li>
              ✓Photocopy of valid ID of the representative (1 copy)
            </li>
            <li>
              b. For representatives other than family (friends, neighbors, etc.)
            </li>
            <li>
              ✓ Special Power of Attorney (SPA) (1 copy)
            </li>
            <li>
              ✓ Photocopy of valid ID of the owner of document/s (1 copy)
            </li>
            <li>
              ✓ Photocopy of valid ID of the representative (1 copy)
            </li>
          </ul>
          <p><b>Note:</b> For second copy of documents such as Second copy of diploma, etc. Please bring a </p>
        </div>
        <div v-else-if="request.response.status === RequestStatus.WaitingForPayment" class="mt-xl">
          <p>
            <b>Payment:</b>
            Pay via gcash if the status is ready to pickup.
          </p>
          <p class="text-lg font-bold">
            Please pay the following amount to start the process:
          </p>
          <ul class="list-disc pl-xl">
            <li>
              <b>Reference Number:</b> {{ request.response.referenceNumber }}
            </li>
            <li>
              <b>Amount:</b> {{ request.response.amount }}
            </li>
            <li>
              <p>GCash Number: 093-12345678</p>
              <p>Account Name: OMSC Cashier</p>
            </li>
          </ul>
          <div class="max-w-120 w-full">
            <QFile v-model="payment.fields.payment" label="Attach your receipt here">
              <template #prepend>
                <div class="i-hugeicons:document-attachment" />
              </template>
            </QFile>
            <QBtn color="primary" class="mt-xl w-full" @click="uploadPayment">
              Upload Payment
            </QBtn>
          </div>
        </div>
        <div v-else-if="request.response.status === RequestStatus.Submitted" class="mt-xl">
          <p class="text-lg font-bold">
            We have received your request. Please wait for the admin to check your request. Before you proceed to payment.
          </p>
        </div>
        <div v-else class="mt-xl">
          <p class="text-lg font-bold">
            Please wait for the admin to check your request.
          </p>
        </div>
      </div>
      <img :src="pictureURL" alt="Request Picture" class="h-300px">
    </div>
    <QMarkupTable flat class="mt-xl">
      <thead>
        <tr>
          <th class="text-left">
            Date
          </th>
          <th class="text-left">
            Reference Number
          </th>
          <th class="text-left">
            Name
          </th>
          <th class="text-left">
            Details
          </th>
          <th class="text-left">
            Status
          </th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td>
            {{ formatDate(request.response.createdAt) }}
          </td>
          <td>
            {{ request.response.referenceNumber }}
          </td>
          <td>
            {{ formatName(request.response) }}
          </td>
          <td>
            {{ request.response.documentTypeDesc }}
          </td>
          <td>
            {{ request.response.statusDesc }}
          </td>
        </tr>
      </tbody>
    </QMarkupTable>

    <!-- <p> -->
    <!--   <b>Note:</b> Print the receipt if the status is ready to pickup. -->
    <!-- </p> -->

    <p class="mt-xl text-lg font-bold">
      Request History
    </p>
    <QMarkupTable class="mt-xl">
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
        </tr>
      </tbody>
    </QMarkupTable>
  </div>
</template>
