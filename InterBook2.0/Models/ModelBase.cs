using InterBook2._0.BLL;
using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterBook2._0.Models
{
    /// <summary>
    /// Ce Modèle doit être la classe mère de chaque modèle crée.
    /// Il contient des données nécessaire au fonctionnement du _layout (pseudo MasterPage).
    /// </summary>
    public class ModelBase
    {
        /// <summary>
        /// La session courante
        /// </summary>
        public SessionObject SessionObject
        {
            get
            {
                return SessionManager.Current;
            }
        }

        /// <summary>
        /// Titre de l'op
        /// </summary>
        public string Title { get { return App_GlobalResources.Commun.Title; } }

        /// <summary>
        /// Sujet du mail de contact (Visible dans le footer)
        /// </summary>
        public string SujetMailContact { get { return App_GlobalResources.Commun.SujetMailContact; } }

        /// <summary>
        /// Footer
        /// </summary>
        public string ReglementTitle { get { return App_GlobalResources.Commun.ReglementTitle; } }

        public string ReglementContent { get { return App_GlobalResources.Commun.ReglementContent; } }

        public string CharteTitle { get { return App_GlobalResources.Commun.CharteTitle; } }

        public string CharteContent { get { return App_GlobalResources.Commun.CharteContent; } }

        private string _mentionlegale = App_GlobalResources.Commun.Mentionlegale;
        /// <summary>
        /// Mentions légales de l'application. Si les mentions doivent changer selon la vue, il suffit de le changer dans le modèle de la vue.
        /// Exemple: page de diffusion => DiffusionModel.MentionLegale = blabla
        /// </summary>
        public string MentionLegale { get { return _mentionlegale; } set { _mentionlegale = value; } }

        private int? Idu { get { return SessionObject.Util != null && SessionObject.Util.IdU > 0 ? SessionObject.Util.IdU : new Nullable<int>(); } }
    }
}