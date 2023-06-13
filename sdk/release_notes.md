## What's Changed

<!-- Please add your release notes in the following format:
- My change description (#PR/#issue)
-->

### Microsoft.Azure.Functions.Worker.Sdk <version> (meta package)

- <entry>

### Microsoft.Azure.Functions.Worker.Sdk.Analyzers <version>

- Add analyzer for SupportsDeferredBindingAttribute #1367
- Added an analyzer that will show a warning for types not supported by a binding attribute (#1505)
- Added an analyzer that will suggest a code refactor for all of the types supported by a binding attribute (#1604)

### Microsoft.Azure.Functions.Worker.Sdk.Generators <version>

- Parse named arguments by type (#1877)
- Refactor source gen to walk dependent assemblies (#1896)
- Add diagnostic descriptor logs for parsing binding arguments in source gen (#1882)
