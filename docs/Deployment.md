# Deployment

## Production environment

The KABA Platform API runs on the KABA VPS.

## Runtime

- Ubuntu 24.04
- .NET 8
- systemd service
- Local application listener: `127.0.0.1:5050`
- Public endpoint: `https://connector.kaba.com.gh`
- Apache reverse proxy managed through Plesk
- Let's Encrypt TLS certificate

## Service

The application runs as:

`kaba-connector.service`

## Source control workflow

Development takes place on the MacBook or iMac.

The normal workflow is:

1. Pull the latest changes from GitHub.
2. Develop and test locally.
3. Commit and push to GitHub.
4. Pull and publish on the production VPS.

Automated deployment may be added later through GitHub Actions.
