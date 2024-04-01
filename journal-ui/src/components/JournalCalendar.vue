<script setup lang="ts">
import FullCalendar from '@fullcalendar/vue3'
import dayGridPlugin from '@fullcalendar/daygrid'
import interactionPlugin from '@fullcalendar/interaction'
import type { CalendarOptions, EventApi, EventClickArg, EventInput } from '@fullcalendar/core/index.js';
import {Modal} from 'bootstrap';

import {reactive, onMounted, ref} from 'vue'

let todayStr = new Date().toISOString().replace(/T.*$/, '')
let myModal:any | null = null;

onMounted(() => {
  myModal = new Modal(document.getElementById('exampleModal'),{backdrop:true})
})
let selectedJournalBalance = ref<Record<string, any>>({optionsEndingBalance:{},stocksEndingBalance:{}});

const calendarOptions:CalendarOptions =  reactive({
        plugins: [ dayGridPlugin, interactionPlugin ],
        initialView: 'dayGridMonth',
        headerToolbar: {
          left: '',
          center: 'title',
        },
        weekends:false,
        eventClick:(selectionInfo:EventClickArg)=>{
          console.log(selectionInfo)
          selectedJournalBalance.value = selectionInfo.event.extendedProps;
          myModal?.toggle()
        },
        events:(info, successCallback, failureCallBack)=>{

          const tradeBalance = {balanceDate:'2024-03-01', optionsEndingBalance: {balance:100, numberOfTrades: 1}, stocksEndingBalance:{balance:100, numberOfTrades: 1}, overallBalance:200, overallNumberOfTrades:2 };
          const textColor = tradeBalance.overallBalance > 0 ? 'green': 'red'
          const events:EventInput[] = [
            { 
              title: tradeBalance.overallBalance.toString(), 
              start: '2024-03-01', 
              classNames:['align-middle'],
              display:'list',
              backgroundColor:'white',
              textColor:textColor,
              borderColor:'white',
              extendedProps:tradeBalance
            }
          ]
          successCallback(events);
        }
        });
</script>

<template>
  <!-- Modal -->
<div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Trade data for {{ selectedJournalBalance.balanceDate }}</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
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
              <th scope="row">Balance</th>
              <td>${{selectedJournalBalance.optionsEndingBalance.balance}}</td>
              <td>${{selectedJournalBalance.stocksEndingBalance.balance}}</td>
            </tr>
            <tr>
              <th scope="row">Num Trading</th>
              <td>{{selectedJournalBalance.optionsEndingBalance.numberOfTrades}}</td>
              <td>{{selectedJournalBalance.stocksEndingBalance.numberOfTrades}}</td>
            </tr>
            <tr>
              <th scope="row">Buy</th>
              <td>${{selectedJournalBalance.optionsEndingBalance.numberOfTrades}}</td>
              <td>${{selectedJournalBalance.stocksEndingBalance.numberOfTrades}}</td>
            </tr>
            <tr>
              <th scope="row">Sold</th>
              <td>${{selectedJournalBalance.optionsEndingBalance.numberOfTrades}}</td>
              <td>${{selectedJournalBalance.stocksEndingBalance.numberOfTrades}}</td>
            </tr>
          </tbody>
        </table>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>
      </div>
    </div>
  </div>
</div>
  <FullCalendar :options="calendarOptions" >
    <template v-slot:eventContent='arg'>
      <p class="fs-3 fw-bold">${{ arg.event.extendedProps.overallBalance }}</p>
      <p class="fs-6 fw-lighter">Num of Trades: {{ arg.event.extendedProps.overallNumberOfTrades }}</p>
    </template>
    </FullCalendar>
</template>