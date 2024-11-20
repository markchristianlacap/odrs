import type { Role } from '~/enums/role'

export interface UserRes {
  id: string
  firstName: string
  lastName: string
  middleName?: string
  extensionName?: string
  email: string
  contactNumber: string
  address: string
  birthdate: string
  roleDesc: string
  role: Role
}
