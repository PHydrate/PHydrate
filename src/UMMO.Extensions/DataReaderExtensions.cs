using System.Data;

namespace UMMO.Extensions
{
    public static class DataReaderExtensions
    {
        public static T GetValue<T>(this IDataRecord dataRecord, string columnName)
        {
            return dataRecord.GetValue<T>(dataRecord.GetOrdinal(columnName));
        }

        public static T GetValue<T>(this IDataRecord dataRecord, int columnOrdinal)
        {
            object value = dataRecord[columnOrdinal];
            if (typeof (T).IsValueType)
                return value == null ? default(T) : (T) value;
            return (T) value;
        }
    }
}