🏏 POCKET CRICKET — MECHANICS BIBLE (FINAL UPDATED VERSION)

🎮 1. Game Overview
- Name: Pocket Cricket
- Players: 2
- Deck: Standard 52‑card deck
- Core Mechanic: Sequential card placement into a 3×3 matrix, resolving rules manually.
- Design Goal: A skill‑based cricket simulation using deterministic card logic.
🧩 2. Core Experience
Players must:
- Detect rules manually
- Cover cards manually
- Follow strict placement order
- Resolve rules in the order they appear
- Avoid misses
- Manage pressure as matrix fills
- Make strategic decisions under constraints
🃏 3. Card Definitions
Rank (numeric cards only)
- Ace = 1
- 2–10 = numeric values
- J/Q/K = no numeric value (Rank = 0 or null)
Face
- None
- Jack
- Queen
- King
Suit
- Hearts
- Diamonds
- Clubs
- Spades
Suits do not affect gameplay.
🧱 4. Match Structure
Two innings:
1. Player A bowls → Player B bats
2. Player B bowls → Player A bats
Toss
- Both players play one sample segment until blocked or miss
- Total cards placed = toss score
- Higher score wins toss
- Winner chooses bat/bowl
🔄 5. Sequential Placement Rule (CRITICAL)
Players MUST place cards in this exact order:
1. r1c1
2. r1c2
3. r1c3
4. r2c1
5. r2c2
6. r2c3
7. r3c1
8. r3c2
9. r3c3
✔ Player cannot choose any random empty cell
✔ System enforces sequential placement
✔ Player cannot place a card if matrix is full
✔ Player cannot place a card if any rule is pending
🟥 6. Rule Priority System (CRITICAL)
Rules must be resolved in the order they appear.
This is the most important mechanic in Pocket Cricket.
✔ When a rule appears, it enters a Rule Queue
✔ Player must resolve the first rule in the queue
✔ Player cannot skip rules
✔ Player cannot resolve later rules first
✔ Player cannot place new cards until queue is empty
✔ Player cannot end turn until queue is empty
✔ Wrong order = MISS
🟦 7. Rule Types
7.1 11‑Pair Rule
Two numeric cards whose ranks sum to 11.
- Player must cover higher card first, then lower.
- If rule appears, it enters queue.
- Must be resolved before any later rule.
7.2 Face Card Rule (K → Q → J)
If K, Q, J appear:
- Cover in strict order: K → Q → J
- If rule appears while resolving another rule, it enters queue after that rule.
- Must be resolved after earlier rules.
7.3 Trio Rule
Three identical ranks or faces in:
- Row
- Column
- Diagonal
When trio appears:
- Cover all three cards
- In any order
- But only after earlier rules in queue.
🟧 8. Rule Interaction Example (Your Scenario)
Matrix contains:
- K
- Q
- 7
- 4
Step 1 — 7+4 is an 11‑pair → enters queue
Queue: [11‑Pair]

Player covers 7.
Step 2 — J appears
Now K‑Q‑J rule appears → enters queue
Queue: [11‑Pair, KQJ]
Step 3 — Player MUST finish 11‑pair first
Player covers 4.
Step 4 — Now resolve K → Q → J
In that order.
❌ If player covers Q before finishing 11‑pair → MISS
❌ If player covers K before finishing 11‑pair → MISS
❌ If player covers J before finishing 11‑pair → MISS
🟥 9. Cover‑Before‑Placement Rule
✔ If any rule exists, player MUST cover it before placing next card
✔ If player tries to place a card while rules exist → MISS
✔ If player tries to end turn while rules exist → MISS
🟦 10. Full Matrix Rule
Matrix is full when all 9 cells have active cards.

When full:
- Player must resolve all rules in queue
- Player must click End Turn
- If queue not empty → MISS
- If queue empty → normal end of segment
Player cannot place new cards.
🟥 11. Miss Conditions (Final)
A MISS occurs if:
1. Player fails to cover a valid rule
- Missed 11‑pair
- Missed KQJ
- Missed trio
2. Player covers incorrectly
- Wrong card
- Wrong order
- Wrong trio
- Covering when no rule exists
- Covering a numeric card not part of 11‑pair
- Covering face card when rule not active
3. Player places a card while rules exist
4. Player ends turn while rules exist
5. Player tries to place a card when matrix is full
Miss Result
- Batting: 0 runs
- Bowling: 0 wickets
- Segment ends immediately
🏏 12. Scoring Rules
Bowling
- Every 20 cards placed = 1 wicket
- Wickets fall only at multiples of 20
- Miss = 0 wickets
Batting
- Runs = total cards placed (active + covered)
- Miss = 0 runs
🏆 13. Win Conditions
- Higher total runs wins
- If chasing team reaches target → immediate win
- Tie → Super Over
- If still tied → tiebreaker