<script setup lang="ts">
definePageMeta({
  layout: 'home',
})
const isSuccess = ref(false)
const route = useRoute()
const form = useForm({
  token: route.query.token as string || '',
  password: '',
  confirmPassword: '',
})
async function onSubmit() {
  await form.submit(async () => {
    await api.post('/reset-password', form.fields)
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
            Reset Password
          </p>
          <p class="mb-xl text-primary">
            Please enter your new password and login to your account.
          </p>
          <QBanner v-if="isSuccess" class="text-positive border-1 border-green rounded">
            <template #avatar>
              <QIcon>
                <div class="i-hugeicons:checkmark-circle-01 text-3xl" />
              </QIcon>
            </template>
            Password successfully changed. You can now login.
          </QBanner>
          <div v-else>
            <QInput
              v-model="form.fields.password"
              label="New Password"
              :error-message="form.getError('password')"
              :error="form.hasError('password')"
              placeholder="Type your new password"
              type="password"
            >
              <template #prepend>
                <div class="i-hugeicons:mail-01" />
              </template>
            </QInput>
            <QInput
              v-model="form.fields.confirmPassword"
              label="Confirm Password"
              :error-message="form.getError('confirmPassword')"
              :error="form.hasError('confirmPassword')"
              placeholder="Confirm your new password"
              type="password"
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
                Confirm
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
