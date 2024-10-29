<script setup lang="ts">
const { user, logout } = useUser()
const router = useRouter()
async function onLogout() {
  await logout()
  router.push('/login')
}
</script>

<template>
  <QBtnDropdown v-if="user" flat no-caps auto-close>
    <template #label>
      <span class="i-hugeicons:user-circle mr-sm text-lg" />
      <span>{{ user?.firstName }}</span>
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
      <QItem clickable to="/user/reports">
        <QItemSection avatar>
          <QIcon>
            <div class="i-hugeicons:school-report-card" />
          </QIcon>
        </QItemSection>
        <QItemSection> Report </QItemSection>
      </QItem>
      <QItem clickable to="/user/configurations">
        <QItemSection avatar>
          <QIcon>
            <div class="i-hugeicons:money-receive-square" />
          </QIcon>
        </QItemSection>
        <QItemSection> Payment Details Configurations </QItemSection>
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
</template>
