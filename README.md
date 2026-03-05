IRCSharp
========

Event-driven, asynchronous IRC library.

This is still very rough and going through a heavy refactoring cycle. The message propagation system isn't quite where I want it to be yet, and the actual socket connection logic isn't anywhere near as efficient as it could (or should) be. On the plus side, it's functional.

The integration tests are a little bit flaky (as integration tests so frequently are), since they rely on starting up an actual IRC server (default is localhost:5454).

## 2026 Surprise Update!
No one cares about IRC anymore! Even less than they did 13 years ago! Everyone uses Discord. Whatever, man!

This is slightly resurrected mainly because it's a solid testbed for me to play around with some Agentic AI stuff. I'm using OpenCode + Qwen 3.5 (because I'm way too cheap to pay for fancy-pants cloud models, yet paradoxically paid way too much money for an RTX 5090 last year).

So, I had gen AI upgrade this to .NET 10, change from MSTest to xUnit, and, just for fun, see if it could understand my code well enough to implement a real IRC bot to play a simple guessing game. I oscillate between saying "Wow, holy crap, that's cool!" and "Wow, AI is dumb!", but this is still a fun learning exercise.

