using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StarWindXLib;

namespace StarWindXExtLib
{
    public class ParameterAppender : IAppender
    {
        private List<Param> additionalParams;
        private List<Param> AdditionalParams => additionalParams ??= new List<Param>();

        public void AppendParam(string paramName, string paramValue)
        {
            AdditionalParams.Add(new Param {Name = paramName, Value = paramValue});
        }

        public void AppendParams(IParameters pars)
        {
            Append(pars, this);
            additionalParams?.ForEach(par => pars.AppendParam(par.Name, par.Value));
        }

        public Parameters GenerateParams()
        {
            var pars = new Parameters();
            AppendParams(pars);
            return pars;
        }

        public T GenerateParams<T>() where T : IParameters, new()
        {
            var pars = new T();
            AppendParams(pars);
            return pars;
        }

        private static string ToString(MemberInfo info, object obj)
        {
            return obj switch
            {
                string str => str,
                bool b when info.GetCustomAttribute<BoolToStringAttribute>(true) is { } attr => b
                    ? attr.TrueString
                    : attr.FalseString,
                bool _ => throw new Exception(),
                int i when i == 0 && info.GetCustomAttribute<IntZeroAttribute>(true) is { } attr => attr.Value,
                int i => i.ToString(),
                DateTime date => ((DateTimeOffset) date).ToUnixTimeSeconds().ToString(),
                TimeSpan span => span.Seconds.ToString(),
                _ when obj.GetType().BaseType == typeof(Enum) => EnumFormat.EnumToString(obj),
                _ => obj.ToString()
            };
        }

        private static void Append(IParameters pars, object obj, string prefix = "")
        {
            var properties = obj.GetType().GetProperties();

            bool IsEnabled(IConditional attr)
            {
                if (attr.Enabled) return true;
                return (bool) properties.Single(
                    info => info.GetCustomAttribute<EnableParamAttribute>(true)?.CheckName(attr.Name) ?? false
                ).GetValue(obj);
            }

            foreach (var info in properties)
            {
                if (!info.CanRead) continue;
                if (info.GetCustomAttribute<ParamAttribute>(true) is { } attr)
                {
                    if (IsEnabled(attr) && ToString(info, info.GetValue(obj)) is { } str)
                        pars.AppendParam(prefix + attr.Name, str);
                }
                else if (info.GetCustomAttribute<FlatParamAttribute>(true) is { } flat)
                {
                    if (IsEnabled(flat)) Append(pars, info.GetValue(obj), flat.Prefix);
                }
                else if (info.GetCustomAttribute<SubParamAttribute>(true) is { } sub)
                {
                    if (!IsEnabled(sub)) continue;
                    var newPars = pars.AppendParams(sub.Name);
                    Append(newPars, info.GetValue(obj), sub.Prefix);
                }
            }
        }

        private struct Param
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }
}