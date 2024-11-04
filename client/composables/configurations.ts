export function useConfigurations() {
  return reactive({
    data: [] as any[],
    paymentDetails: {
      accountName: '',
      accountNumber: '',
    },
    async fetch() {
      const { data } = await api.get('/user/configurations')
      this.data = data
      this.paymentDetails.accountName = data.find((c: any) => c.property === 'accountName')?.value || ''
      this.paymentDetails.accountNumber = data.find((c: any) => c.property === 'accountNumber')?.value || ''
    },
  })
}
