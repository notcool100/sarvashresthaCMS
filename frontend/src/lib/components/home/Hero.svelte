<section class="relative min-h-screen overflow-hidden" id="top">
  <div class="absolute inset-0">
    <img
      class="h-full w-full object-cover scale-105 animate-kenburns"
      alt="Sunrise over Himalayan peaks"
      src="https://images.unsplash.com/photo-1501785888041-af3ef285b470?q=80&w=2000&auto=format&fit=crop"
    />
    <div class="absolute inset-0 bg-gradient-to-b from-black/20 via-black/10 to-surface"></div>
  </div>

  <div class="relative z-10 mx-auto max-w-6xl px-6 pt-36 pb-24">
    <div class="max-w-3xl">
      <p class="uppercase tracking-[0.4em] text-secondary text-xs md:text-sm reveal">The Modern Sanctuary</p>
      <h1 class="mt-6 font-[var(--font-headline)] text-5xl md:text-7xl text-white leading-tight reveal delay-1">
        Where the Soul Finds Stillness
      </h1>
      <p class="mt-6 text-white/80 text-lg md:text-xl leading-relaxed reveal delay-2">
        Perched above the clouds, Sarvashrestha is a curated retreat of slow mornings, cedar-scented
        evenings, and panoramic Himalayan light.
      </p>
      <div class="mt-10 flex flex-col sm:flex-row gap-4 reveal delay-3">
        <a class="btn-primary" href="/booking">Book Your Escape</a>
        <a class="btn-outline" href="/rooms">Explore Suites</a>
      </div>

      <div class="mt-12 grid grid-cols-1 sm:grid-cols-3 gap-6 text-white/80">
        <div class="glass-card reveal delay-2">
          <span class="stat-number">4,200m</span>
          <p class="stat-label">Elevation</p>
        </div>
        <div class="glass-card reveal delay-3">
          <span class="stat-number">48</span>
          <p class="stat-label">Private Suites</p>
        </div>
        <div class="glass-card reveal delay-4">
          <span class="stat-number">24/7</span>
          <p class="stat-label">Concierge</p>
        </div>
      </div>
    </div>
  </div>
</section>

<style>
  .btn-primary {
    background: linear-gradient(135deg, #0c3a1a, #1b5e20);
    color: white;
    padding: 0.85rem 2rem;
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

  .btn-primary:hover {
    transform: translateY(-2px);
    box-shadow: 0 18px 30px rgba(12, 58, 26, 0.25);
  }

  .btn-outline {
    border: 1px solid rgba(255, 255, 255, 0.4);
    color: white;
    padding: 0.85rem 2rem;
    border-radius: 999px;
    font-weight: 600;
    font-size: 0.75rem;
    letter-spacing: 0.2em;
    text-transform: uppercase;
    display: inline-flex;
    align-items: center;
    justify-content: center;
    transition: background 0.3s ease, transform 0.3s ease;
  }

  .btn-outline:hover {
    background: rgba(255, 255, 255, 0.15);
    transform: translateY(-2px);
  }

  .glass-card {
    background: rgba(255, 255, 255, 0.12);
    border: 1px solid rgba(255, 255, 255, 0.2);
    border-radius: 1.25rem;
    padding: 1.5rem;
    backdrop-filter: blur(14px);
  }

  .stat-number {
    font-size: 1.5rem;
    font-weight: 700;
    letter-spacing: 0.1em;
  }

  .stat-label {
    text-transform: uppercase;
    font-size: 0.65rem;
    letter-spacing: 0.3em;
    margin-top: 0.4rem;
  }

  .reveal {
    opacity: 0;
    transform: translateY(14px);
    animation: fadeUp 0.9s ease forwards;
  }

  .delay-1 { animation-delay: 0.1s; }
  .delay-2 { animation-delay: 0.2s; }
  .delay-3 { animation-delay: 0.35s; }
  .delay-4 { animation-delay: 0.5s; }

  @keyframes fadeUp {
    to {
      opacity: 1;
      transform: translateY(0);
    }
  }

  @keyframes kenburns {
    0% {
      transform: scale(1.05) translateY(0);
    }
    100% {
      transform: scale(1.12) translateY(-2%);
    }
  }

  .animate-kenburns {
    animation: kenburns 18s ease-in-out infinite alternate;
  }

  @media (prefers-reduced-motion: reduce) {
    .reveal,
    .animate-kenburns {
      animation: none;
      opacity: 1;
      transform: none;
    }
  }
</style>
