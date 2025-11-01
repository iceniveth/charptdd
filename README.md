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
