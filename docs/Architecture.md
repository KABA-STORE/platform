# KABA Platform Architecture

## Purpose

The KABA Platform is the backend and integration layer for the KABA ecosystem.

Its first responsibility is to provide a secure connection between ChatGPT-driven workflows and PrestaShop. The platform is designed to grow later into inventory, order management, supplier integration, mobile applications, AI services, reporting, and warehouse operations.

## Solution structure

- `Kaba.Platform.Api` — Public HTTP API and integration endpoints
- `Kaba.Platform.Core` — Business rules, interfaces, and shared domain models
- `Kaba.Platform.Infrastructure` — Persistence, logging, configuration, and external infrastructure
- `Kaba.Platform.PrestaShop` — PrestaShop authentication and API integration
- `Kaba.Platform.AI` — AI-assisted product and content services

## Architectural principles

- PrestaShop credentials remain on the server and are never exposed to clients.
- The public API exposes only approved business operations.
- Products should be created as inactive drafts by default.
- Every write operation should be validated and logged.
- External systems are accessed through dedicated adapters.
- The API, business rules, and infrastructure concerns remain separated.
