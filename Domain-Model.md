## Card

### Responsibility

Represents one standard playing card used in PocketCricket.

A Card contains only the identity of the card. It does not contain game rules, placement logic, or scoring logic.

---

### Type

Value Object

Card equality is based on its values, not object identity.

Two cards with the same Suit and Rank represent the same card definition.

---

### Structure

A Card contains:

- Suit
- Rank

Example:

| Card | Suit | Rank |
|---|---|---|
| Ace of Hearts | Hearts | Ace |
| Seven of Clubs | Clubs | Seven |
| King of Spades | Spades | King |

---

### Derived Properties

The following values are calculated from Rank:

#### Face

| Rank | Face |
|---|---|
| Jack | Jack |
| Queen | Queen |
| King | King |
| All others | None |

#### Game Value

| Rank | Game Value |
|---|---|
| Ace | 1 |
| Two - Ten | Numeric value |
| Jack | 0 |
| Queen | 0 |
| King | 0 |

---

### Does Not Own

Card does not own:

- Deck management
- Shuffle logic
- Draw logic
- Matrix placement
- Covered state
- Rule detection
- Scoring
- Match state

## Cards

## Matrix

## Rules

## Scoring

## Segment

## Match

## Multiplayer