using InterBook2._0.BLL.Mail;
using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;

namespace InterBook2._0.BLL
{
    public class MailManager
    {
        public static void SendMail(MailBase mailbase, Dictionary<String, String> varsAdd)
        { 
            UE_envoi uee = mailbase.UeEnvoi;

            //si pas spam strap on envoie
            Util util = uee.Util;
            Util_Postal up = util.Util_Postal;
            Util_Email ue = uee.Util_Email;

            // On crée une liste de variable à remplacer dans les templates mails.
            //[[IDUE]]&i=[[IDU]]&uid=[[UID]]&idm=[[IDM]]&e=[[EMAIL]]
            Dictionary<String, String> vars = new Dictionary<String, String> 
            {
                {"[[prenom]]", Capitalize(up != null ? up.prenom : "")},
                {"[[prenomurl]]", CapitalizeAndEncode(up != null ? up.prenom : "")},
                {"[[server]]", mailbase.PathServer},
                {"[[serverurl]]", EncodeUrl(mailbase.PathServer)},
                {"[[localhost]]", "http://" + HttpContext.Current.Request.Url.Authority},
                {"[[email]]", ue.email},
                {"[[idm]]", uee.id_mailing.ToString()},
                {"[[idue]]",  uee.id_UE_envoi.ToString()},
                {"[[uid]]",  util.uid.ToString()},
                {"[[idu]]",  util.IdU.ToString()}
            };
            vars = vars.Union(varsAdd).ToDictionary(k => k.Key, v => v.Value);

            if (up != null && up.id_Civilite != 0)
                vars.Add("[[civ]]", up.id_Civilite == 1 ? "M." : "Mme");

            //création des paramètres à envoyer en GET dans le lien miroir 
            StringBuilder paramLienMiroir = new StringBuilder();
            foreach (KeyValuePair<String, String> element in vars)
                paramLienMiroir.AppendFormat("&{0}={1}", element.Key.Replace("[[", "").Replace("]]", ""), element.Value);

            paramLienMiroir.Replace("idto=0", "idto=1");

            //rajout des variables dynamiques
            vars.Add("[[lienmiroir]]", mailbase.LienMiroir);
            vars.Add("[[param]]", paramLienMiroir.ToString());

            if (mailbase.Sujet.ToLower().Contains("[[nom]]"))
                mailbase.Sujet = mailbase.Sujet.Replace("[[nom]]", CapitalizeAndRemoveNonLetter(up.nom));

            if (mailbase.Sujet.ToLower().Contains("[[prenom]]"))
                mailbase.Sujet = mailbase.Sujet.Replace("[[prenom]]", CapitalizeAndRemoveNonLetter(up.prenom));

            StreamReader streamReader = new StreamReader(HttpContext.Current.Server.MapPath("/Media/mail/" + string.Format("{0:0000}", uee.id_mailing) + ".html"));
            string content = streamReader.ReadToEnd();

            foreach (KeyValuePair<String, String> kvp in vars)
            {
                content = content.Replace(kvp.Key, kvp.Value);
            }

            // On envoie le mail  avec x-idmail de la forme [[NOMDELABASE]]_[[ID_MAILING]]_[[IDU_EMAIL]]_[[IDENVOI]]
            //using (var client = new SmtpClient("smtp.gmail.com", 587)
            //{
            //    Credentials = new NetworkCredential("qdu13eme@gmail.com", "Janvier;"),
            //    EnableSsl = true
            //})
            //{
            //    client.Send(mailbase.EmailNoReply, SessionManager.Current.Util.Util_Email.email.ToString(), mailbase.Sujet, content);
            //}

            var fromAddress = new MailAddress(mailbase.MailFrom, mailbase.NameFrom);
            var toAddress = new MailAddress(ue.email.ToString());
            string subject = mailbase.Sujet;
            string body = content;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("qdu13eme@gmail.com", "Janvier;")
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
        }

        public static void SendMailReservation(MailBase mailbase)
        {
            UE_envoi uee = mailbase.UeEnvoi;

            //si pas spam strap on envoie
            Util util = uee.Util;
            Util_Postal up = util.Util_Postal;
            Util_Email ue = util.Util_Email;

            // On crée une liste de variable à remplacer dans les templates mails.
            //[[IDUE]]&i=[[IDU]]&uid=[[UID]]&idm=[[IDM]]&e=[[EMAIL]]
            Dictionary<String, String> vars = new Dictionary<string, string> 
            {
                {"[[prenom]]", Capitalize(up != null ? up.prenom : "")},
                {"[[prenomurl]]", CapitalizeAndEncode(up != null ? up.prenom : "")},
                {"[[server]]", mailbase.PathServer},
                {"[[serverurl]]", EncodeUrl(mailbase.PathServer)},
                {"[[localhost]]", "http://" + HttpContext.Current.Request.Url.Authority},
                {"[[email]]", ue.email},
                {"[[idm]]", uee.id_mailing.ToString()},
                {"[[idue]]",  uee.id_UE_envoi.ToString()},
                {"[[uid]]",  util.uid.ToString()},
                {"[[idu]]",  util.IdU.ToString()}
            };

            if (up != null && up.id_Civilite != 0)
                vars.Add("[[civ]]", up.id_Civilite == 1 ? "M." : "Mme");

            //création des paramètres à envoyer en GET dans le lien miroir 
            StringBuilder paramLienMiroir = new StringBuilder();
            foreach (KeyValuePair<String, String> element in vars)
                paramLienMiroir.AppendFormat("&{0}={1}", element.Key.Replace("[[", "").Replace("]]", ""), element.Value);

            paramLienMiroir.Replace("idto=0", "idto=1");

            //rajout des variables dynamiques
            vars.Add("[[lienmiroir]]", mailbase.LienMiroir);
            vars.Add("[[param]]", paramLienMiroir.ToString());

            if (mailbase.Sujet.ToLower().Contains("[[nom]]"))
                mailbase.Sujet = mailbase.Sujet.Replace("[[nom]]", CapitalizeAndRemoveNonLetter(up.nom));

            if (mailbase.Sujet.ToLower().Contains("[[prenom]]"))
                mailbase.Sujet = mailbase.Sujet.Replace("[[prenom]]", CapitalizeAndRemoveNonLetter(up.prenom));

            StreamReader streamReader = new StreamReader(HttpContext.Current.Server.MapPath("/Media/mail/" + string.Format("{0:0000}", uee.id_mailing) + ".html"));
            string content = streamReader.ReadToEnd();

            foreach (KeyValuePair<String, String> kvp in vars)
            {
                content = content.Replace(kvp.Key, kvp.Value);
            }

            // On envoie le mail  avec x-idmail de la forme [[NOMDELABASE]]_[[ID_MAILING]]_[[IDU_EMAIL]]_[[IDENVOI]]
            //using (var client = new SmtpClient("smtp.gmail.com", 587)
            //{
            //    Credentials = new NetworkCredential("qdu13eme@gmail.com", "Janvier;"),
            //    EnableSsl = true
            //})
            //{
            //    client.Send(mailbase.EmailNoReply, SessionManager.Current.Util.Util_Email.email.ToString(), mailbase.Sujet, content);
            //}

            var fromAddress = new MailAddress(mailbase.EmailNoReply, mailbase.NameFrom);
            var toAddress = new MailAddress(SessionManager.Current.Util.Util_Email.email.ToString(), up != null ? up.prenom : null);
            string subject = mailbase.Sujet;
            string body = content;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("qdu13eme@gmail.com", "Janvier;")
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
        }

        public static string Capitalize(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            if (value.Length == 1)
                return value.ToUpper();

            return char.ToUpper(value[0]) + value.Substring(1).ToLower();

        }

        public static string CapitalizeAndEncode(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            if (value.Length == 1)
                return value.ToUpper();

            string cap = char.ToUpper(value[0]) + value.Substring(1).ToLower();
            return EncodeUrl(cap);
        }

        public static string EncodeUrl(string value)
        {
            return HttpUtility.UrlEncode(value);
        }

        public static string CapitalizeAndRemoveNonLetter(string value)
        {
            string normalizedString = value.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                Char c = normalizedString[i];
                if (Char.IsLetterOrDigit(c) || Char.IsWhiteSpace(c) || c.ToString() == "-")
                    stringBuilder.Append(c);
            }

            return Capitalize(stringBuilder.ToString());
        }

    }
}