<script setup lang="ts">
definePageMeta({
  layout: 'home',
})
const form = useForm({
  email: '',
})
async function onSubmit() {
  await form.submit(async () => {
    await api.post('/forgot-password', form.fields)
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
            Please enter your email and wait for a link to reset your password to be sent.
          </p>
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
          <NuxtLink to="/login" class="mt-xl flex text-primary font-bold">
            <div class="i-hugeicons:arrow-left-02 mr-1 text-xl" />
            Back to Login
          </NuxtLink>
        </form>
      </QCardSection>
    </QCard>
  </div>
</template>
