<script lang="ts">
  import NavBar from "$lib/components/home/NavBar.svelte";
  import FooterSection from "$lib/components/home/FooterSection.svelte";
  import Lightbox from "$lib/components/ui/Lightbox.svelte";
  import { flip } from "svelte/animate";
  import { scale, fade } from "svelte/transition";
  import { reveal } from "$lib/actions/reveal";

  const categories = ["All", "Suites", "Dining", "Wellness", "Experiences"];

  // Placeholder high-quality images based on Unsplash
  const allImages = [
    { id: 1, src: "https://images.unsplash.com/photo-1540555700478-4be289fbecef?q=80&w=900&auto=format&fit=crop", alt: "Mountain Spa View", category: "Wellness" },
    { id: 2, src: "https://images.unsplash.com/photo-1505691938895-1758d7feb511?q=80&w=900&auto=format&fit=crop", alt: "Master Suite Interior", category: "Suites" },
    { id: 3, src: "https://images.unsplash.com/photo-1504674900247-0877df9cc836?q=80&w=900&auto=format&fit=crop", alt: "Gourmet Dining Experience", category: "Dining" },
    { id: 4, src: "https://images.unsplash.com/photo-1469474968028-56623f02e42e?q=80&w=900&auto=format&fit=crop", alt: "Himalayan View Trek", category: "Experiences" },
    { id: 5, src: "https://images.unsplash.com/photo-1500530855697-b586d89ba3ee?q=80&w=900&auto=format&fit=crop", alt: "Forest Ritual", category: "Wellness" },
    { id: 6, src: "https://images.unsplash.com/photo-1500375592092-40eb2168fd21?q=80&w=900&auto=format&fit=crop", alt: "Infinity Pool", category: "Experiences" },
    { id: 7, src: "https://images.unsplash.com/photo-1514933651103-005eec06c04b?q=80&w=900&auto=format&fit=crop", alt: "Cocktail Bar", category: "Dining" },
    { id: 8, src: "https://images.unsplash.com/photo-1445019980597-93fa8acb246c?q=80&w=900&auto=format&fit=crop", alt: "Deluxe Room", category: "Suites" },
    { id: 9, src: "https://images.unsplash.com/photo-1544161515-4ab6ce6db874?q=80&w=900&auto=format&fit=crop", alt: "Massage Therapy", category: "Wellness" },
    { id: 10, src: "https://images.unsplash.com/photo-1510798831971-661eb04b3739?q=80&w=900&auto=format&fit=crop", alt: "Local Culture Tour", category: "Experiences" }
  ];

  let currentCategory = $state("All");
  
  let filteredImages = $derived(
    currentCategory === "All" 
      ? allImages 
      : allImages.filter(img => img.category === currentCategory)
  );

  let lightboxOpen = $state(false);
  let lightboxIndex = $state(0);

  function openLightbox(index: number) {
    lightboxIndex = index;
    lightboxOpen = true;
  }
</script>

<svelte:head>
  <title>Gallery | Sarvashrestha</title>
  <meta name="description" content="Explore the visual story of Sarvashrestha, a sanctuary in the Himalayas." />
</svelte:head>

<NavBar />

<!-- Hero Section -->
<header class="bg-surface pt-32 pb-16 md:pt-40 md:pb-24 px-6 relative border-b border-surface-container-high/50">
  <div class="mx-auto max-w-7xl text-center">
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
    <div class="mb-12 flex flex-wrap justify-center gap-2 md:gap-4 sticky top-24 z-30 bg-surface/80 backdrop-blur-md py-4 -mx-6 px-6 sm:mx-0 sm:px-0">
      {#each categories as category}
        <button 
          class="rounded-full px-5 py-2 text-sm font-medium tracking-wide transition-all duration-300"
          class:bg-on-surface={currentCategory === category}
          class:text-surface={currentCategory === category}
          class:bg-surface-container-high={currentCategory !== category}
          class:text-on-surface-variant={currentCategory !== category}
          class:hover:bg-surface-container-highest={currentCategory !== category}
          onclick={() => currentCategory = category}
        >
          {category}
        </button>
      {/each}
    </div>

    <!-- Masonry Grid -->
    <div class="columns-1 gap-6 sm:columns-2 lg:columns-3 xl:columns-4 space-y-6">
      {#each filteredImages as image, i (image.id)}
        <!-- svelte-ignore a11y_click_events_have_key_events -->
        <!-- svelte-ignore a11y_no_static_element_interactions -->
        <div 
          class="break-inside-avoid relative group overflow-hidden rounded-xl cursor-pointer shadow-sm hover:shadow-xl transition-shadow duration-300 bg-surface-container"
          in:scale={{duration: 400, delay: i * 50, start: 0.9}}
          animate:flip={{duration: 400}}
          onclick={() => openLightbox(i)}
        >
          <img 
            src={image.src} 
            alt={image.alt}
            class="w-full object-cover transition-transform duration-700 group-hover:scale-105"
            loading="lazy"
          />
          <div class="absolute inset-0 bg-gradient-to-t from-black/70 via-black/10 to-transparent opacity-0 transition-opacity duration-300 group-hover:opacity-100 flex flex-col justify-end p-6">
            <span class="text-white/80 text-xs font-semibold uppercase tracking-widest mb-1">{image.category}</span>
            <p class="text-white font-[var(--font-headline)] text-lg">{image.alt}</p>
          </div>
        </div>
      {/each}
    </div>

    {#if filteredImages.length === 0}
      <div class="text-center py-24 text-on-surface-variant" in:fade>
        No images found for this category.
      </div>
    {/if}

  </div>
</main>

<FooterSection />

<Lightbox 
  images={filteredImages}
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
</style>
