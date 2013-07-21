using System;
using System.Collections.Generic;
using System.Reflection;

namespace IrcSharp.Core.Messages.Propagation
{
    internal static class MessagePropagatorReflector
    {
        internal static IEnumerable<Tuple<string, TDelegate>> GetReceivedMessagePropagators<TAttribute, TDelegate>(this object obj)
            where TAttribute : ReceivedMessagePropagatorAttribute
            where TDelegate : class
        {
            var messageProcessorsMethods = obj.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var methodInfo in messageProcessorsMethods)
            {
                var methodAttributes = (TAttribute[])methodInfo.GetCustomAttributes(typeof(TAttribute), true);
                if (methodAttributes.Length <= 0)
                {
                    continue;
                }
                var methodDelegate = (TDelegate)(object)Delegate.CreateDelegate(typeof(TDelegate), obj, methodInfo);

                // Get each attribute applied to method.
                foreach (var attribute in methodAttributes)
                {
                    yield return Tuple.Create(attribute.CommandName, methodDelegate);
                }
            }
        }
    }
}