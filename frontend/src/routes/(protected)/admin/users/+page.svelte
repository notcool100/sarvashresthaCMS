<script lang="ts">
    import { onMount } from "svelte";
    import { bookingService } from "$lib/services/bookingService";
    import type { users } from "$lib/types/api";
    import { goto } from "$app/navigation";
    import { invalidateAll } from "$app/navigation";
    import { usersService } from "$lib/services/usersService";

    function gotoCreatePage() {
        goto("/admin/users/create");
    }

    let sucessMessage = $state("");
    let Users = $state<users[]>([]);

    let loading = $state(true);
    let error = $state("");
    let saving = $state(false);
    let editingId = $state<number | null>(null);
    let uploading = $state(false);
    let selectedRoom = $state("");
    // async function deletebooking(id: number) {
    //     console.log(id, "this is booking ");
    //     const result = confirm("Do you want to delete this item?");

    //     if (result) {
    //         // YES clicked
    //         console.log("Deleted ID:", id);
    //         const response = await bookingService.remove(id);
    //         console.log(response, "this is deletd booking id");
    //         if (response.success == true) {
    //             await loadusers();
    //         }
    //     } else {
    //         // NO clicked
    //         console.log("Cancelled");
    //     }
    // }

    async function loadusers() {
        loading = true;
        error = "";
        try {
            const response = await usersService.getAll();
            console.log(response, " this is get all. room respinse");
            if (response.success == true && response.data) {
                Users = response.data;
            } else {
                error = response.message || "Failed to load rooms";
            }
        } catch (e) {
            error = "Failed to load rooms";
        } finally {
            loading = false;
        }
    }

    onMount(loadusers);
</script>

<!-- <button onclick={gotoCreatePage}> create </button> -->

<div class="overflow-x-auto p-4">
    <table class="min-w-full bg-white rounded-2xl shadow-md overflow-hidden">
        <!-- Header -->
        <thead class="bg-gray-100 text-gray-700 uppercase text-sm">
            <tr>
               
               
                <th class="py-3 px-4 text-left">User Name</th>
                <th class="py-3 px-4 text-left">User Email</th>
                
            </tr>
        </thead>

        <!-- Body -->
        <tbody class="text-gray-600 text-sm">
            {#each Users as x, i}
                <tr class="border-b hover:bg-gray-50 transition">
                    <td class="py-3 px-4">{i + 1}</td>
              
                    <td class="py-3 px-4 font-medium text-gray-800">
                        {x.name}
                    </td>
                    <td class="py-3 px-4">{x.email}</td>
                    
                    <!-- Status Badge -->
                    <!-- <td class="py-3 px-4">
                        <span
                            class="px-3 py-1 rounded-full text-xs font-semibold
              {x.status === 'confirmed'
                                ? 'bg-green-100 text-green-700'
                                : x.status === 'pending'
                                  ? 'bg-yellow-100 text-yellow-700'
                                  : 'bg-red-100 text-red-700'}"
                        >
                            {x.status}
                        </span>
                    </td> -->

                    <!-- Buttons -->
                    <td class="py-3 px-4 text-center space-x-2">
                        <button
                            class="bg-blue-500 hover:bg-blue-600 text-white px-3 py-1 rounded-lg text-xs"
                            onclick={() =>
                                goto(`/admin/users/create?id=${x.id}`)}
                        >
                            Edit
                        </button>
                        <!-- <button
                            class="bg-red-500 hover:bg-red-600 text-white px-3 py-1 rounded-lg text-xs"
                            onclick={() => deletebooking(x.id)}
                        >
                            Delete
                        </button> -->
                    </td>
                </tr>
            {/each}
        </tbody>
    </table>
</div>
