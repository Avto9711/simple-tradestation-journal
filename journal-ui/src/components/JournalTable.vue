<script setup lang="ts">
 import useJournalStore from '@/stores/journal'
 import VueTableLite from "vue3-table-lite/ts";
 import {  reactive, ref } from 'vue';
 import JournalDetailModal, { type JournalDetailComponent } from '@/components/JournalDetailModal.vue'
import type { JournalBalance } from '@/models';

 const store = useJournalStore();

 let selectedJournalBalance = ref<JournalBalance | null>(null);
  const detailModalComponent = ref<JournalDetailComponent>();

const rowClicked = (rowData:JournalBalance)=>{
  selectedJournalBalance.value = rowData
  detailModalComponent?.value?.toggleModal()

}

 const table = reactive({
      isLoading: false,
      columns: [
        {
          label: "Date",
          field: "balanceDate",
          width: "3%",
          sortable: true,
          isKey: true,
        },
        {
          label: "Balance",
          field: "overallBalance",
          width: "10%",
          sortable: true,
        },
        {
          label: "Num. Trades",
          field: "overallNumberOfTrades",
          width: "15%",
          sortable: true,
        },
      ],
      sortable: {
        order: "id",
        sort: "asc",
      },
      rows:store.journalBalances,
      total:store.journalTotal
    });
</script>

<template>
<JournalDetailModal v-if="selectedJournalBalance" :journalBalance="selectedJournalBalance" ref="detailModalComponent"  />

<VueTableLite
  class="table table-bordered border-primary"
  :columns="table.columns"
  @row-clicked="rowClicked"
  :is-static-mode="true"
  :rows="table.rows"
  :total="table.total"
  @is-finished="table.isLoading = false"
  ></VueTableLite>
</template>
<style scoped>

</style>
