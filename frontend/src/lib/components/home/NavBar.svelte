<script lang="ts">
  import { authStore } from "$lib/stores/authStore";
</script>

<nav
  class="fixed top-0 left-0 right-0 z-50 bg-white/70 backdrop-blur-md border-b border-black/5"
>
  <div class="mx-auto max-w-7xl px-6 py-4 flex items-center justify-between">
    <div class="flex flex-col leading-none">
      <span
        class="font-[var(--font-headline)] text-2xl tracking-tight text-on-surface"
        >SARBASHRESTHA</span
      >
      <span class="text-[0.65rem] tracking-[0.4em] text-on-surface-variant"
        >HOSPITALITY Pvt.Ltd.</span
      >
    </div>

    <div class="hidden md:flex items-center gap-10 text-sm">
      <a class="nav-link" href="/">Home</a>
      <a class="nav-link" href="/rooms">Rooms</a>
      <a class="nav-link" href="/gallery">Gallery</a>
      <a class="nav-link" href="/#experiences">Experiences</a>
      <a class="nav-link" href="/#dining">Dining</a>
      <a class="nav-link" href="/#contact">Contact</a>

      {#if $authStore}
        <a href="/user" class="btn-primary">Dashboard</a>
      {:else}
        <a href="/login" class="btn-ghost">Login</a>
        <a href="/booking" class="btn-primary">Book Now</a>
      {/if}
    </div>
  </div>
</nav>

<style>
  .nav-link {
    color: var(--color-on-surface-variant);
    font-weight: 500;
    letter-spacing: 0.08em;
    text-transform: uppercase;
    font-size: 0.7rem;
    transition: color 0.3s ease;
  }

  .nav-link:hover {
    color: var(--color-secondary);
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
    transition:
      transform 0.3s ease,
      box-shadow 0.3s ease;
  }

  .btn-primary:hover {
    transform: translateY(-2px);
    box-shadow: 0 18px 30px rgba(12, 58, 26, 0.25);
  }

  .btn-ghost {
    color: var(--color-on-surface-variant);
    font-size: 0.75rem;
    letter-spacing: 0.2em;
    text-transform: uppercase;
  }

  @media (max-width: 768px) {
    nav {
      background: rgba(255, 255, 255, 0.92);
    }
  }
</style>
