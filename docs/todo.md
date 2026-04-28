# TODO — Timer Implementation

## Phase 1: Project Setup

- [x] Create `src/` folder structure
- [x] Update `tik.csproj` if needed (nullable, implicit usings already enabled)

## Phase 2: Core Logic

- [x] `ArgParser.cs` — parse and validate `-t` / `-u` args, print usage on error
- [x] `TimeConverter.cs` — convert value + unit to total seconds, validate > 0

## Phase 3: Timer Engine

- [x] `TimerEngine.cs` — countdown loop with 1-second tick, call renderer on each tick

## Phase 4: Console Renderer

- [x] `ConsoleRenderer.cs` — in-place progress bar with `Console.SetCursorPosition`
- [x] Color support via `Console.ForegroundColor` (no escape codes)
- [x] `HH:MM:SS` display
- [x] Finish animation: ASCII spinner (~2 sec) + success message

## Phase 5: Entry Point

- [x] `Program.cs` — wire `ArgParser` → `TimeConverter` → `TimerEngine`
- [x] Clean exit codes

## Phase 6: Verification

- [x] Manual test: seconds, minutes, hours units and shorthands
- [x] Manual test: invalid inputs (bad unit, negative value, missing args)
- [x] Visual acceptance: progress bar renders cleanly, no scroll jitter
- [x] Visual acceptance: finish animation looks good
