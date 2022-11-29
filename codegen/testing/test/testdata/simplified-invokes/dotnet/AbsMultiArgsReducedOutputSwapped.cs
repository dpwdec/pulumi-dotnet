// *** WARNING: this file was generated by test. ***
// *** Do not edit by hand unless you're certain you know what you are doing! ***

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Pulumi.Serialization;

namespace Pulumi.Std
{
    public static class AbsMultiArgsReducedOutputSwapped
    {
        /// <summary>
        /// Returns the absolute value of a given float. 
        /// Example: abs(1) returns 1, and abs(-1) would also return 1, whereas abs(-3.14) would return 3.14.
        /// </summary>
        public static async Task<double> InvokeAsync(double b, double a, InvokeOptions? invokeOptions = null)
        {
            var builder = ImmutableDictionary.CreateBuilder<string, object?>();
            builder["b"] = b;
            builder["a"] = a;
            var args = new global::Pulumi.DictionaryInvokeArgs(builder.ToImmutableDictionary());
            return await global::Pulumi.Deployment.Instance.InvokeAsync<double>("std:index:AbsMultiArgsReducedOutputSwapped", args, invokeOptions.WithDefaults());
        }

        /// <summary>
        /// Returns the absolute value of a given float. 
        /// Example: abs(1) returns 1, and abs(-1) would also return 1, whereas abs(-3.14) would return 3.14.
        /// </summary>
        public static Output<double> Invoke(Input<double> b, Input<double> a, InvokeOptions? invokeOptions = null)
        {
            var builder = ImmutableDictionary.CreateBuilder<string, object?>();
            builder["b"] = b;
            builder["a"] = a;
            var args = new global::Pulumi.DictionaryInvokeArgs(builder.ToImmutableDictionary());
            return global::Pulumi.Deployment.Instance.Invoke<double>("std:index:AbsMultiArgsReducedOutputSwapped", args, invokeOptions.WithDefaults());
        }
    }
}
