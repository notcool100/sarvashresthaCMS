<script lang="ts">
    import { onMount } from 'svelte';
    import { galleryService } from '$lib/services/galleryService';
    import { fileService } from '$lib/services/fileService';
    import { PUBLIC_API_BASE_URL } from '$env/static/public';
    import type { GalleryItem, GalleryItemRequest } from '$lib/types/api';
    import { fade, scale } from 'svelte/transition';
    import { flip } from 'svelte/animate';

    let items = $state<GalleryItem[]>([]);
    let loading = $state(true);
    let error = $state('');
    let saving = $state(false);
    let uploading = $state(false);

    const categories = ["Suites", "Dining", "Wellness", "Experiences", "Exterior", "Events"];
    
    let form = $state<GalleryItemRequest>({
        imageUrl: '',
        altText: '',
        category: 'Suites',
        displayOrder: 0
    });

    async function loadItems() {
        loading = true;
        try {
            const response = await galleryService.getAll();
            if (response.success && response.data) {
                items = response.data;
            } else {
                error = response.message || 'Failed to load gallery items';
            }
        } catch (e) {
            error = 'Failed to load gallery items';
        } finally {
            loading = false;
        }
    }

    async function handleFileUpload(e: Event) {
        const input = e.target as HTMLInputElement;
        if (!input.files || input.files.length === 0) return;
        
        uploading = true;
        try {
            // Reusing room images upload logic for now as it's the same file storage path
            const response = await fileService.uploadRoomImages(input.files);
            if (response.success && response.data && response.data.length > 0) {
                form.imageUrl = response.data[0];
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

    async function saveItem(e: SubmitEvent) {
        e.preventDefault();
        if (!form.imageUrl) {
            error = 'Please upload an image';
            return;
        }
        saving = true;
        error = '';
        try {
            const response = await galleryService.create(form);
            if (response.success) {
                form = {
                    imageUrl: '',
                    altText: '',
                    category: 'Suites',
                    displayOrder: items.length + 1
                };
                await loadItems();
            } else {
                error = response.message || 'Failed to save gallery item';
            }
        } catch (e) {
            error = 'Failed to save gallery item';
        } finally {
            saving = false;
        }
    }

    async function deleteItem(id: number) {
        if (!confirm('Are you sure you want to delete this image?')) return;
        try {
            const response = await galleryService.remove(id);
            if (response.success) {
                items = items.filter(i => i.id !== id);
            } else {
                error = response.message || 'Failed to delete item';
            }
        } catch (e) {
            error = 'Failed to delete item';
        }
    }

    function getFullImageUrl(path: string) {
        if (!path) return '';
        if (path.startsWith('http')) return path;
        const base = PUBLIC_API_BASE_URL.replace('/api', '');
        return `${base}${path}`;
    }

    onMount(loadItems);
</script>

<div class="space-y-10" in:fade>
    <header class="flex items-end justify-between">
        <div>
            <h2 class="font-headline text-3xl text-emerald-900 tracking-tight mb-2">Gallery Management</h2>
            <p class="text-stone-500 font-medium">Add and organize visual stories for your guests.</p>
        </div>
    </header>

    {#if error}
        <div class="p-4 rounded-xl border border-rose-200 bg-rose-50 text-rose-700 text-sm font-semibold" in:fade>
            {error}
        </div>
    {/if}

    <div class="grid grid-cols-1 lg:grid-cols-[1fr_350px] gap-8 items-start">
        
        <!-- Gallery Grid -->
        <section class="bg-surface-container-lowest rounded-2xl p-6 shadow-sm border border-stone-100 min-h-[600px]">
            <h3 class="font-headline text-xl text-emerald-900 mb-6">Current Gallery</h3>
            
            {#if loading}
                <div class="flex items-center justify-center h-64">
                    <div class="w-8 h-8 border-4 border-emerald-900/20 border-t-emerald-900 rounded-full animate-spin"></div>
                </div>
            {:else if items.length === 0}
                <div class="flex flex-col items-center justify-center h-64 text-stone-400">
                    <span class="material-symbols-outlined text-5xl mb-2">image_not_supported</span>
                    <p>No images in gallery yet.</p>
                </div>
            {:else}
                <div class="grid grid-cols-2 md:grid-cols-3 xl:grid-cols-4 gap-4">
                    {#each items as item (item.id)}
                        <div 
                            class="group relative aspect-square rounded-xl overflow-hidden bg-stone-100 border border-stone-100 hover:shadow-lg transition-all duration-300"
                            animate:flip={{duration: 400}}
                        >
                            <img src={getFullImageUrl(item.imageUrl)} alt={item.altText} class="w-full h-full object-cover transition-transform duration-500 group-hover:scale-110" />
                            <div class="absolute inset-0 bg-gradient-to-t from-black/80 via-transparent to-transparent opacity-0 group-hover:opacity-100 transition-opacity flex flex-col justify-end p-3">
                                <span class="text-[10px] font-bold text-white/70 uppercase tracking-widest">{item.category}</span>
                                <p class="text-xs text-white font-medium truncate">{item.altText || 'No caption'}</p>
                                <button 
                                    class="mt-2 w-full py-1.5 bg-rose-500/20 hover:bg-rose-500 text-white text-[10px] font-bold uppercase rounded-lg backdrop-blur-md transition-colors"
                                    onclick={() => deleteItem(item.id)}
                                >
                                    Delete
                                </button>
                            </div>
                        </div>
                    {/each}
                </div>
            {/if}
        </section>

        <!-- Sidebar Add Form -->
        <aside class="sticky top-10 space-y-6">
            <form 
                onsubmit={saveItem}
                class="bg-surface-container-lowest rounded-2xl p-6 shadow-xl border border-stone-100 space-y-5"
            >
                <h3 class="font-headline text-xl text-emerald-900">Add New Image</h3>
                
                <!-- Upload Area -->
                <div class="relative group aspect-video rounded-xl border-2 border-dashed border-stone-200 bg-stone-50 overflow-hidden flex flex-col items-center justify-center transition-all hover:border-emerald-500 hover:bg-emerald-50/30">
                    {#if form.imageUrl}
                        <img src={getFullImageUrl(form.imageUrl)} alt="Preview" class="w-full h-full object-cover" />
                        <button 
                            type="button"
                            class="absolute top-2 right-2 p-1.5 bg-rose-500 text-white rounded-full shadow-lg opacity-0 group-hover:opacity-100 transition-opacity"
                            onclick={() => form.imageUrl = ''}
                        >
                            <span class="material-symbols-outlined text-sm">close</span>
                        </button>
                    {:else if uploading}
                        <div class="w-8 h-8 border-4 border-emerald-900/10 border-t-emerald-900 rounded-full animate-spin mb-2"></div>
                        <span class="text-[10px] font-bold text-emerald-900 uppercase">Uploading...</span>
                    {:else}
                        <label class="cursor-pointer flex flex-col items-center">
                            <input type="file" accept="image/*" class="hidden" onchange={handleFileUpload} />
                            <span class="material-symbols-outlined text-3xl text-stone-300 mb-2">cloud_upload</span>
                            <span class="text-[10px] font-bold text-stone-400 uppercase tracking-widest">Click to upload</span>
                        </label>
                    {/if}
                </div>

                <div class="space-y-4">
                    <div>
                        <label for="category" class="block text-[10px] font-bold uppercase tracking-widest text-stone-400 mb-2">Category</label>
                        <select 
                            id="category"
                            bind:value={form.category}
                            class="w-full rounded-xl border border-stone-200 bg-white px-4 py-3 text-sm focus:ring-2 focus:ring-emerald-500 transition-all outline-none"
                        >
                            {#each categories as cat}
                                <option value={cat}>{cat}</option>
                            {/each}
                        </select>
                    </div>

                    <div>
                        <label for="altText" class="block text-[10px] font-bold uppercase tracking-widest text-stone-400 mb-2">Caption / Alt Text</label>
                        <input 
                            id="altText"
                            type="text"
                            bind:value={form.altText}
                            placeholder="Describe the image..."
                            class="w-full rounded-xl border border-stone-200 bg-white px-4 py-3 text-sm focus:ring-2 focus:ring-emerald-500 transition-all outline-none"
                        />
                    </div>

                    <div>
                        <label for="order" class="block text-[10px] font-bold uppercase tracking-widest text-stone-400 mb-2">Display Order</label>
                        <input 
                            id="order"
                            type="number"
                            bind:value={form.displayOrder}
                            class="w-full rounded-xl border border-stone-200 bg-white px-4 py-3 text-sm focus:ring-2 focus:ring-emerald-500 transition-all outline-none"
                        />
                    </div>
                </div>

                <button 
                    type="submit"
                    disabled={saving || uploading || !form.imageUrl}
                    class="w-full py-4 bg-emerald-900 text-amber-400 rounded-xl font-bold text-xs uppercase tracking-widest hover:opacity-90 disabled:opacity-50 transition-all shadow-lg hover:shadow-emerald-900/20"
                >
                    {saving ? 'Adding...' : 'Add to Gallery'}
                </button>
            </form>
        </aside>
    </div>
</div>
