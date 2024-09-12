<script setup lang="ts">
import type { DocumentType } from '~/enums/document-type'
import type { CopyType } from '~/enums/request-type'
import type { Semester } from '~/enums/semester'
import type { YearLevel } from '~/enums/year-level'
import { documentTypes } from '~/options/document-types'
import { semesters } from '~/options/semesters'
import { yearLevels } from '~/options/year-levels'

const campuses = useRequest(() => api.get('/options/campuses').then(r => r.data))
const courses = useRequest(() => api.get('/options/courses').then(r => r.data))
const form = useForm({
  documentType: null as DocumentType | null,
  copyType: null as CopyType | null,
  lastName: '',
  firstName: '',
  middleName: '' as string | undefined,
  extensionName: '' as string | undefined,
  contactNumber: '',
  birthdate: null,
  address: '',
  purpose: '',
  lastAttendanceStartYear: null as number | null,
  lastAttendanceEndYear: null as number | null,
  lastAttendanceSemester: null as Semester | null,
  lastAttendanceYearLevel: null as YearLevel | null,
  lastAttendanceSection: '',
  lastAttendanceCampusId: null as string | null,
  lastAttendanceCourseId: null as string | null,
  isGraduate: null as boolean | null,
})
function onSubmit() {
  form.submit(async (fields) => {
    await api.post('/requests', fields)
  })
}
onMounted(() => {
  campuses.submit()
  courses.submit()
})
</script>

<template>
  <QCard class="mx-auto mt-xl container">
    <QCardSection>
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
        <QBanner v-if="form.hasErrors()" class="text-negative mt-xl border-1 border-red rounded">
          <template #avatar>
            <QIcon>
              <div class="i-hugeicons:cancel-circle text-3xl" />
            </QIcon>
          </template>
          There's an error please check all the highlighted fields.
        </QBanner>
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
        <div class="mt-xl">
          <p class="font-bold">
            Last Attendance:
          </p>
          <div class="grid gap-sm lg:grid-cols-2">
            <QSelect
              v-model="form.fields.lastAttendanceCampusId"
              :options="campuses.response"
              option-value="id"
              option-label="name"
              emit-value
              map-options
              label="Select Campus"
              :error="form.hasError('lastAttendanceCampusId')"
            />
            <QSelect
              v-model="form.fields.lastAttendanceCourseId"
              :options="courses.response"
              option-value="id"
              option-label="name"
              emit-value
              map-options
              label="Select Course"
              :error="form.hasError('lastAttendanceCourseId')"
            />
            <div class="flex items-center gap-2">
              <p class="mr-sm">
                Academic Year
              </p>
              <QInput
                v-model="form.fields.lastAttendanceStartYear"
                class="max-w-40"
                label="Start Year"
                type="number"
                hide-bottom-space
                :error="form.hasError('lastAttendanceStartYear')"
              />
              <p class="font-bold">
                to
              </p>
              <QInput
                v-model="form.fields.lastAttendanceEndYear"
                class="max-w-40"
                :error="form.hasError('lastAttendanceEndYear')"
                label="End Year"
                hide-bottom-space
                type="number"
              />
            </div>
            <div class="flex items-center gap-2">
              <p>
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
              <p class="mr-5xl">
                Year Level
              </p>
              <QSelect
                v-model="form.fields.lastAttendanceYearLevel"
                label="Select your last year level"
                class="flex-1"
                :error="form.hasError('lastAttendanceYearLevel')"
                :options="yearLevels"
                option-value="value"
                option-label="label"
                map-options
                emit-value
                hide-bottom-space
                type="number"
              />
            </div>
            <div class="flex items-center gap-2">
              <p class="mr-lg">
                Section
              </p>
              <QInput
                v-model="form.fields.lastAttendanceSection"
                hide-bottom-space
                class="flex-1"
                label="Enter your last section"
                :error="form.hasError('lastAttendanceSection')"
              />
            </div>
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
          class="mb-lg"
          type="textarea"
        />
        <QExpansionItem
          label="Personal Information"
          caption="You can edit your Personal Information for this request here." icon="person" :model-value="true"
          header-class="text-primary font-bold bg-blue-1 rounded-xl border-1 border-blue-2"
        >
          <QCard flat>
            <QCardSection>
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
            </QCardSection>
          </QCard>
        </QExpansionItem>

        <div class="grid my-2xl gap-sm">
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
    </QCardSection>
  </QCard>
</template>
