<script lang="ts">
    import { onMount } from 'svelte';
    import { offerService } from '$lib/services/offerService';
    import { roomService } from '$lib/services/roomService';
    import { fileService } from '$lib/services/fileService';
    import { PUBLIC_API_BASE_URL } from '$env/static/public';
    import type { Offer, OfferRequest, Room } from '$lib/types/api';

    let offers = $state<Offer[]>([]);
    let rooms = $state<Room[]>([]);
    let loading = $state(true);
    let error = $state('');
    let saving = $state(false);
    let editingId = $state<number | null>(null);
    let uploading = $state(false);

    const emptyForm: OfferRequest = {
        title: '',
        description: '',
        discountType: 'percent',
        discountValue: 0,
        startDate: new Date().toISOString().split('T')[0],
        endDate: new Date(Date.now() + 7 * 24 * 60 * 60 * 1000).toISOString().split('T')[0],
        isActive: true,
        appliesToAllRooms: true,
        roomIds: [],
        imageUrl: ''
    };

    let form = $state<OfferRequest>({ ...emptyForm });

    function resetForm() {
        form = { ...emptyForm };
        editingId = null;
    }

    async function loadData() {
        loading = true;
        error = '';
        try {
            const [offerRes, roomRes] = await Promise.all([
                offerService.getAll(),
                roomService.getAll()
            ]);

            if (offerRes.success && offerRes.data) {
                offers = offerRes.data;
            }
            if (roomRes.success && roomRes.data) {
                rooms = roomRes.data;
            }
        } catch (e) {
            error = 'Failed to load data';
        } finally {
            loading = false;
        }
    }

    function startEdit(offer: Offer) {
        editingId = offer.id;
        form = {
            title: offer.title,
            description: offer.description,
            discountType: offer.discountType,
            discountValue: offer.discountValue,
            startDate: offer.startDate.split('T')[0],
            endDate: offer.endDate.split('T')[0],
            isActive: offer.isActive,
            appliesToAllRooms: offer.appliesToAllRooms,
            roomIds: [...offer.roomIds],
            imageUrl: offer.imageUrl || ''
        };
    }

    async function saveOffer(e: SubmitEvent) {
        e.preventDefault();
        saving = true;
        error = '';
        try {
            if (editingId) {
                const response = await offerService.update(editingId, form);
                if (!response.success) {
                    error = response.message || 'Failed to update offer';
                    return;
                }
            } else {
                const response = await offerService.create(form);
                if (!response.success) {
                    error = response.message || 'Failed to create offer';
                    return;
                }
            }
            resetForm();
            await loadData();
        } catch (e) {
            error = 'Failed to save offer';
        } finally {
            saving = false;
        }
    }

    async function deleteOffer(id: number) {
        if (!window.confirm('Are you sure you want to delete this offer?')) return;
        try {
            const response = await offerService.remove(id);
            if (response.success) {
                offers = offers.filter(o => o.id !== id);
            } else {
                error = response.message || 'Failed to delete offer';
            }
        } catch (e) {
            error = 'Failed to delete offer';
        }
    }

    async function handleFileUpload(e: Event) {
        const input = e.target as HTMLInputElement;
        if (!input.files || input.files.length === 0) return;
        
        uploading = true;
        try {
            const response = await fileService.uploadOfferImage(input.files[0]);
            if (response.success && response.data) {
                form.imageUrl = response.data;
            } else {
                error = response.message || 'Upload failed';
            }
        } catch (err) {
            error = 'Upload failed';
        } finally {
            uploading = false;
            input.value = '';
        }
    }

    function getFullImageUrl(path: string) {
        if (!path) return '';
        if (path.startsWith('http')) return path;
        const base = PUBLIC_API_BASE_URL.replace('/api', '');
        return `${base}${path}`;
    }

    function toggleRoom(roomId: number) {
        if (form.roomIds.includes(roomId)) {
            form.roomIds = form.roomIds.filter(id => id !== roomId);
        } else {
            form.roomIds = [...form.roomIds, roomId];
        }
    }

    onMount(loadData);
</script>

<section class="space-y-10">
    <header class="flex items-end justify-between">
        <div>
            <h2 class="font-headline text-3xl text-emerald-900 tracking-tight mb-2">Special Offers</h2>
            <p class="text-stone-500 font-medium">Create and manage promotions for your rooms.</p>
        </div>
        <button
            class="px-6 py-3 rounded-xl bg-emerald-900 text-amber-400 font-bold text-sm tracking-wide uppercase hover:opacity-90 transition-opacity"
            onclick={resetForm}
        >
            New Offer
        </button>
    </header>

    {#if error}
        <div class="p-4 rounded-lg border border-rose-200 bg-rose-50 text-rose-700 text-sm font-semibold">
            {error}
        </div>
    {/if}

    <div class="grid grid-cols-1 lg:grid-cols-[1.2fr_0.8fr] gap-8">
        <!-- Offers List -->
        <section class="bg-surface-container-lowest rounded-xl overflow-hidden shadow-sm border border-stone-100">
            <div class="px-6 py-4 border-b border-surface-container flex items-center justify-between">
                <h3 class="font-headline text-xl text-emerald-900">Current Offers</h3>
                {#if loading}
                    <span class="text-xs text-stone-400">Loading...</span>
                {/if}
            </div>
            <div class="overflow-x-auto">
                <table class="w-full text-left border-collapse">
                    <thead>
                        <tr class="bg-surface-container-low">
                            <th class="px-6 py-3 text-xs tracking-widest uppercase font-bold text-stone-400">Offer</th>
                            <th class="px-6 py-3 text-xs tracking-widest uppercase font-bold text-stone-400">Discount</th>
                            <th class="px-6 py-3 text-xs tracking-widest uppercase font-bold text-stone-400">Validity</th>
                            <th class="px-6 py-3 text-xs tracking-widest uppercase font-bold text-stone-400">Status</th>
                            <th class="px-6 py-3 text-xs tracking-widest uppercase font-bold text-stone-400 text-right">Actions</th>
                        </tr>
                    </thead>
                    <tbody class="divide-y divide-surface-container">
                        {#if !loading && offers.length === 0}
                            <tr>
                                <td class="px-6 py-10 text-sm text-stone-500 text-center" colspan="5">No active offers.</td>
                            </tr>
                        {:else}
                            {#each offers as offer}
                                <tr class="hover:bg-surface-container-low/50 transition-colors">
                                    <td class="px-6 py-4">
                                        <div class="flex items-center gap-3">
                                            {#if offer.imageUrl}
                                                <img src={offer.imageUrl} alt={offer.title} class="w-10 h-10 rounded object-cover border border-stone-100" />
                                            {/if}
                                            <div>
                                                <div class="text-sm font-bold text-on-surface">{offer.title}</div>
                                                <div class="text-xs text-stone-500 line-clamp-1">{offer.description}</div>
                                            </div>
                                        </div>
                                    </td>
                                    <td class="px-6 py-4 text-sm text-stone-600 font-medium">
                                        {offer.discountValue}{offer.discountType === 'percent' ? '%' : '$'}
                                    </td>
                                    <td class="px-6 py-4 text-xs text-stone-500 uppercase tracking-tighter">
                                        {offer.startDate.split('T')[0]} - {offer.endDate.split('T')[0]}
                                    </td>
                                    <td class="px-6 py-4">
                                        <span class="px-2 py-1 text-[10px] font-bold uppercase rounded {offer.isActive ? 'bg-emerald-100 text-emerald-800' : 'bg-stone-100 text-stone-600'}">
                                            {offer.isActive ? 'Active' : 'Inactive'}
                                        </span>
                                    </td>
                                    <td class="px-6 py-4 text-right">
                                        <div class="flex items-center justify-end gap-2">
                                            <button
                                                class="px-3 py-2 text-xs font-bold uppercase rounded bg-surface-container-high text-emerald-900 hover:bg-surface-container-highest transition-colors"
                                                onclick={() => startEdit(offer)}
                                            >
                                                Edit
                                            </button>
                                            <button
                                                class="px-3 py-2 text-xs font-bold uppercase rounded bg-rose-50 text-rose-700 hover:bg-rose-100 transition-colors"
                                                onclick={() => deleteOffer(offer.id)}
                                            >
                                                Delete
                                            </button>
                                        </div>
                                    </td>
                                </tr>
                            {/each}
                        {/if}
                    </tbody>
                </table>
            </div>
        </section>

        <!-- Create/Edit Form -->
        <aside class="bg-surface-container-lowest rounded-xl shadow-sm border border-stone-100 p-6 h-fit sticky top-6">
            <h3 class="font-headline text-xl text-emerald-900 mb-6">{editingId ? 'Edit Offer' : 'Create New Offer'}</h3>
            <form onsubmit={saveOffer} class="space-y-5">
                <div>
                    <label for="offer-title" class="block text-xs font-bold tracking-widest uppercase text-stone-400 mb-2">Title</label>
                    <input
                        id="offer-title"
                        class="w-full rounded-lg border border-stone-200 bg-white px-3 py-2 text-sm focus:border-emerald-500 focus:ring-1 focus:ring-emerald-500 outline-none transition-all"
                        bind:value={form.title}
                        placeholder="Summer Sale 2024"
                        required
                    />
                </div>

                <div>
                    <label for="offer-desc" class="block text-xs font-bold tracking-widest uppercase text-stone-400 mb-2">Description</label>
                    <textarea
                        id="offer-desc"
                        class="w-full rounded-lg border border-stone-200 bg-white px-3 py-2 text-sm min-h-[80px]"
                        bind:value={form.description}
                        placeholder="Get 20% off on all mountain view rooms..."
                    ></textarea>
                </div>

                <div class="grid grid-cols-2 gap-4">
                    <div>
                        <label for="discount-type" class="block text-xs font-bold tracking-widest uppercase text-stone-400 mb-2">Type</label>
                        <select
                            id="discount-type"
                            class="w-full rounded-lg border border-stone-200 bg-white px-3 py-2 text-sm"
                            bind:value={form.discountType}
                        >
                            <option value="percent">Percentage (%)</option>
                            <option value="fixed">Fixed Amount ($)</option>
                        </select>
                    </div>
                    <div>
                        <label for="discount-val" class="block text-xs font-bold tracking-widest uppercase text-stone-400 mb-2">Value</label>
                        <input
                            id="discount-val"
                            type="number"
                            class="w-full rounded-lg border border-stone-200 bg-white px-3 py-2 text-sm"
                            bind:value={form.discountValue}
                            required
                        />
                    </div>
                </div>

                <div class="grid grid-cols-2 gap-4">
                    <div>
                        <label for="start-date" class="block text-xs font-bold tracking-widest uppercase text-stone-400 mb-2">Start Date</label>
                        <input
                            id="start-date"
                            type="date"
                            class="w-full rounded-lg border border-stone-200 bg-white px-3 py-2 text-sm"
                            bind:value={form.startDate}
                            required
                        />
                    </div>
                    <div>
                        <label for="end-date" class="block text-xs font-bold tracking-widest uppercase text-stone-400 mb-2">End Date</label>
                        <input
                            id="end-date"
                            type="date"
                            class="w-full rounded-lg border border-stone-200 bg-white px-3 py-2 text-sm"
                            bind:value={form.endDate}
                            required
                        />
                    </div>
                </div>

                <div>
                    <label for="offer-image" class="block text-xs font-bold tracking-widest uppercase text-stone-400 mb-2">Banner Image</label>
                    <div class="space-y-4">
                        {#if form.imageUrl}
                            <div class="relative group aspect-video rounded-xl overflow-hidden border border-stone-100 shadow-sm bg-stone-50">
                                <img src={getFullImageUrl(form.imageUrl)} alt="Banner Preview" class="w-full h-full object-cover" />
                                <button 
                                    type="button"
                                    class="absolute top-2 right-2 p-1.5 bg-rose-500 text-white rounded-full opacity-0 group-hover:opacity-100 transition-opacity"
                                    onclick={() => form.imageUrl = ''}
                                >
                                    <svg xmlns="http://www.w3.org/2000/svg" class="h-4 w-4" viewBox="0 0 20 20" fill="currentColor">
                                        <path fill-rule="evenodd" d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z" clip-rule="evenodd" />
                                    </svg>
                                </button>
                            </div>
                        {/if}
                        
                        <label class="cursor-pointer border-2 border-dashed border-stone-200 rounded-xl py-8 flex flex-col items-center justify-center hover:border-emerald-300 hover:bg-emerald-50 transition-all">
                            <input type="file" accept="image/*" class="hidden" onchange={handleFileUpload} disabled={uploading} />
                            {#if uploading}
                                <div class="flex items-center gap-2 text-emerald-600 font-bold text-xs uppercase tracking-widest animate-pulse">
                                    Uploading...
                                </div>
                            {:else}
                                <div class="flex flex-col items-center gap-2">
                                    <span class="text-2xl text-stone-300">📁</span>
                                    <span class="text-[10px] font-bold uppercase text-stone-400 tracking-widest">
                                        {form.imageUrl ? 'Change Banner' : 'Upload Banner Image'}
                                    </span>
                                </div>
                            {/if}
                        </label>
                    </div>
                </div>

                <div class="space-y-4 pt-2">
                    <label class="flex items-center gap-3 text-sm font-medium text-emerald-900 cursor-pointer">
                        <input type="checkbox" class="w-4 h-4 rounded border-stone-300 text-emerald-600 focus:ring-emerald-500" bind:checked={form.isActive} />
                        Active and visible
                    </label>

                    <div class="bg-surface-container-low p-4 rounded-xl space-y-4">
                        <label class="flex items-center gap-3 text-sm font-medium text-emerald-900 cursor-pointer">
                            <input type="checkbox" class="w-4 h-4 rounded border-stone-300 text-emerald-600 focus:ring-emerald-500" bind:checked={form.appliesToAllRooms} />
                            Apply to all rooms
                        </label>

                        {#if !form.appliesToAllRooms}
                            <div class="pt-2 border-t border-surface-container border-dashed">
                                <span class="block text-[10px] font-bold uppercase text-stone-400 mb-3 tracking-widest inline-block bg-white px-2 rounded -mt-6 ml-2">Select Rooms</span>
                                <div class="max-h-[150px] overflow-y-auto space-y-2 pr-2 custom-scrollbar">
                                    {#each rooms as room}
                                        <button
                                            type="button"
                                            class="w-full text-left px-3 py-2 rounded-lg text-xs font-semibold transition-all {form.roomIds.includes(room.id) ? 'bg-emerald-900 text-amber-400 shadow-md' : 'bg-white text-stone-600 border border-stone-100 hover:border-emerald-200'}"
                                            onclick={() => toggleRoom(room.id)}
                                        >
                                            {room.name}
                                            {#if form.roomIds.includes(room.id)}
                                                <span class="float-right">✓</span>
                                            {/if}
                                        </button>
                                    {/each}
                                </div>
                            </div>
                        {/if}
                    </div>
                </div>

                <div class="flex items-center gap-3 pt-4">
                    <button
                        type="submit"
                        class="flex-1 py-3 rounded-xl bg-emerald-900 text-amber-400 font-bold text-xs tracking-widest uppercase shadow-lg shadow-emerald-900/10 hover:shadow-emerald-900/20 active:scale-[0.98] transition-all disabled:opacity-60"
                        disabled={saving}
                    >
                        {saving ? 'Saving...' : editingId ? 'Update Offer' : 'Create Offer'}
                    </button>
                    {#if editingId}
                        <button
                            type="button"
                            class="px-5 py-3 rounded-xl border border-stone-200 text-xs font-bold tracking-widest uppercase text-stone-600 hover:bg-stone-50 transition-colors"
                            onclick={resetForm}
                        >
                            Cancel
                        </button>
                    {/if}
                </div>
            </form>
        </aside>
    </div>
</section>

<style>
    .custom-scrollbar::-webkit-scrollbar {
        width: 4px;
    }
    .custom-scrollbar::-webkit-scrollbar-track {
        background: transparent;
    }
    .custom-scrollbar::-webkit-scrollbar-thumb {
        background: #e5e7eb;
        border-radius: 10px;
    }
</style>
