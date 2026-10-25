# Cards

## Card

### Responsibility

Represents one standard playing card used in PocketCricket.

A Card contains only card identity information. It does not contain gameplay rules, scoring logic, placement logic, or match logic.

---

### Type

Value Object

A Card has no independent identity. Two cards with the same suit and rank represent the same card definition.

---

### Structure

A Card consists of:

- Suit
- Rank
- Face

The game value is derived from the card definition and is not stored separately.

Example:

| Card | Rank | Face | Game Value |
|---|---|---|---|
| Ace of Hearts | Ace | None | 1 |
| Seven of Clubs | Seven | None | 7 |
| Jack of Spades | Jack | Jack | 0 |
| Queen of Diamonds | Queen | Queen | 0 |
| King of Hearts | King | King | 0 |

---

### CardSuit

Represents the standard playing card suits.

Values:

- Clubs
- Diamonds
- Hearts
- Spades

---

### CardRank

Represents the identity of a card.

Values:

- Ace
- Two
- Three
- Four
- Five
- Six
- Seven
- Eight
- Nine
- Ten
- Jack
- Queen
- King

CardRank does not represent PocketCricket scoring value.

---

### CardFace

Represents special face card identity.

Values:

- None
- Jack
- Queen
- King

---

### Game Value Rules

Game value is calculated from the card definition:

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
- Trio detection
- Pair of 11 detection
- KQJ detection
- Scoring

These responsibilities belong to other domain objects.

## Cards

## Matrix

## Rules

## Scoring

## Segment

## Match

## Multiplayer