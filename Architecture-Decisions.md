# Architecture Decisions

## ADR-001

Title
Clean Architecture

Status
Accepted

Date
2026-10-25

Decision
The solution follows Clean Architecture.

Layers:

Domain
Application
Infrastructure
Web

Reason

- Separation of concerns
- Testability
- Dependency inversion
- Independent domain model

Impact

All future modules must follow this dependency direction.

ADR-002

Card is an immutable Value Object.

Accepted.

Reason...

Impact...

ADR-003

Card uses Suit + Rank only.

Face is derived.

Accepted.

ADR-004

Deck will internally use Stack<Card>.

ADR-005	Centralized Exception Handling Strategy
ADR-006	Technical Logging + Game Event Logging

Accepted.

ADR-007
Title
Deck owns shuffling.

Decision
The Deck receives an IRandomProvider through constructor injection.

The Deck owns all shuffle behavior.

IRandomProvider only supplies random numbers.

It has no knowledge of PocketCricket domain objects.

Status:
Accepted

ADR-008

Title:
Interface and Implementation Organization

Decision:
Interfaces and implementations are separated by architectural responsibility.

Domain contains contracts.
Infrastructure contains technical implementations.
Test projects contain test-specific implementations.

Status:
Accepted

ADR-009: Domain service abstractions (e.g., IRandomProvider) live in the Domain because they are required for domain behavior.
ADR-010: Repository interfaces live in the Application layer because persistence is an application concern. Infrastructure implements those interfaces.