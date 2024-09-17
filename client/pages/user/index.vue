<script lang="ts" setup>
const { user } = useUser()
const request = useRequest(() => api.get('/user/requests/recent').then(r => r.data))
onMounted(() => request.submit())
</script>

<template>
  <div class="mx-auto mt-xl container">
    <QCard flat bordered>
      <QCardSection>
        <div class="flex justify-between gap-sm">
          <div>
            <p class="text-xl">
              Welcome back
              <span class="font-bold">
                {{ user?.firstName }} </span>,
            </p>
            <p class="text-gray-6">
              You can manage documents requested here
            </p>
          </div>
          <div class="flex flex-col gap-sm">
            <QBtn color="primary" to="/user/requests">
              View Requests <div class="i-hugeicons:arrow-right-02 text-2xl" />
            </QBtn>
          </div>
        </div>
      </QCardSection>
    </QCard>

    <QCard flat bordered class="mt-xl">
      <QCardSection>
        <p class="text-lg font-bold">
          Recent Requests
        </p>
        <QMarkupTable flat>
          <thead>
            <tr>
              <th class="text-left">
                Reference #
              </th>
              <th class="text-left">
                Name
              </th>
              <th class="text-left">
                Document
              </th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="row in request.response" :key="row.id">
              <td>
                {{ row.referenceNumber }}
              </td>
              <td>
                {{ `${row.lastName} ${row.firstName} ${row.middleName} ${row.extensionName}` }}
              </td>
              <td>
                {{ row.documentTypeDesc }}
              </td>
            </tr>
            <tr v-if="!request.response.length">
              <td colspan="3">
                No requests found
              </td>
            </tr>
          </tbody>
        </QMarkupTable>
      </QCardSection>
    </QCard>
  </div>
</template>
