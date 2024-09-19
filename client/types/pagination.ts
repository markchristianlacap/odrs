export interface PagedReq {
  page: number
  rowsPerPage: number
}
export interface PagedRes<T = any> {
  currentPage: number
  pageCount: number
  pageSize: number
  rowsCount: number
  rows: T[]
}
