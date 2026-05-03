<script lang="ts">
  import { browser } from "$app/environment";
  import { page } from "$app/stores";
  import { authStore } from "$lib/stores/authStore";

  const navItems = [
    { href: "/", label: "Home" },
    { href: "/rooms", label: "Rooms" },
    { href: "/gallery", label: "Gallery" },
    { href: "/#experiences", label: "Experiences" },
    { href: "/#dining", label: "Dining" },
    { href: "/#contact", label: "Contact" }
  ];

  let isMenuOpen = $state(false);

  $effect(() => {
    $page.url.pathname;
    $page.url.search;
    isMenuOpen = false;
  });

  $effect(() => {
    if (!browser) return;
    document.body.style.overflow = isMenuOpen ? "hidden" : "";

    return () => {
      document.body.style.overflow = "";
    };
  });
</script>

<nav
  class="fixed top-0 left-0 right-0 z-50 border-b border-black/5 bg-white/70 backdrop-blur-md"
>
  <div class="nav-shell mx-auto flex max-w-7xl items-center justify-between gap-4 px-4 py-4 sm:px-6">
    <a class="brand min-w-0 flex flex-col leading-none" href="/" aria-label="Sarbashrestha home">
      <span
        class="font-[var(--font-headline)] text-xl tracking-tight text-on-surface sm:text-2xl"
        >SARBASHRESTHA</span
      >
      <span class="brand-subtitle text-[0.58rem] tracking-[0.32em] text-on-surface-variant sm:text-[0.65rem] sm:tracking-[0.4em]"
        >HOSPITALITY Pvt.Ltd.</span
      >
    </a>

    <div class="hidden items-center gap-10 text-sm md:flex">
      {#each navItems as item}
        <a class="nav-link" href={item.href}>{item.label}</a>
      {/each}

      {#if $authStore}
        <a href="/user" class="btn-primary">Dashboard</a>
      {:else}
        <a href="/login" class="btn-ghost">Login</a>
        <a href="/booking" class="btn-primary">Book Now</a>
      {/if}
    </div>

    <button
      class="menu-toggle md:hidden"
      type="button"
      aria-expanded={isMenuOpen}
      aria-controls="mobile-menu"
      aria-label={isMenuOpen ? "Close navigation menu" : "Open navigation menu"}
      onclick={() => (isMenuOpen = !isMenuOpen)}
    >
      <span class="material-symbols-outlined">{isMenuOpen ? "close" : "menu"}</span>
    </button>
  </div>

  <div
    id="mobile-menu"
    class:open={isMenuOpen}
    class="mobile-menu md:hidden"
  >
    <div class="mobile-panel">
      <div class="mobile-links">
        {#each navItems as item}
          <a class="mobile-link" href={item.href} onclick={() => (isMenuOpen = false)}>
            {item.label}
          </a>
        {/each}
      </div>

      <div class="mobile-actions">
        {#if $authStore}
          <a href="/user" class="btn-primary w-full" onclick={() => (isMenuOpen = false)}>Dashboard</a>
        {:else}
          <a href="/login" class="btn-ghost mobile-ghost" onclick={() => (isMenuOpen = false)}>Login</a>
          <a href="/booking" class="btn-primary w-full" onclick={() => (isMenuOpen = false)}>Book Now</a>
        {/if}
      </div>
    </div>
  </div>
</nav>

<style>
  .brand-subtitle {
    white-space: nowrap;
  }

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

  .menu-toggle {
    width: 2.75rem;
    height: 2.75rem;
    border-radius: 999px;
    display: inline-flex;
    align-items: center;
    justify-content: center;
    background: rgba(255, 255, 255, 0.92);
    border: 1px solid rgba(0, 0, 0, 0.08);
    color: var(--color-on-surface);
    box-shadow: 0 12px 24px rgba(0, 0, 0, 0.08);
  }

  .mobile-menu {
    position: absolute;
    top: 100%;
    left: 0;
    right: 0;
    padding: 0 1rem 1rem;
    opacity: 0;
    pointer-events: none;
    transform: translateY(-0.5rem);
    transition:
      opacity 0.2s ease,
      transform 0.2s ease;
  }

  .mobile-menu.open {
    opacity: 1;
    pointer-events: auto;
    transform: translateY(0);
  }

  .mobile-panel {
    display: grid;
    gap: 1rem;
    border: 1px solid rgba(0, 0, 0, 0.06);
    border-radius: 1.25rem;
    background: rgba(255, 255, 255, 0.98);
    padding: 1rem;
    max-height: calc(100dvh - 5rem);
    overflow-y: auto;
    box-shadow: 0 24px 48px rgba(0, 0, 0, 0.14);
  }

  .mobile-links {
    display: grid;
    gap: 0.35rem;
  }

  .mobile-link {
    display: block;
    padding: 0.9rem 1rem;
    border-radius: 0.9rem;
    color: var(--color-on-surface);
    font-size: 0.8rem;
    font-weight: 700;
    letter-spacing: 0.16em;
    text-transform: uppercase;
    background: var(--color-surface-container-low);
  }

  .mobile-actions {
    display: grid;
    gap: 0.75rem;
  }

  .mobile-ghost {
    display: inline-flex;
    width: 100%;
    min-height: 3rem;
    align-items: center;
    justify-content: center;
    border-radius: 999px;
    background: var(--color-surface-container-low);
  }

  @media (max-width: 768px) {
    nav {
      background: rgba(255, 255, 255, 0.92);
    }
  }
</style>
