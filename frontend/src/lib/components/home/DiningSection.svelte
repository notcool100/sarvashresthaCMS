<section class="bg-[var(--color-surface-container)] py-24" id="dining">
  <div class="mx-auto max-w-7xl px-6 grid lg:grid-cols-12 gap-12 items-center">
    <div class="lg:col-span-6">
      <img
        class="rounded-3xl shadow-2xl"
        alt="Elegant dining hall"
        src="https://images.unsplash.com/photo-1414235077428-338989a2e8c0?q=80&w=1600&auto=format&fit=crop"
      />
    </div>
    <div class="lg:col-span-6">
      <p class="uppercase tracking-[0.3em] text-secondary text-xs">Dining</p>
      <h2 class="mt-4 font-[var(--font-headline)] text-4xl md:text-5xl">Cuisine Rooted in the Mountains</h2>
      <p class="mt-6 text-on-surface-variant leading-relaxed">
        Our culinary team sources directly from Himalayan farms. Expect fire-roasted vegetables,
        aromatic herbs, and Himalayan salt rituals with every course.
      </p>
      <div class="mt-8 grid sm:grid-cols-2 gap-6">
        <div class="menu-card">
          <h3>Alpine Breakfast</h3>
          <p>Stone-ground grains, honey, and seasonal fruit.</p>
        </div>
        <div class="menu-card">
          <h3>Lantern Supper</h3>
          <p>Charcoal hearth menus and slow-braised lamb.</p>
        </div>
        <div class="menu-card">
          <h3>Garden Lunch</h3>
          <p>Warm bread, wild greens, and herbal broths.</p>
        </div>
        <div class="menu-card">
          <h3>Tea Lounge</h3>
          <p>Rare single-origin teas and pastry pairings.</p>
        </div>
      </div>
    </div>
  </div>
</section>

<style>
  .menu-card {
    background: white;
    border-radius: 1rem;
    padding: 1.25rem 1.5rem;
    box-shadow: 0 14px 30px rgba(0, 0, 0, 0.06);
    transition: transform 0.4s ease, box-shadow 0.4s ease;
  }

  .menu-card:hover {
    transform: translateY(-4px);
    box-shadow: 0 18px 40px rgba(0, 0, 0, 0.12);
  }

  .menu-card h3 {
    font-weight: 600;
    margin-bottom: 0.35rem;
  }

  .menu-card p {
    color: var(--color-on-surface-variant);
    font-size: 0.9rem;
  }
</style>
