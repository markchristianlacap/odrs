export default defineNuxtRouteMiddleware(async (to) => {
  const guestRoutes = [
    '/login',
    '/register',
    '/forgot-password',
    '/reset-password',
  ]
  const { user, fetchUser } = useUser()
  if (guestRoutes.includes(to.path)) {
    if (!user.value) {
      await fetchUser()
    }
    if (user.value) {
      return navigateTo('/user')
    }
  }

  // check if route to is `/user/*`
  if (to.path.startsWith('/user')) {
    if (!user.value) {
      await fetchUser()
    }
    if (!user.value) {
      return navigateTo('/login')
    }
  }
})
