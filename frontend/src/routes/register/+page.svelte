<script lang="ts">
  import { authService } from "$lib/services/authService";
  import { authStore } from "$lib/stores/authStore";
  import { goto } from "$app/navigation";

  let username = $state("");
  let email = $state("");
  let password = $state("");
  let confirmPassword = $state("");
  let error = $state("");
  let loading = $state(false);

  async function handleRegister(e: SubmitEvent) {
    e.preventDefault();
    if (password !== confirmPassword) {
      error = "Passwords do not match";
      return;
    }

    loading = true;
    error = "";

    try {
      const response = await authService.register({
        username,
        email,
        password,
      });

      if (response.success && response.data) {
        authStore.set(response.data);
        goto("/(protected)/user"); // Self-registration always redirects to the user dashboard
      } else {
        error = response.message || "Registration failed";
      }
    } catch (e) {
      error = "An unexpected error occurred";
    } finally {
      loading = false;
    }
  }
</script>

<div class="register-container">
  <div class="card">
    <div class="header">
      <h1>Create Account</h1>
      <p>Join Sarvashrestha today</p>
    </div>

    <form onsubmit={handleRegister}>
      <div class="input-group">
        <label for="username">Username</label>
        <input
          type="text"
          id="username"
          bind:value={username}
          placeholder="Anjal"
          required
        />
      </div>

      <div class="input-group">
        <label for="email">Email Address</label>
        <input
          type="email"
          id="email"
          bind:value={email}
          placeholder="anjal@example.com"
          required
        />
      </div>

      <div class="input-group">
        <label for="password">Password</label>
        <input
          type="password"
          id="password"
          bind:value={password}
          placeholder="••••••••"
          required
        />
      </div>

      <div class="input-group">
        <label for="confirmPassword">Confirm Password</label>
        <input
          type="password"
          id="confirmPassword"
          bind:value={confirmPassword}
          placeholder="••••••••"
          required
        />
      </div>

      {#if error}
        <div class="error-message">
          {error}
        </div>
      {/if}

      <button type="submit" disabled={loading}>
        {#if loading}
          Creating Account...
        {:else}
          Create Account
        {/if}
      </button>
    </form>

    <div class="footer">
      <p>Already have an account? <a href="/login">Log in</a></p>
    </div>
  </div>
</div>

<style>
  .register-container {
    min-height: 100vh;
    display: flex;
    align-items: center;
    justify-content: center;
    background: radial-gradient(circle at top left, #1c1c1c, #0a0a0a);
    padding: 2rem;
  }

  .card {
    width: 100%;
    max-width: 480px;
    background: rgba(255, 255, 255, 0.03);
    backdrop-filter: blur(20px);
    border: 1px solid rgba(255, 255, 255, 0.05);
    border-radius: 24px;
    padding: 3rem;
    box-shadow: 0 25px 50px -12px rgba(0, 0, 0, 0.5);
  }

  .header {
    text-align: center;
    margin-bottom: 2.5rem;
  }

  h1 {
    font-size: 2.25rem;
    color: white;
    font-weight: 700;
    margin-bottom: 0.5rem;
    letter-spacing: -0.025em;
  }

  p {
    color: #a3a3a3;
    font-size: 1rem;
  }

  form {
    display: flex;
    flex-direction: column;
    gap: 1.25rem;
  }

  .input-group {
    display: flex;
    flex-direction: column;
    gap: 0.5rem;
  }

  label {
    color: #e5e5e5;
    font-size: 0.875rem;
    font-weight: 500;
  }

  input {
    background: rgba(255, 255, 255, 0.05);
    border: 1px solid rgba(255, 255, 255, 0.1);
    border-radius: 12px;
    padding: 0.75rem 1rem;
    color: white;
    font-size: 1rem;
    transition: all 0.2s ease;
  }

  input:focus {
    outline: none;
    border-color: #3b82f6;
    background: rgba(255, 255, 255, 0.08);
    box-shadow: 0 0 0 4px rgba(59, 130, 246, 0.15);
  }

  .error-message {
    background: rgba(239, 68, 68, 0.1);
    color: #f87171;
    padding: 0.875rem;
    border-radius: 12px;
    font-size: 0.875rem;
    text-align: center;
    border: 1px solid rgba(239, 68, 68, 0.2);
  }

  button {
    background: #3b82f6;
    color: white;
    border: none;
    border-radius: 12px;
    padding: 1rem;
    font-size: 1rem;
    font-weight: 600;
    cursor: pointer;
    transition: all 0.2s ease;
    margin-top: 0.5rem;
  }

  button:hover:not(:disabled) {
    background: #2563eb;
    transform: translateY(-1px);
    box-shadow: 0 4px 12px rgba(59, 130, 246, 0.3);
  }

  button:disabled {
    opacity: 0.7;
    cursor: not-allowed;
  }

  .footer {
    text-align: center;
    margin-top: 2rem;
  }

  .footer a {
    color: #3b82f6;
    text-decoration: none;
    font-weight: 500;
  }

  .footer a:hover {
    text-decoration: underline;
  }
</style>
