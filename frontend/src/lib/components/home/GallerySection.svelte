<section class="mx-auto max-w-7xl px-6 py-24" id="gallery">
  <div class="flex items-end justify-between gap-6">
    <div>
      <p class="uppercase tracking-[0.3em] text-secondary text-xs">Gallery</p>
      <h2 class="mt-4 font-[var(--font-headline)] text-4xl md:text-5xl">A Glimpse of the Sanctuary</h2>
    </div>
    <a class="hidden md:inline-block text-xs uppercase tracking-widest text-on-surface-variant hover:text-secondary" href="/booking">
      Reserve Your Stay
    </a>
  </div>

  <div class="mt-12 grid grid-cols-2 md:grid-cols-4 gap-4">
    <img class="gallery-tile" alt="Mountain spa" src="https://images.unsplash.com/photo-1508385082359-f83f1f6c94e2?q=80&w=900&auto=format&fit=crop" />
    <img class="gallery-tile" alt="Tea ceremony" src="https://images.unsplash.com/photo-1504674900247-0877df9cc836?q=80&w=900&auto=format&fit=crop" />
    <img class="gallery-tile" alt="Himalayan view" src="https://images.unsplash.com/photo-1469474968028-56623f02e42e?q=80&w=900&auto=format&fit=crop" />
    <img class="gallery-tile" alt="Resort suite" src="https://images.unsplash.com/photo-1505691938895-1758d7feb511?q=80&w=900&auto=format&fit=crop" />
    <img class="gallery-tile" alt="Forest ritual" src="https://images.unsplash.com/photo-1500530855697-b586d89ba3ee?q=80&w=900&auto=format&fit=crop" />
    <img class="gallery-tile" alt="Dining" src="https://images.unsplash.com/photo-1504674900247-0877df9cc836?q=80&w=900&auto=format&fit=crop" />
    <img class="gallery-tile" alt="Resort pool" src="https://images.unsplash.com/photo-1500375592092-40eb2168fd21?q=80&w=900&auto=format&fit=crop" />
    <img class="gallery-tile" alt="Suite detail" src="https://images.unsplash.com/photo-1505691938895-1758d7feb511?q=80&w=900&auto=format&fit=crop" />
  </div>
</section>

<style>
  .gallery-tile {
    width: 100%;
    height: 220px;
    object-fit: cover;
    border-radius: 1rem;
    transition: transform 0.4s ease, box-shadow 0.4s ease;
  }

  .gallery-tile:hover {
    transform: translateY(-6px);
    box-shadow: 0 18px 40px rgba(0, 0, 0, 0.18);
  }
</style>
