<script lang="ts" setup>
const { user, fetchUser } = useUser()
onMounted(async () => {
  if (user.value)
    return
  await fetchUser()
})
</script>

<template>
  <div>{{ user }}</div>
</template>
