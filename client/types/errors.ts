export type ErrorResponse<T> = {
  [K in keyof T | 'generalErrors']?: string[];
}
