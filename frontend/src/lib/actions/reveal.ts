export type RevealOptions = {
  rootMargin?: string;
  threshold?: number;
  once?: boolean;
};

export function reveal(node: HTMLElement, options: RevealOptions = {}) {
  const { rootMargin = "0px 0px -10% 0px", threshold = 0.15, once = true } = options;

  if (typeof window === "undefined" || typeof IntersectionObserver === "undefined") {
    node.classList.add("is-visible");
    return {
      destroy() {}
    };
  }

  const observer = new IntersectionObserver(
    (entries) => {
      entries.forEach((entry) => {
        if (entry.isIntersecting) {
          node.classList.add("is-visible");
          if (once) observer.unobserve(node);
        } else if (!once) {
          node.classList.remove("is-visible");
        }
      });
    },
    { rootMargin, threshold }
  );

  const delay = node.getAttribute("data-delay");
  if (delay) {
    node.style.setProperty("--reveal-delay", delay);
  }

  observer.observe(node);

  return {
    destroy() {
      observer.unobserve(node);
    }
  };
}
