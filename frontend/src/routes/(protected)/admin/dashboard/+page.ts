import type { LoadEvent } from '@sveltejs/kit';
import { getDashboard } from '$lib/services/dashboardService';

export const load = async ({ fetch, parent }: LoadEvent) => {
    const { user } = await parent();
    try {
        const data = await getDashboard(fetch, user?.accessToken);
        return { dashboard: data };
    } catch (err) {
        return { dashboard: null, error: err instanceof Error ? err.message : 'Unknown error' };
    }
};
