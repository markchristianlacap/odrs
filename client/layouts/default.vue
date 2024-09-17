<script setup lang="ts">
const { user, logout } = useUser()
const router = useRouter()
async function onLogout() {
  await logout()
  router.push('/login')
}
</script>

<template>
  <QLayout>
    <QHeader bordered reveal class="bg-white text-primary">
      <div class="flex flex-wrap items-center justify-between">
        <div class="flex items-center">
          <img
            src="~/assets/img/odrs-logo.png"
            class="max-w-15"
            alt="ODRS Logo"
          >
          <p class="text-lg">
            OMSC <b>|ODRS</b>
          </p>
        </div>
        <QTabs dense>
          <QRouteTab flat to="/">
            Home
          </QRouteTab>
          <QRouteTab flat to="/requests/form">
            Request Documents
          </QRouteTab>
          <QRouteTab flat to="/contact-us">
            Contact Us
          </QRouteTab>
          <QBtnDropdown v-if="user" flat no-caps auto-close>
            <template #label>
              <span class="i-hugeicons:user-circle mr-sm text-lg" />
              <span class="hidden md:!inline">{{ user?.firstName }}</span>
            </template>
            <QList>
              <QItem clickable to="/user">
                <QItemSection avatar>
                  <QIcon>
                    <div class="i-hugeicons:user-circle" />
                  </QIcon>
                </QItemSection>
                <QItemSection> Dashboard </QItemSection>
              </QItem>
              <QItem clickable to="/user/requests">
                <QItemSection avatar>
                  <QIcon>
                    <div class="i-hugeicons:document-attachment" />
                  </QIcon>
                </QItemSection>
                <QItemSection> Requested Documents </QItemSection>
              </QItem>
              <QItem clickable to="/user/update-profile">
                <QItemSection avatar>
                  <QIcon>
                    <div class="i-hugeicons:system-update-01" />
                  </QIcon>
                </QItemSection>
                <QItemSection> Update Profile </QItemSection>
              </QItem>
              <QItem clickable to="/user/change-password">
                <QItemSection avatar>
                  <QIcon>
                    <div class="i-hugeicons:circle-lock-check-01" />
                  </QIcon>
                </QItemSection>
                <QItemSection> Change Password </QItemSection>
              </QItem>
              <QItem clickable @click="onLogout">
                <QItemSection avatar>
                  <QIcon>
                    <div class="i-hugeicons:logout-circle-01" />
                  </QIcon>
                </QItemSection>
                <QItemSection> Logout </QItemSection>
              </QItem>
            </QList>
          </QBtnDropdown>
        </QTabs>
      </div>
    </QHeader>
    <QPageContainer>
      <slot />
    </QPageContainer>
  </QLayout>
</template>
