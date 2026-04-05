<script lang="ts">
    import { onMount } from 'svelte';
    import { page } from '$app/stores';
    import { bookingService } from '$lib/services/bookingService';
    import type { BookingCreateRequest } from '$lib/types/api';

    const emptyForm: BookingCreateRequest = {
    id: 0,
    room_id: 0,
    guest_name: '',
    email: '',
    checkin: 0,
    checkout: 0,
    price: 0,
    discountamount: 0,
    finalprice: 0,
    status: '',
    createdat: 0
    };

    let submitdata = $state<BookingCreateRequest>({ ...emptyForm });
    let error = $state('');
    let loading = $state(false);

    const id = $derived($page.url.searchParams.get('id'));

    async function loadRoom(bookingId: number) {
        loading = true;
        error = '';
        try {
            const response = await bookingService.getById(bookingId);
            if (response.success && response.data) {
                submitdata = { ...response.data };
            } else {
                error = response.message || 'No room found';
            }
        } catch {
            error = 'Failed to get the room';
        } finally {
            loading = false;
        }
    }
async function submit(){
error="";
try{
if(id != null || id != ""){
const response = await bookingService.create(submitdata);
}
}
catch(c){
error =  "Failed to create the room";
}
}
    onMount(() => {
        if (id) {
            const parsed = Number(id);
            if (!Number.isNaN(parsed)) {
                void loadRoom(parsed);
            }
        }
    });
</script>

<main class="space-y-4">
    {#if error}
        <div class="p-3 rounded border border-rose-200 bg-rose-50 text-rose-700 text-sm">{error}</div>
    {/if}

    <form class="space-y-3">
        <label class="block text-sm font-medium text-stone-600">
            Room Name
            <input
                class="mt-1 w-full rounded border border-stone-200 px-3 py-2 text-sm"
                type="text"
                bind:value={submitdata.name}
                placeholder="Room name"
            />

        </label>
        <label>
        description
        <input type="text" bind:value={submitdata.description}
        placeholder="Room Description"
        />
        </label>
    </form>
</main>
