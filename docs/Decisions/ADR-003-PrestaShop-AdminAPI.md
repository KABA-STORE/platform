# ADR-003: Use the PrestaShop Admin API

## Status

Accepted

## Context

PrestaShop provides both the older Webservice API and the newer Admin API.

The KABA store runs PrestaShop 9.1, and the connector needs secure server-to-server access with granular permissions.

## Decision

Use the PrestaShop Admin API with OAuth2 Client Credentials authentication.

## Consequences

The connector can use scoped access and short-lived access tokens.

The permanent client secret remains on the production server.

The platform will isolate PrestaShop-specific logic inside `Kaba.Platform.PrestaShop`.
