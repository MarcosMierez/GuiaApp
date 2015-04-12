namespace System.Web.Mvc
{
    public enum LoggerEnum
    {
        Success = 1,
        Info = 2,
        Warning = 3,
        Error = 4
    }
    public static class LoggerhHelper
    {
        public static void Flash(this Controller controller, string message, LoggerEnum type = LoggerEnum.Success)
        {
            var classe = "";
            switch (type)
            {
                case LoggerEnum.Success: classe = "alert-success";
                    break;
                case LoggerEnum.Info: classe = "alert-info";
                    break;
                case LoggerEnum.Warning: classe = "alert-warning";
                    break;
                case LoggerEnum.Error: classe = "alert-danger";
                    break;
            }
            controller.TempData[classe] = message;
        }
    }
}