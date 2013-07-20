using System;
using System.Collections.Generic;
using System.Reflection;

namespace IrcSharp.Core.Messages.Propagation
{
    internal static class MessagePropagatorReflector
    {
        public static IEnumerable<Tuple<string, TDelegate>> GetMessagePropagators<TAttribute, TDelegate>(this object obj)
            where TAttribute : MessagePropagatorAttribute
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