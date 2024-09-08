export default defineNuxtRouteMiddleware((to) => {
  const { user } = useUser()
  const guestRoutes = ['/login', '/register', '/forgot-password', '/reset-password']
  if (user.value) {
    if (guestRoutes.includes(to.path)) {
      return navigateTo('/user')
    }
  }
})
