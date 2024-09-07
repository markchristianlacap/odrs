<template>
  <QLayout>
    <QHeader elevated reveal>
      <QToolbar>
        <QToolbarTitle>
          <span class="font-bold">
            OMSC ODRS
          </span>
        </QToolbarTitle>
        <q-space />
        <QBtn flat to="/">
          Home
        </QBtn>
        <QBtn flat to="/login">
          Get Documents
        </QBtn>
        <QBtn flat to="/contact-us">
          Contact Us
        </QBtn>
      </QToolbar>
    </QHeader>
    <QPageContainer>
      <slot />
    </QPageContainer>
  </QLayout>
</template>
