# TimeTaken

## Goal
This package makes testing how long it takes for something to run. Built this to test things like which is faster 1 million iterations of "string".Any() vs "string" == "".

## Description 
Simplifies testing how long different ways of doing things in c# take.


## Example: 
```
var testResults = (new Dictionary<string, Action>
				{ { "testString == \"\"", () => { var testString = ""; var isEmpty = testString == ""; } }
				, { "testString == string.Empty", () => { var testString = ""; var isEmpty = testString == string.Empty; } }
				, { "string.Equals(testString, \"\"", () => { var testString = ""; var isEmpty = string.Equals(testString, ""); } }
				, { "string.Equals(testString, string.Empty)", () => { var testString = ""; var isEmpty = string.Equals(testString, string.Empty); } }
				, { "string.IsNullOrEmpty(testString)", () => { var testString = ""; var isEmpty = string.IsNullOrEmpty(testString); } }
				, { "\"\".Equals(testString)", () => { var testString = ""; var isEmpty = "".Equals(testString); } }
				, { "string.Empty.Equals(testString)", () => { var testString = ""; var isEmpty = string.Empty.Equals(testString); } }
				, { "testString.Any()", () => { var testString = ""; var isEmpty = testString.Any(); } }
				, { "testString.Count() == 0", () => { var testString = ""; var isEmpty = testString.Count() == 0; } }
				, { "testString.Length == 0", () => { var testString = ""; var isEmpty = testString.Length == 0; } }
				}).ExecutionTimeGet(iterations);
testResults.ExecutionTimeDisplayElapsedTicks(iterations);
```
