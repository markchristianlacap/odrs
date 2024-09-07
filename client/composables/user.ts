import type { UserRes } from '~/types/user'

export function useUser() {
  const user = useState<UserRes | null>('user')
  async function fetchUser() {
    const { data } = await api.get('/user')
    user.value = data
  }
  return {
    user,
    fetchUser,
  }
}
