# KABA Platform Coding Standards

## Purpose

These standards keep the KABA Platform understandable, consistent, secure, and maintainable across developers and development machines.

## Documentation and comments

### Public types

Every public class, interface, record, enum, property, and method must include XML documentation using `///`.

The documentation should explain:

- The responsibility of the type or member
- Important behaviour
- Expected inputs and outputs
- Relevant security or lifecycle considerations

### Internal types

Internal types should include XML documentation when their purpose is not immediately obvious or when they represent an important integration contract.

### Private implementation

Private methods and fields do not require comments when their names clearly describe their purpose.

Comments should be added when they explain:

- Why a particular implementation was chosen
- Concurrency behaviour
- Security decisions
- Caching or expiry behaviour
- Workarounds
- Non-obvious business rules

### Commenting philosophy

Comments must explain **why**, not merely repeat **what** the code already says.

Bad example:

```csharp
// Set the access token.
_cachedAccessToken = token;
