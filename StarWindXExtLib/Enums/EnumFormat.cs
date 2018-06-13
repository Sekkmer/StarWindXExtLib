﻿using System;
using System.Reflection;

namespace StarWindXExtLib {
    public static partial class EnumFormat {
        public static string EnumToString(object obj) {
            if (obj.GetType().GetField(obj.ToString()).GetCustomAttribute<StringValueAttribute>() is var attr) {
                return attr.Value;
            }
            return obj.ToString();
        }

        public static void EnumFromString(object obj, string str) {
            Type type = obj.GetType();
            if (type.BaseType != typeof(Enum))
                throw new ArgumentException("obj must be of type System.Enum");
            str = str.ToLower();
            foreach (object value in Enum.GetValues(type)) {
                if (type.GetField(value.ToString()).GetCustomAttribute<StringValueAttribute>() is var attr) {
                    obj = value;
                    return;
                }
            }
            throw new ArgumentException("string is not recognised");
        }
    }
}