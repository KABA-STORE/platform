# ADR-001: Modular KABA Platform Architecture

## Status

Accepted

## Context

The initial requirement was a small connector for creating and managing PrestaShop products. The expected scope may later expand into AI services, inventory, orders, mobile applications, warehouse operations, and ERP integration.

## Decision

Use a modular .NET solution with separate projects for:

- API
- Core business logic
- Infrastructure
- PrestaShop integration
- AI services

## Consequences

This requires more structure at the beginning, but it reduces coupling and makes future expansion easier.

The connector is treated as the first capability of the wider KABA Platform rather than as a disposable script.
