<script setup lang="ts">
const isSuccess = ref(false)
const form = useForm({
  email: '',
})
async function onSubmit() {
  await form.submit(async () => {
    await api.post('/forgot-password', form.fields)
    isSuccess.value = true
  })
}
</script>

<template>
  <div class="mx-auto mt-5xl max-w-120 w-full">
    <QCard>
      <QCardSection>
        <form @submit.prevent="onSubmit">
          <p class="text-xl font-bold">
            Forgot Password
          </p>
          <p class="mb-xl text-primary">
            Please enter your email and wait for a link to reset your password
            to be sent.
          </p>
          <QBanner
            v-if="isSuccess"
            class="text-positive border-1 border-green rounded"
          >
            <template #avatar>
              <QIcon>
                <div class="i-hugeicons:checkmark-circle-01 text-3xl" />
              </QIcon>
            </template>
            Email successfully sent. Please check your inbox.
          </QBanner>
          <div v-else>
            <QInput
              v-model="form.fields.email"
              label="Email"
              :error-message="form.getError('email')"
              :error="form.hasError('email')"
              placeholder="Type your email"
              type="email"
              hint="Must be a valid email address"
            >
              <template #prepend>
                <div class="i-hugeicons:mail-01" />
              </template>
            </QInput>
            <div class="mt-xl text-right">
              <QBtn
                color="primary"
                class="w-full"
                :loading="form.loading"
                type="submit"
              >
                Send Link
              </QBtn>
            </div>
          </div>
          <NuxtLink to="/login" class="mt-xl flex text-primary font-bold">
            <div class="i-hugeicons:arrow-left-02 mr-1 text-xl" />
            Back to Login
          </NuxtLink>
        </form>
      </QCardSection>
    </QCard>
  </div>
</template>
