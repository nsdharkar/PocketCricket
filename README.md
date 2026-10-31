# PocketCricket

Architecture
- ✅ Clean Architecture
- ✅ SOLID principles
- ✅ Dependency Injection
- ✅ Domain-Driven Design (pragmatic)
- ✅ One public type per file
- ✅ Immutable Value Objects where appropriate
- ✅ Thin controllers (later)
- ✅ Domain independent of Infrastructure and Web

Development Process:

1. Requirement Review
2. Domain Design
3. Architecture Review
4. Strict TDD (RED → GREEN → REFACTOR)
5. Unit Testing
6. Integration Testing (if applicable)
7. Regression Testing
8. Security Review
9. Performance Review
10. Concurrency Review
11. Architecture Risk Register Review
12. Documentation Update
13. Git Commit
	
Testing Standards
Current
- ✅ xUnit
- ✅ FluentAssertions
- ✅ Moq
Planned
- Coverlet
- ReportGenerator
- Stryker.NET
- FsCheck
- BenchmarkDotNet
- NBomber
- WebApplicationFactory
- Playwright
Exception Handling Strategy (ADR-005)
Decision: Accepted
Domain
- Validate invariants.
- Throw exceptions only when an invalid domain state is being created.
- Never log.
Application
- Coordinate workflows.
- Translate domain exceptions when appropriate.
- No HTTP concerns.
Infrastructure
- Handle external system failures.
- Implement logging.
Web
- Own:
  - GlobalExceptionMiddleware
  - Error response formatting
  - HTTP status code mapping
Logging Strategy (ADR-006)
We will have two distinct logging streams.
1. Technical Logs
Purpose:
- Errors
- Exceptions
- Startup
- Infrastructure failures
- Diagnostics
Framework:
- Serilog
Example:
[Error]
Database connection failed.

2. Game Event Logs
Purpose:
- Match replay
- Game debugging
- Player dispute resolution
- Rule verification
- Tournament auditing
Example:
MatchId: 1024
Segment: 2
Turn: 14
Player: PlayerA
Action: Covered Pair of 11
Result: Success

This is not an exception log. It's a record of game actions.
Documentation Rules
Every document has a single responsibility.
Document	Purpose
Architecture-Decisions	Accepted architectural decisions (ADRs)
Architecture-Evolution	Why an architecture decision changed
Domain-Model	Current implemented domain model
Glossary	Domain terminology
Coding-Standards	Coding rules and conventions
Testing-Strategy	Testing philosophy, tools, and process
Module-Completion-Checklist	Definition of Done for every module
Architecture-Risk-Register	Deferred architectural risks and decisions
Game-Mechanics	Authoritative game rules


No document should contain placeholders, speculative designs, or duplicate information.
Architecture Risk Register
Open items today:
ID	Status	Revisit When
AR-001	Deferred	Deck implementation (uniqueness)
AR-002	Deferred	First serialization boundary
AR-003	Deferred	First complex aggregate validation


Once ADR-005 is documented, AR-003 can be refined or partially resolved based on the chosen validation approach.
Module Completion Criteria
A module is complete only if:
- Functional implementation is finished.
- All planned unit tests pass.
- Integration tests (if applicable) pass.
- Regression tests are added for any bug fixed during development.
- Security, performance, and concurrency reviews are completed.
- Documentation is updated.
- Architecture Risk Register is reviewed.
- Changes are committed to Git.