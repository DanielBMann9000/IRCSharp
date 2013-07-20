IRCSharp
========

Event-driven, asynchronous IRC library.

This is still very rough and going through a heavy refactoring cycle. The message propagation system isn't quite where I want it to be yet, and the actual socket connection logic isn't anywhere near as efficient as it could (or should) be. On the plus side, it's functional.

The integration tests are a little bit flaky (as integration tests so frequently are), since they rely on starting up an actual IRC server (default is localhost:5454).