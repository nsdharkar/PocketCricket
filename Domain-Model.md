Card
Purpose:
Represents one physical playing card.

Type:
Value Object

Contains:
- Suit
- Face
- NumericValue

Does not contain:
- Scoring rules
- Placement rules
- Deck logic

Matrix
Purpose:
Represents the 3x3 playing area.

Owns:
- Nine cells
- Placement validation

Does not own:
- Cricket rules
- Scoring

Rule Detection
Purpose:
Identifies available cricket actions.

Owns:
- Rule identification

Does not own:
- Card drawing
- Player turns