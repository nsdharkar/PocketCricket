| ID | Module | Status | Trigger | Owner |
|----|---------|----------|-----------|--------|
| AR-001 | Cards | Deferred | Deck | Architecture |
| AR-002 | Cards | Deferred | DTO/API | Architecture |
| AR-003 | Domain | Deferred | Validation Strategy | Architecture |
| AR-004 | Cards | Accepted | N/A | Architecture |

AR-001
Module
Cards
Title
Deck must guarantee card uniqueness.
Status
Resolved

Resolution:
Deck creates cards using every Suit × Rank combination.
Unit tests verify that every card instance is unique.

Verification:
DeckTests.Constructor_ShouldCreateDeckWithUniqueCards()

AR-002
Module
Cards
Title
Serialization boundary validation.
Status
Deferred
Reason
Currently nothing is deserialized.

When we introduce:
- API
- SignalR
- Database
we must ensure invalid enum values cannot bypass validation.
Trigger
First DTO / API implementation.
AR-003
Module
Entire Domain
Title
Domain validation strategy.
Status
Deferred
Reason
We haven't yet decided the project-wide strategy.

Questions include:
- Guard clauses?
- Domain exceptions?
- Result<T>?
- Notification pattern?
We should not make this decision piecemeal.
Trigger
First complex aggregate (Deck or Match) that requires richer validation.
AR-004
Module
Cards
Title
PocketCricket-specific GameValue.
Status
Accepted Architecture Decision
Reason
Initially considered a risk.

After discussion:

PocketCricket is the domain.

GameValue belongs naturally to the Card.

No further action required.




## CARD-005

Module:
Cards

Status:
Deferred

Reason:
Serialization does not exist yet.

Trigger:
When API DTOs or persistence models are introduced.

Owner:
Architecture

Priority:
Medium

ADR-007

Title

Deck owns shuffling.

Decision

Deck receives IRandomProvider through constructor injection.

Status

Accepted