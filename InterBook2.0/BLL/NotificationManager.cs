
using System;
using System.IO;
using System.Net;
using System.Text;
namespace InterBook2._0.BLL
{
    public class NotificationManager
    {
        public static string SendNotification(string deviceId, string title, string message, string arg)
        {
            //Device Moto G
            //deviceId = "APA91bGNt6fM0rpPmQxLs79RqLHqxWBDWkMm7D6cNdiBdvr5jwVP0vbqyX0SPqdzemgXUuRqUnyjiNRRaeiN0lIewIJhh2xzKuxKznn1L9-Qdd0vzrXmSBKYVNPbqu7tPU_extgE_rH51b59gQSLrCwnkYXkb0GSSgdGl8dx61eGZD7fh-rT2QA";

            //Device Emaulateur
            //deviceId = "APA91bEi_7592mGiSEIl-yM2G7pq7xlfCMYoiNN-nzP_0pyfqUQBNn8w0Mg_TNRw-VwqGUTm1AH2bltcCw9RkJRozbgd43NtlvHtY3gQZ8y-FXFxUM0_EAHaxRrNnHcYNsPl-fAq4cMM7HB-ILI5MxBkt8GE706NtVHCWZqRgFxzJ7p8DOGUizA";

            string GoogleAppID = "AIzaSyDNTCyPULQDPmiJA1Tt0hV2Cr1iUkRyEDs";
            var SENDER_ID = "200212735020";
            WebRequest tRequest;
            tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", GoogleAppID));

            tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

            //Changer collapse_key ?
            //String collaspeKey = Guid.NewGuid().ToString("n");
            //
            string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.title=" + title + "&data.title=" + message + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + deviceId;
            postData = String.IsNullOrEmpty(arg) ? postData : postData + "&" + arg;
            
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