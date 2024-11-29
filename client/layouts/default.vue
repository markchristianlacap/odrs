<script setup lang="ts">
const $q = useQuasar()
const isMobile = computed(() => $q.screen.lt.sm)
</script>

<template>
  <QLayout>
    <QHeader bordered reveal class="bg-white text-primary">
      <div class="flex flex-wrap items-center justify-between">
        <div class="w-full flex items-center justify-between gap-sm sm:w-auto">
          <div class="flex items-center justify-between">
            <img
              src="~/assets/img/odrs-logo.png"
              class="max-w-15"
              alt="ODRS Logo"
            >
            <p class="text-lg">
              OMSC <b>|ODRS</b>
            </p>
          </div>
          <UserDropdown v-if="isMobile" />
        </div>
        <QTabs
          dense
          no-caps
          :class="{ 'w-full': isMobile }"
        >
          <QRouteTab flat to="/">
            <div class="i-hugeicons:home-05 text-xl" />
            Home
          </QRouteTab>
          <QRouteTab flat to="/overview">
            <div class="i-hugeicons:information-circle text-xl" />
            Overview
          </QRouteTab>
          <QRouteTab flat to="/requests/form">
            <div class="i-hugeicons:keyframes-double-remove text-xl" />
            Request Documents
          </QRouteTab>
          <QRouteTab flat to="/how-to-use">
            <div class="i-hugeicons:question text-xl" />
            How To Use
          </QRouteTab>
          <QRouteTab flat to="/contact-us">
            <div class="i-hugeicons:contact text-xl" />
            Contact Us
          </QRouteTab>
          <UserDropdown v-if="!isMobile" />
        </QTabs>
      </div>
    </QHeader>
    <QPageContainer>
      <QPage padding>
        <slot />
      </QPage>
    </QPageContainer>
  </QLayout>
</template>
