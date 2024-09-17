<script setup lang="ts">
const route = useRoute()
const id = computed(() => route.params.id)
const request = useRequest(() =>
  api.get(`/user/requests/${id.value}`).then(r => r.data),
)
onMounted(() => request.submit())
</script>

<template>
  <div class="mx-auto mt-xl container">
    <div class="flex items-center justify-between">
      <div>
        <p class="mb-xl text-xl font-bold">
          Request #{{ request.response.referenceNumber }}
        </p>
        <p class="mb-sm text-lg">
          Request Details
        </p>
        <ul class="list-disc pl-xl space-y-2">
          <li>
            <b>
              {{ request.response.requesterTypeDesc }}
            </b>
          </li>
          <li>
            <b> Type of Document: </b>
            {{ request.response.documentTypeDesc }}
          </li>
        </ul>
        <p class="my-sm text-lg">
          Personal Information
        </p>
        <ul class="list-disc pl-xl space-y-2">
          <li><b>Student Number:</b> {{ request.response.studentNumber }}</li>
          <li>
            <b>Name:</b> {{ request.response.lastName }}
            {{ request.response.middleName }} {{ request.response.firstName }}
            {{ request.response.extensionName }}
          </li>
          <li><b>Email:</b> {{ request.response.email }}</li>
          <li><b>Campus:</b> {{ request.response.campusName }}</li>
          <li><b>Program:</b> {{ request.response.programName }}</li>
          <li>
            <b>Status:</b>
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>
