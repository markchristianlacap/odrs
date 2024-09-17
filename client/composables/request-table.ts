import type { QTableProps } from 'quasar'
import type { PagedReq, PagedRes } from '~/types/pagination'

type TPaged = QTableProps['pagination']
export function useRequestTable<TRes = any, TReq = PagedReq>(
  req: (params: TReq) => Promise<PagedRes<TRes>>,
  params: TReq = {} as TReq,
) {
  return reactive({
    pagination: { rowsPerPage: 15, page: 1 } as TPaged,
    request: params,
    response: { rows: [] as TRes[] } as PagedRes<TRes>,
    loading: false,
    resetPagination() {
      this.pagination = { rowsPerPage: 15, page: 1 }
    },
    resetRequest() {
      this.request = params
    },
    async submit() {
      this.loading = true
      const params = { ...this.request, ...this.pagination }
      this.response = await req(params)
      if (this.pagination?.rowsPerPage)
        this.pagination.rowsNumber = this.response.rowsCount
      this.loading = false
    },
    async onRequest(props: { pagination: TPaged, filter?: PagedReq | any }) {
      this.pagination = props.pagination
      this.request = props.filter
      await this.submit()
    },
  })
}
