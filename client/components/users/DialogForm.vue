<script setup lang="ts">
import type { Role } from '~/enums/role'
import { roles } from '~/options/roles'

const props = defineProps<{
  dialog: boolean
  form: typeof form.fields
  currentId?: string | null
}>()
const emits = defineEmits<{
  (e: 'update:dialog', value: boolean): void
  (e: 'success'): void
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
  role: null as Role | null,
})
watch(() => props.form, (value) => {
  form.fields = value
})
function onSubmit() {
  form.submit(async (fields) => {
    if (props.currentId) {
      await api.put(`/user/users/${props.currentId}`, fields)
    }
    else {
      await api.post('/user/users', fields)
    }
    $q.notify({
      message: 'User account successfully saved.',
      color: 'positive',
    })
    emits('success')
    dialog.value = false
  })
}
watch(dialog, (value) => {
  if (!value)
    form.reset()
})
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
            type="date"
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
            v-if="!currentId"
            v-model="form.fields.password"
            label="Password"
            :error-message="form.getError('password')"
            :error="form.hasError('password')"
            placeholder="Type your password"
            type="password"
          />
          <QInput
            v-if="!currentId"
            v-model="form.fields.confirmPassword"
            label="Confirm Password"
            :error-message="form.getError('confirmPassword')"
            :error="form.hasError('confirmPassword')"
            placeholder="Type your confirm password"
            type="password"
          />
          <q-select
            v-model="form.fields.role"
            :options="roles"
            emit-value
            map-options
            option-value="value"
            option-label="label"
            label="Select Role"
            :error="form.hasError('role')"
            :error-message="form.getError('role')"
            class="mb-sm"
            placeholder="Select role you want to assign"
          />

          <div class="grid grid-cols-2 gap-sm">
            <QBtn
              color="negative"
              class="w-full"
              @click="dialog = false"
            >
              Cancel
            </QBtn>
            <QBtn
              type="submit"
              class="w-full"
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
