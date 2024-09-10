import type { UserRes } from '~/types/user'

export function useUser() {
  const user = useState<UserRes | null>('user')
  async function fetchUser() {
    try {
      const { data } = await api.get('/user')
      user.value = data
    }
    catch {}
  }
  async function logout() {
    await api.post('/logout')
    user.value = null
  }
  return {
    user,
    fetchUser,
    logout,
  }
}
