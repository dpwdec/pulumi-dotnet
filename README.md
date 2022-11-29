﻿# .NET Language Provider

A .NET language provider for Pulumi.

## Installing the [nuget](https://www.nuget.org/packages/Pulumi) package
```
dotnet add package Pulumi
```

## Example Pulumi program with .NET and C#

Here's a simple example of a Pulumi app written in C# that creates some simple
AWS resources:

```c#
using System.Collections.Generic;
using System.Threading.Tasks;
using Pulumi;
using Pulumi.Aws.S3;

Deployment.RunAsync(() =>
{
    // Create the bucket, and make it public.
    var bucket = new Bucket(name, new () 
    { 
        Acl = "public-read" 
    });

    // Add some content.
    var content = new BucketObject("basic-content", new ()
    {
        Acl = "public-read",
        Bucket = bucket.Id,
        ContentType = "text/plain; charset=utf8",
        Key = "hello.txt",
        Source = new StringAsset("Made with ❤, Pulumi, and .NET"),
    });

    // Return some values that will become the Outputs of the stack.
    return new Dictionary<string, object>
    {
        ["hello"] = "world",
        ["bucket-id"] = bucket.Id,
        ["content-id"] = content.Id,
        ["object-url"] = Output.Format($"http://{bucket.BucketDomainName}/{content.Key}"),
    };
});
```

Make a Pulumi.yaml file:

```
$ cat Pulumi.yaml

name: hello-dotnet
runtime: dotnet
```

Then, configure it:

```
$ pulumi stack init hello-dotnet
$ pulumi config set aws:region us-west-2
```

And finally, `pulumi preview` and `pulumi up` as you would any other Pulumi project.

## Building

```bash
cd ./build
dotnet run build-sdk
```

## Running tests for the Pulumi SDK
```bash
cd ./build
dotnet run test-sdk
```

## Running tests for the Pulumi Automation SDK
```bash
cd ./build
dotnet run test-automation-sdk
```

## Public API Changes

When making changes to the code you may get the following compilation
error:

```
error RS0016: Symbol XYZ' is not part of the declared API.
```

This indicates a change in public API. If you are developing a change
and this is intentional, add the new API elements to
`PublicAPI.Shipped.txt` corresponding to your project (some IDEs
will do this automatically for you, but manual additions are fine as
well).
