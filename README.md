# KABA Platform

The KABA Platform is the core backend and integration layer powering the KABA ecosystem.

## Current scope

The first implementation provides a secure connector between KABA and PrestaShop.

## Solution structure

- `src/Kaba.Platform.Api` — Public API endpoints
- `src/Kaba.Platform.Core` — Core business rules and shared models
- `src/Kaba.Platform.Infrastructure` — Infrastructure and persistence services
- `src/Kaba.Platform.PrestaShop` — PrestaShop integration
- `src/Kaba.Platform.AI` — AI-related services
- `tests` — Automated tests
- `docs` — Technical documentation
- `.github/workflows` — CI/CD workflows

## Build

```bash
dotnet build Kaba.Platform.sln
