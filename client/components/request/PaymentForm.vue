<script lang="ts" setup>
defineProps<{
  referenceNumber: string
}>()
</script>

<template>
  <div>
    <p class="mb-xl text-lg">
      Payment Details
    </p>
    <ul>
      <li><b>Reference Number:</b> {{ referenceNumber }}</li>
    </ul>
  </div>
</template>
