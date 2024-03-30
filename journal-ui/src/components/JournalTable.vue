<script setup lang="ts">
 import useJournalStore from '@/stores/journal'
 import VueTableLite from "vue3-table-lite/ts";
 import { reactive } from 'vue';

 const store = useJournalStore();

 // Fake Data for 'asc' sortable
const sampleData1 = (offst, limit) => {
  offst = offst + 1;
  let data = [];
  for (let i = offst; i <= limit; i++) {
    data.push({
      id: i,
      name: "TEST" + i,
      email: "test" + i + "@example.com",
    });
  }
  return data;
};

// Fake Data for 'desc' sortable
const sampleData2 = (offst, limit) => {
  let data = [];
  for (let i = limit; i > offst; i--) {
    data.push({
      id: i,
      name: "TEST" + i,
      email: "test" + i + "@example.com",
    });
  }
  return data;
};

 const table = reactive({
      isLoading: false,
      columns: [
        {
          label: "ID",
          field: "id",
          width: "3%",
          sortable: true,
          isKey: true,
        },
        {
          label: "Name",
          field: "name",
          width: "10%",
          sortable: true,
        },
        {
          label: "Email",
          field: "email",
          width: "15%",
          sortable: true,
        },
      ],
      rows: [],
      totalRecordCount: 0,
      sortable: {
        order: "id",
        sort: "asc",
      },
    });

    /**
     * Search Event
     */
    const doSearch = (offset, limit, order, sort) => {
      table.isLoading = true;
      setTimeout(() => {
        table.isReSearch = offset == undefined ? true : false;
        if (offset >= 10 || limit >= 20) {
          limit = 20;
        }
        if (sort == "asc") {
          table.rows = sampleData1(offset, limit);
        } else {
          table.rows = sampleData2(offset, limit);
        }
        table.totalRecordCount = 20;
        table.sortable.order = order;
        table.sortable.sort = sort;
      }, 600);
    };

    // First get data
    doSearch(0, 10, "id", "asc");
  

</script>

<template>
	<div class="container-md">
		<div class="row p-2">
			<div class="col-10">
				<h3>Weekly | Monthly <small class="text-body-secondary">Journal</small> </h3>
			</div>
			<div class="col-2 mt-2">
				<div class="form-check form-switch">
					<input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckChecked" checked>
					<label class="form-check-label" for="flexSwitchCheckChecked">Monthly</label>
				</div>
			</div>
		</div>
		<div class="row">
			<div class="col">
				<VueTableLite
					class="table table-bordered border-primary"
					:is-loading="table.isLoading"
					:columns="table.columns"
					:rows="table.rows"
					:total="table.totalRecordCount"
					:sortable="table.sortable"
					:messages="table.messages"
					@do-search="doSearch"
					@is-finished="table.isLoading = false"
					></VueTableLite>
			</div>
		</div>


	</div>

</template>
<style scoped>

</style>
