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
    <QHeader elevated reveal>
      <QToolbar>
        <QToolbarTitle>
          <span class="font-bold">
            OMSC ODRS
          </span>
        </QToolbarTitle>
        <QSpace />
        <div>
          <QBtn flat to="/">
            Home
          </QBtn>
          <QBtn flat to="/login">
            Documents
          </QBtn>
          <QBtn flat to="/contact-us">
            Contact Us
          </QBtn>
          <QBtnDropdown flat no-caps>
            <template #label>
              <span class="i-hugeicons:user-circle mr-sm text-lg" />
              <span>{{ user?.firstName }}</span>
            </template>
            <QList>
              <QItem clickable to="/user/update-profile">
                <QItemSection avatar>
                  <QIcon>
                    <div class="i-hugeicons:system-update-01" />
                  </QIcon>
                </QItemSection>
                <QItemSection>
                  Update Profile
                </QItemSection>
              </QItem>

              <QItem clickable to="/user/change-password">
                <QItemSection avatar>
                  <QIcon>
                    <div class="i-hugeicons:circle-lock-check-01" />
                  </QIcon>
                </QItemSection>
                <QItemSection>
                  Change Password
                </QItemSection>
              </QItem>
              <QItem clickable @click="onLogout">
                <QItemSection avatar>
                  <QIcon>
                    <div class="i-hugeicons:logout-circle-01" />
                  </QIcon>
                </QItemSection>
                <QItemSection>
                  Logout
                </QItemSection>
              </QItem>
            </QList>
          </QBtnDropdown>
        </div>
      </QToolbar>
    </QHeader>
    <QPageContainer>
      <slot />
    </QPageContainer>
  </QLayout>
</template>
