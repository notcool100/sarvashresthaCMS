<script lang="ts">
    import { onMount } from 'svelte';
    import { bookingService } from '$lib/services/bookingService';
    import { fileService } from '$lib/services/fileService';
    import { PUBLIC_API_BASE_URL } from '$env/static/public';
    import type { booking, BookingCreateRequest } from '$lib/types/api';
    import { goto } from '$app/navigation';

    let Booking = $state<booking[]>([]);
    let loading = $state(true);
    let error = $state('');
    let saving = $state(false);
    let editingId = $state<number | null>(null);
    let uploading = $state(false);

    const emptyForm: booking = {
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

    let form = $state<BookingCreateRequest>({ ...emptyForm });

    function resetForm() {
        form = { ...emptyForm };
        editingId = null;
    }

    async function loadRooms() {
        loading = true;
        error = '';
        try {
            const response = await bookingService.getAll();
            if (response.success && response.data) {
                Booking = response.data;
            } else {
                error = response.message || 'Failed to load rooms';
            }
        } catch (e) {
            error = 'Failed to load rooms';
        } finally {
            loading = false;
        }
    }
    async function gotoCreatePage(){
        goto('/admin/bookings/create')
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
    createdat: booking.createdat
};
    }

    async function saveRoom(e: SubmitEvent) {
        e.preventDefault();
        saving = true;
        error = '';
        try {
            if (editingId) {
                const response = await bookingService.update({ id: editingId, status: form.status });
                if (!response.success) {
                    error = response.message || 'Failed to update room';
                    return;
                }
            } else {
                const response = await bookingService.create(form);
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
            const response = await bookingService.remove(id);
            if (!response.success) {
                error = response.message || 'Failed to delete room';
                return;
            }
            Booking = Booking.filter((r) => r.id !== id);
        } catch (e) {
            error = 'Failed to delete room';
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
        if (!path) return '';
        if (path.startsWith('http')) return path;
        // PUBLIC_API_BASE_URL usually ends in /api, so we need to adjust
        const base = PUBLIC_API_BASE_URL.replace('/api', '');
        return `${base}${path}`;
    }

    onMount(loadRooms);
</script>


<button onclick={gotoCreatePage}>
create
</button>