<script setup lang="ts">
import { isAxiosError } from 'axios'

const route = useRoute()
const referenceNumber = route.params.referenceNumber as string
const status = ref<200 | 404 >(200)
const request = useRequest(() =>
  api.get(`/requests/${referenceNumber}`).then(r => r.data),
)

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
    <p class="mt-xl text-xl font-bold">
      Check Request Status
    </p>
    <p class="text-primary">
      <b>Reference Number:</b>
      {{ request.response.referenceNumber }}
    </p>
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
            Payment
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
            {{ request.response.payment }}
          </td>
          <td>
            {{ request.response.status }}
          </td>
        </tr>
      </tbody>
    </QMarkupTable>
  </div>
</template>
