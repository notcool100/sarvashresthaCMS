<script lang="ts">
  import { authService } from '$lib/services/authService';
  import { authStore } from '$lib/stores/authStore';
  import { goto } from '$app/navigation';
  import { UserRole } from '$lib/types/api';

  let email = '';
  let password = '';
  let error = '';
  let loading = false;

  async function handleLogin() {
    loading = true;
    error = '';
    
    try {
      const response = await authService.login({ email, password });
      
      if (response.success && response.data) {
        authStore.set(response.data);
        
        // Role-based redirection
        if (response.data.role === UserRole.Admin) {
          goto('/(protected)/admin/items');
        } else if (response.data.role === UserRole.Staff) {
          goto('/(protected)/staff/dashboard'); // Staff dashboard (to be created)
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
      <p>Log in to manage your resort bookings</p>
    </div>

    <form on:submit|preventDefault={handleLogin}>
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
        {#if loading} Logging in... {:else} Continue {/if}
      </button>
    </form>

    <div class="footer">
      <p>Don't have an account? <a href="/register">Create one</a></p>
    </div>
  </div>
</div>

<style>
  .login-container {
    min-height: 100vh;
    display: flex;
    align-items: center;
    justify-content: center;
    background: radial-gradient(circle at top left, #1c1c1c, #0a0a0a);
    padding: 2rem;
  }

  .card {
    width: 100%;
    max-width: 440px;
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
    gap: 1.5rem;
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
    padding: 0.875rem 1rem;
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
    margin-top: 1rem;
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
