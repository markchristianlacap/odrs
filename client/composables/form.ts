import { isAxiosError } from 'axios'
import type { ErrorResponse } from '~/types/errors'

export function useForm<T = any>(fields: T) {
  return reactive({
    fields: JSON.parse(JSON.stringify(fields)) as typeof fields,
    loading: false,
    errors: {} as ErrorResponse<T>,
    async submit(submitter: (fields: T) => Promise<any>) {
      if (this.loading)
        return
      this.errors = {}
      this.loading = true
      try {
        await submitter(this.fields)
      }
      catch (e) {
        if (isAxiosError(e))
          this.errors = e?.response?.data?.errors ?? {}
        else throw e
      }
      this.loading = false
    },
    reset() {
      this.fields = fields
    },
    getError(field: keyof ErrorResponse<T>) {
      return this.errors[field]?.[0]
    },
    hasError(field: keyof ErrorResponse<T>) {
      return !!this.getError(field)
    },
    hasErrors() {
      return Object.keys(this.errors).length > 0
    },
  })
}
