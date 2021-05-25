
Make a test class for every seperate class named NameTests.cs
For every method in the class you're testing make a [testmethod] for testing each possible execution path.
Pls use the format below :)


// MethodName //
[TestMethod]
    public void Name_Scenario_ExpectedBehavior() // Name_Scenario_ExpectedBehavior{
        // Arrange
        -space for initializing objects-

        // Act
        -space to act on the object (methodcall)-

        // Assert
        -space to verify results, use "Assert." <-- select method- 
     }