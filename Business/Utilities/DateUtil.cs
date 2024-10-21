namespace Business.Utilities
{
    public static class DateUtil
    {
        public static string DateToString(DateTime? date, string dateFormat = "dd/MM/yyyy")
        {
            try
            {
                return date == null ? "" : ((DateTime)date).ToString(dateFormat);
            }
            catch (Exception ex)
            {
                //todo-log
                //LogUtil.Error(ex);
                return null;
            }
        }
        public static string DateTimeToString(DateTime? date, string dateFormat = "d/M/yyyy H:m:s")
        {
            try
            {
                return date == null ? "" : ((DateTime)date).ToLocalTime().ToString(dateFormat);
            }
            catch (Exception ex)
            {
                //todo-log
                //LogUtil.Error(ex);
                return null;
            }
        }
        public static DateTime? StringToDate(string dateStr, string dateFormat = "d/M/yyyy", bool logEx = false)
        {
            try
            {
                return DateTime.SpecifyKind(DateTime.ParseExact(dateStr, dateFormat, null), DateTimeKind.Local);
            }
            catch (Exception ex)
            {
                if (logEx)
                {
                    //todo-log
                    //LogUtil.Error(ex);
                }
                return null;
            }
        }
        public static DateTime? StringToDateTime(string dateStr, string dateFormat = "d/M/yyyy H:m:s", bool logEx = false)
        {
            try
            {
                return DateTime.SpecifyKind(DateTime.ParseExact(dateStr, dateFormat, null), DateTimeKind.Local);
            }
            catch (Exception ex)
            {
                if (logEx)
                {
                    //todo-log
                    //LogUtil.Error(ex);
                }
                return null;
            }
        }
    }
}
