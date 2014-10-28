using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterBook2._0.Models
{
    /// <summary>
    /// Ce modèle contient les données reçues en paramètre de la Home
    /// </summary>
    public class RequestModel : ModelBase
    {
        // ID Unique pour relog
        public string uid { get; set; }

        /// <summary>
        /// Lorque certain paramètres sont obligatoires, cette mèthode permet de faire ce contrôle.
        /// Il suffit de tester les paramètres ici
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return true;
        }
    }
}