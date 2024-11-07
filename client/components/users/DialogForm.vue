<script setup lang="ts">
const props = defineProps<{
  dialog: boolean
  form: typeof form.fields
  id?: string | null
}>()
const emits = defineEmits<{
  (e: 'update:dialog', value: boolean): void
}>()
const $q = useQuasar()
const { dialog } = useVModels(props, emits)
const form = useForm({
  firstName: '',
  lastName: '',
  middleName: null,
  extensionName: null,
  email: '',
  contactNumber: '',
  birthdate: null,
  address: '',
  password: '',
  confirmPassword: '',
})
watchDeep(() => props.form, (value) => {
  form.fields = value
})
function onSubmit() {
  form.submit(async (fields) => {
    if (props.id) {
      await api.put(`/user/users/${props.id}`, fields)
    }
    else {
      await api.post('/user/users', fields)
    }
    $q.notify({
      message: 'User account successfully saved.',
      color: 'positive',
    })
    dialog.value = false
  })
}
</script>

<template>
  <QDialog v-model="dialog">
    <QCard class="max-w-xl w-full">
      <QCardSection>
        <p class="text-lg font-bold">
          User Account Form
        </p>
        <QForm @submit="onSubmit">
          <QInput
            v-model="form.fields.firstName"
            label="First Name"
            :error-message="form.getError('firstName')"
            :error="form.hasError('firstName')"
            placeholder="Type your first name"
          />
          <QInput
            v-model="form.fields.lastName"
            label="Last Name"
            :error-message="form.getError('lastName')"
            :error="form.hasError('lastName')"
            placeholder="Type your last name"
          />
          <QInput
            v-model="form.fields.middleName"
            label="Middle Name"
            :error-message="form.getError('middleName')"
            :error="form.hasError('middleName')"
            placeholder="Type your middle name"
          />
          <QInput
            v-model="form.fields.extensionName"
            label="Extension Name"
            :error-message="form.getError('extensionName')"
            :error="form.hasError('extensionName')"
            placeholder="Type your extension name"
          />
          <QInput
            v-model="form.fields.email"
            label="Email"
            type="email"
            :error-message="form.getError('email')"
            :error="form.hasError('email')"
            placeholder="Type your email"
          />
          <QInput
            v-model="form.fields.contactNumber"
            label="Contact Number"
            :error-message="form.getError('contactNumber')"
            :error="form.hasError('contactNumber')"
            placeholder="Type your contact number"
          />
          <QInput
            v-model="form.fields.birthdate"
            label="Birthdate"
            :error-message="form.getError('birthdate')"
            :error="form.hasError('birthdate')"
            placeholder="Type your birthdate"
          />
          <QInput
            v-model="form.fields.address"
            label="Address"
            :error-message="form.getError('address')"
            :error="form.hasError('address')"
            placeholder="Type your address"
          />
          <QInput
            v-if="!id"
            v-model="form.fields.password"
            label="Password"
            :error-message="form.getError('password')"
            :error="form.hasError('password')"
            placeholder="Type your password"
            type="password"
          />
          <QInput
            v-if="!id"
            v-model="form.fields.confirmPassword"
            label="Confirm Password"
            :error-message="form.getError('confirmPassword')"
            :error="form.hasError('confirmPassword')"
            placeholder="Type your confirm password"
            type="password"
          />

          <div class="flex justify-end gap-sm">
            <QBtn
              color="negative"
              @click="dialog = false"
            >
              Cancel
            </QBtn>
            <QBtn
              type="submit"
              color="primary"
            >
              Save
            </QBtn>
          </div>
        </QForm>
      </QCardSection>
    </QCard>
  </QDialog>
</template>
