import type { UserRes } from '~/types/user'

export function useUser() {
  const user = useState<UserRes | null>('user')
  async function fetchUser() {
    try {
      const { data } = await api.get('/user')
      user.value = data
    }
    catch {
      // do nothing
    }
  }
  return {
    user,
    fetchUser,
  }
}
