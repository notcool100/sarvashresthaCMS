<script lang="ts">
    import { onMount } from 'svelte';

    let { data } = $props();
    let dashboard = $derived(data.dashboard);
    let error = $derived(data.error);

    function formatDate(dateString: string) {
        return new Date(dateString).toLocaleDateString('en-US', {
            month: 'short',
            day: 'numeric',
            year: 'numeric'
        });
    }

    function formatCurrency(amount: number) {
        return new Intl.NumberFormat('en-US', {
            style: 'currency',
            currency: 'USD'
        }).format(amount);
    }
</script>

{#if error}
    <div class="p-10 text-error font-bold">
        Error loading dashboard: {error}
    </div>
{:else if !dashboard}
    <div class="p-10 text-stone-500 font-medium animate-pulse">
        Loading Sanctuary Performance Metrics...
    </div>
{:else}
    <!-- Main Content -->
        <header class="mb-12 flex justify-between items-end">
            <div>
                <h2 class="font-headline text-4xl text-emerald-900 tracking-tight mb-2">Sanctuary Performance</h2>
                <p class="text-stone-500 font-medium">Real-time hospitality metrics for Annapurna Sanctuary rooms.</p>
            </div>
            <div class="flex items-center gap-4">
                <button class="px-6 py-3 rounded-xl bg-surface-container-low text-emerald-900 font-bold text-sm tracking-wide uppercase flex items-center gap-2 hover:bg-surface-container-high transition-colors">
                    <span class="material-symbols-outlined text-sm">calendar_today</span>
                    Last 30 Days
                </button>
                <button class="px-6 py-3 rounded-xl bg-primary text-white font-bold text-sm tracking-wide uppercase flex items-center gap-2 hover:translate-y-[-2px] transition-transform shadow-lg shadow-primary/10">
                    <span class="material-symbols-outlined text-sm">download</span>
                    Export Report
                </button>
            </div>
        </header>

        <!-- Stats Bento Grid -->
        <div class="grid grid-cols-1 md:grid-cols-3 gap-8 mb-12">
            <!-- Revenue Card -->
            <div class="bg-surface-container-lowest p-8 rounded-xl relative overflow-hidden group shadow-sm border border-stone-100">
                <div class="absolute top-0 right-0 p-4 opacity-10 group-hover:opacity-20 transition-opacity">
                    <span class="material-symbols-outlined text-6xl">payments</span>
                </div>
                <h3 class="font-sans text-xs tracking-widest uppercase font-bold text-stone-400 mb-4">Total Revenue</h3>
                <p class="font-headline text-4xl text-emerald-900 font-bold mb-2">{formatCurrency(dashboard.stats.totalRevenue)}</p>
                <div class="flex items-center gap-2 {dashboard.stats.revenueGrowthPercentage >= 0 ? 'text-emerald-600' : 'text-error'} font-bold text-sm">
                    <span class="material-symbols-outlined text-sm">{dashboard.stats.revenueGrowthPercentage >= 0 ? 'trending_up' : 'trending_down'}</span>
                    {dashboard.stats.revenueGrowthPercentage >= 0 ? '+' : ''}{dashboard.stats.revenueGrowthPercentage}% vs last month
                </div>
            </div>

            <!-- Occupancy Card -->
            <div class="bg-surface-container-lowest p-8 rounded-xl relative overflow-hidden group shadow-sm border border-stone-100">
                <div class="absolute top-0 right-0 p-4 opacity-10 group-hover:opacity-20 transition-opacity">
                    <span class="material-symbols-outlined text-6xl">bed</span>
                </div>
                <h3 class="font-sans text-xs tracking-widest uppercase font-bold text-stone-400 mb-4">Average Occupancy</h3>
                <p class="font-headline text-4xl text-emerald-900 font-bold mb-2">{dashboard.stats.averageOccupancy}%</p>
                <div class="flex items-center gap-2 text-emerald-600 font-bold text-sm">
                    <span class="material-symbols-outlined text-sm">trending_up</span>
                    +{dashboard.stats.occupancyGrowthPercentage}% vs last month
                </div>
            </div>

            <!-- Bookings Card -->
            <div class="bg-surface-container-lowest p-8 rounded-xl relative overflow-hidden group shadow-sm border border-stone-100">
                <div class="absolute top-0 right-0 p-4 opacity-10 group-hover:opacity-20 transition-opacity">
                    <span class="material-symbols-outlined text-6xl">event_available</span>
                </div>
                <h3 class="font-sans text-xs tracking-widest uppercase font-bold text-stone-400 mb-4">New Bookings</h3>
                <p class="font-headline text-4xl text-emerald-900 font-bold mb-2">{dashboard.stats.newBookings}</p>
                <div class="flex items-center gap-2 {dashboard.stats.bookingsGrowthPercentage >= 0 ? 'text-emerald-600' : 'text-error'} font-bold text-sm">
                    <span class="material-symbols-outlined text-sm">{dashboard.stats.bookingsGrowthPercentage >= 0 ? 'trending_up' : 'trending_down'}</span>
                    {dashboard.stats.bookingsGrowthPercentage >= 0 ? '+' : ''}{dashboard.stats.bookingsGrowthPercentage}% vs last month
                </div>
            </div>
        </div>

        <!-- Trend Chart & Recent Status -->
        <div class="grid grid-cols-1 lg:grid-cols-12 gap-8">
            <!-- Revenue Trend -->
            <div class="lg:col-span-8 bg-surface-container-lowest p-8 rounded-xl shadow-sm border border-stone-100">
                <div class="flex justify-between items-center mb-8">
                    <h3 class="font-headline text-2xl text-emerald-900">Revenue Growth Trend</h3>
                    <div class="flex gap-4">
                        <span class="flex items-center gap-2 text-xs font-bold uppercase tracking-wider text-stone-500">
                            <span class="w-3 h-3 rounded-full bg-emerald-900"></span> Current Year
                        </span>
                        <span class="flex items-center gap-2 text-xs font-bold uppercase tracking-wider text-stone-300">
                            <span class="w-3 h-3 rounded-full bg-stone-200"></span> Previous Year
                        </span>
                    </div>
                </div>
                
                <div class="h-80 w-full flex items-end justify-between gap-2 pt-10 px-4">
                    {#each dashboard.revenueTrend as item}
                        <div class="flex-1 flex flex-col items-center gap-2 h-full justify-end group relative">
                            <!-- Tooltip -->
                            <div class="absolute -top-10 bg-emerald-900 text-white text-[10px] px-2 py-1 rounded opacity-0 group-hover:opacity-100 transition-opacity whitespace-nowrap z-10 font-bold">
                                {formatCurrency(item.revenue)}
                            </div>
                            
                            <!-- Current Year Bar -->
                            <div class="w-full bg-emerald-900/10 rounded-t-lg relative flex items-end overflow-hidden" style="height: 100%">
                                <div class="w-full bg-emerald-900 rounded-t-sm transition-all duration-700 ease-out" style="height: {(item.revenue / Math.max(...dashboard.revenueTrend.map((t: any) => t.revenue))) * 100}%"></div>
                            </div>
                        </div>
                    {/each}
                </div>
                <div class="flex justify-between mt-6 px-4 text-xs font-bold text-stone-400 uppercase tracking-widest">
                    {#each dashboard.revenueTrend as item}
                        <span class="flex-1 text-center">{item.month}</span>
                    {/each}
                </div>
            </div>

            <!-- Recent Status -->
            <div class="lg:col-span-4 bg-surface-container-lowest p-8 rounded-xl shadow-sm border border-stone-100 flex flex-col">
                <h3 class="font-headline text-2xl text-emerald-900 mb-6">Recent Status</h3>
                <div class="space-y-6 flex-1">
                    {#each dashboard.recentStatus as room}
                        <div class="flex items-center justify-between group">
                            <div class="flex items-center gap-4">
                                <div class="w-12 h-12 rounded-lg overflow-hidden bg-stone-100 border border-stone-200">
                                    <img src={room.imageUrl} alt={room.name} class="w-full h-full object-cover" />
                                </div>
                                <div>
                                    <p class="text-sm font-bold text-on-surface">{room.name}</p>
                                    <p class="text-xs text-stone-500">{room.subtitle}</p>
                                </div>
                            </div>
                            <span class="px-2 py-1 text-[10px] font-bold uppercase rounded {room.status === 'Active' ? 'bg-emerald-100 text-emerald-800' : 'bg-amber-100 text-amber-800'}">
                                {room.status}
                            </span>
                        </div>
                    {/each}
                </div>
                <button class="mt-8 w-full py-4 border border-outline-variant text-stone-600 font-bold text-xs tracking-widest uppercase hover:bg-surface-container-high transition-colors rounded-lg">
                    Full Inventory Report
                </button>
            </div>
        </div>

        <!-- Latest Bookings Table -->
        <section class="mt-12 bg-surface-container-lowest rounded-xl overflow-hidden shadow-sm border border-stone-100">
            <div class="px-8 py-6 border-b border-surface-container flex justify-between items-center">
                <h3 class="font-headline text-2xl text-emerald-900">Latest Bookings</h3>
                <a class="text-secondary font-bold text-xs tracking-widest uppercase flex items-center gap-1 hover:text-emerald-700 transition-colors" href="/admin/bookings">
                    View All <span class="material-symbols-outlined text-sm">arrow_forward</span>
                </a>
            </div>
            <div class="overflow-x-auto">
                <table class="w-full text-left border-collapse">
                    <thead>
                        <tr class="bg-surface-container-low">
                            <th class="px-8 py-4 font-sans text-xs tracking-widest uppercase font-bold text-stone-400">Guest Name</th>
                            <th class="px-8 py-4 font-sans text-xs tracking-widest uppercase font-bold text-stone-400">Room Type</th>
                            <th class="px-8 py-4 font-sans text-xs tracking-widest uppercase font-bold text-stone-400">Check-in</th>
                            <th class="px-8 py-4 font-sans text-xs tracking-widest uppercase font-bold text-stone-400">Status</th>
                            <th class="px-8 py-4 font-sans text-xs tracking-widest uppercase font-bold text-stone-400 text-right">Amount</th>
                        </tr>
                    </thead>
                    <tbody class="divide-y divide-surface-container">
                        {#each dashboard.recentBookings as booking}
                            <tr class="hover:bg-surface-container-low/50 transition-colors">
                                <td class="px-8 py-5 text-sm font-bold text-on-surface">{booking.guestName}</td>
                                <td class="px-8 py-5 text-sm text-stone-600">{booking.roomName}</td>
                                <td class="px-8 py-5 text-sm text-stone-600">{formatDate(booking.checkIn)}</td>
                                <td class="px-8 py-5">
                                    <span class="flex items-center gap-2 text-xs font-bold {booking.status === 'Confirmed' || booking.status === 'Completed' ? 'text-emerald-700' : 'text-secondary'}">
                                        <span class="w-1.5 h-1.5 rounded-full {booking.status === 'Confirmed' || booking.status === 'Completed' ? 'bg-emerald-600' : 'bg-secondary'}"></span>
                                        {booking.status}
                                    </span>
                                </td>
                                <td class="px-8 py-5 text-sm font-bold text-on-surface text-right">{formatCurrency(booking.amount)}</td>
                            </tr>
                        {/each}
                    </tbody>
                </table>
            </div>
        </section>
{/if}

<style>
    :global(body) {
        background-color: #fcf9f4;
    }
</style>
