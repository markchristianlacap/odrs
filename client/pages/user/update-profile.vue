<script setup lang="ts">
const { user, fetchUser } = useUser()
const isSuccess = ref(false)
const form = useForm({
  firstName: user.value?.firstName,
  lastName: user.value?.lastName,
  middleName: user.value?.middleName,
  extensionName: user.value?.extensionName,
  email: user.value?.email,
  contactNumber: user.value?.contactNumber,
  birthdate: user.value?.birthdate,
  address: user.value?.address,
})
function onSubmit() {
  form.submit(async (fields) => {
    await api.post('/user/update-profile', fields)
    isSuccess.value = true
    await fetchUser()
  })
}
</script>

<template>
  <div class="mx-auto mt-xl max-w-xl">
    <QCard flat bordered>
      <QCardSection>
        <p class="text-lg font-bold">
          Update Profile
        </p>
        <p class="mb-sm text-primary">
          Please enter your personal information.
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
          Profile successfully updated.
        </QBanner>
        <QForm @submit="onSubmit">
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
          <div class="grid grid-cols-2 gap-2">
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
              type="date"
              :error-message="form.getError('birthdate')"
              :error="form.hasError('birthdate')"
            >
              <template #prepend>
                <div class="i-hugeicons:calendar-01" />
              </template>
            </QInput>
          </div>
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
