<script lang="ts">
  import NavBar from "$lib/components/home/NavBar.svelte";
  import FooterSection from "$lib/components/home/FooterSection.svelte";
  import Lightbox from "$lib/components/ui/Lightbox.svelte";
  import { flip } from "svelte/animate";
  import { scale, fade } from "svelte/transition";
  import { galleryService } from "$lib/services/galleryService";
  import { PUBLIC_API_BASE_URL } from "$env/static/public";
  import { onMount } from "svelte";
  import type { GalleryItem } from "$lib/types/api";

  const categories = ["All", "Suites", "Dining", "Wellness", "Experiences", "Exterior", "Events"];
  
  let allImages = $state<GalleryItem[]>([]);
  let loading = $state(true);
  let currentCategory = $state("All");
  
  let filteredImages = $derived(
    currentCategory === "All" 
      ? allImages 
      : allImages.filter(img => img.category === currentCategory)
  );

  let lightboxOpen = $state(false);
  let lightboxIndex = $state(0);

  async function loadImages() {
    loading = true;
    try {
      const response = await galleryService.getAll();
      if (response.success && response.data) {
        allImages = response.data;
      }
    } catch (e) {
      console.error('Failed to load gallery', e);
    } finally {
      loading = false;
    }
  }

  function getFullImageUrl(path: string) {
    if (!path) return '';
    if (path.startsWith('http')) return path;
    const base = PUBLIC_API_BASE_URL.replace('/api', '');
    return `${base}${path}`;
  }

  function openLightbox(index: number) {
    lightboxIndex = index;
    lightboxOpen = true;
  }

  onMount(loadImages);
</script>

<svelte:head>
  <title>Gallery | Sarvashrestha</title>
  <meta name="description" content="Explore the visual story of Sarvashrestha, a sanctuary in the Himalayas." />
</svelte:head>

<NavBar />

<!-- Hero Section -->
<header class="bg-surface pt-32 pb-16 md:pt-40 md:pb-24 px-6 relative border-b border-surface-container-high/50 overflow-hidden">
  <!-- Subtle gradient background element -->
  <div class="absolute -top-24 -right-24 w-96 h-96 bg-emerald-500/5 blur-[120px] rounded-full"></div>
  <div class="absolute -bottom-24 -left-24 w-96 h-96 bg-secondary/5 blur-[120px] rounded-full"></div>
  
  <div class="mx-auto max-w-7xl text-center relative z-10">
    <p class="uppercase tracking-[0.4em] text-secondary text-xs sm:text-sm mb-4 font-semibold" in:fade={{duration: 800, delay: 100}}>Visual Story</p>
    <h1 class="font-[var(--font-headline)] text-5xl md:text-6xl lg:text-7xl text-on-surface mb-6" in:fade={{duration: 800, delay: 200}}>
      Discover <span class="italic font-light">the Sanctuary</span>
    </h1>
    <p class="mx-auto max-w-2xl text-on-surface-variant md:text-lg font-light leading-relaxed" in:fade={{duration: 800, delay: 300}}>
      Immerse yourself in our serene spaces, exquisite culinary journeys, and the breathtaking natural beauty that surrounds us.
    </p>
  </div>
</header>

<main class="bg-surface pb-32">
  <div class="mx-auto max-w-7xl px-6 lg:px-8 pt-12">
    
    <!-- Category Filters -->
    <div class="mb-12 flex flex-wrap justify-center gap-2 md:gap-4 sticky top-24 z-30 bg-surface/80 backdrop-blur-md py-4 -mx-6 px-6 sm:mx-0 sm:px-0 rounded-2xl">
      {#each categories as category}
        <button 
          class="rounded-full px-6 py-2.5 text-sm font-medium tracking-wide transition-all duration-500 transform hover:scale-105 {currentCategory === category ? 'bg-emerald-900 text-amber-400 shadow-lg shadow-emerald-900/20' : 'bg-surface-container-high text-on-surface-variant hover:bg-surface-container-highest'}"
          onclick={() => currentCategory = category}
        >
          {category}
        </button>
      {/each}
    </div>

    <!-- Masonry Grid -->
    {#if loading}
      <div class="flex items-center justify-center min-h-[400px]">
        <div class="w-12 h-12 border-4 border-emerald-900/10 border-t-emerald-900 rounded-full animate-spin"></div>
      </div>
    {:else}
      <div class="columns-1 gap-8 sm:columns-2 lg:columns-3 xl:columns-4 space-y-8">
        {#each filteredImages as image, i (image.id)}
          <!-- svelte-ignore a11y_click_events_have_key_events -->
          <!-- svelte-ignore a11y_no_static_element_interactions -->
          <div 
            class="break-inside-avoid relative group overflow-hidden rounded-2xl cursor-pointer shadow-sm hover:shadow-2xl transition-all duration-700 bg-surface-container-low"
            in:scale={{duration: 600, delay: i * 50, start: 0.95}}
            animate:flip={{duration: 600}}
            onclick={() => openLightbox(i)}
          >
            <img 
              src={getFullImageUrl(image.imageUrl)} 
              alt={image.altText}
              class="w-full object-cover transition-transform duration-[1.5s] ease-out group-hover:scale-110"
              loading="lazy"
            />
            
            <!-- Hover Overlay -->
            <div class="absolute inset-0 bg-gradient-to-t from-emerald-950/90 via-emerald-950/20 to-transparent opacity-0 transition-all duration-500 group-hover:opacity-100 flex flex-col justify-end p-8 transform translate-y-4 group-hover:translate-y-0">
              <span class="text-amber-400 text-[10px] font-bold uppercase tracking-[0.2em] mb-2 block">{image.category}</span>
              <p class="text-white font-[var(--font-headline)] text-xl mb-1 leading-tight">{image.altText || 'Untitled Moment'}</p>
              <div class="w-8 h-0.5 bg-amber-400/50 transform origin-left scale-x-0 group-hover:scale-x-100 transition-transform duration-700 delay-100"></div>
            </div>

            <!-- Subtle Border Reveal -->
            <div class="absolute inset-0 border border-white/10 rounded-2xl pointer-events-none group-hover:border-white/20 transition-colors duration-500"></div>
          </div>
        {/each}
      </div>

      {#if filteredImages.length === 0}
        <div class="text-center py-32 text-on-surface-variant flex flex-col items-center" in:fade>
          <span class="material-symbols-outlined text-4xl mb-4 opacity-20">filter_vintage</span>
          <p class="font-light italic">More stories coming soon to this collection...</p>
        </div>
      {/if}
    {/if}

  </div>
</main>

<FooterSection />

<Lightbox 
  images={filteredImages.map(img => ({ ...img, src: getFullImageUrl(img.imageUrl), alt: img.altText }))}
  currentIndex={lightboxIndex}
  isOpen={lightboxOpen}
  onClose={() => lightboxOpen = false}
  onNext={() => lightboxIndex = (lightboxIndex + 1) % filteredImages.length}
  onPrev={() => lightboxIndex = (lightboxIndex - 1 + filteredImages.length) % filteredImages.length}
/>

<style>
  :global(html) {
    scroll-behavior: smooth;
  }

  .columns-1 {
    column-fill: balance;
  }
</style>
