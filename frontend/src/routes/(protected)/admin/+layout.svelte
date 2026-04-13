<script lang="ts">
    let { children } = $props();
</script>

<nav class="h-screen w-64 fixed left-0 top-0 bg-stone-100 dark:bg-stone-950 flex flex-col py-6 z-50 overflow-y-auto">
    <div class="px-6 mb-10">
        <h1 class="font-headline text-xl text-emerald-900 dark:text-emerald-100 mb-1">CMS Portal</h1>
        <p class="font-sans text-sm tracking-wide uppercase font-bold text-stone-600 opacity-60">Room Management</p>
    </div>
    <div class="flex-1 space-y-1">
        <a class="bg-emerald-900 text-amber-400 rounded-lg mx-2 px-4 py-3 flex items-center gap-3 transition-all duration-300" href="/admin/dashboard">
            <span class="material-symbols-outlined">dashboard</span>
            <span class="font-sans text-sm tracking-wide uppercase font-bold">Overview</span>
        </a>
        <a class="text-stone-600 dark:text-stone-400 hover:bg-stone-200 dark:hover:bg-stone-900 mx-2 px-4 py-3 flex items-center gap-3 transition-transform hover:translate-x-1" href="/admin/rooms">
            <span class="material-symbols-outlined">bed</span>
            <span class="font-sans text-sm tracking-wide uppercase font-bold">Rooms</span>
        </a>
        <a class="text-stone-600 dark:text-stone-400 hover:bg-stone-200 dark:hover:bg-stone-900 mx-2 px-4 py-3 flex items-center gap-3 transition-transform hover:translate-x-1" href="/admin/bookings">
            <span class="material-symbols-outlined">calendar_month</span>
            <span class="font-sans text-sm tracking-wide uppercase font-bold">Bookings</span>
        </a>
        <a class="text-stone-600 dark:text-stone-400 hover:bg-stone-200 dark:hover:bg-stone-900 mx-2 px-4 py-3 flex items-center gap-3 transition-transform hover:translate-x-1" href="/admin/offers">
            <span class="material-symbols-outlined">sell</span>
            <span class="font-sans text-sm tracking-wide uppercase font-bold">Offers</span>
        </a>
        <a class="text-stone-600 dark:text-stone-400 hover:bg-stone-200 dark:hover:bg-stone-900 mx-2 px-4 py-3 flex items-center gap-3 transition-transform hover:translate-x-1" href="/admin/users">
            <span class="material-symbols-outlined">group</span>
            <span class="font-sans text-sm tracking-wide uppercase font-bold">Users</span>
        </a>
    </div>
    <div class="mt-auto border-t border-stone-200 dark:border-stone-800 pt-6">
        <div class="px-6 py-4 mt-2">
            <div class="flex items-center gap-3 mb-6">
                <div class="w-10 h-10 rounded-full bg-surface-container-highest overflow-hidden">
                    <img class="w-full h-full object-cover" src="https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?w=100&h=100&fit=crop" alt="Admin Profile" />
                </div>
                <div>
                    <p class="text-sm font-bold text-on-surface">Admin Profile</p>
                    <p class="text-xs text-stone-500">Super Admin</p>
                </div>
            </div>
            <button class="w-full py-3 px-4 bg-emerald-900 text-amber-400 rounded-lg text-xs font-bold tracking-widest uppercase hover:opacity-90 transition-opacity">
                View Live Site
            </button>
        </div>
    </div>
</nav>

<main class="ml-64 p-10 min-h-screen bg-surface">
    {@render children()}
</main>
