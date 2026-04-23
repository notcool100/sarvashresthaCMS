<section class="bg-[var(--color-surface-container-high)] py-24" id="contact">
  <div class="mx-auto max-w-6xl px-6 text-center">
    <p class="uppercase tracking-[0.3em] text-secondary text-xs">Plan Your Visit</p>
    <h2 class="mt-4 font-[var(--font-headline)] text-4xl md:text-5xl">Your Gateway to The East and The West</h2>
    <p class="mt-6 text-on-surface-variant max-w-2xl mx-auto">
      Share your travel dates and preferences. Our concierge will craft a personalized itinerary
      within 24 hours.
    </p>

    <div class="mt-10 grid md:grid-cols-3 gap-6">
      <div class="contact-card">
        <span class="material-symbols-outlined">call</span>
        <p class="contact-title">Reservations</p>
        <p class="contact-detail">+977 61 555 888</p>
      </div>
      <div class="contact-card">
        <span class="material-symbols-outlined">mail</span>
        <p class="contact-title">Email</p>
        <p class="contact-detail">stay@sarvashrestha.com</p>
      </div>
      <div class="contact-card">
        <span class="material-symbols-outlined">location_on</span>
        <p class="contact-title">Location</p>
        <p class="contact-detail">Pokhara, Nepal</p>
      </div>
    </div>

    <div class="mt-12">
      <a class="btn-primary" href="/booking">Start Planning</a>
    </div>
  </div>
</section>

<style>
  .contact-card {
    background: white;
    border-radius: 1.25rem;
    padding: 2rem 1.5rem;
    box-shadow: 0 18px 40px rgba(0, 0, 0, 0.08);
    transition: transform 0.4s ease, box-shadow 0.4s ease;
  }

  .contact-card:hover {
    transform: translateY(-6px);
    box-shadow: 0 22px 50px rgba(0, 0, 0, 0.12);
  }

  .contact-card span {
    font-size: 2rem;
    color: var(--color-secondary);
  }

  .contact-title {
    margin-top: 0.75rem;
    font-weight: 600;
  }

  .contact-detail {
    margin-top: 0.35rem;
    color: var(--color-on-surface-variant);
  }

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
</style>
