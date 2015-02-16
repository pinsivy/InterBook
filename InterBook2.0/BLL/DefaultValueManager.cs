using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterBook2._0.BLL
{
    public static class DefaultValueManager
    {
        /// <summary>
        ///  Méthode surtout utilisé pour remplir les valeurs getdate() par défaut des objets pour les insertions en base
        /// </summary>
        /// <returns>Retourne la getdate() SQL</returns>
        public static DateTime ReturnSQLDate()
        {
            IBWS ws = new IBWS();
            return ws.ReturnSQLDate();
        }

        /// <summary>
        /// Méthode surtout utilisé pour remplir les valeurs uniqueidentifier par défaut des objets pour les insertions
        /// </summary>
        /// <returns>Retourne le newid() SQL</returns>
        public static Guid ReturnSQLGuid()
        {
            IBWS ws = new IBWS();
            return ws.ReturnSQLGuid();
        }
    }
}