<script setup lang="ts">
const $q = useQuasar()
const isMobile = computed(() => $q.screen.lt.md)
</script>

<template>
  <QLayout>
    <QHeader bordered reveal class="bg-white text-primary">
      <div class="flex flex-wrap items-center justify-between">
        <div class="w-full flex items-center justify-between gap-sm md:w-auto">
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
          dense items-center
          :class="{ 'w-full': isMobile }"
        >
          <QRouteTab flat to="/">
            Home
          </QRouteTab>
          <QRouteTab flat to="/requests/form">
            Request Documents
          </QRouteTab>
          <QRouteTab flat to="/contact-us">
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
