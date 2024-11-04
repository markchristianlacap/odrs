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
  <div class="mx-auto container">
    <p class="text-xl font-bold">
      Payment Details Configurations
    </p>
    <QForm class="mt-xl max-w-xl" @submit="submit">
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
          Save Configurations
        </QBtn>
      </div>
    </QForm>
  </div>
</template>
