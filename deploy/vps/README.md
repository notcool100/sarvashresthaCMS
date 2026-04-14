# VPS Configuration Files

These files are exported from the current UAT VPS environment to make it easier to recreate the environment for production.

## Nginx Configuration
**Path**: `/etc/nginx/sites-available/sarvashresthaCMS_UAT.conf`

To apply:
1. Copy `nginx/sarvashresthaCMS_UAT.conf` to `/etc/nginx/sites-available/your-site.conf`.
2. Link it: `ln -s /etc/nginx/sites-available/your-site.conf /etc/nginx/sites-enabled/`.
3. Test and reload: `nginx -t && systemctl reload nginx`.

## Systemd Services
**Path**: `/etc/systemd/system/`

To apply:
1. Copy `systemd/sarvashrestha-backend.service` and `systemd/sarvashrestha-frontend.service` to `/etc/systemd/system/`.
2. Reload systemd: `systemctl daemon-reload`.
3. Enable and start:
   ```bash
   systemctl enable sarvashrestha-backend.service
   systemctl start sarvashrestha-backend.service
   systemctl enable sarvashrestha-frontend.service
   systemctl start sarvashrestha-frontend.service
   ```

## Directory Structure
The configurations expect the following directory structure:
- `/var/www/sarvashresthaCMS_UAT/`
  - `backend/publish/` (Output of `dotnet publish`)
  - `frontend/` (Output of `pnpm build` and `package.json`)
  - `backend/publish/uploads/` (Static image uploads)
