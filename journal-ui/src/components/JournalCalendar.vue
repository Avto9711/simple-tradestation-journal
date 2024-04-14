<script setup lang="ts">
import FullCalendar from '@fullcalendar/vue3'
import dayGridPlugin from '@fullcalendar/daygrid'
import interactionPlugin from '@fullcalendar/interaction'
import type { CalendarOptions, EventClickArg, EventInput } from '@fullcalendar/core/index.js';
import useJournalStore from '@/stores/journal';
import {reactive, ref, watch} from 'vue'
import type { JournalBalance, JournalBalanceWithMergePositions } from '@/models';
import JournalDetailModal, { type JournalDetailComponent } from '@/components/JournalDetailModal.vue'

const transformJournalBalancesToEventsObject = (journalBalances:JournalBalance[]):EventInput[] => {

return journalBalances.map(journalBalance => {
  const textColor = journalBalance.overallBalance > 0 ? 'green': 'red'

  const eventInput: EventInput = { 
            title: journalBalance.overallBalance.toString(), 
            start: journalBalance.balanceDate, 
            classNames:['align-middle'],
            display:'list',
            backgroundColor:'white',
            textColor:textColor,
            borderColor:'white',
            extendedProps:journalBalance
          }
          return eventInput;
}) as EventInput[]
};

const journalStore = useJournalStore();

let journalBalances =  ref<EventInput>(transformJournalBalancesToEventsObject(journalStore.journalBalancesMergedTrades));
const detailModalComponent = ref<JournalDetailComponent>();

watch(journalStore.$state,(value)=>{
debugger
  
  const newJournalBalances = value.journalBalances;
  if(newJournalBalances.length){
    journalBalances.value = transformJournalBalancesToEventsObject(newJournalBalances);
    console.log(newJournalBalances)
  }else{
    journalBalances.value = [];
  }
})




let selectedJournalBalance = ref<JournalBalanceWithMergePositions | null>(null);

const calendarOptions:CalendarOptions =  reactive({
        plugins: [ dayGridPlugin, interactionPlugin ],
        initialView: 'dayGridMonth',
        headerToolbar: {
          left: '',
          center: 'title',
        },
        weekends:false,
        eventClick:(selectionInfo:EventClickArg)=>{
          selectedJournalBalance.value = selectionInfo.event.extendedProps as JournalBalanceWithMergePositions;
          detailModalComponent?.value?.toggleModal()
        },
        events: journalBalances
        });
</script>

<template>
  
<JournalDetailModal v-if="selectedJournalBalance" :journalBalance="selectedJournalBalance" ref="detailModalComponent"  />
  <FullCalendar :options="calendarOptions" >
    <template v-slot:eventContent='arg'>
      <p class="fs-3 fw-bold">${{ arg.event.extendedProps.overallBalance }}</p>
      <p class="fs-6 fw-lighter">Num of Trades: {{ arg.event.extendedProps.overallNumberOfTrades }}</p>
    </template>
    </FullCalendar>
</template>