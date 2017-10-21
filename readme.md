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

__Example:__
```CSharp
// This method responds to both commands 'sayhello' and 'sh'
// When either command is used a notification will be created
// which displays 'Hello!'
public void Initialize(Proxy proxy)
{
    var listener = new CommandListener(proxy);
    listener.On("sayhello", "sh").Then((items) =>
    {
        Client client = items[0] as Client;
        string[] args = items[1] as string[];

        client.SendToClient(PluginUtils.CreateNotification(client.ObjectId, "Hello!"));
    }).Catch((items) =>
    {
        Exception e = items[0] as Exception;
        Console.WriteLine("An error occurred: " + e.Message);
    });
}
```

There are a few new methods shown in this example code: `.Then(...)` and `.Catch(...)`.
These work in the same way that JavaScript Promises work.

These methods are part of the type `KType.Types.Internal.Promise`, the method signature for both methods is `(Action<object[]> action)`.

The parameter is a method which will be invoked with a list of parameters as an `object[]`.
Calling `.Then(...)` or `.Catch(...)` on a listener doesn't immediately execute the method passed as the parameter. When the promise 'resolves' the function passed to `.Then(...)` will be invoked. If there is an Exception raised while invoking the method, then the method passed to `.Catch(...)` will be invoked.

Any promise which has a `.Then(...)` must also have a `.Catch(...)`. If there is no catch and an Exception is thrown while invoking the method in `.Then(...)` then an `UnhandledPromiseRejectionException` will be thrown.

__Notes:__
The `CommandListener` always resolves any promises with the same list of parameters.

The fist parameter is the `Client` instance which is passed to the `CommandListener` when the command is used.
This parameter can be accessed by using the following code:
```CSharp
listener.On("testcommand").Then((items) => 
{
    Client client = items[0] as Client;
});
```

The second parameter is an array of strings which contains any arguments passed to the command. If there are no arguments then an empty array will be passed instead of null.
This parameter can be access by using the following code:
```CSharp
listener.On("say").Then((items) => 
{
    string[] args = items[1] as string[];
    Console.WriteLine("Saying: " + args[0]);
});
// Using the command '/say hello` will result in the following output.
// 
// Saying: hello
//
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