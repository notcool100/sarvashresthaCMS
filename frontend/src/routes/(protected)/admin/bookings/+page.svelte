<script lang="ts">
    import { onMount } from 'svelte';
     import { bookingService } from "$lib/services/bookingService";
      import type { booking, BookingCreateRequest, Room } from "$lib/types/api";
  import { goto } from "$app/navigation";

  function gotoCreatePage(){
    goto('/admin/bookings/create');
  }
  let sucessMessage= $state('');
    let Booking = $state<booking[]>([]);
 
    let loading = $state(true);
    let error = $state("");
    let saving = $state(false);
    let editingId = $state<number | null>(null);
    let uploading = $state(false);
    let selectedRoom = $state("");

  async function loadBookings() {
        loading = true;
        error = "";
        try {
            const response = await bookingService.getAll();
            console.log(response, " this is get all. room respinse");
            if (response.success == true && response.data) {
                Booking = response.data;
            } else {
                error = response.message || "Failed to load rooms";
            }
        } catch (e) {
            error = "Failed to load rooms";
        } finally {
            loading = false;
        }
    }


   onMount(loadBookings);
   </script>

<button onclick={gotoCreatePage}>
create
</button>
<table>
<thead>
<tr>
<th>
s.n
</th>
<th>
room no
</th>

<th>
guest name
</th>
<th>
email
</th>
<th>
check in 
</th>
<th>
checkout
</th>
<th>
status 
</th>
<th>
action
</th>

</tr>

</thead>
<tbody>
<tr>
<td>
1
</td>
<td>
101
</td>

<td>
kristina 
</td>
<td>
k@gmail.com
</td>
<td>
9am
</td>
<td>
9am
</td>
<td>
pending
</td>
<td>

<button> edit</button>
<button> delete</button>
</td>

</tr>
</tbody>
</table>