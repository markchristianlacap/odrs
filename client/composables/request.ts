export function useRequest<TRes = any, TReq = any>(
  req: (params: TReq) => Promise<TRes>,
  params: TReq = {} as TReq,
) {
  return reactive({
    request: params,
    response: null as TRes | null,
    loading: false,
    async submit() {
      this.loading = true
      console.log(this.request)
      this.response = await req(this.request)
      this.loading = false
    },
  })
}
