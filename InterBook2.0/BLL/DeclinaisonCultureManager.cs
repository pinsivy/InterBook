using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace InterBook2._0.BLL
{
    public static class DeclinaisonCultureManager
    {
        public static void SetCulture()
        {

            CultureInfo mCurrentUICulture = null;


            //Ordre de test pour la culture
            //1) Langue du Cookie
            //2) URL de la langue
            //3) En fonction de l'ifrom
            //4) Langue du Navigateur
            //5) Langue Set de l'op par défaut

            //on ne refait pas le test de la culture si la culture est déja en session
            //SessionManager.Current.CurrentUICulture = null;
            if (SessionManager.Current.CurrentUICulture != null && System.Threading.Thread.CurrentThread.CurrentUICulture.Name != SessionManager.Current.CurrentUICulture.Name)
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = SessionManager.Current.CurrentUICulture;
            }
            else if (SessionManager.Current.CurrentUICulture == null)
            {

                //on vérifie au cas ou les propriétés de la classe ApplicationObject.cs n'ont pas été générés
                if ((ApplicationObject.DomainesInfos == null) || (ApplicationObject.CulturesInfos == null))
                {
                    ApplicationManager.SetDomaineDictionnary();
                    ApplicationManager.SetCultureDictionnary();
                }


                //on récupére la collection de refdomaine et de refculture de la classe ApplicationObject.cs qui représente les données en base
                List<Ref_DomaineSimple> rfc = ApplicationObject.DomainesInfos;
                List<Ref_CultureSimple> rcc = ApplicationObject.CulturesInfos;


                //test du cookie
                HttpCookie CookieLangue = new HttpCookie("CookieLangue");
                CookieLangue = HttpContext.Current.Request.Cookies["CookieLangue"];

                //1) Si le cookie est présent, on prend la valeur du cookie pour set la culture
                if (CookieLangue != null)
                {
                    mCurrentUICulture = new CultureInfo(CookieLangue.Value);
                }
                else
                {
                    //2) On va regarder si le domaine du httpcontext est bien un domaine existant dans la collection de domaines
                    bool urlOK = false;
                    foreach (Ref_DomaineSimple item in rfc)
                    {
                        //on set la culture du domaine qu'on connait
                        if (item.Domaine.Equals(HttpContext.Current.Request["SERVER_NAME"], StringComparison.OrdinalIgnoreCase))
                        {
                            urlOK = true;
                            Ref_CultureSimple rcs = GetRefCultureByIdDeclCult((int)item.id_Declinaison_Culture);
                            mCurrentUICulture = new CultureInfo(rcs.description);
                            break;
                        }
                    }

                    //3) On va regarder si on connait l'idfrom ou pas 
                    if (!urlOK)
                    {
                        Int32 Id_From;
                        bool IdFromOK = false;
                        List<Ref_FromSimple> rfrc = new List<Ref_FromSimple>();
                        if (Int32.TryParse(HttpContext.Current.Request["idfrom"], out Id_From))
                        {
                            rfrc = GetRefFromByIdfrom(Id_From);
                            if (rfrc.Count > 0)
                            {
                                IdFromOK = true;
                            }
                        }


                        //si idfrom est présent en base ou non, si oui on set la culture correspondant pour l'idfrom
                        if (IdFromOK)
                        {
                            Ref_CultureSimple rcs = GetRefCultureByIdDeclCult((int)rfrc[0].id_Declinaison_Culture);
                            mCurrentUICulture = new CultureInfo(rcs.description);
                        }
                        else
                        {
                            //4) On va regarder si la langue du navigateur est connu dans nos cultures dans la collection d'applications
                            bool cultureOK = false;
                            string cultureNavigateur;

                            //regarde si au moins une langue est défini dans la navigateur du joueur
                            if (HttpContext.Current.Request.UserLanguages != null)
                            {
                                //on parcours la liste des langues définis dans le navigateur du joueur
                                for (int it = 0; it < (HttpContext.Current.Request.UserLanguages.Length); it++)
                                {
                                    //pour chaque langue on va tester sur toutes les cultures
                                    foreach (Ref_CultureSimple item in rcc)
                                    {
                                        //obligation de faire un split car certaines langues ça renvoie par exemple en-US;XXX
                                        cultureNavigateur = HttpContext.Current.Request.UserLanguages[it].Split(';')[0];
                                        if (item.description.Equals(cultureNavigateur))
                                        {
                                            cultureOK = true;
                                            mCurrentUICulture = new CultureInfo(cultureNavigateur);
                                            break;
                                        }

                                    }
                                    //on sort de la boucle des qu'on trouve une culture
                                    if (cultureOK == true)
                                    {
                                        break;
                                    }
                                }
                            }


                            //5) On met la langue par défaut de l'OP
                            if (!cultureOK)
                            {
                                mCurrentUICulture = new CultureInfo("fr-FR");
                            }
                        }
                    }

                    //on stocke la culture en session
                    SessionManager.Current.CurrentUICulture = mCurrentUICulture;
                }
            }
            //on met la bonne culture
            System.Threading.Thread.CurrentThread.CurrentUICulture = SessionManager.Current.CurrentUICulture;
        }

        /// <summary>
        /// Méthode qui va permettre de set l'id_declinaison_culture
        /// </summary>
        public static int SetDeclinaisonCulture()
        {
            int retour = 0;

            //on verifie si la variable d'application n'est pas vide
            if (ApplicationObject.DomainesInfos == null)
            {
                ApplicationManager.SetDomaineDictionnary();
            }

            List<Ref_DomaineSimple> rdc = ApplicationObject.DomainesInfos;
            string itemDomaine;
            foreach (Ref_DomaineSimple item in rdc)
            {
                itemDomaine = item.Domaine;
                if (item.Domaine.Contains("localhost"))
                    itemDomaine = itemDomaine.Substring(0, 9);
                //on set l'id_declinaison_culture correspondant
                if (itemDomaine.Equals(HttpContext.Current.Request["SERVER_NAME"], StringComparison.OrdinalIgnoreCase))
                {
                    retour = (int)item.id_Declinaison_Culture;
                    break;
                }
            }
            return retour;

        }

        public static List<Ref_DomaineSimple> SelectAllRefDomaine()
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.SelectAllRefDomaine();
        }

        public static List<Ref_CultureSimple> SelectAllRefCulture()
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.SelectAllRefCulture();
        }

        public static List<Ref_Declinaison_CultureSimple> SelectAllRefDeclinaisonCulture()
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.SelectAllRefDeclinaisonCulture();
        }

        public static List<Ref_FromSimple> GetRefFromByIdfrom(int idf)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.GetRefFromByIdfrom(idf);
        }

        public static Ref_CultureSimple GetRefCultureByIdDeclCult(int iddc)
        {
            if (SessionManager.Current.ws == null)
                SessionManager.Current.ws = new IBWS();
            return SessionManager.Current.ws.GetRefCultureByIdDeclCult(iddc);
        }

        //public static Ref_Declinaison_Culture GetById(int id)
        //{
        //    return new Select().From<Ref_Declinaison_Culture>()
        //        .Where(Ref_Declinaison_Culture.Columns.Id_Declinaison_Culture).IsEqualTo(id)
        //        .ExecuteSingle<Ref_Declinaison_Culture>();
        //}
    }
}
