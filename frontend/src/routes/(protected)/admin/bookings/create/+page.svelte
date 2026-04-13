<script lang="ts">
    import { onMount } from "svelte";
    
    import { page } from "$app/stores";
    import { bookingService } from "$lib/services/bookingService";
    import { fileService } from "$lib/services/fileService";
    import { PUBLIC_API_BASE_URL } from "$env/static/public";
    import type { booking, BookingCreateRequest, Room } from "$lib/types/api";
    import { goto } from "$app/navigation";
    // import {roomService} from '$lib/services/roomService';
    import { roomService } from "$lib/services/roomService";
    
    let sucessMessage = $state("");
    let bookingId = $state($page.url.searchParams.get("id"));
    let Booking = $state<booking[]>([]);
    let Rooms = $state<Room[]>([]);
    let loading = $state(true);
    let error = $state("");
    let saving = $state(false);
    let editingId = $state<number | null>(null);
    let uploading = $state(false);
    
    let selectedRoom = $state("");
    function formatDate(date: string) {
        return date ? date.split("T")[0] : "";
    }
    const emptyForm: booking = {
        id: 0,
        roomId: 0,
        guestName: "",
        guestEmail: "",
        checkIn: "",
        checkOut: "",
        totalPrice: 0,
        discountAmount: 0,
        finalprice: 0,
        status: "",
        numberOfPeople: 1,
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
            roomId: booking.roomId,
            guestName: booking.guestName,
            guestEmail: booking.guestEmail,
            checkIn: booking.checkIn,
            checkOut: booking.checkOut,
            totalPrice: booking.totalPrice,
            discountAmount: booking.discountAmount,
            finalprice: booking.finalprice,
            status: booking.status,
            numberOfPeople: booking.numberOfPeople,
            createdat: booking.createdat,
        };
    }
    async function loadBookingById(id: number) {
        loading = true;
        error = "";
        try {
            const response = await bookingService.getById(id);
            console.log(response, " this is get all. room respinse");
            if (response.success == true && response.data) {
                const test = response.data;
                form = {
                    ...test,
                    checkIn: formatDate(test.checkIn),
                    checkOut: formatDate(test.checkOut),
                };
            } else {
                error = response.message || "Failed to load rooms";
            }
        } catch (e) {
            error = "Failed to load rooms";
        } finally {
            loading = false;
        }
    }
    async function saveBoooking(e: SubmitEvent) {
        console.log(selectedRoom, "this is selected room");
        e.preventDefault();
        saving = true;
        error = "";
        console.log(form, "form before ");
        form.roomId = Number(selectedRoom);
        console.log(form, "fprm after room");
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
                    // goto("/admin/bookings");
                    resetForm();
                    sucessMessage = "booking createdd sucesfully";
                    error = "";
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
       
    onMount(() => {
        if (bookingId != null || bookingId != undefined) {
            console.log("this is booking id ", bookingId);
            loadBookingById(Number(bookingId));
            loadRooms();
            // 👉 you can load specific booking here (edit mode)
            // example:
            // loadBookingById(bookingId);
        } else {
            console.log("room loading");

            loadRooms(); // ✅ correct function call
        }
    });
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
    {#if sucessMessage}
        <div
            class="p-4 rounded-lg border border-green-200 bg-green-50 text-green-700 text-sm font-semibold"
        >
            {sucessMessage}
        </div>
    {/if}
    <aside
        class="bg-surface-container-lowest rounded-xl shadow-sm border border-stone-100 p-6"
    >
        <h3 class="font-headline text-xl text-emerald-900 mb-4">
            {editingId ? "Edit Booking" : "Create Booking"}
        </h3>

        <form class="space-y-4" onsubmit={saveBoooking}>
            <!-- Guest Name -->
            <div>
                <label
                    for="guestName"
                    class="block text-xs font-bold uppercase text-stone-400 mb-2"
                    >Guest Name</label
                >
                <input
                    id="guestName"
                    class="w-full border px-3 py-2 rounded-lg"
                    bind:value={form.guestName}
                    required
                />
            </div>

            <!-- Email -->
            <div>
                <label
                    for="guestEmail"
                    class="block text-xs font-bold uppercase text-stone-400 mb-2"
                    >Email</label
                >
                <input
                    id="guestEmail"
                    type="email"
                    class="w-full border px-3 py-2 rounded-lg"
                    bind:value={form.guestEmail}
                    required
                />
            </div>

            <!-- Room ID -->
            <div>
                <div class="w-full max-w-md">
                    <label
                        for="roomSelect"
                        class="block text-xs font-bold uppercase text-stone-400 mb-2"
                    >
                        Rooms
                    </label>

                    <select
                        id="roomSelect"
                        name="roomSelect"
                        bind:value={selectedRoom}
                        class="w-full px-4 py-2 border border-gray-300 rounded-xl shadow-sm
               focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500
               bg-white text-gray-700 transition duration-200"
                    >
                        <option
                            value=""
                            disabled
                            selected
                            class="text-gray-400"
                        >
                            Choose a room
                        </option>

                        {#each Rooms as r}
                            <option value={r.id} class="text-gray-700">
                                {r.name}
                            </option>
                        {/each}
                    </select>
                </div>
            </div>

            <!-- Dates -->
            <div class="grid grid-cols-2 gap-4">
                <div>
                    <label
                        for="checkIn"
                        class="block text-xs font-bold uppercase text-stone-400 mb-2"
                        >Check-in</label
                    >
                    <input
                        id="checkIn"
                        type="date"
                        class="w-full border px-3 py-2 rounded-lg"
                        bind:value={form.checkIn}
                    />
                </div>

                <div>
                    <label
                        for="checkOut"
                        class="block text-xs font-bold uppercase text-stone-400 mb-2"
                        >Check-out</label
                    >
                    <input
                        id="checkOut"
                        type="date"
                        class="w-full border px-3 py-2 rounded-lg"
                        bind:value={form.checkOut}
                    />
                </div>
            </div>

            <!-- Number of People -->
            <div>
                <label
                    for="numberOfPeople"
                    class="block text-xs font-bold uppercase text-stone-400 mb-2"
                    >Number of People</label
                >
                <input
                    id="numberOfPeople"
                    type="number"
                    min="1"
                    class="w-full border px-3 py-2 rounded-lg"
                    bind:value={form.numberOfPeople}
                    required
                />
            </div>

            <!-- Price -->
            <div>
                <label
                    for="totalPrice"
                    class="block text-xs font-bold uppercase text-stone-400 mb-2"
                    >Price</label
                >
                <input
                    id="totalPrice"
                    type="number"
                    class="w-full border px-3 py-2 rounded-lg"
                    bind:value={form.totalPrice}
                />
            </div>

            <!-- Discount -->
            <div>
                <label
                    for="discountAmount"
                    class="block text-xs font-bold uppercase text-stone-400 mb-2"
                    >Discount</label
                >
                <input
                    id="discountAmount"
                    type="number"
                    class="w-full border px-3 py-2 rounded-lg"
                    bind:value={form.discountAmount}
                />
            </div>

            <!-- Final Price (auto) -->
            <div>
                <label
                    for="finalPrice"
                    class="block text-xs font-bold uppercase text-stone-400 mb-2"
                    >Final Price</label
                >
                <input
                    id="finalPrice"
                    type="number"
                    class="w-full border px-3 py-2 rounded-lg bg-gray-100"
                    value={form.totalPrice - form.discountAmount}
                    readonly
                />
            </div>

            <!-- Status -->
            <div>
                <label
                    for="status"
                    class="block text-xs font-bold uppercase text-stone-400 mb-2"
                    >Status</label
                >
                <select
                    id="status"
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
