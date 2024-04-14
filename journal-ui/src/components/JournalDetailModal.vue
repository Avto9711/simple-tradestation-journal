<script setup lang="ts">
import {Modal} from 'bootstrap';
import type { JournalBalanceWithMergePositions } from "@/models";
import VueTableLite from "vue3-table-lite/ts";

import { onMounted, defineProps, reactive } from 'vue';

const { journalBalance} = defineProps<{journalBalance: JournalBalanceWithMergePositions | null}>()


export type JournalDetailComponent =  {
    toggleModal: () => void;
}

let modal:Modal;


onMounted(() => {
  modal = new Modal(document.getElementById('journalDetail') as HTMLElement,{backdrop:true})
})

const toggleModal = ()=>{
    modal.toggle();
}

defineExpose({toggleModal})

const table = reactive({
      isLoading: false,
      columns: [
        {
          label: "Type",
          field: "assetType",
          sortable: true,
        },
        {
          label: "Open Or Close",
          field: "openOrClose",
          sortable: true,
        },
        {
          label: "Ticker",
          field: "symbol",
          width: "10%",
          sortable: true,
        },
        {
          label: "Price",
          field: "executionPrice",
          sortable: true,
        },
        {
          label: "Qty",
          field: "quantity",
          sortable: true,
        },
        {
          label: "Commission",
          field: "commission",
          sortable: true,
        },
        {
          label: "Open Time",
          field: "openedDateTime",
          sortable: true,
          isKey: true,
        },
        {
          label: "Close Time",
          field: "closedDateTime",
          sortable: true,
        },
      ],
      sortable: {
        order: "id",
        sort: "asc",
      },
      rows: journalBalance?.mergedTradePositions ?? [],
      total:journalBalance?.mergedTradePositions.length ?? 0
  });

</script>
<template>
  <!-- Modal -->
  <div v-if="journalBalance" class="modal fade" id="journalDetail" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-xl">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Trade data for {{ journalBalance.balanceDate }}</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">

      <ul class="nav nav-tabs" id="myTab" role="tablist">
        <li class="nav-item" role="presentation">
          <button class="nav-link active" id="home-tab" data-bs-toggle="tab" data-bs-target="#home-tab-pane" type="button" role="tab" aria-controls="home-tab-pane" aria-selected="true">Overall</button>
        </li>
        <li class="nav-item" role="presentation">
          <button class="nav-link" id="profile-tab" data-bs-toggle="tab" data-bs-target="#profile-tab-pane" type="button" role="tab" aria-controls="profile-tab-pane" aria-selected="false">Trades</button>
        </li>
      </ul>
        <div class="tab-content" id="myTabContent">
          <div class="tab-pane fade show active" id="home-tab-pane" role="tabpanel" aria-labelledby="home-tab" tabindex="0">
            <div class="row">
                  <table class="table">
                  <thead>
                    <tr>
                      <th scope="col"></th>
                      <th scope="col">Option Trading</th>
                      <th scope="col">Stock Trading</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr>
                      <th scope="row">Num Trading</th>
                      <td>{{journalBalance.optionsEndingBalance.numberOfTrades}}</td>
                      <td>{{journalBalance.stocksEndingBalance.numberOfTrades}}</td>
                    </tr>
                    <tr>
                      <th scope="row">Balance</th>
                      <td>${{journalBalance.optionsEndingBalance.balance}}</td>
                      <td>${{journalBalance.stocksEndingBalance.balance}}</td>
                    </tr>

                    <tr>
                      <th scope="row">Buy</th>
                      <td>${{journalBalance.optionsEndingBalance.buyAmount}}</td>
                      <td>${{journalBalance.stocksEndingBalance.buyAmount}}</td>
                    </tr>
                    <tr>
                      <th scope="row">Sold</th>
                      <td>${{journalBalance.optionsEndingBalance.sellAmount}}</td>
                      <td>${{journalBalance.stocksEndingBalance.sellAmount}}</td>
                    </tr>
                    <tr>
                      <th scope="row">Commissions</th>
                      <td>${{journalBalance.optionsEndingBalance.commissions}}</td>
                      <td>${{journalBalance.stocksEndingBalance.commissions}}</td>
                    </tr>
                  </tbody>
                </table>
                </div>
                <div class="row p-2">
                  <div class="row">
                    Comments:
                  </div>
                  <div class="row">
                    <textarea v-model="journalBalance.comments"></textarea>

                  </div>
                </div>
          </div>
          <div class="tab-pane fade" id="profile-tab-pane" role="tabpanel" aria-labelledby="profile-tab" tabindex="0">
            <VueTableLite
              class="table table-bordered border-primary"
              :columns="table.columns"
              :is-static-mode="true"
              :rows="table.rows"
              :total="table.total"
              @is-finished="table.isLoading = false"
              groupingKey="symbol"
              :hasGroupToggle="true"
              :start-collapsed="false"
              :is-keep-collapsed="false"
              ></VueTableLite>
          </div>
        </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
      </div>
    </div>
  </div>
</div>


</template>