<script lang="ts">
    import { authStore } from '$lib/stores/authStore';
    import { goto } from '$app/navigation';
    let { children } = $props();

    function logout() {
        authStore.set(null);
        document.cookie = 'auth_data=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;';
        goto('/login');
    }
</script>

<nav class="h-screen w-64 fixed left-0 top-0 bg-stone-100 dark:bg-stone-950 flex flex-col py-6 z-50 overflow-y-auto">
    <div class="px-6 mb-10">
        <h1 class="font-headline text-xl text-emerald-900 dark:text-emerald-100 mb-1">CMS Portal</h1>
        <p class="font-sans text-sm tracking-wide uppercase font-bold text-stone-600 opacity-60">Room Management</p>
    </div>
    <div class="flex-1 space-y-1">
        <a class="text-stone-600 dark:text-stone-400 hover:bg-stone-200 dark:hover:bg-stone-900 mx-2 px-4 py-3 flex items-center gap-3 transition-transform hover:translate-x-1" href="/user">
            <span class="material-symbols-outlined">dashboard</span>
            <span class="font-sans text-sm tracking-wide uppercase font-bold">Overview</span>
        </a>
        <a class="text-stone-600 dark:text-stone-400 hover:bg-stone-200 dark:hover:bg-stone-900 mx-2 px-4 py-3 flex items-center gap-3 transition-transform hover:translate-x-1" href="/user/bookings">
            <span class="material-symbols-outlined">calendar_month</span>
            <span class="font-sans text-sm tracking-wide uppercase font-bold">My Bookings</span>
        </a>
    </div>
    <div class="mt-auto border-t border-stone-200 dark:border-stone-800 pt-6">
        <div class="px-6 py-4 mt-2">
            <div class="flex items-center gap-3 mb-6">
                <div class="w-10 h-10 rounded-full bg-emerald-100 text-emerald-800 flex items-center justify-center font-bold text-lg overflow-hidden">
                    {$authStore?.username?.charAt(0)?.toUpperCase() || 'U'}
                </div>
                <div>
                    <p class="text-sm font-bold text-stone-800 truncate w-32">{$authStore?.username || 'Guest User'}</p>
                    <p class="text-xs text-stone-500">Customer</p>
                </div>
            </div>
            <button onclick={logout} class="w-full mt-3 py-3 px-4 bg-red-900/10 text-red-600 rounded-lg text-xs font-bold tracking-widest uppercase hover:bg-red-900/20 transition-colors flex justify-center items-center gap-2">
                <span class="material-symbols-outlined text-sm">logout</span>
                Logout
            </button>
        </div>
    </div>
</nav>

<main class="ml-64 p-10 min-h-screen bg-surface">
    {@render children()}
</main>
