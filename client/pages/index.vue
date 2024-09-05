<script setup lang="ts">
definePageMeta({
  layout: 'home',
})
</script>

<template>
  <div>
    <div class="mx-auto mt-5xl flex items-center justify-center gap-sm container">
      <div>
        <img src="~/assets/img/omsc-logo.png" alt="OMSC Logo" class="w-50">
      </div>
      <div>
        <span class="block text-2xl font-bold">Welcome to</span>
        <span class="block text-xl font-bold">Occidental Mindoro State College</span>
        <span class="block text-xl font-bold">Online Document Request System</span>
        <p class="mt-sm font-bold">
          Open Hours
        </p>
        <ul>
          <li>
            Monday 7:00 am - 4:00 pm
          </li>
          <li>
            Tuesday - Thursday 8:00 am - 5:00 pm
          </li>
        </ul>
        <QBtn label="Get Started" color="primary" />
      </div>
    </div>
  </div>
</template>
