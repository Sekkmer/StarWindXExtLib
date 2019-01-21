using StarWindXLib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StarWindXExtLib {

    public class ParameterAppender : IAppender {

        private struct Param {
            public string Name { get; set; }
            public string Value { get; set; }
        }

        private List<Param> additionalParams;
        private List<Param> AdditionalParams => additionalParams ?? (additionalParams = new List<Param>());

        public void AppendParam(string paramName, string paramValue) {
            AdditionalParams.Add(new Param { Name = paramName, Value = paramValue });
        }

        public void AppendParams(IParameters pars) {
            Append(pars, this);
            additionalParams?.ForEach(par => pars.AppendParam(par.Name, par.Value));
        }

        public Parameters GenerateParams() {
            var pars = new Parameters();
            AppendParams(pars);
            return pars;
        }

        public T GenerateParams<T>() where T : IParameters, new() {
            var pars = new T();
            AppendParams(pars);
            return pars;
        }       

        private static string ToString(MemberInfo info, object obj) {
            if (obj is string str) {
                return str;
            } else if (obj is bool b) {
                if (info.GetCustomAttribute<BoolToStringAttribute>(true) is BoolToStringAttribute attr) {
                    return b ? attr.TrueString : attr.FalseString;
                }
                System.Windows.Forms.MessageBox.Show(info.Name);
                System.Windows.Forms.MessageBox.Show(obj.ToString());
                return null;
            } else if (obj.GetType().BaseType == typeof(Enum)) {
                return EnumFormat.EnumToString(obj);
            } else if (obj is int i) {
                if (i == 0 && info.GetCustomAttribute<IntZeroAttribute>(true) is IntZeroAttribute attr) {
                    return attr.Value;
                }
                return i.ToString();
            } else if (obj is DateTime date) {
                return ((DateTimeOffset)date).ToUnixTimeSeconds().ToString();
            } else if (obj is TimeSpan span) {
                return span.Seconds.ToString();
            } else {
                return obj.ToString();
            }
        }

        private static void Append(IParameters pars, object obj, string prefix = "") {
            var properties = obj.GetType().GetProperties();
            bool IsEnabled(IConditional attr) {
                if (attr.Enabled) { return true; }
                return (bool)properties.Single(
                    info => info.GetCustomAttribute<EnableParamAttribute>(true)?.CheckName(attr.Name) ?? false
                    ).GetValue(obj);
            }
            foreach (var info in properties) {
                if (!info.CanRead) { continue; }
                if (info.GetCustomAttribute<ParamAttribute>(true) is ParamAttribute attr) {
                    if (IsEnabled(attr) && ToString(info, info.GetValue(obj)) is string str) {
                        pars.AppendParam(prefix + attr.Name, str);
                    }
                } else if (info.GetCustomAttribute<FlatParamAttribute>(true) is FlatParamAttribute flat) {
                    if (IsEnabled(flat)) {
                        Append(pars, info.GetValue(obj), flat.Prefix);
                    }
                } else if (info.GetCustomAttribute<SubParamAttribute>(true) is SubParamAttribute sub) {
                    if (IsEnabled(sub)) {
                        var newPars = pars.AppendParams(sub.Name);
                        Append(newPars, info.GetValue(obj), sub.Prefix);
                    }
                }
            }
        }
    }
}