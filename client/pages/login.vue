<script setup lang="ts">
const router = useRouter()
const form = useForm({
  email: '',
  password: '',
  remember: false,
})
async function onSubmit() {
  await form.submit(async () => {
    await api.post('/login', form.fields)
    router.push('/user')
  })
}
</script>

<template>
  <div class="mx-auto mt-5xl max-w-120 w-full">
    <QCard>
      <QCardSection>
        <form @submit.prevent="onSubmit">
          <p class="text-xl font-bold">
            Login
          </p>
          <p class="mb-2xl text-primary">
            Please enter your email and password
          </p>
          <QBanner
            v-if="form.hasError('generalErrors')"
            class="text-negative border-1 border-red rounded"
          >
            <template #avatar>
              <QIcon>
                <div class="i-hugeicons:cancel-circle text-3xl" />
              </QIcon>
            </template>
            {{ form.getError("generalErrors") }}
          </QBanner>
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
          <div class="flex items-center justify-between">
            <QCheckbox v-model="form.fields.remember" label="Remember me" />
            <NuxtLink to="/forgot-password" class="text-primary font-bold">
              Forgot password?
            </NuxtLink>
          </div>
          <div class="mt-xl text-right">
            <QBtn
              color="primary"
              class="w-full"
              type="submit"
              :loading="form.loading"
            >
              Login Now
            </QBtn>
          </div>
        </form>
      </QCardSection>
    </QCard>
  </div>
</template>
