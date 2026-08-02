# ADR-002: Use .NET 8

## Status

Accepted

## Context

The platform requires a reliable server-side framework with strong API, security, dependency-injection, configuration, logging, and OpenAPI support.

## Decision

Use ASP.NET Core on .NET 8.

The repository includes `global.json` to pin development and build environments to the .NET 8 SDK family.

## Consequences

The production VPS and development Macs must have the .NET 8 SDK or runtime installed.

Newer SDKs may also be installed, but the repository should continue selecting .NET 8 until a deliberate upgrade decision is made.
