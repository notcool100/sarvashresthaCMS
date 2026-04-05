<script lang="ts">
  import { fade, scale } from 'svelte/transition';
  import { onMount } from 'svelte';

  let { 
    images = [],
    currentIndex = 0,
    isOpen = false,
    onClose = () => {},
    onNext = () => {},
    onPrev = () => {}
  }: {
    images?: Array<{src: string, alt?: string, [key: string]: any}>,
    currentIndex?: number,
    isOpen?: boolean,
    onClose?: () => void,
    onNext?: () => void,
    onPrev?: () => void
  } = $props();

  function handleKeydown(event: KeyboardEvent) {
    if (!isOpen) return;
    if (event.key === 'Escape') onClose();
    if (event.key === 'ArrowRight') onNext();
    if (event.key === 'ArrowLeft') onPrev();
  }

  // Prevent background scrolling when lightbox is open
  $effect(() => {
    if (typeof window !== 'undefined') {
      if (isOpen) {
        document.body.style.overflow = 'hidden';
      } else {
        document.body.style.overflow = '';
      }
    }
  });
</script>

<svelte:window on:keydown={handleKeydown} />

{#if isOpen && images.length > 0}
  <!-- svelte-ignore a11y_click_events_have_key_events -->
  <!-- svelte-ignore a11y_no_noninteractive_element_interactions -->
  <div 
    class="fixed inset-0 z-50 flex items-center justify-center bg-black/90 backdrop-blur-sm"
    transition:fade={{ duration: 200 }}
    onclick={onClose}
    role="dialog"
    aria-modal="true"
    tabindex="-1"
  >
    <div 
      class="relative flex h-full w-full items-center justify-center p-4 md:p-12"
      onclick={(e) => e.stopPropagation()}
      role="document"
    >
      <!-- Close Button -->
      <button 
        class="absolute right-4 top-4 z-10 p-2 text-white/70 transition hover:text-white md:right-8 md:top-8"
        onclick={onClose}
        aria-label="Close"
      >
        <span class="material-symbols-outlined text-3xl md:text-4xl hover:scale-110 transition-transform">close</span>
      </button>

      <!-- Navigation Arrows (only if multiple images) -->
      {#if images.length > 1}
        <button 
          class="absolute left-4 top-1/2 z-10 -translate-y-1/2 p-2 text-white/50 transition hover:text-white md:left-8"
          onclick={(e) => { e.stopPropagation(); onPrev(); }}
          aria-label="Previous image"
        >
          <span class="material-symbols-outlined text-4xl md:text-5xl hover:-translate-x-1 transition-transform">chevron_left</span>
        </button>

        <button 
          class="absolute right-4 top-1/2 z-10 -translate-y-1/2 p-2 text-white/50 transition hover:text-white md:right-8"
          onclick={(e) => { e.stopPropagation(); onNext(); }}
          aria-label="Next image"
        >
          <span class="material-symbols-outlined text-4xl md:text-5xl hover:translate-x-1 transition-transform">chevron_right</span>
        </button>
      {/if}

      <!-- Main Image -->
      <div 
        class="relative flex flex-col items-center shadow-2xl"
        in:scale={{ duration: 300, start: 0.95 }}
      >
        <img 
          src={images[currentIndex].src} 
          alt={images[currentIndex].alt || 'Gallery image'}
          class="max-h-[85vh] max-w-full rounded-md object-contain"
          loading="lazy"
        />
        
        {#if images[currentIndex].alt}
          <div class="absolute bottom-0 left-0 w-full rounded-b-md bg-gradient-to-t from-black/80 to-transparent p-6 pt-12 text-center text-white opacity-0 transition-opacity hover:opacity-100 sm:opacity-100">
            <p class="font-body text-sm tracking-wider uppercase">{images[currentIndex].alt}</p>
          </div>
        {/if}
      </div>

      <!-- Counter -->
      {#if images.length > 1}
        <div class="absolute bottom-6 left-1/2 -translate-x-1/2 text-sm text-white/70 font-body uppercase tracking-widest">
          {currentIndex + 1} / {images.length}
        </div>
      {/if}
    </div>
  </div>
{/if}


