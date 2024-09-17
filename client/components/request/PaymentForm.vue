<script lang="ts" setup>
defineProps<{
  referenceNumber: string
}>()
</script>

<template>
  <div>
    <p>
      Payment Details
    </p>
    <p>
      Reference Number: {{ referenceNumber }}
    </p>
  </div>
</template>
