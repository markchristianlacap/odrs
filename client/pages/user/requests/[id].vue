<script setup lang="ts">
const route = useRoute()
const id = computed(() => route.params.id)
const $q = useQuasar()
const request = useRequest(() =>
  api.get(`/user/requests/${id.value}`).then(r => r.data),
)
function approve() {
  $q.dialog({
    title: 'Approve Confirmation',
    message: 'Are you sure you want to approve this request?',
  }).onOk(async () => {
    await api.post(`/user/requests/${id.value}/approve`)
    $q.notify({
      message: 'Approved successfully',
      type: 'success',
    })
  })
}
function reject() {
  $q.dialog({
    title: 'Reject Confirmation',
    message: 'Are you sure you want to reject this request?',
    color: 'negative',
  }).onOk(async () => {
    await api.post(`/user/requests/${id.value}/reject`)
    $q.notify({
      message: 'Reject successfully',
      type: 'success',
    })
  })
}
onMounted(() => request.submit())
</script>

<template>
  <div v-if="request.response" class="mx-auto mt-xl container">
    <div class="flex items-center justify-between">
      <div>
        <div class="flex justify-between gap-sm">
          <p class="mb-xl text-xl font-bold">
            Request #{{ request.response.referenceNumber }}
          </p>
          <div class="flex gap-sm">
            <QBtn label="Approve" @click="approve" />
            <QBtn label="Reject" color="negative" @click="reject" />
          </div>
        </div>
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
