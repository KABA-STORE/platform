# PrestaShop Integration

## Platform

- PrestaShop 9.1
- Admin API
- OAuth2 Client Credentials authentication

## API client

Client ID:

`kaba-connector`

The client secret is stored only in the production server environment and must never be committed to Git.

## Current permissions

The connector has access to selected product-management resources, including products, categories, manufacturers, features, attributes, suppliers, and tax information.

It does not currently have access to customers, employees, payments, orders, modules, or store configuration.

## Verified operations

- OAuth access-token generation
- Category listing
- Product listing

## Category reference

`Everyday Living` currently uses category ID `47`.
