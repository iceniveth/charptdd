### Commands

Create new solution: `dotnet new sln`
Create new project: `dotnet new webapi -o Uqs.Weather`
Add project to solution: `dotnet sln add Uqs.Weather`

### Service Lifetime

If your concern is performance, then think of a singleton. Then, the next step is checking whether
the service is thread-safe, either by reading its documentation or doing other types of investigation.

Then, fall down to scoped if relevant, and then fall down to transient. The safest option is always
transient—if in doubt, then choose transient!

### Dependency Inject

Watch out for randoms. They are hard to test due to unpredictability. Should move it in DI.

### Terms

#### SUT

System under test.
Code executed un the MethodName in the test is called SUT or code under test (CUT).

### Test Doubles

#### Dummies

Use this as much as possible over others.

#### Stubs

are classes that respond with canned, pre-coded behavior.
Easy to write & read but hard to maintain than mocks.

#### Spies

are extra functionality added to a stub class to reveal what happened inside the stub. Follows the interface whereas mocks dont.

#### Mocks

Similar to stubs but use a trick to generate a behavir w/o implmenting the complete class. It reduces the amount of code to create test double.

#### Fakes

libraries that mimic part or all of a real-life equivalent, and they exist in order to facilitate testing. E.g. EF Core in-memory database, it is designed for testing only..

---

For any object that should not be part of the SUT or for an unused dependency, use a dummy.
To build and test dependencies, use mocks. Add fakes where it makes sense.

### Sintegration Test

See page 141.

| Unit Test                     | Sintegration Test                          | Integration Test            |
| ----------------------------- | ------------------------------------------ | --------------------------- |
| All test doubles dependencies | Few test doubles and few real dependencies | No test double dependencies |
