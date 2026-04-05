<section class="mx-auto max-w-7xl px-6 py-24 grid lg:grid-cols-12 gap-12 items-center" id="about">
  <div class="lg:col-span-5">
    <p class="uppercase tracking-[0.3em] text-secondary text-xs">Ancient Wisdom, Modern Comfort</p>
    <h2 class="mt-4 font-[var(--font-headline)] text-4xl md:text-5xl">Elevated Living in the Heart of the Himalayas</h2>
    <p class="mt-6 text-on-surface-variant leading-relaxed">
      Designed with local stone, hand-woven textiles, and natural light, each space is built to bring
      calm into the everyday. The resort is a pathway to stillness, with curated rituals rooted in
      Himalayan tradition.
    </p>
    <div class="mt-8 flex items-center gap-3 text-secondary font-semibold tracking-widest uppercase text-xs">
      Our Philosophy
      <span class="material-symbols-outlined">arrow_right_alt</span>
    </div>
  </div>

  <div class="lg:col-span-7 relative">
    <div class="aspect-[4/5] overflow-hidden rounded-2xl shadow-2xl">
      <img
        class="h-full w-full object-cover animate-float"
        alt="Luxury resort overlooking a green valley"
        src="https://images.unsplash.com/photo-1501117716987-c8e2a64f6c78?q=80&w=1600&auto=format&fit=crop"
      />
    </div>
    <div class="hidden md:block absolute -bottom-10 -left-10 bg-white/90 glass-panel p-8 rounded-2xl shadow-xl max-w-xs">
      <span class="material-symbols-outlined text-secondary text-3xl">spa</span>
      <p class="mt-4 font-[var(--font-headline)] italic text-lg">
        “The air here carries the scent of cedar and the weight of timelessness.”
      </p>
      <p class="mt-3 text-xs uppercase tracking-widest text-on-surface-variant">Guest Review, 2025</p>
    </div>
  </div>
</section>

<style>
  @keyframes float {
    0% {
      transform: translateY(0);
    }
    50% {
      transform: translateY(-6px);
    }
    100% {
      transform: translateY(0);
    }
  }

  .animate-float {
    animation: float 7s ease-in-out infinite;
  }

  @media (prefers-reduced-motion: reduce) {
    .animate-float {
      animation: none;
    }
  }
</style>
