﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StarWindXLib;

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
                if (info.GetCustomAttribute<BoolToStringAttribute>(true) is var attr) {
                    return b ? attr.TrueString : attr.FalseString;
                }
                return null;
            } else if (obj.GetType().BaseType == typeof(Enum)) {
                return EnumFormat.EnumToString(obj);
            } else if (obj is int i) {
                if (i == 0 && info.GetCustomAttribute<IntZeroAttribute>(true) is var attr) {
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
                return (bool)properties.Single(info => info.GetCustomAttribute<EnableParamAttribute>(true)?.CheckName(attr.Name) ?? false)
                    .GetValue(obj);
            }
            foreach (var info in properties) {
                if (!info.CanRead) { continue; }
                var value = info.GetValue(obj);
                if (info.GetCustomAttribute<ParamAttribute>(true) is var attr) {
                    if (IsEnabled(attr) && ToString(info, value) is var str) {
                        pars.AppendParam(prefix + attr.Name, str);
                    }
                } else if (info.GetCustomAttribute<FlatParamAttribute>(true) is var flat) {
                    if (IsEnabled(flat)) {
                        Append(pars, value, flat.Prefix);
                    }
                } else if (info.GetCustomAttribute<SubParamAttribute>(true) is var sub) {
                    if (IsEnabled(sub)) {
                        var newPars = pars.AppendParams(sub.Name);
                        Append(newPars, value, sub.Prefix);
                    }
                }
            }
        }
    }
}