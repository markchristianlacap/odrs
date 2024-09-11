<script setup lang="ts">
const isSuccess = ref(false)
const form = useForm({
  firstName: '',
  lastName: '',
  middleName: '',
  extensionName: '',
  email: '',
  contactNumber: '',
  birthdate: null,
  address: '',
  password: '',
  confirmPassword: '',
  agree: false,
})
async function onSubmit() {
  await form.submit(async () => {
    await api.post('/register', form.fields)
    isSuccess.value = true
  })
}
</script>

<template>
  <div class="mx-auto my-5xl max-w-120 w-full">
    <QCard>
      <QCardSection>
        <QForm @submit="onSubmit">
          <div v-if="isSuccess">
            <QBanner class="text-positive mt-xl border-1 border-green">
              <template #avatar>
                <QIcon>
                  <div class="i-hugeicons:checkmark-circle-01 text-3xl" />
                </QIcon>
              </template>
              Registration Successful. You can now login using your credentials provided.
            </QBanner>
          </div>
          <template v-else>
            <p class="text-xl font-bold">
              Registration
            </p>
            <p class="text-primary">
              Please enter your personal information and password for registration.
            </p>
            <QInput
              v-model="form.fields.lastName"
              label="Last Name"
              :error-message="form.getError('lastName')"
              :error="form.hasError('lastName')"
              placeholder="Type your Last Name"
            >
              <template #prepend>
                <i class="i-hugeicons:user-circle" />
              </template>
            </QInput>
            <QInput
              v-model="form.fields.firstName"
              label="First Name"
              :error-message="form.getError('firstName')"
              :error="form.hasError('firstName')"
              placeholder="Type your First Name"
            >
              <template #prepend>
                <i class="i-hugeicons:user-circle" />
              </template>
            </QInput>
            <div class="grid grid-cols-2 gap-2">
              <QInput
                v-model="form.fields.middleName"
                label="Middle Name"
                :error-message="form.getError('middleName')"
                :error="form.hasError('middleName')"
                placeholder="Type your Last Name"
              >
                <template #prepend>
                  <i class="i-hugeicons:user-circle" />
                </template>
              </QInput>
              <QInput
                v-model="form.fields.extensionName"
                label="Extension Name"
                :error-message="form.getError('extensionName')"
                :error="form.hasError('extensionName')"
                placeholder="Type your Extension"
              />
            </div>
            <QInput
              v-model="form.fields.contactNumber"
              label="Contact Number"
              :error-message="form.getError('contactNumber')"
              :error="form.hasError('contactNumber')"
              placeholder="Type your contact number"
            >
              <template #prepend>
                <div class="i-hugeicons:call" />
              </template>
            </QInput>
            <QInput
              v-model="form.fields.birthdate"
              label="Birthdate"
              :error-message="form.getError('birthdate')"
              :error="form.hasError('birthdate')"
              placeholder="Type your birthdate"
              type="date"
            >
              <template #prepend>
                <div class="i-hugeicons:calendar-01" />
              </template>
            </QInput>
            <QInput
              v-model="form.fields.address"
              label="Address"
              :error-message="form.getError('address')"
              :error="form.hasError('address')"
              placeholder="Type your address"
              type="textarea"
            >
              <template #prepend>
                <div class="i-hugeicons:location-01" />
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
                type="submit"
                :disable="!form.fields.agree"
              >
                Register Now
              </QBtn>
            </div>
          </template>
          <p class="mb-sm mt-2xl text-center">
            Already have an account?
            <NuxtLink to="/login" class="text-primary font-bold">
              Login now
            </NuxtLink>
          </p>
        </QForm>
      </QCardSection>
    </QCard>
  </div>
</template>
