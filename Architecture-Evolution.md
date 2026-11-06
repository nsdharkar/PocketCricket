2026-10-25

Card

Originally proposed:

Suit
Rank
Face

Changed to:

Suit
Rank

Reason:

Face is derived.

Reference:

ADR-003

Deck

Originally considered:

List<Card>

Changed to:

Stack<Card>

Reason:

Closer to real deck behavior.

2026-11-01

Discussed whether to use InvalidOperationException or a custom DeckEmptyException.

Decision:
Custom exception chosen because an empty deck is a business concept, not a generic programming error.

Alternative rejected:
InvalidOperationException.

Reason:
Less expressive and harder to distinguish in centralized logging.

ADR-012 — Feature namespaces are plural

Decision

Use plural names for feature namespaces.

Examples:
PocketCricket.Domain.Cards
PocketCricket.Domain.Decks
PocketCricket.Domain.Matches
PocketCricket.Domain.Rules

Classes remain singular:
Card
Deck
Match
Rule

Reason
- Avoid namespace/type ambiguity (Deck.Deck).
- Better readability.
- Consistent with feature-based organization.
- Prevents CS0118-style conflicts.


## 2026-11-03

### Domain Exception Hierarchy Implemented

Implemented PocketCricketException as the base exception for all domain-specific business errors.

Added DeckEmptyException to prevent leaking implementation-specific exceptions from Stack<Card>.

Deck now validates its own invariants before drawing a card.
