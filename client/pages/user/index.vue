<script lang="ts" setup>
const { user } = useUser()
</script>

<template>
  <div class="mx-auto mt-xl container">
    <QCard flat bordered>
      <QCardSection>
        <div class="flex justify-between gap-sm">
          <div>
            <p class="text-xl">
              Welcome back
              <span class="font-bold">
                {{ user?.firstName }}
              </span>,
            </p>
            <p class="text-gray-6">
              You can now request documents from us.
            </p>
          </div>
          <div class="flex flex-col gap-sm">
            <QBtn label="Open Documents" color="primary" />
            <QBtn label="New Request" color="primary" outline />
          </div>
        </div>
      </QCardSection>
    </QCard>

    <QCard flat bordered class="mt-xl">
      <QCardSection>
        <p class="text-lg font-bold">
          Recent Documents
        </p>
        <QMarkupTable flat>
          <thead>
            <tr>
              <th class="text-left">
                Document Name
              </th>
              <th class="text-left">
                Requested On
              </th>
              <th>Status</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td colspan="3">
                No requests found
              </td>
            </tr>
          </tbody>
        </QMarkupTable>
      </QCardSection>
    </QCard>
  </div>
</template>
