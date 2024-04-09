<script setup lang="ts">
	import JournalTable from '../components/JournalTable.vue';
	import JournalCalendar from '../components/JournalCalendar.vue';
	import {ref} from 'vue'
	const isCalendarChecked =  ref(true);
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
			<div class="col">
				<div class="card border-danger bg-light mb-3" style="max-width: 18rem;">
				<div class="card-header h5">July</div>
					<div class="card-body">
						<h1 class="display-4">$45</h1>
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
