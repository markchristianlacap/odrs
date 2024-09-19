<script setup lang="ts">
const isSuccess = ref(false)
const form = useForm({
  currentPassword: '',
  newPassword: '',
  confirmPassword: '',
})
function onSubmit() {
  form.submit(async (fields) => {
    await api.post('/user/change-password', fields)
    isSuccess.value = true
    form.reset()
  })
}
</script>

<template>
  <div class="mx-auto mt-xl max-w-xl">
    <QCard flat bordered>
      <QCardSection>
        <p class="text-lg font-bold">
          Change Password
        </p>
        <p class="mb-sm text-primary">
          Please enter your password and new password.
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
          Password successfully updated.
        </QBanner>
        <QForm @submit="onSubmit">
          <QInput
            v-model="form.fields.currentPassword"
            label="Current Password"
            :error-message="form.getError('currentPassword')"
            :error="form.hasError('currentPassword')"
            placeholder="Type your current password"
            type="password"
          >
            <template #prepend>
              <i class="i-hugeicons:circle-lock-01" />
            </template>
          </QInput>
          <QInput
            v-model="form.fields.newPassword"
            label="New Password"
            :error-message="form.getError('newPassword')"
            :error="form.hasError('newPassword')"
            placeholder="Type your new password"
            type="password"
          >
            <template #prepend>
              <i class="i-hugeicons:circle-lock-01" />
            </template>
          </QInput>
          <QInput
            v-model="form.fields.confirmPassword"
            label="Confirm Password"
            :error-message="form.getError('confirmPassword')"
            :error="form.hasError('confirmPassword')"
            placeholder="Type your confirm password"
            type="password"
          >
            <template #prepend>
              <i class="i-hugeicons:circle-lock-01" />
            </template>
          </QInput>

          <QBtn
            type="submit"
            color="primary"
            :loading="form.loading"
            class="mt-xl w-full"
          >
            Update
          </QBtn>
        </QForm>
      </QCardSection>
    </QCard>
  </div>
</template>
