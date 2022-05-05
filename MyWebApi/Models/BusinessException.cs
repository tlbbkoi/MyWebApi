using System;
using System.Globalization;

namespace MyWebApi.Models
{
    /// <summary>
    /// Exception type for validate exceptions
    /// </summary>
    public class BusinessException : Exception
    {
        public const string ErrorCode = "error_code";
        public BusinessException()
        { }

        public BusinessException(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture,
           message, args))
        {
        }

        public BusinessException(string message)
            : base(message)
        { }

        public BusinessException(string message, Exception innerException)
            : base(message, innerException)
        { }
        public BusinessException(string message, int code) : base(message)
        {
            Data.Add(ErrorCode, code);
        }
    }
}