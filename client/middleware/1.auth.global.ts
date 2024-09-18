export default defineNuxtRouteMiddleware(async (to) => {
  const guestRoutes = ['/login', '/forgot-password', '/reset-password']
  const { user, fetchUser } = useUser()
  if (!user.value) {
    await fetchUser()
  }
  if (guestRoutes.includes(to.path)) {
    if (user.value) {
      return navigateTo('/user')
    }
  }

  // check if route to is `/user/*`
  if (to.path.startsWith('/user')) {
    if (!user.value) {
      return navigateTo('/login')
    }
  }
})
