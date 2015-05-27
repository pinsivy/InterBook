using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterBook2._0.BLL.Mail
{
    public class MailBase
    {
        public MailBase(string sujet, string lienmiroir, string nameFrom, string mailFrom, int? idMailing, Util util, Util_Email ue)
        {
            this.Sujet = sujet;
            this.LienMiroir = lienmiroir;
            this.NameFrom = nameFrom;
            this.MailFrom = mailFrom;

            if (idMailing.HasValue)
            {
                this.UeEnvoi = new UE_envoi
                {
                    dEnvoi = DateTime.Now,
                    id_mailing = idMailing.Value,
                    idu = util.IdU,
                    idu_email = ue.idu_Email
                };
                UeEnvoiManager.InsertLine(this.UeEnvoi);
            }
        }

        public string Sujet { get; set; }
        public string LienMiroir { get; private set; }
        public string NameFrom { get; private set; }
        public string MailFrom { get; private set; }
        public string PathServer { get { return BaseManager.PathServer; } }
        public string EmailSupport { get { return BaseManager.EmailSupport; } }
        public string EmailNoReply { get { return BaseManager.EmailNoReply; } }
        public UE_envoi UeEnvoi { get; set; }
    }
}