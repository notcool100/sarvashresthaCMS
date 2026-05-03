<script lang="ts">
  import { onMount } from "svelte";
  import { page } from "$app/stores";
  import NavBar from "$lib/components/home/NavBar.svelte";
  import FooterSection from "$lib/components/home/FooterSection.svelte";
  import type { Room } from "$lib/types/api";
  import { bookingService } from "$lib/services/bookingService";
  import { roomService } from "$lib/services/roomService";
  import { authStore } from "$lib/stores/authStore";
  import { goto } from "$app/navigation";
  import type { booking, BookingCreateRequest } from "$lib/types/api";

  let Booking = $state<booking[]>([]);
  let rooms = $state<Room[]>([]);
  let isLoading = $state(true);
  const emptyForm: BookingCreateRequest = {
    id: 0,
    roomId: 0,
    guestName: "",
    guestEmail: "",
    checkIn: "",
    checkOut: "",
    totalPrice: 0,
    discountAmount: 0,
    finalprice: 0,
    status: "Pending",
    numberOfPeople: 2,
    createdat: 0,
  };

  let form = $state<BookingCreateRequest>({ ...emptyForm });
  let isSaving = $state(false);
  let errorMessage = $state("");
  let successMessage = $state("");

  const nights = 6;
  const sustainabilityFee = 120;
  const taxes = 360;

  import { getImageUrl } from "$lib/utils/image";

  const fallbackImages = [
    "https://images.unsplash.com/photo-1505691938895-1758d7feb511?q=80&w=1200&auto=format&fit=crop",
    "https://images.unsplash.com/photo-1505693416388-ac5ce068fe85?q=80&w=1200&auto=format&fit=crop",
    "https://images.unsplash.com/photo-1507089947368-19c1da9775ae?q=80&w=1200&auto=format&fit=crop",
  ];

  const selectedId = $derived($page.url.searchParams.get("room"));
  const selectedRoom = $derived(
    rooms.find((room) => String(room.id) === String(selectedId)) ?? rooms[0],
  );

  const calculatedNights = $derived(() => {
    if (!form.checkIn || !form.checkOut) return 1;
    const start = new Date(form.checkIn);
    const end = new Date(form.checkOut);
    const diffTime = Math.abs(end.getTime() - start.getTime());
    const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
    return diffDays > 0 ? diffDays : 1;
  });

  const nightsCount = $derived(calculatedNights());
  const subtotal = $derived((selectedRoom?.pricePerNight ?? 0) * nightsCount);
  const total = $derived(subtotal + sustainabilityFee + taxes);

  const heroImage = (room?: Room) => getImageUrl(room?.imageUrls?.[0]) || fallbackImages[0];
  const galleryImages = (room?: Room) => {
    const list = room?.imageUrls?.length ? room.imageUrls.map(url => getImageUrl(url)) : fallbackImages;
    return list.slice(0, 3);
  };

  async function submitReservation() {
    if (!selectedRoom) {
      errorMessage = "Please select a room before submitting your reservation.";
      return;
    }

    if (!$authStore) {
      goto(`/login?redirect=${encodeURIComponent(`/booking?room=${selectedRoom.id}`)}`);
      return;
    }

    if (!form.guestName || !form.guestEmail || !form.checkIn || !form.checkOut) {
      errorMessage = "Please fill in all required fields.";
      return;
    }

    const start = new Date(form.checkIn);
    const end = new Date(form.checkOut);
    if (end <= start) {
      errorMessage = "Check-out date must be after check-in date.";
      return;
    }

    isSaving = true;
    errorMessage = "";
    successMessage = "";

    try {
      const payload: BookingCreateRequest = {
        ...form,
        roomId: selectedRoom.id,
        totalPrice: total,
        finalprice: total,
        discountAmount: 0,
        status: "Pending"
      };

      const response = await bookingService.create(payload);
      if (response.success) {
        successMessage = "Your reservation has been submitted successfully!";
        form = { ...emptyForm };
        
        // Redirect to user bookings after a brief delay
        setTimeout(() => {
          goto('/user/bookings');
        }, 1500);
      } else {
        errorMessage = response.message || "Failed to submit reservation.";
      }
    } catch (e) {
      console.error(e);
      errorMessage = "An unexpected error occurred. Please try again.";
    } finally {
      isSaving = false;
    }
  }

  const bathLabel = (room?: Room) => {
    const amenity = room?.amenities?.find((item) =>
      item.toLowerCase().includes("bath"),
    );
    return amenity ? `Attached ${amenity}` : "Attached Bath";
  };
  const highlightItems = (room?: Room) =>
    room?.amenities?.length
      ? room.amenities.slice(0, 3)
      : ["Private rituals", "Mountain views", "Concierge service"];

  onMount(async () => {
    const response = await roomService.getAll();
    rooms = response.data ?? [];
    isLoading = false;
  });
</script>

<svelte:head>
  <title>Booking | Sarvashrestha Resort & Spa</title>
  <link
    href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&display=swap"
    rel="stylesheet"
  />
</svelte:head>

<NavBar />

<main class="pt-24 sm:pt-28 pb-16 sm:pb-24 px-4 sm:px-6 max-w-7xl mx-auto bg-surface text-on-surface">
  <div class="grid grid-cols-12 gap-8 xl:gap-10">
    <div class="col-span-12 min-w-0 lg:col-span-8 space-y-10 sm:space-y-12">
      <section>
        <div class="section-heading mb-6 sm:mb-8">
          <span class="font-[var(--font-headline)] text-3xl sm:text-4xl text-secondary"
            >01</span
          >
          <h2 class="font-[var(--font-headline)] text-3xl sm:text-4xl tracking-tight">
            Select Your Sanctuary
          </h2>
        </div>
        <div class="panel space-y-6 sm:space-y-8">
          <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
            <div class="field">
              <label for="booking-dates">Check-in & Check-out</label>
              <div class="date-range">
                <input
                  id="checkIn"
                  type="date"
                  class="date-input"
                  bind:value={form.checkIn}
                />
                <span class="date-separator">to</span>
                <input
                  id="checkout"
                  type="date"
                  class="date-input"
                  bind:value={form.checkOut}
                />
              </div>
            </div>
            <div class="field">
              <label for="travelers-count">Travelers</label>
              <div class="input-shell stepper">
                <span class="material-symbols-outlined">group</span>
                <div class="stepper-controls">
                  <button 
                    type="button"
                    class="step-btn" 
                    onclick={() => form.numberOfPeople = Math.max(1, form.numberOfPeople - 1)}
                    disabled={form.numberOfPeople <= 1}
                  >
                    <span class="material-symbols-outlined text-sm">remove</span>
                  </button>
                  <span class="count-display">{form.numberOfPeople} {form.numberOfPeople === 1 ? 'Person' : 'People'}</span>
                  <button 
                    type="button"
                    class="step-btn" 
                    onclick={() => form.numberOfPeople = Math.min(selectedRoom?.capacity || 10, form.numberOfPeople + 1)}
                    disabled={form.numberOfPeople >= (selectedRoom?.capacity || 10)}
                  >
                    <span class="material-symbols-outlined text-sm">add</span>
                  </button>
                </div>
              </div>
              {#if selectedRoom && form.numberOfPeople >= selectedRoom.capacity}
                <p class="capacity-hint text-[10px] text-amber-600 mt-1 uppercase tracking-wider font-bold">Max capacity for this room reached</p>
              {/if}
            </div>
          </div>

          <div class="space-y-4">
            <div class="room-header">
              <span class="block">Selected Room</span>
              <a class="change-link" href="/rooms">Change room</a>
            </div>
            <div class="selected-room">
              <div class="selected-hero">
                <img src={heroImage(selectedRoom)} alt={selectedRoom?.name} />
                <div class="selected-info">
                  <h3 class="font-[var(--font-headline)] text-2xl">
                    {selectedRoom?.name}
                  </h3>
                  <p class="text-sm text-on-surface-variant">
                    {selectedRoom?.description}
                  </p>
                  <div class="selected-meta">
                    <span
                      ><span class="material-symbols-outlined">square_foot</span
                      >
                      {selectedRoom?.sizeSqFt} ft²</span
                    >
                    <span
                      ><span class="material-symbols-outlined">king_bed</span>
                      {selectedRoom?.bedType}</span
                    >
                    <span
                      ><span class="material-symbols-outlined">bathroom</span>
                      {bathLabel(selectedRoom)}</span
                    >
                    <span
                      ><span class="material-symbols-outlined">landscape</span>
                      {selectedRoom?.viewType || "Mountain View"}</span
                    >
                  </div>
                </div>
              </div>
              <div class="gallery-grid">
                {#each galleryImages(selectedRoom) as photo}
                  <img src={photo} alt={`${selectedRoom?.name} view`} />
                {/each}
              </div>
              <div class="highlight-grid">
                {#each highlightItems(selectedRoom) as item}
                  <div class="highlight-card">
                    <span class="material-symbols-outlined">auto_awesome</span>
                    <p>{item}</p>
                  </div>
                {/each}
              </div>
            </div>
          </div>
        </div>
      </section>

      <section>
        <div class="section-heading mb-6 sm:mb-8">
          <span class="font-[var(--font-headline)] text-3xl sm:text-4xl text-secondary"
            >02</span
          >
          <h2 class="font-[var(--font-headline)] text-3xl sm:text-4xl tracking-tight">
            Personal Details
          </h2>
        </div>
        <div class="panel space-y-6">
          <div class="">
            <div class="field mb-4">
              <label class="block mb-1" for="guestName">Full Name</label>
              <input
                id="guestName"
                type="text"
                placeholder="Tenjing"
                class="form-input"
                bind:value={form.guestName}
              />
            </div>
          </div>
          <div class="field mb-4">
            <label class="block mb-1" for="guestEmail">Email Address</label>
            <input
              id="guestEmail"
              type="email"
              placeholder="expedition@himalayas.com"
              class="form-input"
              bind:value={form.guestEmail}
            />
          </div>
          <!-- <div class="field">
            <label class="text-gray-200 mb-1 block">Special Requirements</label>
            <textarea
              class="w-full p-3 border border-black rounded-lg text-black placeholder-gray-500 focus:outline-none focus:ring-1"
              rows="4"
              placeholder="Dietary preferences, arrival time, or celebratory occasions..."
              
            ></textarea>
          </div> -->
          <!-- Dates moved to top -->
          <!-- <div class="field mb-4">
            <label class="block text-gray-200 mb-1">Discount Amount</label>
            <input
              type="number"
              placeholder="0"
              class="w-full p-3 border border-black rounded-lg text-black focus:outline-none focus:ring-1"
            />
          </div> -->

          <!-- Price Display -->
          <div class="mt-4 p-4 rounded-lg space-y-2 border border-black/10">
            <p class="flex justify-between"><span>Total Price:</span> <span>${subtotal.toFixed(2)}</span></p>
            <p class="flex justify-between"><span>Fees & Taxes:</span> <span>${(sustainabilityFee + taxes).toFixed(2)}</span></p>
            <p class="font-bold text-lg flex justify-between border-t border-black/10 pt-2">
              <span>Final Price:</span> <span>${total.toFixed(2)}</span>
            </p>
          </div>

          {#if errorMessage}
            <div class="p-4 bg-red-100 text-red-700 rounded-lg text-sm">
              {errorMessage}
            </div>
          {/if}

          {#if successMessage}
            <div class="p-4 bg-green-100 text-green-700 rounded-lg text-sm">
              {successMessage}
            </div>
          {/if}
        </div>
      </section>
    </div>

    <aside class="col-span-12 min-w-0 lg:col-span-4">
      <div class="space-y-4 sm:space-y-6 lg:sticky lg:top-28">
        <div class="summary-card">
          <div class="summary-image">
            <img src={heroImage(selectedRoom)} alt={selectedRoom?.name} />
            <div class="overlay-title">Your Himalayan Sanctuary</div>
          </div>
          <div class="summary-body">
            <div class="space-y-3 text-sm">
              <div class="flex justify-between">
                <span class="muted">Stay Duration</span><span class="font-bold"
                   >{nightsCount} Nights</span
                >
              </div>
              <div class="flex justify-between">
                <span class="muted">Room Type</span><span class="font-bold"
                  >{selectedRoom?.name}</span
                >
              </div>
              <div class="flex justify-between">
                <span class="muted">Guests</span><span class="font-bold"
                  >{form.numberOfPeople} People</span
                >
              </div>
            </div>
            <div class="border-t border-black/10 pt-4 space-y-2 text-sm">
              <div class="flex justify-between">
                <span
                  >${(selectedRoom?.pricePerNight ?? 0).toFixed(2)} x {nightsCount} nights</span
                ><span>${subtotal.toFixed(2)}</span>
              </div>
              <div class="flex justify-between">
                <span>Sustainability Fee</span><span
                  >${sustainabilityFee.toFixed(2)}</span
                >
              </div>
              <div class="flex justify-between">
                <span>Local Taxes</span><span>${taxes.toFixed(2)}</span>
              </div>
            </div>
            <div class="pt-3 flex justify-between items-end">
              <div>
                <p class="muted text-[10px] uppercase tracking-[0.2em]">
                  Total Amount
                </p>
                <h3 class="font-[var(--font-headline)] text-3xl text-[#1b5e20]">
                  ${total.toFixed(2)}
                </h3>
              </div>
              <p
                class="text-[10px] text-on-surface-variant/70 italic text-right"
              >
                All-inclusive gourmet<br />dining included
              </p>
            </div>
            <button class="cta" onclick={()=>submitReservation()} disabled={isSaving || !selectedRoom}>
              {#if isSaving}
                Processing...
              {:else}
                Complete Reservation
              {/if}
            </button>
            <div
              class="flex items-center justify-center gap-2 text-[10px] uppercase tracking-widest font-bold text-on-surface-variant"
            >
              <span class="material-symbols-outlined text-sm"
                >verified_user</span
              >
              Secure SSL Encrypted Payment
            </div>
          </div>
        </div>

        <div class="trust-card">
          <span class="material-symbols-outlined">psychology_alt</span>
          <div>
            <p class="font-[var(--font-headline)] text-sm italic">
              "The silence here is a presence you can feel. A transformative
              stay."
            </p>
            <p class="text-[9px] uppercase tracking-widest mt-1">
              — Condé Nast Traveler
            </p>
          </div>
        </div>
      </div>
    </aside>
  </div>
</main>

<FooterSection />

<style>
  .section-heading {
    display: flex;
    align-items: flex-start;
    gap: 0.9rem;
  }

  .panel {
    background: var(--color-surface-container-low);
    border-radius: 1rem;
    padding: clamp(1.25rem, 3vw, 2rem);
  }

  .field label,
  .block {
    display: block;
    font-size: 0.6rem;
    letter-spacing: 0.2rem;
    text-transform: uppercase;
    color: var(--color-on-surface-variant);
    margin-bottom: 0.4rem;
  }

  .date-range {
    display: grid;
    gap: 0.65rem;
  }

  .date-input,
  .form-input {
    width: 100%;
    padding: 0.85rem 1rem;
    background: white;
    border: 1px solid rgba(0, 0, 0, 0.12);
    border-radius: 0.75rem;
    color: var(--color-on-surface);
  }

  .date-input:focus,
  .form-input:focus {
    outline: none;
    border-color: #1b5e20;
    box-shadow: 0 0 0 3px rgba(27, 94, 32, 0.12);
  }

  .date-separator {
    display: inline-flex;
    align-items: center;
    justify-content: center;
    font-size: 0.72rem;
    font-weight: 700;
    letter-spacing: 0.14rem;
    text-transform: uppercase;
    color: var(--color-on-surface-variant);
  }

  .input-shell {
    display: flex;
    align-items: center;
    gap: 0.75rem;
    background: white;
    padding: 0.9rem 1rem;
    border-radius: 0.75rem;
    border: 1px solid rgba(0, 0, 0, 0.08);
  }

  .stepper-controls {
    display: flex;
    align-items: center;
    gap: 1rem;
    flex: 1;
    justify-content: flex-end;
  }

  .step-btn {
    width: 24px;
    height: 24px;
    border-radius: 50%;
    display: flex;
    align-items: center;
    justify-content: center;
    background: var(--color-surface-container-high);
    color: var(--color-on-surface);
    transition: all 0.2s ease;
  }

  .step-btn:hover:not(:disabled) {
    background: #1b5e20;
    color: white;
  }

  .step-btn:disabled {
    opacity: 0.3;
    cursor: not-allowed;
  }

  .count-display {
    font-weight: 700;
    font-size: 0.9rem;
    min-width: 5rem;
    text-align: center;
  }

  .room-header {
    display: flex;
    justify-content: space-between;
    align-items: flex-start;
    gap: 1rem;
    flex-wrap: wrap;
  }

  .change-link {
    font-size: 0.65rem;
    text-transform: uppercase;
    letter-spacing: 0.2rem;
    color: #1b5e20;
    font-weight: 700;
  }

  .selected-room {
    background: white;
    border-radius: 1rem;
    padding: clamp(1rem, 3vw, 1.5rem);
    box-shadow: 0 18px 40px rgba(0, 0, 0, 0.08);
  }

  .selected-hero {
    display: grid;
    gap: 1.2rem;
    align-items: center;
  }

  .selected-hero img {
    width: 100%;
    height: clamp(200px, 35vw, 240px);
    object-fit: cover;
    border-radius: 0.9rem;
  }

  .selected-info p {
    margin-top: 0.4rem;
  }

  .selected-meta {
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(120px, 1fr));
    gap: 0.75rem;
    margin-top: 1rem;
    font-size: 0.7rem;
    letter-spacing: 0.16rem;
    text-transform: uppercase;
    color: var(--color-on-surface-variant);
  }

  .selected-meta span {
    display: inline-flex;
    align-items: center;
    gap: 0.35rem;
  }

  .gallery-grid {
    margin-top: 1.2rem;
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(110px, 1fr));
    gap: 0.6rem;
  }

  .gallery-grid img {
    width: 100%;
    height: clamp(120px, 24vw, 150px);
    object-fit: cover;
    border-radius: 0.7rem;
  }

  .highlight-grid {
    margin-top: 1.2rem;
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(140px, 1fr));
    gap: 0.75rem;
  }

  .highlight-card {
    border-radius: 0.8rem;
    background: var(--color-surface-container-low);
    padding: 0.9rem 1rem;
    display: flex;
    gap: 0.6rem;
    align-items: flex-start;
    font-size: 0.85rem;
  }

  .highlight-card span {
    color: #1b5e20;
  }

  @media (min-width: 768px) {
    .selected-hero {
      grid-template-columns: 1.1fr 1fr;
    }
  }

  .summary-card {
    background: var(--color-surface-container-high);
    border-radius: 1rem;
    overflow: hidden;
    box-shadow: 0 24px 60px rgba(0, 0, 0, 0.12);
  }

  .summary-body {
    padding: clamp(1.25rem, 3vw, 1.75rem);
    display: grid;
    gap: 1.25rem;
  }

  .summary-image {
    position: relative;
    height: clamp(150px, 28vw, 190px);
  }

  .summary-image img {
    width: 100%;
    height: 100%;
    object-fit: cover;
  }

  .overlay-title {
    position: absolute;
    inset: 0;
    display: flex;
    align-items: flex-end;
    padding: 1rem 1.5rem;
    background: linear-gradient(to top, rgba(0, 0, 0, 0.6), transparent);
    color: white;
    font-family: var(--font-headline);
    font-size: clamp(1.05rem, 2.8vw, 1.35rem);
  }

  .muted {
    color: var(--color-on-surface-variant);
  }

  .cta {
    width: 100%;
    background: linear-gradient(135deg, #0c3a1a, #1b5e20);
    color: white;
    padding: 1rem 1.2rem;
    border-radius: 0.8rem;
    text-transform: uppercase;
    letter-spacing: 0.2rem;
    font-weight: 800;
    font-size: 0.75rem;
    transition:
      transform 0.2s ease,
      box-shadow 0.2s ease;
  }

  .cta:hover {
    transform: translateY(-2px);
    box-shadow: 0 18px 30px rgba(12, 58, 26, 0.25);
  }

  .trust-card {
    background: #1b5e20;
    color: white;
    border-radius: 1rem;
    padding: clamp(1rem, 3vw, 1.5rem);
    display: grid;
    gap: 0.75rem;
    align-items: flex-start;
  }

  .trust-card span {
    font-size: 2rem;
    color: #ffe088;
  }

  @media (min-width: 640px) {
    .date-range {
      grid-template-columns: minmax(0, 1fr) auto minmax(0, 1fr);
      align-items: center;
    }
  }

  @media (max-width: 640px) {
    .stepper {
      flex-direction: column;
      align-items: flex-start;
    }

    .stepper-controls {
      width: 100%;
      justify-content: space-between;
    }

    .count-display {
      min-width: 0;
      flex: 1;
    }
  }
</style>
