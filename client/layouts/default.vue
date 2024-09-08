<script setup lang="ts">
const { user } = useUser()
</script>

<template>
  <QLayout>
    <QHeader elevated reveal>
      <QToolbar>
        <QToolbarTitle>
          OMSC ODRS
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
          <QBtn flat no-caps>
            <span class="i-hugeicons:user-circle mr-sm text-lg" />
            <span>{{ user?.name }}</span>
          </QBtn>
        </div>
      </QToolbar>
    </QHeader>
    <QPageContainer>
      <slot />
    </QPageContainer>
  </QLayout>
</template>
