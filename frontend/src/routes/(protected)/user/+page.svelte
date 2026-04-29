<script lang="ts">
  import { authStore } from '$lib/stores/authStore';
  import { bookingService } from '$lib/services/bookingService';
  import { onMount } from 'svelte';
  
  let stats = $state({
    total: 0,
    upcoming: 0,
    completed: 0
  });
  
  onMount(async () => {
    const res = await bookingService.getMyBookings();
    if (res.success && res.data) {
      stats.total = res.data.length;
      stats.upcoming = res.data.filter(b => b.status.toLowerCase() !== 'completed' && b.status.toLowerCase() !== 'cancelled').length;
      stats.completed = res.data.filter(b => b.status.toLowerCase() === 'completed').length;
    }
  });
</script>

<div class="space-y-6">
  <div class="bg-white p-8 rounded-2xl shadow-sm border border-stone-100">
    <h1 class="text-3xl font-[var(--font-headline)] text-emerald-900 mb-2">
      Welcome back, {$authStore?.username || 'Guest'}!
    </h1>
    <p class="text-stone-500">Here is an overview of your bookings and activity.</p>
  </div>

  <div class="grid grid-cols-1 md:grid-cols-3 gap-6">
    <div class="bg-white p-6 rounded-2xl shadow-sm border border-stone-100 flex items-center gap-4">
      <div class="w-12 h-12 rounded-full bg-blue-100 text-blue-600 flex items-center justify-center">
        <span class="material-symbols-outlined">receipt_long</span>
      </div>
      <div>
        <p class="text-stone-500 text-sm font-semibold uppercase tracking-wider">Total Bookings</p>
        <p class="text-2xl font-bold text-stone-800">{stats.total}</p>
      </div>
    </div>

    <div class="bg-white p-6 rounded-2xl shadow-sm border border-stone-100 flex items-center gap-4">
      <div class="w-12 h-12 rounded-full bg-amber-100 text-amber-600 flex items-center justify-center">
        <span class="material-symbols-outlined">schedule</span>
      </div>
      <div>
        <p class="text-stone-500 text-sm font-semibold uppercase tracking-wider">Upcoming Stays</p>
        <p class="text-2xl font-bold text-stone-800">{stats.upcoming}</p>
      </div>
    </div>

    <div class="bg-white p-6 rounded-2xl shadow-sm border border-stone-100 flex items-center gap-4">
      <div class="w-12 h-12 rounded-full bg-emerald-100 text-emerald-600 flex items-center justify-center">
        <span class="material-symbols-outlined">task_alt</span>
      </div>
      <div>
        <p class="text-stone-500 text-sm font-semibold uppercase tracking-wider">Completed</p>
        <p class="text-2xl font-bold text-stone-800">{stats.completed}</p>
      </div>
    </div>
  </div>
</div>
