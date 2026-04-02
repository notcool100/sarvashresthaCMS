<script lang="ts">
  import { authStore } from "$lib/stores/authStore";
</script>

<nav class="navbar">
  <div class="nav-content">
    <div class="logo">
      <span class="logo-text">SARVASHRESTHA</span>
      <span class="logo-sub">RESORT & SPA</span>
    </div>

    <div class="nav-links">
      <a href="#about">About</a>
      <a href="#services">Services</a>
      <a href="#contact">Contact</a>

      {#if $authStore}
        <a href="/(protected)/user" class="btn-primary">Dashboard</a>
      {:else}
        <a href="/login" class="btn-primary">Login</a>
        <a href="/register" class="btn-primary">Book Now</a>
      {/if}
    </div>
  </div>
</nav>

<main>test</main>

<style>
  .navbar {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    z-index: 1000;
    padding: 1.5rem 0;
    background: rgba(10, 10, 10, 0.1);
    backdrop-filter: blur(8px);
    border-bottom: 1px solid rgba(255, 255, 255, 0.05);
  }

  .nav-content {
    max-width: 1200px;
    margin: 0 auto;
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 0 2rem;
  }

  .logo {
    display: flex;
    flex-direction: column;
    line-height: 1;
  }

  .logo-text {
    font-size: 1.5rem;
    font-weight: 700;
    letter-spacing: 0.1em;
    color: white;
  }

  .logo-sub {
    font-size: 0.65rem;
    font-weight: 500;
    letter-spacing: 0.4em;
    color: var(--text-secondary);
    margin-top: 2px;
  }

  .nav-links {
    display: flex;
    align-items: center;
    gap: 2.5rem;
  }

  .nav-links a:not(.btn-primary):not(.btn-secondary) {
    font-size: 0.875rem;
    font-weight: 500;
    color: var(--text-secondary);
    transition: color 0.3s ease;
  }

  .nav-links a:hover {
    color: white;
  }

  .btn-primary {
    background: white;
    color: black !important;
    padding: 0.75rem 1.75rem;
    border-radius: 100px;
    font-weight: 600;
    font-size: 0.875rem;
    transition: all 0.3s ease;
  }

  .btn-primary:hover {
    transform: translateY(-2px);
    box-shadow: 0 10px 20px rgba(255, 255, 255, 0.1);
  }

  .btn-secondary {
    font-size: 0.875rem;
    font-weight: 600;
    color: white !important;
  }
</style>
