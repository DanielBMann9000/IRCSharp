# Message Propagation

**Generated:** 2026-03-02
**Context:** Messages submodule

## OVERVIEW

Event-driven message routing system. Routes received/sending/sent messages to typed event handlers.

## WHERE TO LOOK

| Component | File | Role |
|-----------|------|------|
| MessagePropagator | `MessagePropagator.cs` | Core routing engine |
| ReceivedMessagePropagatorAttribute | `ReceivedMessagePropagatorAttribute.cs` | Attribute for received message routing |
| MessagePropagatorReflector | `MessagePropagatorReflector.cs` | Reflection helper |

## STRUCTURE

```
Propagation/
├── MessagePropagator.cs              # Core routing (547 lines)
├── ReceivedMessagePropagatorAttribute.cs  # Routing attribute
└── MessagePropagatorReflector.cs     # Reflection support
```

## CONVENTIONS

- Attribute-based routing for received messages (`[ReceivedMessagePropagator("COMMAND")]`)
- Dictionary-based routing for sending/sent messages (explicit type mapping)
- TokenizeArguments helper for parsing IRC message arguments

## ANTI-PATTERNS

- Large MessagePropagator class — consider splitting by message category
- Dictionary-based sending/sent routing requires manual updates when adding messages