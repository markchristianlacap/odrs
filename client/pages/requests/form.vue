<script setup lang="ts">
import { CollectorType } from '~/enums/collector-type'
import { DocumentType } from '~/enums/document-type'
import type { RequesterType } from '~/enums/requester-type'
import type { Semester } from '~/enums/semester'
import type { YearLevel } from '~/enums/year-level'
import { collectorTypes } from '~/options/collector-types'
import { documentTypes } from '~/options/document-types'
import { requesterTypes } from '~/options/requester-types'
import { semesters } from '~/options/semesters'
import { yearLevels } from '~/options/year-levels'

const $q = useQuasar()
const router = useRouter()
const campuses = useRequest(() =>
  api.get('/options/campuses').then(r => r.data),
)
const programs = useRequest(() =>
  api.get('/options/programs').then(r => r.data),
)
const purposes = [
  'For Employment',
  'For Reference',
  'For Scholarship',
  'For PNP Application',
  'For Napolcom Examination',
  'For Board Examination',
  'For Promotion',
]
const otherPurpose = ref(false)
const form = useForm({
  studentNumber: '',
  email: '',
  documentTypes: [] as DocumentType[],
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
  yearLevel: null as YearLevel | null,
  semester: null as Semester | null,
  section: '',
  campusId: null as string | null,
  programId: null as string | null,
  requesterType: null as RequesterType | null,
  picture: null as File | null,
  authorizationLetter: null as File | null,
  representativeValidId: null as File | null,
  validId: null as File | null,
  specialPowerOfAttorney: null as File | null,
  affidavitOfLoss: null as File | null,
  collectorType: CollectorType.Myself,
})
function onPictureChange(files: readonly any[]) {
  form.fields.picture = files[0]
}
function onSubmit() {
  form.submit(async (fields) => {
    try {
      const formData = new FormData()
      for (const [key, value] of Object.entries(fields)) {
        formData.append(key, value as string)
      }
      form.fields.documentTypes.forEach((documentType) => {
        formData.append('documentTypes[]', documentType.toString())
      })
      const { data } = await api.post('/requests', formData, {
        headers: { 'Content-Type': 'multipart/form-data' },
      })
      $q.notify({
        type: 'positive',
        message:
        'Request submitted successfully. You can track your request status using the reference number.',
      })
      router.push(`/requests/${data.referenceNumber}`)
    }
    catch (e) {
      $q.notify({
        type: 'negative',
        message: 'Request failed. Please check your information.',
      })
      throw e
    }
  })
}
watch(otherPurpose, () => {
  if (otherPurpose.value) {
    form.fields.purpose = ''
  }
})
watch(
  () => form.fields.purpose,
  (v) => {
    if (purposes.includes(v)) {
      otherPurpose.value = false
    }
  },
)
onMounted(() => {
  campuses.submit()
  programs.submit()
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
          Make sure your provided information is correct before submitting the
          request.
        </p>
        <QBanner
          v-if="form.hasErrors()"
          class="text-negative mt-xl border-1 border-red rounded"
        >
          <template #avatar>
            <QIcon>
              <div class="i-hugeicons:cancel-circle text-3xl" />
            </QIcon>
          </template>
          There's an error please check all the highlighted fields.
        </QBanner>
        <div class="grid grid-cols-2 items-center gap-sm">
          <div class="mt-xl">
            <QRadio
              v-for="requesterType in requesterTypes"
              :key="requesterType.value"
              v-model="form.fields.requesterType"
              :val="requesterType.value"
              :label="requesterType.label"
              :color="form.hasError('requesterType') ? 'negative' : 'primary'"
              keep-color
            />
          </div>
          <QInput
            v-model="form.fields.studentNumber"
            label="Student Number"
            :error="form.hasError('studentNumber')"
            :error-message="form.getError('studentNumber')"
          />
        </div>
        <p class="mt-xl font-bold">
          Select document you want to request:
        </p>
        <QCheckbox
          v-for="documentType in documentTypes"
          :key="documentType.value"
          v-model="form.fields.documentTypes"
          :color="form.hasError('documentTypes') ? 'negative' : 'primary'"
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
              v-if="campuses.response"
              v-model="form.fields.campusId"
              :options="campuses.response"
              option-value="id"
              option-label="name"
              emit-value
              map-options
              label="Select Campus"
              :error="form.hasError('campusId')"
            />
            <QSelect
              v-if="programs.response"
              v-model="form.fields.programId"
              :options="programs.response"
              option-value="id"
              option-label="name"
              emit-value
              map-options
              label="Select Course"
              :error="form.hasError('programId')"
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
              <p>Semester</p>
              <QRadio
                v-for="semester in semesters"
                :key="semester.value"
                v-model="form.fields.semester"
                :color="form.hasError('semester') ? 'negative' : 'primary'"
                keep-color
                :error="form.hasError('semester')"
                :val="semester.value"
                :label="semester.label"
              />
            </div>
            <div class="flex items-center gap-2">
              <p class="mr-5xl">
                Year Level
              </p>
              <QSelect
                v-model="form.fields.yearLevel"
                label="Select your last year level"
                class="flex-1"
                :error="form.hasError('yearLevel')"
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
                v-model="form.fields.section"
                hide-bottom-space
                class="flex-1"
                label="Enter your last section"
                :error="form.hasError('section')"
              />
            </div>
          </div>
        </div>
        <p lass="mt-xl font-bold">
          Purpose
        </p>
        <div class="grid grid-cols-2">
          <QRadio
            v-for="purpose in purposes"
            :key="purpose"
            v-model="form.fields.purpose"
            :val="purpose"
            :label="purpose"
          />
        </div>
        <div>
          <QRadio v-model="otherPurpose" :val="true" label="Other" />
          <QInput
            v-model="form.fields.purpose"
            placeholder="Type your purpose"
            :disable="!otherPurpose"
            :error="form.hasError('purpose')"
            :error-message="form.getError('purpose')"
            class="mb-lg"
          />
        </div>
        <QExpansionItem
          label="Personal Information"
          caption="You can edit your Personal Information for this request here."
          icon="person"
          :model-value="true"
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
                  v-model="form.fields.email"
                  placeholder="Type your email"
                  class="col-span-2"
                  type="email"
                  label="Email"
                  :error="form.hasError('email')"
                  :error-message="form.getError('email')"
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
        <QExpansionItem
          label="Requirements"
          caption="Please upload all the needed requirements for this request."
          icon="attachment"
          :model-value="true"
          header-class="text-primary font-bold bg-blue-1 rounded-xl border-1 border-blue-2"
        >
          <p class="mt-xl">
            2x2 Picture
          </p>
          <QUploader label="Attach your 2x2 picture here" class="w-sm" :hide-upload-btn="true" flat bordered @added="onPictureChange" />
          <p v-if="form.hasError('picture')" class="text-negative mt-sm">
            {{ form.getError('picture') }}
          </p>
          <p class="mt-xl">
            Who will collect the documents?
          </p>
          <QRadio
            v-for="collector in collectorTypes"
            :key="collector.value"
            v-model="form.fields.collectorType"
            :val="collector.value"
            :label="collector.label"
            :color="form.hasError('collectorType') ? 'negative' : 'primary'"
            keep-color
          />
          <QFile
            v-if="form.fields.collectorType === CollectorType.ImmediateFamilyMember" v-model="form.fields.authorizationLetter" label="Authorization Letter" class="w-sm" flat bordered
            :error="form.hasError('authorizationLetter')"
            :error-message="form.getError('authorizationLetter')"
          >
            <template #prepend>
              <div class="i-hugeicons:document-attachment" />
            </template>
          </QFile>
          <QFile
            v-if="form.fields.collectorType === CollectorType.AuthorizedRepresentative" v-model="form.fields.specialPowerOfAttorney" label="Special Power of Attorney" class="w-sm"
            flat bordered
            :error="form.hasError('specialPowerOfAttorney')"
            :error-message="form.getError('specialPowerOfAttorney')"
          >
            <template #prepend>
              <div class="i-hugeicons:document-attachment" />
            </template>
          </QFile>
          <QFile
            v-if="form.fields.collectorType !== CollectorType.Myself" v-model="form.fields.validId" label="Owner's Valid ID" class="w-sm"
            flat bordered
            :error="form.hasError('validId')"
            :error-message="form.getError('validId')"
          >
            <template #prepend>
              <div class="i-hugeicons:document-attachment" />
            </template>
          </QFile>
          <QFile
            v-if="form.fields.collectorType !== CollectorType.Myself" v-model="form.fields.representativeValidId" label="Representative's Valid ID" class="w-sm"
            flat bordered
            :error="form.hasError('representativeValidId')"
            :error-message="form.getError('representativeValidId')"
          >
            <template #prepend>
              <div class="i-hugeicons:document-attachment" />
            </template>
          </QFile>
          <QFile
            v-if="form.fields.documentTypes.includes(DocumentType.SecondCopyOfDiploma)"
            v-model="form.fields.affidavitOfLoss" label="Affidavit of Loss" class="w-sm" flat bordered
          >
            <template #prepend>
              <div class="i-hugeicons:document-attachment" />
            </template>
          </QFile>
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
