<script setup lang="ts">
const router = useRouter()
const request = useForm({
  referenceNumber: '',
})
function submit() {
  request.submit(async (fields) => {
    await router.push(`/requests/${fields.referenceNumber}`)
  })
}
</script>

<template>
  <div>
    <div
      class="grid mx-auto mt-5xl items-center justify-center gap-sm container md:grid-cols-2"
    >
      <div>
        <img src="~/assets/img/cover.jpg" alt="ODRS Cover">
      </div>
      <div>
        <div class="flex items-center gap-sm">
          <img src="~/assets/img/omsc-logo.png" alt="OMSC Logo" class="w-40">
          <div>
            <p class="block text-3xl font-bold">
              Welcome to
            </p>
            <p class="block text-2xl text-primary font-bold">
              Occidental Mindoro State College
            </p>
            <p class="block text-3xl font-bold">
              Online Document Request System
            </p>
            <p class="my-sm font-bold">
              Open Hours
            </p>
            <ul>
              <li>Monday 7:00 am - 4:00 pm</li>
              <li>Tuesday - Thursday 8:00 am - 5:00 pm</li>
            </ul>
            <div class="mt-xl flex gap-xl">
              <QBtn label="Get Started" color="primary" to="/requests/form" />
              <QBtn
                label="Contact Us"
                color="primary"
                outline
                to="/contact-us"
              />
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="mx-auto mb-5xl mt-xl container md:mx-0">
      <p class="text-xl font-bold">
        Request Status Tracker
      </p>
      <p class="text-primary">
        Enter your reference number to check the status of your request
      </p>
      <!-- request tracker  -->
      <div class="flex flex-wrap items-center gap-sm">
        <QInput
          v-model="request.fields.referenceNumber"
          label="Enter Reference Number"
          class="flex-1"
          placeholder="Enter reference number here to check the status"
        >
          <template #prepend>
            <div class="i-hugeicons:square-lock-password text-2xl" />
          </template>
        </QInput>
        <QBtn
          color="primary"
          class="w-full md:w-auto"
          :disable="!request.fields.referenceNumber"
          @click="submit"
        >
          <div class="i-hugeicons:search-02 text-xl" />
          Check Status
        </QBtn>
      </div>
    </div>
  </div>
</template>
