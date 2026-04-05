<section class="bg-surface py-24 sm:py-32" id="gallery">
  <div class="mx-auto max-w-7xl px-6 lg:px-8">
    <div class="flex flex-col items-start justify-between gap-6 md:flex-row md:items-end">
      <div>
        <p class="uppercase tracking-[0.3em] text-secondary text-sm font-semibold">Gallery</p>
        <h2 class="mt-4 font-[var(--font-headline)] text-4xl leading-tight sm:text-5xl lg:text-6xl text-on-surface">
          A Glimpse of<br><span class="italic text-on-surface-variant font-light">the Sanctuary</span>
        </h2>
      </div>
      <a 
        class="group inline-flex items-center gap-3 text-sm font-medium uppercase tracking-widest text-on-surface hover:text-secondary transition-colors" 
        href="/gallery"
      >
        View Full Gallery
        <span class="material-symbols-outlined transition-transform duration-300 group-hover:translate-x-2">arrow_right_alt</span>
      </a>
    </div>

    <!-- Asymmetric Masonry Grid -->
    <div class="mt-16 grid grid-cols-1 md:grid-cols-4 grid-rows-[auto] gap-4 md:gap-6">
      
      <!-- Large Featured Image -->
      <a href="/gallery" class="group relative block w-full overflow-hidden rounded-2xl md:col-span-2 md:row-span-2 h-[400px] md:h-[600px]">
        <img class="h-full w-full object-cover transition-transform duration-700 group-hover:scale-105" alt="Mountain spa" src="https://images.unsplash.com/photo-1540555700478-4be289fbecef?q=80&w=1200&auto=format&fit=crop" loading="lazy" />
        <div class="absolute inset-0 bg-gradient-to-t from-black/60 via-transparent to-transparent opacity-0 transition-opacity duration-300 group-hover:opacity-100"></div>
      </a>
      
      <!-- Medium Image Top -->
      <a href="/gallery" class="group relative block w-full overflow-hidden rounded-2xl md:col-span-2 h-[250px] md:h-[288px]">
        <img class="h-full w-full object-cover transition-transform duration-700 group-hover:scale-105" alt="Tea ceremony" src="https://images.unsplash.com/photo-1504674900247-0877df9cc836?q=80&w=900&auto=format&fit=crop" loading="lazy" />
        <div class="absolute inset-0 bg-black/20 opacity-0 transition-opacity duration-300 group-hover:opacity-100"></div>
      </a>

      <!-- Small Image 1 -->
      <a href="/gallery" class="group relative block w-full overflow-hidden rounded-2xl h-[250px] md:h-[288px]">
        <img class="h-full w-full object-cover transition-transform duration-700 group-hover:scale-105" alt="Himalayan view" src="https://images.unsplash.com/photo-1469474968028-56623f02e42e?q=80&w=600&auto=format&fit=crop" loading="lazy" />
        <div class="absolute inset-0 bg-black/20 opacity-0 transition-opacity duration-300 group-hover:opacity-100"></div>
      </a>

      <!-- Small Image 2 -->
      <a href="/gallery" class="group relative block w-full overflow-hidden rounded-2xl h-[250px] md:h-[288px]">
        <img class="h-full w-full object-cover transition-transform duration-700 group-hover:scale-105" alt="Resort suite details" src="https://images.unsplash.com/photo-1505691938895-1758d7feb511?q=80&w=600&auto=format&fit=crop" loading="lazy" />
        <div class="absolute inset-0 bg-black/20 opacity-0 transition-opacity duration-300 group-hover:opacity-100"></div>
      </a>

    </div>
  </div>
</section>
