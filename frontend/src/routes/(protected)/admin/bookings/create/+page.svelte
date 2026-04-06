<script lang="ts">
    import { onMount } from "svelte";
    import { bookingService } from "$lib/services/bookingService";
    import { fileService } from "$lib/services/fileService";
    import { PUBLIC_API_BASE_URL } from "$env/static/public";
    import type { booking, BookingCreateRequest, Room } from "$lib/types/api";
    import { goto } from "$app/navigation";
    // import {roomService} from '$lib/services/roomService';
    import { roomService } from "$lib/services/roomService";

    let Booking = $state<booking[]>([]);
    let Rooms = $state<Room[]>([]);
    let loading = $state(true);
    let error = $state("");
    let saving = $state(false);
    let editingId = $state<number | null>(null);
    let uploading = $state(false);
    let selectedRoom = $state("");
    const emptyForm: booking = {
        id: 0,
        room_id: 0,
        guest_name: "",
        email: "",
        checkin: 0,
        checkout: 0,
        price: 0,
        discountamount: 0,
        finalprice: 0,
        status: "",
        createdat: 0,
    };

    let form = $state<BookingCreateRequest>({ ...emptyForm });

    function resetForm() {
        form = { ...emptyForm };
        editingId = null;
    }

    async function loadRooms() {
        loading = true;
        error = "";
        try {
            const response = await roomService.getAll();
            console.log(response, " this is get all. room respinse");
            if (response.success == true && response.data) {
                Rooms = response.data;
            } else {
                error = response.message || "Failed to load rooms";
            }
        } catch (e) {
            error = "Failed to load rooms";
        } finally {
            loading = false;
        }
    }

    function startEdit(booking: booking) {
        editingId = booking.id;
        form = {
            id: booking.id,
            room_id: booking.room_id,
            guest_name: booking.guest_name,
            email: booking.email,
            checkin: booking.checkin,
            checkout: booking.checkout,
            price: booking.price,
            discountamount: booking.discountamount,
            finalprice: booking.finalprice,
            status: booking.status,
            createdat: booking.createdat,
        };
    }

    async function saveRoom(e: SubmitEvent) {
        e.preventDefault();
        saving = true;
        error = "";
        try {
            if (editingId) {
                const response = await bookingService.update({
                    id: editingId,
                    status: form.status,
                });
                if (!response.success) {
                    error = response.message || "Failed to update room";
                    return;
                }
            } else {
                const response = await bookingService.create(form);
                if (!response.success) {
                    error = response.message || "Failed to create room";
                    return;
                } else {
                    goto("/admin/bookings");
                }
            }
            resetForm();
            await loadRooms();
        } catch (e) {
            error = "Failed to save room";
        } finally {
            saving = false;
        }
    }

    async function deleteRoom(id: number) {
        const confirmed = window.confirm(
            "Delete this room? This cannot be undone.",
        );
        if (!confirmed) return;
        try {
            const response = await bookingService.remove(id);
            if (!response.success) {
                error = response.message || "Failed to delete room";
                return;
            }
            Booking = Booking.filter((r) => r.id !== id);
        } catch (e) {
            error = "Failed to delete room";
        }
    }

    // async function handleFileUpload(e: Event) {
    //     const input = e.target as HTMLInputElement;
    //     if (!input.files || input.files.length === 0) return;

    //     uploading = true;
    //     try {
    //         const response = await fileService.uploadRoomImages(input.files);
    //         if (response.success && response.data) {
    //             form.imageUrls = [...form.imageUrls, ...response.data];
    //         } else {
    //             error = response.message || 'Upload failed';
    //         }
    //     } catch (err) {
    //         error = 'Upload failed';
    //     } finally {
    //         uploading = false;
    //         input.value = '';
    //     }
    // }

    // function removeImage(url: string) {
    //     form.imageUrls = form.imageUrls.filter(u => u !== url);
    // }

    function getFullImageUrl(path: string) {
        if (!path) return "";
        if (path.startsWith("http")) return path;
        // PUBLIC_API_BASE_URL usually ends in /api, so we need to adjust
        const base = PUBLIC_API_BASE_URL.replace("/api", "");
        return `${base}${path}`;
    }

    onMount(loadRooms);
</script>

<section class="space-y-10">
    <header class="flex items-end justify-between">
        <div>
            <h2
                class="font-headline text-3xl text-emerald-900 tracking-tight mb-2"
            >
                Rooms
            </h2>
            <p class="text-stone-500 font-medium">
                Manage your room inventory and availability.
            </p>
        </div>
        <button
            class="px-6 py-3 rounded-xl bg-emerald-900 text-amber-400 font-bold text-sm tracking-wide uppercase hover:opacity-90 transition-opacity"
            onclick={resetForm}
        >
            reset
        </button>
    </header>

    {#if error}
        <div
            class="p-4 rounded-lg border border-rose-200 bg-rose-50 text-rose-700 text-sm font-semibold"
        >
            {error}
        </div>
    {/if}

    <aside
        class="bg-surface-container-lowest rounded-xl shadow-sm border border-stone-100 p-6"
    >
        <h3 class="font-headline text-xl text-emerald-900 mb-4">
            {editingId ? "Edit Booking" : "Create Booking"}
        </h3>

        <form class="space-y-4" onsubmit={saveRoom}>
            <!-- Guest Name -->
            <div>
                <label
                    class="block text-xs font-bold uppercase text-stone-400 mb-2"
                    >Guest Name</label
                >
                <input
                    class="w-full border px-3 py-2 rounded-lg"
                    bind:value={form.guest_name}
                    required
                />
            </div>

            <!-- Email -->
            <div>
                <label
                    class="block text-xs font-bold uppercase text-stone-400 mb-2"
                    >Email</label
                >
                <input
                    type="email"
                    class="w-full border px-3 py-2 rounded-lg"
                    bind:value={form.email}
                    required
                />
            </div>

            <!-- Room ID -->
            <div>
                <label
                    class="block text-xs font-bold uppercase text-stone-400 mb-2"
                    >Rooms</label
                >
                <!-- <input
                    type="number"
                    class="w-full border px-3 py-2 rounded-lg"
                    bind:value={form.room_id}
                /> -->

                <select id="course" name="course" bind:value={selectedRoom}>
                    {#each Rooms as r}
                        <option value={r.id}>{r.name}</option>
                    {/each}
                </select>
            </div>

            <!-- Dates -->
            <div class="grid grid-cols-2 gap-4">
                <div>
                    <label
                        class="block text-xs font-bold uppercase text-stone-400 mb-2"
                        >Check-in</label
                    >
                    <input
                        type="date"
                        class="w-full border px-3 py-2 rounded-lg"
                        bind:value={form.checkin}
                    />
                </div>

                <div>
                    <label
                        class="block text-xs font-bold uppercase text-stone-400 mb-2"
                        >Check-out</label
                    >
                    <input
                        type="date"
                        class="w-full border px-3 py-2 rounded-lg"
                        bind:value={form.checkout}
                    />
                </div>
            </div>

            <!-- Price -->
            <div>
                <label
                    class="block text-xs font-bold uppercase text-stone-400 mb-2"
                    >Price</label
                >
                <input
                    type="number"
                    class="w-full border px-3 py-2 rounded-lg"
                    bind:value={form.price}
                />
            </div>

            <!-- Discount -->
            <div>
                <label
                    class="block text-xs font-bold uppercase text-stone-400 mb-2"
                    >Discount</label
                >
                <input
                    type="number"
                    class="w-full border px-3 py-2 rounded-lg"
                    bind:value={form.discountamount}
                />
            </div>

            <!-- Final Price (auto) -->
            <div>
                <label
                    class="block text-xs font-bold uppercase text-stone-400 mb-2"
                    >Final Price</label
                >
                <input
                    type="number"
                    class="w-full border px-3 py-2 rounded-lg bg-gray-100"
                    value={form.price - form.discountamount}
                    readonly
                />
            </div>

            <!-- Status -->
            <div>
                <label
                    class="block text-xs font-bold uppercase text-stone-400 mb-2"
                    >Status</label
                >
                <select
                    class="w-full border px-3 py-2 rounded-lg"
                    bind:value={form.status}
                >
                    <option value="">Select</option>
                    <option value="Pending">Pending</option>
                    <option value="Confirmed">Confirmed</option>
                    <option value="Cancelled">Cancelled</option>
                </select>
            </div>

            <!-- Submit -->
            <button
                class="w-full py-3 bg-emerald-900 text-amber-400 rounded-lg text-xs font-bold uppercase"
            >
                {editingId ? "Update Booking" : "Create Booking"}
            </button>
        </form>
    </aside>
</section>
