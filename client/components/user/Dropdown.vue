<script setup lang="ts">
import { Role } from '~/enums/role'

const { user, logout } = useUser()
const router = useRouter()
const $q = useQuasar()
const role = computed(() => user.value?.role)
async function onLogout() {
  $q.dialog({
    title: 'Logout Confirmation',
    message: 'Are you sure you want to logout?',
    ok: {
      color: 'negative',
    },
    cancel: true,
  }).onOk(async () => {
    await logout()
    router.push('/login')
  })
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

      <QItem v-if="role === Role.Admin" clickable to="/user/users">
        <QItemSection avatar>
          <QIcon>
            <div class="i-hugeicons:user-switch" />
          </QIcon>
        </QItemSection>
        <QItemSection> Users Management</QItemSection>
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
