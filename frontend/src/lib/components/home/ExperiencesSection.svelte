<section class="mx-auto max-w-7xl px-6 py-24" id="experiences">
  <div class="grid lg:grid-cols-12 gap-12 items-start">
    <div class="lg:col-span-5">
      <p class="uppercase tracking-[0.3em] text-secondary text-xs">Experiences</p>
      <h2 class="mt-4 font-[var(--font-headline)] text-4xl md:text-5xl">Moments That Ground You</h2>
      <p class="mt-6 text-on-surface-variant leading-relaxed">
        Explore glacier walks, tea ceremonies, and private meditation pavilions. Every itinerary is
        designed by our wellness team to match your rhythm and intentions.
      </p>
      <div class="mt-10 grid gap-4">
        <div class="experience-item">
          <span class="material-symbols-outlined">self_improvement</span>
          <div>
            <h3>Sunrise Breathwork</h3>
            <p>Guided at the ridge with Himalayas in view.</p>
          </div>
        </div>
        <div class="experience-item">
          <span class="material-symbols-outlined">hiking</span>
          <div>
            <h3>Glacier Day Trails</h3>
            <p>Private treks led by expert Sherpa guides.</p>
          </div>
        </div>
        <div class="experience-item">
          <span class="material-symbols-outlined">restaurant</span>
          <div>
            <h3>Chef’s Fire Table</h3>
            <p>Seasonal tasting menus with local harvests.</p>
          </div>
        </div>
      </div>
    </div>

    <div class="lg:col-span-7 grid sm:grid-cols-2 gap-6">
      <div class="experience-card">
        <img
          alt="Meditation pavilion"
          src="https://images.unsplash.com/photo-1506126613408-eca07ce68773?q=80&w=1000&auto=format&fit=crop"
        />
        <div>
          <h3>Forest Meditation</h3>
          <p>Silent walks and guided mindfulness sessions.</p>
        </div>
      </div>
      <div class="experience-card">
        <img
          alt="Spa ritual"
          src="https://images.unsplash.com/photo-1544161515-4ab6ce6db874?q=80&w=1000&auto=format&fit=crop"
        />
        <div>
          <h3>Himalayan Spa Rituals</h3>
          <p>Hot stone therapies with herbal infusions.</p>
        </div>
      </div>
      <div class="experience-card">
        <img
          alt="Tea ceremony"
          src="https://images.unsplash.com/photo-1517705008128-361805f42e86?q=80&w=1000&auto=format&fit=crop"
        />
        <div>
          <h3>Tea & Sound Ceremony</h3>
          <p>Evening rituals under lantern light.</p>
        </div>
      </div>
      <div class="experience-card">
        <img
          alt="Mountain dining"
          src="https://images.unsplash.com/photo-1528712306091-ed0763094c98?q=80&w=1000&auto=format&fit=crop"
        />
        <div>
          <h3>Summit Table</h3>
          <p>Private dinners with panoramic views.</p>
        </div>
      </div>
    </div>
  </div>
</section>

<style>
  .experience-item {
    display: grid;
    grid-template-columns: auto 1fr;
    gap: 1rem;
    align-items: start;
    padding: 1rem 0;
    border-bottom: 1px solid rgba(0, 0, 0, 0.08);
  }

  .experience-item h3 {
    font-weight: 600;
  }

  .experience-item p {
    color: var(--color-on-surface-variant);
    font-size: 0.9rem;
  }

  .experience-card {
    background: white;
    border-radius: 1.25rem;
    overflow: hidden;
    box-shadow: 0 18px 40px rgba(0, 0, 0, 0.08);
    display: grid;
    gap: 0.75rem;
    transition: transform 0.4s ease, box-shadow 0.4s ease;
  }

  .experience-card:hover {
    transform: translateY(-6px);
    box-shadow: 0 24px 50px rgba(0, 0, 0, 0.12);
  }

  .experience-card img {
    width: 100%;
    height: 200px;
    object-fit: cover;
  }

  .experience-card div {
    padding: 1rem 1.25rem 1.5rem;
  }

  .experience-card h3 {
    font-weight: 600;
    margin-bottom: 0.4rem;
  }

  .experience-card p {
    color: var(--color-on-surface-variant);
    font-size: 0.9rem;
  }
</style>
