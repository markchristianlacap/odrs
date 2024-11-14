<script setup lang="ts">
import { CollectorType } from '~/enums/collector-type'
import { DocumentType } from '~/enums/document-type'
import { RequesterType } from '~/enums/requester-type'
import type { RequesterType as TRequesterType } from '~/enums/requester-type'
import type { Semester } from '~/enums/semester'
import type { YearLevel } from '~/enums/year-level'
import { collectorTypes } from '~/options/collector-types'
import { documentTypes } from '~/options/document-types'
import { requesterTypes } from '~/options/requester-types'
import { semesters } from '~/options/semesters'
import { yearLevels } from '~/options/year-levels'

const $q = useQuasar()
const documents = ref<DocumentType[]>([])
const router = useRouter()
const campuses = useRequest(() =>
  api.get('/options/campuses').then(r => r.data),
)
const programs = useRequest(params =>
  api.get('/options/programs', { params }).then(r => r.data), {
  campusId: '',
})
const purposes = [
  'For Employment Purposes Only',
  'For Reference Purposes Only',
  'For Scholarship Purposes Only',
  'For PNP Application Purposes Only',
  'For NAPOLCOM Examination Purposes Only',
  'For Board Examination Purposes Only',
  'For Promotion Purposes Only',
  'For Evaluation Purposes Only',
]
const form = useForm({
  studentNumber: '',
  email: '',
  documents: [] as {
    type: DocumentType
    purpose: string
    copies: number
    otherPurpose: boolean
  }[],
  lastName: '',
  firstName: '',
  middleName: '' as string | undefined,
  extensionName: '' as string | undefined,
  contactNumber: '+639',
  birthdate: null,
  address: '',
  lastAttendanceStartYear: null as number | null,
  lastAttendanceEndYear: null as number | null,
  yearLevel: null as YearLevel | null,
  semester: null as Semester | null,
  section: '',
  campusId: null as string | null,
  programId: null as string | null,
  requesterType: null as TRequesterType | null,
  picture: null as File | null,
  authorizationLetter: null as File | null,
  representativeValidId: null as File | null,
  validId: null as File | null,
  specialPowerOfAttorney: null as File | null,
  affidavitOfLoss: null as File | null,
  birthCertificate: null as File | null,
  requestLetter: null as File | null,
  collectorType: CollectorType.Owner,
  representative: '',
  documentToBeCertified: null as File | null,
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
      form.fields.documents.forEach((document) => {
        formData.append('documents[]', JSON.stringify(document))
      })
      if (form.fields.requesterType === RequesterType.Alumni) {
        formData.delete('yearLevel')
      }
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
function getDocumentTypeLabel(type: DocumentType) {
  return documentTypes.find(t => t.value === type)?.label
}

// sync documents with form.fields.documents -> document.type but retaining copies and purpose
watch(documents, (v) => {
  form.fields.documents = v.map((type) => {
    const document = form.fields.documents.find(d => d.type === type)
    return {
      type,
      purpose: document?.purpose ?? '',
      copies: document?.copies ?? 1,
      otherPurpose: false,
    }
  })
})
onMounted(() => {
  campuses.submit()
})
watch(() => form.fields.campusId, (v) => {
  form.fields.programId = null
  programs.request.campusId = v || ''
  programs.submit()
})
</script>

<template>
  <QCard class="mx-auto mt-xl container" flat bordered>
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
        <div class="mt-xl">
          Are you:
          <QRadio
            v-for="requesterType in requesterTypes"
            :key="requesterType.value"
            v-model="form.fields.requesterType"
            :label="requesterType.label"
            :val="requesterType.value"
            :color="form.hasError('requesterType') ? 'negative' : 'primary'"
            keep-color
          />
        </div>
        <QInput
          v-model="form.fields.studentNumber"
          label="Student Number (Optional)"
          :error="form.hasError('studentNumber')"
          :error-message="form.getError('studentNumber')"
          class="w-sm"
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
                <!-- {{ form.fields.requesterType === RequesterType.Alumni ? 'Year Graduated' : "Last Attended" }} -->
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
            <div
              v-if="form.fields.requesterType === RequesterType.FormerStudent"
              class="flex items-center gap-2"
            >
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
                label="Enter your last section (Optional)"
                :error="form.hasError('section')"
              />
            </div>
          </div>
        </div>
        <div>
          <p class="mt-xl font-bold">
            Select document you want to request:
          </p>
          <QCheckbox
            v-for="documentType in documentTypes"
            :key="documentType.value"
            v-model="documents"
            :color="form.hasError('documents') ? 'negative' : 'primary'"
            keep-color
            :val="documentType.value"
            :label="documentType.label"
          />
        </div>
        <ul class="mt-xl">
          <li
            v-for="document, i in form.fields.documents"
            :key="document.type"
          >
            <div class="flex items-center gap-2">
              <p>
                How many copies do you need for <b> {{ getDocumentTypeLabel(document.type) }}</b>?
              </p>
              <QInput
                v-model="document.copies"
                mask="##"
                class="w-120px"
                :min="1"
                dense
                type="number"
              />
            </div>
            <p lass="mt-xl font-bold">
              Purpose for <b>{{ getDocumentTypeLabel(document.type) }}</b>
            </p>
            <div class="grid grid-cols-2">
              <!-- @vue-expect-error -->
              <QRadio
                v-for="purpose in purposes"
                :key="purpose"
                v-model="document.purpose"
                :val="purpose"
                :label="purpose"
                :color="form.hasError(`documents[${i}].purpose`) ? 'negative' : 'primary'"
                keep-color
              />
            </div>
            <div>
              <QRadio v-model="document.otherPurpose" :val="true" label="Other" />
              <!-- @vue-expect-error -->
              <QInput
                v-model="document.purpose"
                placeholder="Type your purpose"
                :disable="!document.otherPurpose"
                :error="form.hasError(`documents[${i}].purpose`)"
                :error-message="form.getError(`documents[${i}].purpose`)"
                class="mb-lg"
              />
            </div>
          </li>
        </ul>

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
                  mask="+63-###-###-####"
                  hint="E.g. +63-933-456-7890"
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
          <QUploader
            label="Attach your 2x2 picture here" class="w-sm" :hide-upload-btn="true" flat bordered accept="image/*"

            @added="onPictureChange"
          />
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

            v-if="form.fields.collectorType === CollectorType.ImmediateFamilyMember"
            v-model="form.fields.authorizationLetter" accept="image/*" label="Authorization Letter" class="w-sm" flat bordered
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

            accept="image/*"
          >
            <template #prepend>
              <div class="i-hugeicons:document-attachment" />
            </template>
          </QFile>
          <QFile
            v-model="form.fields.validId" label="Owner's Valid ID" class="w-sm"
            flat bordered
            :error="form.hasError('validId')"
            :error-message="form.getError('validId')"
            accept="image/*"
          >
            <template #prepend>
              <div class="i-hugeicons:document-attachment" />
            </template>
          </QFile>
          <QInput
            v-if="form.fields.collectorType !== CollectorType.Owner"
            v-model="form.fields.representative"
            label="Name of Representative"
            class="w-sm" flat
            :error="form.hasError('representative')"
            :error-message="form.getError('representative')"
          />
          <QFile
            v-if="form.fields.collectorType !== CollectorType.Owner" v-model="form.fields.representativeValidId" label="Representative's Valid ID" class="w-sm"
            flat bordered
            :error="form.hasError('representativeValidId')"
            :error-message="form.getError('representativeValidId')"

            accept="image/*"
          >
            <template #prepend>
              <div class="i-hugeicons:document-attachment" />
            </template>
          </QFile>
          <QFile
            v-if="form.fields.documents.some(x => x.type === DocumentType.SecondCopyOfDiploma)"
            v-model="form.fields.affidavitOfLoss" label="Affidavit of Loss" class="w-sm" flat bordered
            :error="form.hasError('affidavitOfLoss')"
            :error-message="form.getError('affidavitOfLoss')"
            accept="image/*"
          >
            <template #prepend>
              <div class="i-hugeicons:document-attachment" />
            </template>
          </QFile>
          <QFile
            v-if="form.fields.documents.some(x => x.type === DocumentType.SecondCopyOfDiploma)"
            v-model="form.fields.birthCertificate" label="PSA Birth Certificate" class="w-sm" flat bordered
            accept="image/*"
            :error="form.hasError('birthCertificate')"
            :error-message="form.getError('birthCertificate')"
          >
            <template #prepend>
              <div class="i-hugeicons:document-attachment" />
            </template>
          </QFile>
          <QFile
            v-if="form.fields.documents.some(x => x.type === DocumentType.HonorableDismissal)"
            v-model="form.fields.requestLetter" label="Request Letter/Return Slip" class="w-sm" flat bordered
            accept="image/*"
            :error="form.hasError('requestLetter')"
            :error-message="form.getError('requestLetter')"
          >
            <template #prepend>
              <div class="i-hugeicons:document-attachment" />
            </template>
          </QFile>
          <QFile
            v-if="form.fields.documents.some(x => x.type === DocumentType.Authentication)"
            v-model="form.fields.documentToBeCertified" label="Document to be authenticated" class="w-sm" flat bordered
            accept="image/*"
            :error="form.hasError('documentToBeCertified')"
            :error-message="form.getError('documentToBeCertified')"
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
            <QIcon left>
              <div class="i-hugeicons:checkmark-square-04" />
            </QIcon>
            Submit
          </QBtn>
        </div>
      </QForm>
    </QCardSection>
  </QCard>
</template>
