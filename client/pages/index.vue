<script setup lang="ts">
definePageMeta({
  layout: 'home',
})
</script>

<template>
  <div>
    <div class="grid mx-auto mt-5xl items-center justify-center gap-sm pa-lg container md:grid-cols-2">
      <div>
        <img src="~/assets/img/cover.jpg" alt="ODRS Cover">
      </div>
      <div>
        <div class="flex items-center gap-sm">
          <img src="~/assets/img/omsc-logo.png" alt="OMSC Logo" class="w-40">
          <div>
            <p class="block text-3xl font-bold">
              Welcome to
            </p>
            <p class="block text-2xl text-primary font-bold">
              Occidental Mindoro State College
            </p>
            <p class="block text-3xl font-bold">
              Online Document Request System
            </p>
            <p class="my-sm font-bold">
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
            <QBtn label="Get Started" color="primary" to="/login" class="mt-sm" />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
