using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace Easy_Http.Helpers
{
    public static class TypeExtentions
    {
        public static IEnumerable<T> GetAttributes<T>(this ICustomAttributeProvider source, bool inherit) where T : Attribute
        {
            var attrs = source.GetCustomAttributes(typeof(T), inherit);

            return (attrs != null) ? (T[])attrs : Enumerable.Empty<T>();
        }

        public static IEnumerable<FieldInfo> GetFieldsWithAttribute<T, A>(this T type, A attribute, Func<A, bool> funcToSearch)
            where A : Attribute,
            new() where T : Type
        {
            return type.GetFields().Where(f => f.GetAttributes<A>(false).Any(funcToSearch));
        }

        public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<T, A>(this T type, A attribute, Func<A, bool> funcToSearch)
            where A : Attribute,
            new() where T : Type
        {
            return type.GetProperties().Where(f => f.GetAttributes<A>(false).Any(funcToSearch));
        }

        public static async System.Threading.Tasks.Task<MultipartFormDataContent> ParseObjectAsync<Model>(this MultipartFormDataContent content, Model model)
            where Model : class
        {
            foreach (var prop in model.GetType().GetProperties())
            {
                var value = prop.GetValue(model);
                if (value == null)
                    continue;

                if (prop.PropertyType == typeof(IFormFile)
                    || prop.PropertyType == typeof(FormFile))
                {
                    using (var ms = new MemoryStream())
                    {
                        IFormFile file = (IFormFile)value;
                        await file.CopyToAsync(ms);
                        content.Add(new ByteArrayContent(ms.ToArray()), prop.Name, file.FileName);
                    }

                    continue;
                }
                else if (prop.PropertyType == typeof(byte[])
                            || prop.PropertyType == typeof(IEnumerable<>))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bf.Serialize(ms, value);
                        content.Add(new ByteArrayContent(ms.ToArray()), prop.Name);
                    }
                    continue;
                }

                content.Add(new StringContent(prop.GetValue(model).ToString()), prop.Name);
            }

            return content;
        }


        public static MultipartFormDataContent ParseObject<Model>(this MultipartFormDataContent content, Model model)
            where Model : class
        {
            foreach (var prop in model.GetType().GetProperties())
            {
                var value = prop.GetValue(model);
                if (value == null)
                    continue;

                if (prop.PropertyType == typeof(IFormFile)
                    || prop.PropertyType == typeof(FormFile))
                {
                    using (var ms = new MemoryStream())
                    {
                        IFormFile file = (IFormFile)value;
                        file.CopyTo(ms);
                        content.Add(new ByteArrayContent(ms.ToArray()), prop.Name, file.FileName);
                    }

                    continue;
                }
                else if (prop.PropertyType == typeof(byte[])
                            || prop.PropertyType == typeof(IEnumerable<>))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bf.Serialize(ms, value);
                        content.Add(new ByteArrayContent(ms.ToArray()), prop.Name);
                    }
                    continue;
                }

                content.Add(new StringContent(value.ToString()), prop.Name);
            }

            return content;
        }
    }
}
