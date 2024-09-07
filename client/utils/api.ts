import request from 'axios'

function axiosInstance() {
  const axiosInstance = request.create({
    baseURL: '/api',
    headers: {
      'Content-Type': 'application/json',
      'X-Requested-With': 'XMLHttpRequest',
    },
    withCredentials: true,
  })
  return axiosInstance
}

export const api = axiosInstance()
