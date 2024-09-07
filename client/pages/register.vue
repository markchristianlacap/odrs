<script setup lang="ts">
definePageMeta({
  layout: 'home',
})
const form = useForm({
  name: '',
  email: '',
  password: '',
  confirmPassword: '',
  agree: false,
})
</script>

<template>
  <div class="mx-auto mt-5xl max-w-120 w-full">
    <QCard>
      <QCardSection>
        <p class="text-xl font-bold">
          Registration
        </p>
        <p class="mb-2xl text-primary">
          Please enter your personal information and password for registration.
        </p>
        <QInput
          v-model="form.fields.name"
          label="Name"
          :error-message="form.getError('name')"
          :error="form.hasError('name')"
          placeholder="Type your name"
        >
          <template #prepend>
            <div class="i-hugeicons:user-circle" />
          </template>
        </QInput>
        <QInput
          v-model="form.fields.email"
          label="Email"
          :error-message="form.getError('email')"
          :error="form.hasError('email')"
          placeholder="Type your email"
          type="email"
        >
          <template #prepend>
            <div class="i-hugeicons:mail-01" />
          </template>
        </QInput>
        <QInput
          v-model="form.fields.password"
          label="Password"
          :error-message="form.getError('password')"
          :error="form.hasError('password')"
          placeholder="Type your password"
          type="password"
        >
          <template #prepend>
            <div class="i-hugeicons:square-lock-01" />
          </template>
        </QInput>
        <QInput
          v-model="form.fields.confirmPassword"
          label="Confirm Password"
          :error-message="form.getError('confirmPassword')"
          :error="form.hasError('confirmPassword')"
          placeholder="Type your password"
          type="password"
        >
          <template #prepend>
            <div class="i-hugeicons:square-lock-01" />
          </template>
        </QInput>
        <div>
          <QCheckbox v-model="form.fields.agree" />
          I accept the <NuxtLink to="/terms" class="text-primary font-bold">
            terms of service
          </NuxtLink> and <NuxtLink to="/privacy" class="text-primary font-bold">
            privacy policy
          </NuxtLink>
        </div>
        <div class="mt-xl text-right">
          <QBtn
            color="primary"
            class="w-full"
            :loading="form.loading"
          >
            Register Now
          </QBtn>
        </div>
        <p class="mb-sm mt-2xl text-center">
          Already have an account?
          <NuxtLink to="/login" class="text-primary font-bold">
            Login now
          </NuxtLink>
        </p>
      </QCardSection>
    </QCard>
  </div>
</template>
