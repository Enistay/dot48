using System;
using System.ComponentModel;
using System.Configuration;
using System.Text;

namespace dot48.Application.Extensions
{
    public static class StringExtensions
    {
        public static TValue ConverterValor<TValue>(this string valor)
        {
            var convert = TypeDescriptor.GetConverter(typeof(TValue));

            return string.IsNullOrEmpty(valor) 
                ? default(TValue) 
                : (TValue)convert.ConvertFromString(valor);
        }

        public static string EncryptorSha256(this string valor)
        {
            if (valor == null)
            {
                return null;
            }

            valor = String.Concat(valor, ConfigurationManager.AppSettings["keyappdot48"]);


            System.Security.Cryptography.SHA256Managed crypt = new System.Security.Cryptography.SHA256Managed();
            StringBuilder hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(valor), 0, Encoding.UTF8.GetByteCount(valor));

            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString();

        }
    }
}