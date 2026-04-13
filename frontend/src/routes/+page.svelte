<script lang="ts">
  import NavBar from "$lib/components/home/NavBar.svelte";
  import Hero from "$lib/components/home/Hero.svelte";
  import AboutSection from "$lib/components/home/AboutSection.svelte";
  import SuitesSection from "$lib/components/home/SuitesSection.svelte";
  import ExperiencesSection from "$lib/components/home/ExperiencesSection.svelte";
  import DiningSection from "$lib/components/home/DiningSection.svelte";
  import GallerySection from "$lib/components/home/GallerySection.svelte";
  import ContactSection from "$lib/components/home/ContactSection.svelte";
  import FooterSection from "$lib/components/home/FooterSection.svelte";
  import { reveal } from "$lib/actions/reveal";
</script>

<svelte:head>
  <link
    href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&display=swap"
    rel="stylesheet"
  />
</svelte:head>

<NavBar />

<main class="bg-surface text-on-surface">
  <Hero />
  <div class="section-reveal" use:reveal><AboutSection /></div>
  <div class="section-reveal" data-delay="0.08s" use:reveal><SuitesSection /></div>
  <div class="section-reveal" data-delay="0.16s" use:reveal><ExperiencesSection /></div>
  <div class="section-reveal" data-delay="0.24s" use:reveal><DiningSection /></div>
  <div class="section-reveal" data-delay="0.32s" use:reveal><GallerySection /></div>
  <div class="section-reveal" data-delay="0.4s" use:reveal><ContactSection /></div>
</main>

<FooterSection />

<style>
  :global(html) {
    scroll-behavior: smooth;
  }

  .section-reveal {
    opacity: 1;
    transform: none;
  }

  .section-reveal:global(.is-visible) {
    animation: sectionFade 0.7s ease both;
    animation-delay: var(--reveal-delay, 0s);
  }

  @keyframes sectionFade {
    from {
      opacity: 0;
      transform: translateY(24px);
    }
    to {
      opacity: 1;
      transform: translateY(0);
    }
  }

  @media (prefers-reduced-motion: reduce) {
    .section-reveal:global(.is-visible) {
      animation: none;
    }
  }
</style>
