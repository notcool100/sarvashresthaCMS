<script lang="ts">
  import { onMount } from "svelte";
  import NavBar from "$lib/components/home/NavBar.svelte";
  import FooterSection from "$lib/components/home/FooterSection.svelte";
  import type { Room } from "$lib/types/api";
  import { roomService } from "$lib/services/roomService";
  import { PUBLIC_API_BASE_URL } from "$env/static/public";

  const fallbackImages = [
    "https://images.unsplash.com/photo-1505691938895-1758d7feb511?q=80&w=1600&auto=format&fit=crop",
    "https://images.unsplash.com/photo-1505693416388-ac5ce068fe85?q=80&w=1600&auto=format&fit=crop",
    "https://images.unsplash.com/photo-1507089947368-19c1da9775ae?q=80&w=1600&auto=format&fit=crop"
  ];

  let rooms = $state<Room[]>([]);
  let isLoading = $state(true);

  onMount(async () => {
    const response = await roomService.getAll();
    rooms = response.data ?? [];
    isLoading = false;
  });

  const roomsDerived = $derived(rooms);
  const featuredRoom = $derived(roomsDerived[0]);
  const secondaryRoom = $derived(roomsDerived[1]);
  const gridRooms = $derived(roomsDerived.slice(2, 8));

  const getFullImageUrl = (path?: string) => {
    if (!path) return "";
    if (path.startsWith("http")) return path;
    const base = PUBLIC_API_BASE_URL.replace("/api", "");
    return `${base}${path}`;
  };

  const imageFor = (room?: Room, index = 0) => {
    const url = room?.imageUrls?.[index] ?? room?.imageUrls?.[0];
    return getFullImageUrl(url) || fallbackImages[index % fallbackImages.length];
  };

  const slug = (room?: Room) => (room ? `/booking?room=${room.id}` : "/booking");
</script>

<svelte:head>
  <title>Rooms | Sarvashrestha Resort & Spa</title>
  <link
    href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&display=swap"
    rel="stylesheet"
  />
</svelte:head>

<NavBar />

<main class="pt-28 pb-24 bg-surface text-on-surface">
  <section class="max-w-7xl mx-auto px-6 mb-16">
    <div class="flex flex-col lg:flex-row items-end justify-between gap-10">
      <div class="max-w-2xl">
        <span class="label-eyebrow">Refined Living</span>
        <h1 class="mt-4 font-[var(--font-headline)] text-5xl md:text-6xl text-[#1b5e20] leading-tight">
          Sanctuary Suites & Residencies
        </h1>
      </div>
      <div class="max-w-sm lg:text-right text-on-surface-variant leading-relaxed">
        Each space is a deliberate intersection of Himalayan tradition and modern stillness, curated
        to provide an unparalleled connection to the peaks.
      </div>
    </div>
  </section>

  <section class="bg-[var(--color-surface-container-low)] py-6 mb-14">
    <div class="max-w-7xl mx-auto px-6 flex flex-col lg:flex-row items-center justify-between gap-8">
      <div class="flex flex-wrap items-center gap-10">
        <div class="filter-chip">
          <label>View by Tier</label>
          <div class="filter-value">
            <span>All Collections</span>
            <span class="material-symbols-outlined">expand_more</span>
          </div>
        </div>
        <div class="filter-chip">
          <label>Nightly Rate</label>
          <div class="filter-value">
            <span>Any Price</span>
            <span class="material-symbols-outlined">expand_more</span>
          </div>
        </div>
        <div class="filter-chip">
          <label>Occupancy</label>
          <div class="filter-value">
            <span>2 Guests</span>
            <span class="material-symbols-outlined">expand_more</span>
          </div>
        </div>
      </div>
      <div class="advanced-filter">
        <span class="material-symbols-outlined">filter_list</span>
        <span>Advanced Filters</span>
      </div>
    </div>
    <div class="max-w-7xl mx-auto px-6 mt-6 flex flex-col lg:flex-row items-center justify-between gap-6">
      <div class="filter-pills">
        <button class="pill is-active">All</button>
        <button class="pill">Suites</button>
        <button class="pill">Villas</button>
        <button class="pill">Signature</button>
        <button class="pill">Mountain View</button>
      </div>
      <div class="filter-input">
        <span class="material-symbols-outlined">search</span>
        <input type="text" placeholder="Search by suite name" />
      </div>
    </div>
  </section>

  <section class="max-w-7xl mx-auto px-6">
    <div class="grid grid-cols-1 lg:grid-cols-12 gap-y-16 gap-x-10">
      {#if featuredRoom}
        <div class="lg:col-span-8 group">
          <div class="relative overflow-hidden rounded-2xl aspect-[16/9] mb-6">
            <a href={slug(featuredRoom)}>
              <img
                class="w-full h-full object-cover transition-transform duration-700 group-hover:scale-105"
                alt={featuredRoom.name}
                src={imageFor(featuredRoom)}
              />
            </a>
            <div class="heritage-overlay absolute bottom-5 right-5 p-6 rounded-xl shadow-2xl max-w-xs">
              <span class="material-symbols-outlined text-secondary block mb-2" style="font-variation-settings: 'FILL' 1;">spa</span>
              <h3 class="font-[var(--font-headline)] text-2xl mb-2">{featuredRoom.name}</h3>
              <p class="text-sm text-on-surface-variant mb-4">
                {featuredRoom.description}
              </p>
              <div class="flex items-center justify-between border-t border-black/10 pt-4">
                <span class="font-semibold text-lg">${featuredRoom.pricePerNight} <span class="text-xs text-on-surface-variant">/ NIGHT</span></span>
                <span class="text-xs font-bold uppercase tracking-widest text-secondary">{featuredRoom.viewType || "Available"}</span>
              </div>
            </div>
          </div>
          <div class="flex flex-wrap gap-6 text-xs uppercase tracking-widest text-on-surface-variant">
            <div class="flex items-center gap-2">
              <span class="material-symbols-outlined text-secondary text-sm">bed</span>
              {featuredRoom.bedType || "Signature Bed"}
            </div>
            <div class="flex items-center gap-2">
              <span class="material-symbols-outlined text-secondary text-sm">bathtub</span>
              Attached Bath
            </div>
            <div class="flex items-center gap-2">
              <span class="material-symbols-outlined text-secondary text-sm">landscape</span>
              {featuredRoom.viewType || "Mountain View"}
            </div>
          </div>
        </div>
      {/if}

      {#if secondaryRoom}
        <div class="lg:col-span-4 group lg:mt-10">
          <div class="bg-[var(--color-surface-container)] rounded-2xl overflow-hidden h-full flex flex-col">
            <div class="relative h-[420px] overflow-hidden">
              <a href={slug(secondaryRoom)}>
                <img
                  class="w-full h-full object-cover transition-transform duration-700 group-hover:scale-110"
                  alt={secondaryRoom.name}
                  src={imageFor(secondaryRoom, 1)}
                />
              </a>
            </div>
            <div class="p-8 flex-grow flex flex-col justify-between">
              <div>
                <h3 class="font-[var(--font-headline)] text-3xl mb-4">{secondaryRoom.name}</h3>
                <p class="text-on-surface-variant leading-relaxed mb-6">
                  {secondaryRoom.description}
                </p>
              </div>
              <div class="pt-6 border-t border-black/10">
                <div class="flex justify-between items-baseline mb-6">
                  <span class="font-semibold text-2xl text-[#1b5e20]">${secondaryRoom.pricePerNight}</span>
                  <span class="text-[10px] uppercase tracking-[0.2rem] text-secondary font-bold">Limited Dates</span>
                </div>
                <a class="w-full block text-center border-2 border-[#1b5e20] text-[#1b5e20] hover:bg-[#1b5e20] hover:text-white py-3 rounded-lg font-label uppercase text-xs font-bold transition-all duration-300" href={slug(secondaryRoom)}>
                  View Details
                </a>
              </div>
            </div>
          </div>
        </div>
      {/if}

      {#each gridRooms as room}
        <div class="lg:col-span-4 group">
          <div class="relative overflow-hidden rounded-2xl aspect-[4/5] mb-5">
            <a href={slug(room)}>
              <img
                class="w-full h-full object-cover transition-transform duration-700 group-hover:scale-105"
                alt={room.name}
                src={imageFor(room, 2)}
              />
            </a>
            <div class="absolute inset-0 bg-gradient-to-t from-[#0c3a1a]/70 to-transparent"></div>
            <div class="absolute bottom-6 left-6 text-white">
              <h3 class="font-[var(--font-headline)] text-2xl">{room.name}</h3>
              <p class="text-xs uppercase tracking-widest opacity-80">${room.pricePerNight} / Night</p>
            </div>
          </div>
        </div>
      {/each}
    </div>
  </section>

  <section class="mt-24 max-w-7xl mx-auto px-6">
    <div class="bg-[#0c3a1a] rounded-3xl p-12 md:p-16 flex flex-col lg:flex-row items-center gap-12 relative overflow-hidden">
      <div class="absolute inset-0 opacity-10 pointer-events-none" style="background-image: radial-gradient(circle at 2px 2px, white 1px, transparent 0); background-size: 26px 26px;"></div>
      <div class="lg:w-1/2 relative z-10">
        <span class="text-secondary-fixed-dim text-xs uppercase tracking-[0.3rem] font-bold block mb-6">
          Curated Journeys
        </span>
        <h2 class="font-[var(--font-headline)] text-4xl md:text-5xl text-white mb-6 leading-tight">
          Beyond the Suite Experience
        </h2>
        <p class="text-white/80 text-lg mb-8 leading-relaxed">
          Every reservation includes a private consultation with our Experience Curator to tailor your
          Himalayan residency to your personal intentions.
        </p>
        <div class="flex flex-wrap gap-4">
          <a class="bg-secondary text-white px-8 py-4 rounded-lg font-label uppercase text-xs font-bold hover:bg-secondary-fixed-dim transition-colors" href="/booking">
            Compare All Rooms
          </a>
          <a class="border border-white/30 text-white px-8 py-4 rounded-lg font-label uppercase text-xs font-bold hover:bg-white/10 transition-colors" href="/#experiences">
            Our Amenities
          </a>
        </div>
      </div>
      <div class="lg:w-1/2 relative">
        <img
          class="rounded-2xl shadow-2xl relative z-10 rotate-3 transition-transform duration-500 hover:rotate-0"
          alt="Artisanal breakfast tray"
          src="https://images.unsplash.com/photo-1504674900247-0877df9cc836?q=80&w=1200&auto=format&fit=crop"
        />
        <div class="absolute -top-8 -right-6 w-52 h-52 bg-secondary/30 rounded-full blur-3xl"></div>
      </div>
    </div>
  </section>

  <section class="mt-24 max-w-7xl mx-auto px-6 grid lg:grid-cols-12 gap-12 items-start">
    <div class="lg:col-span-5">
      <span class="label-eyebrow">Signature Amenities</span>
      <h2 class="mt-4 font-[var(--font-headline)] text-4xl text-[#1b5e20]">Included With Every Stay</h2>
      <p class="mt-5 text-on-surface-variant leading-relaxed">
        Thoughtfully layered services designed to keep you present: private transfers, curated rituals,
        and restorative spaces tuned to the Himalayan rhythm.
      </p>
      <div class="mt-8 grid gap-4">
        <div class="amenity-row">
          <span class="material-symbols-outlined">airport_shuttle</span>
          <div>
            <h3>Private Transfers</h3>
            <p>Seamless pickups from Pokhara and local heliport coordination.</p>
          </div>
        </div>
        <div class="amenity-row">
          <span class="material-symbols-outlined">spa</span>
          <div>
            <h3>Daily Wellness Rituals</h3>
            <p>Sunrise breathwork, sound baths, and guided meditation.</p>
          </div>
        </div>
        <div class="amenity-row">
          <span class="material-symbols-outlined">restaurant</span>
          <div>
            <h3>Farm-to-Table Dining</h3>
            <p>Seasonal tasting menus with private chef options.</p>
          </div>
        </div>
      </div>
    </div>
    <div class="lg:col-span-7 grid sm:grid-cols-2 gap-6">
      <div class="amenity-card">
        <h4>Altitude Sleep Suite</h4>
        <p>Oxygen-balanced sleeping environments for deeper rest.</p>
      </div>
      <div class="amenity-card">
        <h4>Concierge Planning</h4>
        <p>Custom itineraries, treks, and cultural immersions.</p>
      </div>
      <div class="amenity-card">
        <h4>In-Suite Spa</h4>
        <p>Private therapists, herbal soaks, and Himalayan stone rituals.</p>
      </div>
      <div class="amenity-card">
        <h4>Library & Tea Lounge</h4>
        <p>Quiet nooks, rare teas, and artisanal pastries throughout the day.</p>
      </div>
    </div>
  </section>

  <section class="mt-24 bg-[var(--color-surface-container-low)] py-16">
    <div class="max-w-7xl mx-auto px-6">
      <div class="flex flex-col lg:flex-row items-start justify-between gap-10">
        <div class="max-w-xl">
          <span class="label-eyebrow">Room Comparison</span>
          <h2 class="mt-4 font-[var(--font-headline)] text-4xl text-[#1b5e20]">Choose Your Sanctuary</h2>
          <p class="mt-5 text-on-surface-variant">
            Compare suite tiers to find the right balance of privacy, views, and wellness features.
          </p>
        </div>
        <a class="btn-secondary" href="/booking">Request Availability</a>
      </div>

      <div class="mt-12 overflow-x-auto">
        <table class="comparison-table">
          <thead>
            <tr>
              <th>Suite</th>
              <th>View</th>
              <th>Space</th>
              <th>Wellness</th>
              <th>Starting</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>Summit Pavilion</td>
              <td>Panoramic Peaks</td>
              <td>120m²</td>
              <td>Private Meditation Deck</td>
              <td>$1,250</td>
            </tr>
            <tr>
              <td>Forest Haven</td>
              <td>Rhododendron Canopy</td>
              <td>90m²</td>
              <td>Herbal Soak</td>
              <td>$850</td>
            </tr>
            <tr>
              <td>Zen Terrace</td>
              <td>Garden Courtyard</td>
              <td>70m²</td>
              <td>Outdoor Soaking Tub</td>
              <td>$620</td>
            </tr>
            <tr>
              <td>Heritage Wing</td>
              <td>Valley Sunrise</td>
              <td>100m²</td>
              <td>Library + Fireplace</td>
              <td>$910</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </section>

  <section class="mt-24 max-w-7xl mx-auto px-6">
    <div class="grid lg:grid-cols-3 gap-8">
      <div class="testimonial-card">
        <p class="quote">“The Summit Pavilion made every sunrise feel sacred. The staff anticipated every need.”</p>
        <p class="quote-meta">Aditi R. · Kathmandu</p>
      </div>
      <div class="testimonial-card">
        <p class="quote">“Forest Haven is the ultimate retreat. The in-suite spa ritual was unforgettable.”</p>
        <p class="quote-meta">Lucas M. · Sydney</p>
      </div>
      <div class="testimonial-card">
        <p class="quote">“Immaculate design, seamless service, and the best sleep I’ve had in years.”</p>
        <p class="quote-meta">Nina P. · Singapore</p>
      </div>
    </div>
  </section>

  <section class="mt-24 max-w-7xl mx-auto px-6">
    <div class="faq-card">
      <div>
        <span class="label-eyebrow">Planning</span>
        <h2 class="mt-4 font-[var(--font-headline)] text-4xl text-[#1b5e20]">Frequently Asked</h2>
      </div>
      <div class="faq-grid">
        <div>
          <h3>What is the minimum stay?</h3>
          <p>Two nights year-round; three nights during holiday weeks.</p>
        </div>
        <div>
          <h3>Do you offer family suites?</h3>
          <p>Yes, our Heritage Wing can be configured for families or groups.</p>
        </div>
        <div>
          <h3>How do I book experiences?</h3>
          <p>Our concierge schedules experiences prior to arrival or on-site.</p>
        </div>
        <div>
          <h3>Is altitude acclimatization supported?</h3>
          <p>We offer wellness consultations, oxygen support, and flexible itineraries.</p>
        </div>
      </div>
    </div>
  </section>
</main>

<FooterSection />

<style>
  .label-eyebrow {
    font-size: 0.7rem;
    letter-spacing: 0.25rem;
    text-transform: uppercase;
    color: var(--color-secondary);
    font-weight: 700;
    display: block;
  }

  .filter-chip label {
    font-size: 0.6rem;
    letter-spacing: 0.2rem;
    text-transform: uppercase;
    color: var(--color-on-surface-variant);
    display: block;
    margin-bottom: 0.3rem;
  }

  .filter-value {
    display: flex;
    align-items: center;
    gap: 0.5rem;
    font-weight: 700;
    color: #1b5e20;
  }

  .advanced-filter {
    display: inline-flex;
    align-items: center;
    gap: 0.5rem;
    background: var(--color-surface-container-highest);
    padding: 0.75rem 1.5rem;
    border-radius: 999px;
    font-size: 0.7rem;
    font-weight: 700;
    text-transform: uppercase;
    letter-spacing: 0.15rem;
  }

  .filter-pills {
    display: flex;
    flex-wrap: wrap;
    gap: 0.6rem;
  }

  .pill {
    border: 1px solid rgba(0, 0, 0, 0.12);
    padding: 0.45rem 1.2rem;
    border-radius: 999px;
    font-size: 0.7rem;
    letter-spacing: 0.12rem;
    text-transform: uppercase;
    font-weight: 600;
    background: white;
    transition: transform 0.2s ease, box-shadow 0.2s ease;
  }

  .pill:hover {
    transform: translateY(-1px);
    box-shadow: 0 10px 20px rgba(0, 0, 0, 0.08);
  }

  .pill.is-active {
    background: #1b5e20;
    color: white;
    border-color: #1b5e20;
  }

  .filter-input {
    display: flex;
    align-items: center;
    gap: 0.6rem;
    background: white;
    border-radius: 999px;
    padding: 0.5rem 1rem;
    border: 1px solid rgba(0, 0, 0, 0.12);
    min-width: 260px;
  }

  .filter-input input {
    border: none;
    outline: none;
    width: 100%;
    background: transparent;
    font-size: 0.9rem;
  }

  .heritage-overlay {
    backdrop-filter: blur(12px);
    background: rgba(252, 249, 244, 0.88);
  }

  .amenity-row {
    display: grid;
    grid-template-columns: auto 1fr;
    gap: 0.9rem;
    align-items: start;
    padding: 0.75rem 0;
    border-bottom: 1px solid rgba(0, 0, 0, 0.08);
  }

  .amenity-row h3 {
    font-weight: 600;
    margin-bottom: 0.35rem;
  }

  .amenity-row p {
    color: var(--color-on-surface-variant);
    font-size: 0.9rem;
  }

  .amenity-card {
    background: white;
    border-radius: 1.25rem;
    padding: 1.5rem;
    box-shadow: 0 16px 32px rgba(0, 0, 0, 0.08);
  }

  .amenity-card h4 {
    font-weight: 600;
    margin-bottom: 0.4rem;
  }

  .amenity-card p {
    color: var(--color-on-surface-variant);
    font-size: 0.9rem;
  }

  .btn-secondary {
    background: #1b5e20;
    color: white;
    padding: 0.9rem 2.4rem;
    border-radius: 999px;
    font-weight: 600;
    font-size: 0.75rem;
    letter-spacing: 0.2em;
    text-transform: uppercase;
    display: inline-flex;
    align-items: center;
    justify-content: center;
    transition: transform 0.3s ease, box-shadow 0.3s ease;
  }

  .btn-secondary:hover {
    transform: translateY(-2px);
    box-shadow: 0 18px 30px rgba(12, 58, 26, 0.25);
  }

  .comparison-table {
    width: 100%;
    border-collapse: collapse;
    min-width: 640px;
    background: white;
    border-radius: 1rem;
    overflow: hidden;
    box-shadow: 0 16px 32px rgba(0, 0, 0, 0.08);
  }

  .comparison-table th,
  .comparison-table td {
    padding: 1rem 1.25rem;
    text-align: left;
    font-size: 0.9rem;
  }

  .comparison-table thead {
    background: rgba(12, 58, 26, 0.08);
    text-transform: uppercase;
    letter-spacing: 0.15rem;
    font-size: 0.7rem;
    color: #1b5e20;
  }

  .comparison-table tbody tr {
    border-top: 1px solid rgba(0, 0, 0, 0.08);
  }

  .testimonial-card {
    background: white;
    border-radius: 1.25rem;
    padding: 2rem;
    box-shadow: 0 16px 32px rgba(0, 0, 0, 0.08);
  }

  .quote {
    font-family: var(--font-headline);
    font-size: 1.1rem;
    line-height: 1.6;
  }

  .quote-meta {
    margin-top: 1rem;
    text-transform: uppercase;
    letter-spacing: 0.2rem;
    font-size: 0.7rem;
    color: var(--color-on-surface-variant);
  }

  .faq-card {
    background: var(--color-surface-container-low);
    border-radius: 1.5rem;
    padding: 2.5rem;
  }

  .faq-grid {
    margin-top: 2rem;
    display: grid;
    gap: 1.5rem;
  }

  .faq-grid h3 {
    font-weight: 600;
    margin-bottom: 0.4rem;
  }

  .faq-grid p {
    color: var(--color-on-surface-variant);
  }

  @media (min-width: 768px) {
    .faq-grid {
      grid-template-columns: repeat(2, minmax(0, 1fr));
    }
  }
</style>
