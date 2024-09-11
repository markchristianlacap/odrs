<script setup lang="ts">
import type { DocumentType } from '~/enums/document-type'
import type { CopyType } from '~/enums/request-type'
import type { Semester } from '~/enums/semester'
import type { YearLevel } from '~/enums/year-level'
import { copyTypes } from '~/options/copy-types'
import { documentTypes } from '~/options/document-types'
import { semesters } from '~/options/semesters'
import { yearLevels } from '~/options/year-levels'

const { user } = useUser()
const form = useForm({
  documentType: null as DocumentType | null,
  copyType: null as CopyType | null,
  lastName: '',
  firstName: '',
  middleName: '' as string | undefined,
  extensionName: '' as string | undefined,
  contactNumber: '',
  birthdate: '',
  address: '',
  purpose: '',
  lastAttendanceStartYear: null as number | null,
  lastAttendanceEndYear: null as number | null,
  lastAttendanceSemester: null as Semester | null,
  lastAttendanceYearLevel: null as YearLevel | null,
  lastAttendanceSection: '',
  isGraduate: null as boolean | null,
})
function onSubmit() {
  form.submit(async (fields) => {
    await api.post('/user/documents', fields)
  })
}
onMounted(() => {
  if (!user.value)
    return
  form.fields.lastName = user.value.lastName
  form.fields.firstName = user.value.firstName
  form.fields.middleName = user.value.middleName
  form.fields.extensionName = user.value.extensionName
  form.fields.contactNumber = user.value.contactNumber
  form.fields.birthdate = user.value.birthdate
  form.fields.address = user.value.address
})
</script>

<template>
  <div class="mx-auto mt-xl container">
    <QForm @submit="onSubmit">
      <p class="text-xl font-bold">
        Request Form
      </p>

      <p class="text-primary">
        Please fill in the form below
      </p>
      <p class="text-gray-7">
        <b>Reminder:</b>
        Make sure your provided information is correct before submitting the request.
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
        :color="form.hasError('copyType') ? 'negative' : 'primary'"
        keep-color
      />
      <p class="mt-xl font-bold">
        Select document you want to request:
      </p>
      <QRadio
        v-for="documentType in documentTypes"
        :key="documentType.value"
        v-model="form.fields.documentType"
        :color="form.hasError('documentType') ? 'negative' : 'primary'"
        keep-color
        :val="documentType.value"
        :label="documentType.label"
      />
      <div class="mt-xl flex items-center gap-sm">
        <p class="font-bold">
          Status
        </p>
        <div>
          <QRadio
            v-model="form.fields.isGraduate" :val="true"
            label="Graduated"
            :color="form.hasError('isGraduate') ? 'negative' : 'primary'"
            keep-color
          />
        </div>
        <div>
          <QRadio
            v-model="form.fields.isGraduate"
            :color="form.hasError('isGraduate') ? 'negative' : 'primary'"
            keep-color :val="false"
            label="Not Graduated"
          />
        </div>
      </div>
      <div class="mt-xl">
        <p class="font-bold">
          Last Attendance:
        </p>
        <div class="flex items-center gap-2">
          <p>
            Academic Year:
          </p>
          <QInput
            v-model="form.fields.lastAttendanceStartYear"
            type="number"
            :error="form.hasError('lastAttendanceStartYear')"
          />
          <p class="font-bold">
            to
          </p>
          <QInput
            v-model="form.fields.lastAttendanceEndYear"
            :error="form.hasError('lastAttendanceEndYear')"
            type="number"
          />
          <p class="ml-xl">
            Semester
          </p>
          <QRadio
            v-for="semester in semesters"
            :key="semester.value"
            v-model="form.fields.lastAttendanceSemester"
            :color="form.hasError('lastAttendanceSemester') ? 'negative' : 'primary'"
            keep-color
            :error="form.hasError('lastAttendanceSemester')"
            :val="semester.value"
            :label="semester.label"
          />
        </div>
        <div class="flex items-center gap-2">
          <p>
            Year Level:
          </p>
          <QSelect
            v-model="form.fields.lastAttendanceYearLevel"
            class="w-40"
            :error="form.hasError('lastAttendanceYearLevel')"
            :options="yearLevels"
            option-value="value"
            option-label="label"
            map-options
            emit-value
            type="number"
          />
          <p class="ml-xl">
            Section
          </p>
          <QInput
            v-model="form.fields.lastAttendanceSection"
            type="text"
            :error="form.hasError('lastAttendanceSection')"
          />
        </div>
      </div>
      <div class="mt-xl font-bold">
        Purpose
      </div>
      <QInput
        v-model="form.fields.purpose"
        placeholder="Type your purpose"
        :error="form.hasError('purpose')"
        :error-message="form.getError('purpose')"
        type="textarea"
      />
      <QExpansionItem
        label="Personal Information"
        caption="You can edit your Personal Information for this request here." icon="person"
        header-class="text-primary font-bold bg-blue-1 rounded-xl border-1 border-blue-2"
      >
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
      </QExpansionItem>

      <div class="grid grid-cols-2 my-2xl gap-sm">
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
    </QForm>
  </div>
</template>
