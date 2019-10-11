﻿using LetPortal.Core.Persistences;
using LetPortal.Core.Versions;
using LetPortal.Portal.Entities.Databases;
using System;

namespace LetPortal.Versions.Databases
{
    internal class Database_0_0_1 : IVersion
    {
        public string VersionNumber => "0.0.1";

        public void Downgrade(IVersionContext versionContext)
        {
            versionContext.DeleteData<DatabaseConnection>(Constants.CoreDatabaseId);
        }

        public void Upgrade(IVersionContext versionContext)
        {
            DatabaseOptions databaseOptions = versionContext.DatabaseOptions as DatabaseOptions;
            var databaseManagement = new DatabaseConnection
            {
                Id = Constants.CoreDatabaseId,
                ConnectionString = databaseOptions.ConnectionString,
                DatabaseConnectionType = Enum.GetName(typeof(ConnectionType), databaseOptions.ConnectionType).ToLower(),
                DataSource = databaseOptions.Datasource,
                Name = "Database Management"
            };

            versionContext.InsertData(databaseManagement);
        }
    }

}
