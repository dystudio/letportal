﻿using LetPortal.Core.Extensions;
using LetPortal.Core.Persistences;
using LetPortal.Core.Persistences.Attributes;
using LetPortal.Portal.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace LetPortal.Portal.Entities.Databases
{
    [EntityCollection(Name = DatabaseConstants.DatabaseCollection)]
    [Table("databaseconnections")]
    public class DatabaseConnection : BackupableEntity
    {
        public string ConnectionString { get; set; }

        public string DataSource { get; set; }

        public string DatabaseConnectionType { get; set; }

        public ConnectionType GetConnectionType()
        {
            return DatabaseConnectionType.ToEnum<ConnectionType>(true);
        }
    }
}
