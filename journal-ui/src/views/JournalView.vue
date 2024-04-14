<script setup lang="ts">
	import JournalTable from '../components/JournalTable.vue';
	import JournalCalendar from '../components/JournalCalendar.vue';
	import {ref} from 'vue'
import useJournalStore from '@/stores/journal';
	const isCalendarChecked =  ref(true);

	const journalStore = useJournalStore();
</script>

<template>

<div class="container-md p-2">
	<div class="row">
		<div class="row">
			<div class="col-10">
				<h3>Last Months <small class="text-body-secondary">Balances</small> </h3>
			</div>
		</div>
		<div class="row">
			<div class="col" v-for="monthBalance in journalStore.monthlyBalance" >
				<div :class="{ 'border-danger': monthBalance.monthBalance < 0,' border-success': monthBalance.monthBalance > 0 }" class="card  bg-light mb-3" style="max-width: 18rem;">
				<div class="card-header h5">{{monthBalance.monthName}}</div>
					<div class="card-body">
						${{ monthBalance.monthBalance }}	
					</div>
				</div>	
			</div>
		</div>
	</div>

	<div class="row p-2">
		<div class="col-10">
			<h3>Trades <small class="text-body-secondary">Journal</small> </h3>
		</div>
		<div class="col-2 mt-2">
			<div class="form-check form-switch">
				<input class="form-check-input" type="checkbox" role="switch" v-model="isCalendarChecked">
				<label class="form-check-label">Calendar</label>
			</div>
		</div>
	</div>
	<div class="row">
		<div v-show="isCalendarChecked" class="col">
			<JournalCalendar />
		</div>
		<div v-show="!isCalendarChecked" class="col">
			<JournalTable />
		</div>
	</div>
</div>

</template>

<style>

</style>
