<script setup lang="ts">
import {Modal} from 'bootstrap';
import type { JournalBalance } from "@/models";

import { onMounted, defineProps } from 'vue';

defineProps<{journalBalance: JournalBalance | null}>()


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


</script>
<template>
  <!-- Modal -->
  <div v-if="journalBalance" class="modal fade" id="journalDetail" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Trade data for {{ journalBalance.balanceDate }}</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
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
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
      </div>
    </div>
  </div>



</div>


</template>