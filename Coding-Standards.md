Naming

Classes:
PascalCase

Interfaces:
Start with I

Methods:
Verb-based

Properties:
PascalCase

Private fields:
_camelCase

Constants:
PascalCase

Enums:
Singular names

Records:
Immutable

Exceptions:
Meaningful names

SOLID

All modules follow SOLID.

Dependency Injection

Constructor injection only.

No Service Locator.

No static mutable state.

Documentation

Every public member must have XML documentation.

Testing

No production code without tests.

Constructor dependencies must be validated using ArgumentNullException.ThrowIfNull() unless there is a specific reason to perform custom validation.

