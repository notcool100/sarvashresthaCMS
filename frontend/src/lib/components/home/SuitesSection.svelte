<section class="bg-[var(--color-surface-container-low)] py-24" id="suites">
  <div class="mx-auto max-w-7xl px-6">
    <div class="flex items-end justify-between gap-6">
      <div>
        <p class="uppercase tracking-[0.3em] text-secondary text-xs">Accommodations</p>
        <h2 class="mt-4 font-[var(--font-headline)] text-4xl md:text-5xl">Private Havens</h2>
      </div>
      <a class="hidden md:inline-block text-xs uppercase tracking-widest text-on-surface-variant hover:text-secondary" href="/rooms">
        View All Suites
      </a>
    </div>

    <div class="mt-14 grid md:grid-cols-3 gap-10">
      <article class="suite-card">
        <div class="suite-image">
          <img
            alt="Suite with panoramic windows"
            src="https://images.unsplash.com/photo-1505691938895-1758d7feb511?q=80&w=1200&auto=format&fit=crop"
          />
        </div>
        <div class="suite-body">
          <div>
            <h3 class="suite-title">Annapurna Vista Suite</h3>
            <p class="suite-meta">Terrace, Fireplace, Panoramic Views</p>
          </div>
          <p class="suite-price">$850 <span>/night</span></p>
        </div>
      </article>

      <article class="suite-card translate-y-8">
        <div class="suite-image">
          <img
            alt="Garden villa suite"
            src="https://images.unsplash.com/photo-1507089947368-19c1da9775ae?q=80&w=1200&auto=format&fit=crop"
          />
        </div>
        <div class="suite-body">
          <div>
            <h3 class="suite-title">Sanctuary Garden Villa</h3>
            <p class="suite-meta">Private Spa, Infinity Edge Tub, Garden Access</p>
          </div>
          <p class="suite-price">$1,200 <span>/night</span></p>
        </div>
      </article>

      <article class="suite-card">
        <div class="suite-image">
          <img
            alt="Cozy lodge suite"
            src="https://images.unsplash.com/photo-1505693416388-ac5ce068fe85?q=80&w=1200&auto=format&fit=crop"
          />
        </div>
        <div class="suite-body">
          <div>
            <h3 class="suite-title">Evergreen Lodge Loft</h3>
            <p class="suite-meta">Cedar Sauna, Library Nook, Valley Views</p>
          </div>
          <p class="suite-price">$960 <span>/night</span></p>
        </div>
      </article>
    </div>
  </div>
</section>

<style>
  .suite-card {
    background: white;
    border-radius: 1.5rem;
    overflow: hidden;
    box-shadow: 0 24px 60px rgba(0, 0, 0, 0.08);
    transition: transform 0.4s ease, box-shadow 0.4s ease;
  }

  .suite-card:hover {
    transform: translateY(-6px);
    box-shadow: 0 30px 70px rgba(0, 0, 0, 0.12);
  }

  .suite-image {
    aspect-ratio: 10 / 12;
    overflow: hidden;
  }

  .suite-image img {
    width: 100%;
    height: 100%;
    object-fit: cover;
    transition: transform 0.6s ease;
  }

  .suite-card:hover img {
    transform: scale(1.08);
  }

  .suite-body {
    display: flex;
    justify-content: space-between;
    gap: 1rem;
    padding: 1.5rem;
    align-items: flex-start;
  }

  .suite-title {
    font-size: 1.25rem;
    font-weight: 600;
  }

  .suite-meta {
    color: var(--color-on-surface-variant);
    font-size: 0.85rem;
    margin-top: 0.35rem;
  }

  .suite-price {
    font-weight: 700;
    color: #1b5e20;
  }

  .suite-price span {
    font-size: 0.7rem;
    color: var(--color-on-surface-variant);
  }
</style>
