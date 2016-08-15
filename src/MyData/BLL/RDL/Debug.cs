using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Reflection;
using System.Text;
using System.Web;
using Common;

namespace RDL
{
    public abstract class Debug
    {
        private static string ErrorLogEmailTo = ConfigurationManager.AppSettings["ErrorLogEmailTo"];
        private const string SiteName = "MyData";

        private static bool IsLocal
        {
            get { return HttpContext.Current == null || HttpContext.Current.Request.IsLocal; }
        }

        public static void LogError(Exception ex, string additional = "", object parameters = null)
        {
            if (IsLocal)
            {
                throw ex;
            }
            else
            {
                if (ex.InnerException != null)
                    ex = ex.InnerException;

                var errorMessage = GetBodyEmail(ex, additional, parameters);

                var title = "Ошибка на сайте " + SiteName;
                SendEmail(title, errorMessage);
            }
        }

        private static string GetBodyEmail(Exception exception, string additional = "", object parameters = null)
        {
            var sb = new StringBuilder();
            try
            {
                string dateTime = DateTime.Now.ToString();
                HttpContext context = HttpContext.Current;
                
                sb.AppendLine("<br><b>Страница:</b> " + context.Request.Url.Host + context.Request.RawUrl);
                sb.AppendLine("<br><b>Сообщение:</b> " + exception.Message + "<br /><hr /><br />");
                sb.AppendLine("<b>Время возникновения ошибки:</b> " + dateTime);
                sb.AppendLine("<br><b>Источник:</b> " + exception.Source);
                sb.AppendLine("<br><b>Метод:</b> " + exception.TargetSite);
                sb.AppendLine("<br>Дополнительно: " + additional);
                sb.AppendLine("<br><b>Стек трассировки:</b><br>" + exception.StackTrace);

                if (parameters != null)
                {
                    sb.AppendLine(DumpProperties(parameters));
                }

                HttpContext cnt = HttpContext.Current;

                sb.AppendLine("<br><hr />");
                sb.AppendLine("<br><b>Полный путь:</b> " + cnt.Request.Url.ToString());
                sb.AppendLine("<br><b>Пользователь:</b> " + (cnt.User.Identity.Name != "" ? cnt.User.Identity.Name : "Не аутентифицирован"));
                sb.AppendLine("<br><b>Браузер:</b> " + cnt.Request.Browser.Type);
                sb.AppendLine("<br><b>Версия браузера:</b> " + cnt.Request.Browser.Version);
                sb.AppendLine("<br><b>Поисковый робот:</b> " + (cnt.Request.Browser.Crawler ? "Да" : "Нет"));
            }
            catch (Exception ex)
            {
                sb.AppendLine("ОШИБКА В DEBUG: " + ex.ToString());
            }
            return sb.ToString();
        }

        private static void SendEmail(string title, string errorMessage)
        {
            try
            {
                var userSt = ConfigurationManager.AppSettings["EmailFromUser"];
                var passwordSt = ConfigurationManager.AppSettings["EmailFromPassword"];
                var user = new EmailUser(userSt, passwordSt);
                var emailSender = new EmailSender(EmailSMTP.Gmail, user);
                emailSender.Send(ErrorLogEmailTo, title, errorMessage);
            }
            catch // (Exception ex)
            { }
        }

        private static string DumpProperties(object Object)
        {
            try
            {
                StringBuilder TempValue = new StringBuilder();
                TempValue.Append("<table><thead><tr><th>Property Name</th><th>Property Value</th></tr></thead><tbody>");
                Type ObjectType = Object.GetType();
                PropertyInfo[] Properties = ObjectType.GetProperties();
                foreach (PropertyInfo Property in Properties)
                {
                    TempValue.Append("<tr><td>" + Property.Name + "</td><td>");
                    ParameterInfo[] Parameters = Property.GetIndexParameters();
                    if (Property.CanRead && Parameters.Length == 0)
                    {
                        try
                        {
                            object Value = Property.GetValue(Object, null);
                            TempValue.Append(Value == null ? "null" : Value.ToString());
                        }
                        catch { }
                    }
                    TempValue.Append("</td></tr>");
                }
                TempValue.Append("</tbody></table>");
                return TempValue.ToString();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
    }
}