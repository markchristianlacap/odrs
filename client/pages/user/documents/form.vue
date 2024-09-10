<script setup lang="ts">
import type { DocumentType } from '~/enums/document-type'
import type { CopyType } from '~/enums/request-type'
import { copyTypes } from '~/options/copy-types'
import { documentTypes } from '~/options/document-types'

const form = useForm({
  documentType: null as DocumentType | null,
  copyType: null as CopyType | null,
  lastName: '',
  firstName: '',
  middleName: '',
  extensionName: '',
  contactNumber: '',
  birthdate: '',
  address: '',
})
</script>

<template>
  <div class="mx-auto mt-xl container">
    <p class="text-xl font-bold">
      Request Form
    </p>

    <p class="text-primary">
      Please fill in the form below
    </p>
    <p class="mt-xl font-bold">
      Copy type:
    </p>
    <QRadio
      v-for="copyType in copyTypes"
      :key="copyType.value"
      v-model="form.fields.copyType"
      :val="copyType.value"
      :label="copyType.label"
    />
    <p class="mt-xl font-bold">
      Select document you want to request:
    </p>
    <QRadio
      v-for="documentType in documentTypes"
      :key="documentType.value"
      v-model="form.fields.documentType"
      :val="documentType.value"
      :label="documentType.label"
    />

    <p class="mt-xl font-bold">
      Personal Information
    </p>

    <QInput
      v-model="form.fields.lastName"
      label="Last Name"
      :error-message="form.getError('lastName')"
      :error="form.hasError('lastName')"
    />
    <QInput
      v-model="form.fields.firstName"
      :error-message="form.getError('firstName')"
      :error="form.hasError('firstName')"
      label="First Name"
    />
    <div class="grid grid-cols-2 gap-sm">
      <QInput
        v-model="form.fields.middleName"
        label="Middle Name"
        :error="form.hasError('middleName')"
        :error-message="form.getError('middleName')"
      />
      <QInput
        v-model="form.fields.extensionName"
        label="Extension Name"
        :error="form.hasError('extensionName')"
        :error-message="form.getError('extensionName')"
      />
      <QInput
        v-model="form.fields.contactNumber"
        label="Contact Number"
        :error="form.hasError('contactNumber')"
        :error-message="form.getError('contactNumber')"
      />
      <QInput
        v-model="form.fields.birthdate"
        label="Date of Birth"
        :error="form.hasError('birthdate')"
        :error-message="form.getError('birthdate')"
        type="date"
      />
    </div>
    <QInput
      v-model="form.fields.address"
      type="textarea"
      label="Address"
      :error="form.hasError('address')"
      :error-message="form.getError('address')"
    />
    <p class="my-xl text-gray-7">
      <b>Reminder:</b>
      Make sure your personal information is correct before submitting the request.
    </p>
    <div class="grid grid-cols-2 mt-2xl gap-sm">
      <QBtn label="Back to Dashboard" color="primary" to="/user" outline icon="arrow_back" />
      <QBtn
        color="primary"
        class="w-full"
        :loading="form.loading"
        type="submit"
      >
        Submit
      </QBtn>
    </div>
  </div>
</template>
