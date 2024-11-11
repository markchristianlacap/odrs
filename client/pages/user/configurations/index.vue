<script setup lang="ts">
const $q = useQuasar()
const config = useConfigurations()
const paymentDetails = useForm({
  accountNumber: '',
  accountName: '',
})
function submit() {
  paymentDetails.submit(async (fields) => {
    const form = [{
      property: 'accountNumber',
      value: fields.accountNumber,
    }, {
      property: 'accountName',
      value: fields.accountName,
    }]
    await api.post('/user/configurations', { configurations: form })
    $q.notify({
      type: 'positive',
      message: 'Payment Details successfully updated.',
    })
  })
}
async function fetch() {
  await config.fetch()
  paymentDetails.fields.accountNumber = config.paymentDetails.accountNumber
  paymentDetails.fields.accountName = config.paymentDetails.accountName
}
const isValid = computed(() => {
  return !paymentDetails.fields.accountName || !paymentDetails.fields.accountNumber
})
onMounted(() => fetch())
</script>

<template>
  <QCard class="mx-auto max-w-xl" flat>
    <QCardSection>
      <p class="text-xl font-bold">
        Payment Details Configurations
      </p>
      <p class="text-primary">
        Configure payment details here. These details will be used for payments.
      </p>
      <QForm class="mt-xl" @submit="submit">
        <QInput
          v-model="paymentDetails.fields.accountName"
          label="Account Name"
          class="mb-sm"
        />
        <QInput
          v-model="paymentDetails.fields.accountNumber"
          label="Account Number"
          class="mb-sm"
        />
        <div class="text-right">
          <QBtn color="primary" type="submit" :disable="isValid" class="mt-xl">
            <QIcon left>
              <div class="i-hugeicons:checkmark-circle-01" />
            </QIcon>
            Save Configurations
          </QBtn>
        </div>
      </QForm>
    </QCardSection>
  </QCard>
</template>
