using System;
using System.Collections.Generic;
using System.Reflection;
using StarWindXLib;

namespace StarWindXExtLib {
    public interface IDisplayable {
        string UniqueId { get; }
    }

    public abstract class DisplayWriter {
        public abstract void Write(string data);
        public abstract void Write(string name, object value);   

        public bool IsEnabled(Type parent, DisplayAttribute disp) {
            if (Skip.ContainsKey(parent)) {
                return Skip[parent].Contains(disp.Index);
            }
            return true;
        }

        public bool WriteCollections { get; set; } = true;

        public static string StringJoinDelimer { get; set; } = "; ";

        protected static string ToString(object obj) {
            if (obj is List<string> list) {
                return String.Join(StringJoinDelimer, list);
            } else {
                return obj.ToString();
            }
        }

        public Dictionary<Type, List<int>> Skip { get; } = new Dictionary<Type, List<int>>();
    }

    public abstract class LineDisplayWriter : DisplayWriter {
        public override void Write(string data) {
            WriteLine(data);
        }

        public override void Write(string name, object value) {
            WriteLine(
                Surround(name, NameSurround) +
                Delimer +
                Surround(ToString(value), ValueSurround));
        }

        public abstract void WriteLine(string line);

        private static string Surround(string value, string with) {
            return with + value + with;
        }

        public string Delimer { get; set; } = " = ";

        public string NameSurround { get; set; } = "";

        public string ValueSurround { get; set; } = "\"";
    }

    public class ConsoreDisplayWriter : LineDisplayWriter {
        public override void WriteLine(string line) {
            Console.WriteLine(line);
        }
    }

    public abstract class Displayable : IDisplayable {
        public abstract string UniqueId { get; }

        public void WriteUnorder(DisplayWriter writer) {
            WriteUnorder(writer, this);
        }

        private static void WriteUnorder(DisplayWriter writer, object obj) {
            var properties = obj.GetType().GetProperties();
            foreach (var info in properties) {
                if (!info.CanRead) { continue; }
                var value = info.GetValue(obj);
                if (info.GetCustomAttribute<DisplayAttribute>(true) is var attr) {
                    if (writer.IsEnabled(obj.GetType(), attr)) { continue; }
                    if (value is ICollection collection && writer.WriteCollections) {
                        writer.Write("Collection " + attr.Name, attr.CollecionType.ToString());
                        foreach (object sub in collection) {
                            WriteUnorder(writer, sub);
                        }
                        writer.Write("Collection " + attr.Name + " End");
                    } else {
                        writer.Write(attr.Name, value);
                    }
                }
            }
        }
    }
}
