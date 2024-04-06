<script setup lang="ts">
 import useJournalStore from '@/stores/journal'
 import VueTableLite from "vue3-table-lite/ts";
 import { computed, reactive } from 'vue';

 const store = useJournalStore();


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
    });
</script>

<template>
				<VueTableLite
					class="table table-bordered border-primary"
					:is-loading="table.isLoading"
					:columns="table.columns"
					:rows="store.journalBalances"
					:total="store.journalTotal"
					></VueTableLite>
</template>
<style scoped>

</style>
