<template>
  <QLayout>
    <QHeader>
      <QToolbar>
        <QToolbarTitle>
          OMSC ODRS
        </QToolbarTitle>
        <q-space />
        <QBtn flat to="/">
          Home
        </QBtn>
        <QBtn flat>
          Get Documents
        </QBtn>
        <QBtn flat>
          Contact Us
        </QBtn>
      </QToolbar>
    </QHeader>
    <QPageContainer>
      <RouterView />
    </QPageContainer>
  </QLayout>
</template>
