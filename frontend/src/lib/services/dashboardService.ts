import { PUBLIC_API_BASE_URL } from '$env/static/public';

export async function getDashboard(fetchFn: typeof fetch, accessToken?: string) {
    const headers: Record<string, string> = {
        'Content-Type': 'application/json'
    };

    if (accessToken) {
        headers['Authorization'] = `Bearer ${accessToken}`;
    }

    const response = await fetchFn(`${PUBLIC_API_BASE_URL}/Dashboard`, {
        headers
    });
    
    if (!response.ok) {
        throw new Error(`Failed to fetch dashboard data: ${response.status} ${response.statusText}`);
    }
    return response.json();
}
