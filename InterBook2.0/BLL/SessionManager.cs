using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace InterBook2._0.BLL
{
    public static class SessionManager
    {
        public static SessionObject Current
        {
            get
            {
                if (HttpContext.Current.Session["MaSession"] == null)
                    HttpContext.Current.Session["MaSession"] = new SessionObject();

                return HttpContext.Current.Session["MaSession"] as SessionObject;
            }
        }

        public static void ClearSession()
        {
            if (HttpContext.Current.Session["MaSession"] != null)
                HttpContext.Current.Session["MaSession"] = null;
        }

        public static void BuildSession(string email)
        {
            // Création de l'utilisateur
            SessionManager.Current.Util = UtilManager.GetCompletUtilByEmail(email);

            // Si l'utilisateur n'existe pas, on le vire.
            if (!SessionManager.Current.UtilExists())
                return;

            int idu = SessionManager.Current.Util.IdU;

            // Création des optins
            List<Util_ConsentementSimple> consentements = UtilConsentementManager.GetUtilConsentementByIdu(idu);
        }





        public static string SendNotification(string deviceId, string message)
        {
            deviceId = "APA91bGNt6fM0rpPmQxLs79RqLHqxWBDWkMm7D6cNdiBdvr5jwVP0vbqyX0SPqdzemgXUuRqUnyjiNRRaeiN0lIewIJhh2xzKuxKznn1L9-Qdd0vzrXmSBKYVNPbqu7tPU_extgE_rH51b59gQSLrCwnkYXkb0GSSgdGl8dx61eGZD7fh-rT2QA";

            string GoogleAppID = "AIzaSyDNTCyPULQDPmiJA1Tt0hV2Cr1iUkRyEDs";
            var SENDER_ID = "200212735020";
            var value = message;
            WebRequest tRequest;
            tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", GoogleAppID));

            tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

            //Changer collapse_key ?
            //String collaspeKey = Guid.NewGuid().ToString("n");
            //
            string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + deviceId + "";
            Console.WriteLine(postData);
            Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            tRequest.ContentLength = byteArray.Length;

            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse tResponse = tRequest.GetResponse();

            dataStream = tResponse.GetResponseStream();

            StreamReader tReader = new StreamReader(dataStream);

            String sResponseFromServer = tReader.ReadToEnd();


            tReader.Close();
            dataStream.Close();
            tResponse.Close();
            return sResponseFromServer;
        }



    }
}