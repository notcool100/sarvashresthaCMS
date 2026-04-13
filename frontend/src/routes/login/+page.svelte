<script lang="ts">
  import { authService } from '$lib/services/authService';
  import { authStore } from '$lib/stores/authStore';
  import { goto } from '$app/navigation';
  import { UserRole } from '$lib/types/api';

  let email = $state('');
  let password = $state('');
  let error = $state('');
  let loading = $state(false);

  async function handleLogin(e: SubmitEvent) {
    e.preventDefault();
    loading = true;
    error = '';

    try {
      const response = await authService.login({ email, password });

      if (response.success && response.data) {
        authStore.set(response.data);

        // Set cookie
        document.cookie = `auth_data=${JSON.stringify(
          response.data
        )}; path=/; max-age=3600; SameSite=Lax`;

        // Role-based redirect
        if (response.data.role === UserRole.Admin) {
          goto('/admin/dashboard');
        } else if (response.data.role === UserRole.Staff) {
          goto('/(protected)/staff/dashboard');
        } else {
          goto('/(protected)/user');
        }
      } else {
        error = response.message || 'Login failed';
      }
    } catch (e) {
      error = 'An unexpected error occurred';
    } finally {
      loading = false;
    }
  }
</script>

<div class="login-container">
  <div class="card">
    <div class="header">
      <h1>Welcome Back</h1>
      <p>Log in to manage your room bookings</p>
    </div>

    <form onsubmit={handleLogin}>
      <div class="input-group">
        <label for="email">Email Address</label>
        <input
          type="email"
          id="email"
          bind:value={email}
          placeholder="name@example.com"
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

      {#if error}
        <div class="error-message">
          {error}
        </div>
      {/if}

      <button type="submit" disabled={loading}>
        {#if loading}
          Logging in...
        {:else}
          Continue
        {/if}
      </button>
    </form>

    <div class="footer">
      <p>
        Don't have an account?
        <a href="/register">Create one</a>
      </p>
    </div>
  </div>
</div>

<style>
  .login-container {
    min-height: 100vh;
    display: flex;
    align-items: center;
    justify-content: center;
    background: linear-gradient(135deg, #f4f4f2, #f8f8f8);
    padding: 2rem;
  }

  .card {
    width: 100%;
    max-width: 440px;
    background: #ffffff;
    border-radius: 20px;
    padding: 2.5rem;
    box-shadow: 0 20px 40px rgba(0, 0, 0, 0.08);
    border: 1px solid #fefcfa;
  }

  .header {
    text-align: center;
    margin-bottom: 2rem;
  }

  h1 {
    font-size: 2rem;
    color: #92400e;
    font-weight: 700;
    margin-bottom: 0.5rem;
  }

  p {
    color: #78350f;
    font-size: 0.95rem;
  }

  form {
    display: flex;
    flex-direction: column;
    gap: 1.25rem;
  }

  .input-group {
    display: flex;
    flex-direction: column;
    gap: 0.4rem;
  }

  label {
    color: #92400e;
    font-size: 0.85rem;
    font-weight: 600;
  }

  input {
    background: #fffbeb;
    border: 1px solid #552d08;
    border-radius: 10px;
    padding: 0.75rem 0.9rem;
    color: #451a03;
    font-size: 0.95rem;
    transition: all 0.2s ease;
  }

  input::placeholder {
    color: #552d08;
  }

  input:focus {
    outline: none;
    border-color: 552d08;
    background: #fff7ed;
    box-shadow: 0 0 0 3px rgba(245, 158, 11, 0.2);
  }

  .error-message {
    background: #fee2e2;
    color: #b91c1c;
    padding: 0.75rem;
    border-radius: 10px;
    font-size: 0.85rem;
    text-align: center;
    border: 1px solid #fecaca;
  }

  button {
    background: #552d08;
    color: white;
    border: none;
    border-radius: 10px;
    padding: 0.85rem;
    font-size: 1rem;
    font-weight: 600;
    cursor: pointer;
    transition: all 0.2s ease;
    margin-top: 0.5rem;
  }

  button:hover:not(:disabled) {
    background: #d8b994;
    transform: translateY(-1px);
    box-shadow: 0 6px 14px rgba(217, 119, 6, 0.3);
  }

  button:disabled {
    opacity: 0.6;
    cursor: not-allowed;
  }

  .footer {
    text-align: center;
    margin-top: 1.5rem;
  }

  .footer a {
    color: #d97706;
    text-decoration: none;
    font-weight: 600;
  }

  .footer a:hover {
    text-decoration: underline;
  }
</style>