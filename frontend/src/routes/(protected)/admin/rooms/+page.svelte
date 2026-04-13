<script lang="ts">
    import { onMount } from 'svelte';
    import { roomService } from '$lib/services/roomService';
    import { fileService } from '$lib/services/fileService';
    import { PUBLIC_API_BASE_URL } from '$env/static/public';
    import type { Room, RoomCreateRequest } from '$lib/types/api';

    let rooms = $state<Room[]>([]);
    let loading = $state(true);
    let error = $state('');
    let saving = $state(false);
    let editingId = $state<number | null>(null);
    let uploading = $state(false);

    const emptyForm: RoomCreateRequest = {
        name: '',
        location: '',
        description: '',
        pricePerNight: 0,
        capacity: 1,
        isAvailable: true,
        viewType: '',
        amenities: [],
        imageUrls: [],
        bedType: '',
        sizeSqFt: 0
    };

    let form = $state<RoomCreateRequest>({ ...emptyForm });

    function resetForm() {
        form = { ...emptyForm };
        editingId = null;
    }

    async function loadRooms() {
        loading = true;
        error = '';
        try {
            const response = await roomService.getAll();
            if (response.success && response.data) {
                rooms = response.data;
            } else {
                error = response.message || 'Failed to load rooms';
            }
        } catch (e) {
            error = 'Failed to load rooms';
        } finally {
            loading = false;
        }
    }

    function startEdit(room: Room) {
        editingId = room.id;
        form = {
            name: room.name,
            location: room.location,
            description: room.description,
            pricePerNight: room.pricePerNight,
            capacity: room.capacity,
            isAvailable: room.isAvailable,
            viewType: room.viewType,
            amenities: [...room.amenities],
            imageUrls: [...room.imageUrls],
            bedType: room.bedType,
            sizeSqFt: room.sizeSqFt
        };
    }

    async function saveRoom(e: SubmitEvent) {
        e.preventDefault();
        saving = true;
        error = '';
        try {
            if (editingId) {
                const response = await roomService.update({ id: editingId, ...form });
                if (!response.success) {
                    error = response.message || 'Failed to update room';
                    return;
                }
            } else {
                const response = await roomService.create(form);
                if (!response.success) {
                    error = response.message || 'Failed to create room';
                    return;
                }
            }
            resetForm();
            await loadRooms();
        } catch (e) {
            error = 'Failed to save room';
        } finally {
            saving = false;
        }
    }

    async function deleteRoom(id: number) {
        const confirmed = window.confirm('Delete this room? This cannot be undone.');
        if (!confirmed) return;
        try {
            const response = await roomService.remove(id);
            if (!response.success) {
                error = response.message || 'Failed to delete room';
                return;
            }
            rooms = rooms.filter((r) => r.id !== id);
        } catch (e) {
            error = 'Failed to delete room';
        }
    }

    async function handleFileUpload(e: Event) {
        const input = e.target as HTMLInputElement;
        if (!input.files || input.files.length === 0) return;
        
        uploading = true;
        try {
            const response = await fileService.uploadRoomImages(input.files);
            if (response.success && response.data) {
                form.imageUrls = [...form.imageUrls, ...response.data];
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

    function removeImage(url: string) {
        form.imageUrls = form.imageUrls.filter(u => u !== url);
    }

    function getFullImageUrl(path: string) {
        if (!path) return '';
        if (path.startsWith('http')) return path;
        // PUBLIC_API_BASE_URL usually ends in /api, so we need to adjust
        const base = PUBLIC_API_BASE_URL.replace('/api', '');
        return `${base}${path}`;
    }

    onMount(loadRooms);
</script>

<section class="space-y-10">
    <header class="flex items-end justify-between">
        <div>
            <h2 class="font-headline text-3xl text-emerald-900 tracking-tight mb-2">Rooms</h2>
            <p class="text-stone-500 font-medium">Manage your room inventory and availability.</p>
        </div>
        <button
            class="px-6 py-3 rounded-xl bg-emerald-900 text-amber-400 font-bold text-sm tracking-wide uppercase hover:opacity-90 transition-opacity"
            onclick={resetForm}
        >
            New Room
        </button>
    </header>

    {#if error}
        <div class="p-4 rounded-lg border border-rose-200 bg-rose-50 text-rose-700 text-sm font-semibold">
            {error}
        </div>
    {/if}

    <div class="grid grid-cols-1 lg:grid-cols-[1.2fr_0.8fr] gap-8">
        <section class="bg-surface-container-lowest rounded-xl overflow-hidden shadow-sm border border-stone-100">
            <div class="px-6 py-4 border-b border-surface-container flex items-center justify-between">
                <h3 class="font-headline text-xl text-emerald-900">Room List</h3>
                {#if loading}
                    <span class="text-xs text-stone-400">Loading...</span>
                {/if}
            </div>
            <div class="overflow-x-auto">
                <table class="w-full text-left border-collapse">
                    <thead>
                        <tr class="bg-surface-container-low">
                            <th class="px-6 py-3 text-xs tracking-widest uppercase font-bold text-stone-400">Name</th>
                            <th class="px-6 py-3 text-xs tracking-widest uppercase font-bold text-stone-400">Location</th>
                            <th class="px-6 py-3 text-xs tracking-widest uppercase font-bold text-stone-400">Price</th>
                            <th class="px-6 py-3 text-xs tracking-widest uppercase font-bold text-stone-400">Capacity</th>
                            <th class="px-6 py-3 text-xs tracking-widest uppercase font-bold text-stone-400">Status</th>
                            <th class="px-6 py-3 text-xs tracking-widest uppercase font-bold text-stone-400 text-right">Actions</th>
                        </tr>
                    </thead>
                    <tbody class="divide-y divide-surface-container">
                        {#if !loading && rooms.length === 0}
                            <tr>
                                <td class="px-6 py-6 text-sm text-stone-500" colspan="6">No rooms yet.</td>
                            </tr>
                        {:else}
                            {#each rooms as room}
                                <tr class="hover:bg-surface-container-low/50 transition-colors">
                                    <td class="px-6 py-4 text-sm font-semibold text-on-surface">{room.name}</td>
                                    <td class="px-6 py-4 text-sm text-stone-600">{room.location}</td>
                                    <td class="px-6 py-4 text-sm text-stone-600">${room.pricePerNight}</td>
                                    <td class="px-6 py-4 text-sm text-stone-600">{room.capacity}</td>
                                    <td class="px-6 py-4">
                                        <span class="px-2 py-1 text-[10px] font-bold uppercase rounded {room.isAvailable ? 'bg-emerald-100 text-emerald-800' : 'bg-amber-100 text-amber-800'}">
                                            {room.isAvailable ? 'Available' : 'Unavailable'}
                                        </span>
                                    </td>
                                    <td class="px-6 py-4 text-right">
                                        <div class="flex items-center justify-end gap-2">
                                            <button
                                                class="px-3 py-2 text-xs font-bold uppercase rounded bg-surface-container-high text-emerald-900 hover:bg-surface-container-highest"
                                                onclick={() => startEdit(room)}
                                            >
                                                Edit
                                            </button>
                                            <button
                                                class="px-3 py-2 text-xs font-bold uppercase rounded bg-rose-50 text-rose-700 hover:bg-rose-100"
                                                onclick={() => deleteRoom(room.id)}
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

        <aside class="bg-surface-container-lowest rounded-xl shadow-sm border border-stone-100 p-6">
            <h3 class="font-headline text-xl text-emerald-900 mb-4">{editingId ? 'Edit Room' : 'Create Room'}</h3>
            <form class="space-y-4" onsubmit={saveRoom}>
                <div>
                    <label for="room-name" class="block text-xs font-bold tracking-widest uppercase text-stone-400 mb-2">Name</label>
                    <input
                        id="room-name"
                        class="w-full rounded-lg border border-stone-200 bg-white px-3 py-2 text-sm"
                        bind:value={form.name}
                        placeholder="Room name"
                        required
                    />
                </div>
                <div>
                    <label for="room-location" class="block text-xs font-bold tracking-widest uppercase text-stone-400 mb-2">Location</label>
                    <input
                        id="room-location"
                        class="w-full rounded-lg border border-stone-200 bg-white px-3 py-2 text-sm"
                        bind:value={form.location}
                        placeholder="Location"
                        required
                    />
                </div>
                <div>
                    <label for="room-description" class="block text-xs font-bold tracking-widest uppercase text-stone-400 mb-2">Description</label>
                    <textarea
                        id="room-description"
                        class="w-full rounded-lg border border-stone-200 bg-white px-3 py-2 text-sm min-h-[96px]"
                        bind:value={form.description}
                        placeholder="Detailed room description"
                    ></textarea>
                </div>
                <div class="grid grid-cols-2 gap-4">
                    <div>
                        <label for="room-view" class="block text-xs font-bold tracking-widest uppercase text-stone-400 mb-2">View Type</label>
                        <select
                            id="room-view"
                            class="w-full rounded-lg border border-stone-200 bg-white px-3 py-2 text-sm"
                            bind:value={form.viewType}
                        >
                            <option value="">Select View</option>
                            <option value="Mountain">Mountain View</option>
                            <option value="Forest">Forest View</option>
                            <option value="Garden">Garden View</option>
                            <option value="River">River View</option>
                        </select>
                    </div>
                    <div>
                        <label for="room-bed" class="block text-xs font-bold tracking-widest uppercase text-stone-400 mb-2">Bed Type</label>
                        <select
                            id="room-bed"
                            class="w-full rounded-lg border border-stone-200 bg-white px-3 py-2 text-sm"
                            bind:value={form.bedType}
                        >
                            <option value="">Select Bed</option>
                            <option value="Single">Single Bed</option>
                            <option value="Double">Double Bed</option>
                            <option value="Queen">Queen Size</option>
                            <option value="King">King Size</option>
                            <option value="Twin">Twin Beds</option>
                        </select>
                    </div>
                </div>
                <div class="grid grid-cols-2 gap-4">
                    <div>
                        <label for="room-size" class="block text-xs font-bold tracking-widest uppercase text-stone-400 mb-2">Size (sq ft)</label>
                        <input
                            id="room-size"
                            class="w-full rounded-lg border border-stone-200 bg-white px-3 py-2 text-sm"
                            type="number"
                            bind:value={form.sizeSqFt}
                        />
                    </div>
                    <div>
                        <label for="room-capacity" class="block text-xs font-bold tracking-widest uppercase text-stone-400 mb-2">Capacity</label>
                        <input
                            id="room-capacity"
                            class="w-full rounded-lg border border-stone-200 bg-white px-3 py-2 text-sm"
                            type="number"
                            min="1"
                            bind:value={form.capacity}
                            required
                        />
                    </div>
                </div>
                <div>
                    <label for="room-amenities" class="block text-xs font-bold tracking-widest uppercase text-stone-400 mb-2">Amenities (comma-separated)</label>
                    <input
                        id="room-amenities"
                        class="w-full rounded-lg border border-stone-200 bg-white px-3 py-2 text-sm"
                        placeholder="Wifi, AC, Private Bath..."
                        value={form.amenities.join(', ')}
                        oninput={(e) => {
                            const val = (e.target as HTMLInputElement).value;
                            form.amenities = val.split(',').map(s => s.trim()).filter(s => s !== '');
                        }}
                    />
                </div>
                <div>
                    <label for="room-price" class="block text-xs font-bold tracking-widest uppercase text-stone-400 mb-2">
                        Price per Night
                    </label>
                    <input id="room-price" class="w-full rounded-lg border border-stone-200 bg-white px-3 py-2 text-sm"
                        placeholder="Rs 1000" bind:value={form.pricePerNight}>
                        
                </div>
                <div>
                    <label for="room-images" class="block text-xs font-bold tracking-widest uppercase text-stone-400 mb-2">Room Gallery</label>
                    <div class="space-y-3">
                        <div class="grid grid-cols-4 gap-2">
                            {#each form.imageUrls as imgUrl}
                                <div class="relative group aspect-square rounded-lg overflow-hidden border border-stone-100 bg-stone-50">
                                    <img src={getFullImageUrl(imgUrl)} alt="Preview" class="w-full h-full object-cover" />
                                    <button 
                                        type="button"
                                        aria-label="Remove image"
                                        class="absolute top-1 right-1 p-1 bg-rose-500 text-white rounded-full opacity-0 group-hover:opacity-100 transition-opacity"
                                        onclick={() => removeImage(imgUrl)}
                                    >
                                        <svg xmlns="http://www.w3.org/2000/svg" class="h-3 w-3" viewBox="0 0 20 20" fill="currentColor">
                                            <path fill-rule="evenodd" d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z" clip-rule="evenodd" />
                                        </svg>
                                    </button>
                                </div>
                            {/each}
                            <label class="cursor-pointer border-2 border-dashed border-stone-200 rounded-lg aspect-square flex flex-col items-center justify-center hover:border-emerald-300 hover:bg-emerald-50 transition-all">
                                <input type="file" multiple accept="image/*" class="hidden" onchange={handleFileUpload} disabled={uploading} />
                                {#if uploading}
                                    <span class="text-[10px] font-bold text-emerald-600 animate-pulse">...</span>
                                {:else}
                                    <span class="text-xl text-stone-400">+</span>
                                    <span class="text-[8px] font-bold uppercase text-stone-400">Add</span>
                                {/if}
                            </label>
                        </div>
                    </div>
                </div>
                <label class="flex items-center gap-2 text-sm text-stone-600">
                    <input type="checkbox" bind:checked={form.isAvailable} />
                    Available for booking
                </label>
                <div class="flex items-center gap-3 pt-2">
                    <button
                        type="submit"
                        class="flex-1 py-3 rounded-lg bg-emerald-900 text-amber-400 font-bold text-xs tracking-widest uppercase hover:opacity-90 transition-opacity disabled:opacity-60"
                        disabled={saving}
                    >
                        {saving ? 'Saving...' : editingId ? 'Update Room' : 'Create Room'}
                    </button>
                    {#if editingId}
                        <button
                            type="button"
                            class="px-4 py-3 rounded-lg border border-stone-200 text-xs font-bold tracking-widest uppercase text-stone-600 hover:bg-stone-50"
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
