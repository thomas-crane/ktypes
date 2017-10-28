# KTypes
KTypes is a C# Library which provides a helpful set of types and classes which can be used when developing KRelay plugins.

## How to get this Library.
1. Clone this repo to your computer
2. Open `KTypes.csproj` in Visual Studio (Open this with the Version Selector for best results).
3. In Visual Studio, press `Ctrl` + `Shift` + `B` to build the project.
4. Building the project will generate the `KTypes.dll` file in `/bin/Debug`.

## How to utilise this Library.
1. In your KRelay plugin project, add a reference to the file `KTypes.dll`
2. Start using types and classes from KTypes.

## Understanding Promises.
> If you have a good understanding of how Promises in JavaScript work then you will not need to read this section. The Promise type in KTypes is designed to replicate the behaviour of JavaScript promises as accurately as possible.

Promises provide an easy way of attaching a callback to an asynchronous process. This could range from waiting for a command to be used, to waiting for a trade to be accepted or declined.

Promises provide two methods: `.Then(...)`, and `.Catch(...)` to attach callbacks.
Attaching a callback is as simple as calling the `.Then(...)` method and passing the callback action you want to execute. The method signature of `.Then(...)` will depend on the type of promise.

A great time to use promises is when performing HTTP requests. The action to perform once the web request has completed can be defined using promises:
```CSharp
//
// Suppose searchGoogle(string SearchTerm) is some method which
// performs a google search for the specified term, and returns
// the search results as a string array.
//

Console.WriteLine("Starting Google Search");
searchGoogle("KRelay download").Then((searchResults) => {
    for (int i = 0; i < searchResults.length; i++) {
        Console.WriteLine("{0}: {1}", i + 1, searchResults[i]);
    }
});
Console.WriteLine("Continuing Program...");
...

// Output:
//
// Starting Google Search
// Continuing Program...
// 1: GitHub - TheKronks/KRelay
// 2: K Relay - Rotmg Hacks
// 3: [Release] KRelay - MPGH
// ...
```
Note that the action doesn't execute straight away, only when the HTTP request is actually complete.

If a Promise runs in to an error while trying to resolve, it will be rejected instead. If a Promise is rejected, the rejection must be handled properly. This can be done using the `.Catch(...)` method.

The `.Catch(...)` method is similar to the `.Then(...)` method. It takes an action as a parameter, but this action always has the same method signature: `(Action<Exception> action)`. The exception which is passed when the action is executed is the Exception which caused the promise to be rejected.

Using the HTTP request example again, a Catch can be added to handle any errors which might occur:
```CSharp
Console.WriteLine("Starting Google Search");
searchGoogle("KRelay download").Then((searchResults) => {
    ...
}).Catch((error) => {
    Console.WriteLine("Error while searching: " + error.Message);
});
Console.WriteLine("Continuing Program...");
...

// Output:
//
// Starting Google Search
// Continuing Program...
// Error while searching: 500 Internal Server Error
// ...
```

### Notes
All promises should include a `.Catch(...)` part. If a promise is rejected and there is no `.Catch(...)`, then an `UnhandledPromiseRejectionException` will be thrown.

Both `.Catch(...)` and `.Then(...)` return themselves. This means that calls to these methods can be chained together, which can help keep code cleaner. E.g. both of these code snippets are __identical in functionality.__
```CSharp
// Conventional way
var promise = GetPromiseSomehow();
promise.Then(() => {
    Log("Success!");
});
promise.Catch((error) => {
    Log(error.Message);
});
```
```CSharp
// Using method chaining
GetPromiseSomehow().Then(() => {
    Log("Success");
}).Catch((error) => {
    Log(error.Message);
});
```

## Types
### `CommandListener`
`CommandListener` is a utility class which provides an interface similar to JavaScript Promises for handling commands.

`CommandListener` lives in the `KTypes.Types` namespace, so to use it first include it in your plugin.
```CSharp
using System;
using Lib_K_Relay;
using Lib_K_Relay.Utilities;

using KTypes.Types // include this line
...
```

`CommandListener` must be instantiated and passed a reference to the `Proxy` instance of the plugin. So it is best to construct the `CommandListener` inside of the `Initialize(Proxy proxy)` method.
```CSharp
public void Initialize(Proxy proxy)
{
    var listener = new CommandListener(proxy);
}
```

The `CommandListener` instance can then be used to handle commands in a similar method to how JavaScript promises work.

The `listener.On(...)` method can be used to listen for a command. This method can take any number of strings as the parameter. All of the strings passed as parameters will invoke the same response when used as a command.

The `.On(...)` method returns an instance of the `CommandPromise` class. `CommandPromise` always resolves with the same 2 parameters:
 1. The `Client` instance which used the command.
 2. Any arguments passed with the command, as a `string[]`.

__Example:__
```CSharp
// This method responds to both commands 'sayhello' and 'sh'
// When either command is used a notification will be created
// which displays 'Hello!'
public void Initialize(Proxy proxy)
{
    var listener = new CommandListener(proxy);
    listener.On("sayhello", "sh").Then((client, args) =>
    {
        client.SendToClient(PluginUtils.CreateNotification(client.ObjectId, "Hello!"));
    }).Catch((error) =>
    {
        Console.WriteLine("An error occurred: " + error.Message);
    });
}
```

## Contributing
KTypes is designed to be an ongoing project which provides a set of helpful utility classes and types which can be used to make the development of KRelay plugins easier.

If you want to contribute your own utility class or type to the project, you should follow the conventions currently being used. Some guidelines to follow are:

1. Any types or classes which are to be used in KRelay plugins should be in the `KTypes.Types` namespace.
2. Any types or classes which are only used within the `KType` assembly should be in the `KTypes.Types.Internal` namespace.
3. The most restrictive access modifiers should be used. E.g. if a class _can_ be `private` or `sealed`, then it _should_ be `private` or `sealed`.
4. Any possible exceptions thrown should be either handled with a `try/catch` or rethrown. If they are rethrown, then this should be noted in the XML comments on the method. E.g.
```CSharp
/// <summary>
/// An example method
/// </summary>
/// <exception cref="ArgumentNullException">Thrown if no argument is passed</exception>
public void ExampleMethod(string arg)
{
    ...
}
```
5. Branches should be used when introducing a new type. When the type is ready to use and stable the branch containing the new type can be merged back into master. Each branch should only contain __one__ new type. (If the type references another type which has been introduced, both can be included in the same branch).