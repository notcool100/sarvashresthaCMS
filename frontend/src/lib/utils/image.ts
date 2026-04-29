import { PUBLIC_API_BASE_URL } from '$env/static/public';

// Derive the base server URL by removing /api from the end of the PUBLIC_API_BASE_URL
const baseDomain = PUBLIC_API_BASE_URL.replace(/\/api\/?$/, '');

/**
 * Prepends the backend server base URL to relative upload paths.
 * Ensures images work across both local dev and production with separate subdomains.
 */
export function getImageUrl(path: string | undefined | null): string {
    if (!path) return '';
    // If it's already a full URL (like unsplash placeholders), return it as-is
    if (path.startsWith('http://') || path.startsWith('https://')) {
        return path;
    }
    
    // Ensure path starts with a slash
    const normalizedPath = path.startsWith('/') ? path : `/${path}`;
    return `${baseDomain}${normalizedPath}`;
}
