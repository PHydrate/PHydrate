using System;
using System.Data;

namespace UMMO.Extensions
{
    public static class DataRecordExtensions
    {
        public static T Value<T>(this IDataRecord dataRecord, string columnName)
        {
            return dataRecord.Value<T>(dataRecord.GetOrdinal(columnName));
        }

        public static T Value<T>(this IDataRecord dataRecord, int columnOrdinal)
        {
            object value = dataRecord[columnOrdinal];
            if (typeof(T).IsEnum)
            {
                if (value is int)
                    return (T) value;
                return (value is DBNull || value == null) ? default(T) : (T)Enum.Parse(typeof(T), value.ToString());
            }
                
            return (value is DBNull || value == null) ? default(T) : (T) value;
        }
    }
}