﻿using System;
using System.Collections.Generic;
using System.Text;
using LetPortal.Core.Versions;
using LetPortal.Portal.Entities.Pages;
using LetPortal.Portal.Entities.SectionParts;
using LetPortal.Portal.Entities.Shared;

namespace LetPortal.Versions.SectionParts
{
    public class DynamicList_0_0_1 : IVersion
    {
        public string VersionNumber => "0.0.1";

        public void Downgrade(IVersionContext versionContext)
        {
            versionContext.DeleteData<DynamicList>("5d0f09de62c8371c183c8c6f");
            versionContext.DeleteData<DynamicList>("5d0f2dca6ba2fd4ca49e3741");
            versionContext.DeleteData<DynamicList>("5d0f2dca6ba2fd4ca49e3742");
            versionContext.DeleteData<DynamicList>("5d0f2dca6ba2fd4ca49e3743");
            versionContext.DeleteData<DynamicList>("5d0f2dca6ba2fd4ca49e3746");
        }

        public void Upgrade(IVersionContext versionContext)
        {   
            var databaseListSectionPart = new DynamicList
            {
                Id = "5d0f09de62c8371c183c8c6f",
                Name = "database-list-section-part",
                DisplayName = "Databases List",
                Options = Constants.DynamicListOptions,
                ListDatasource = new DynamicListDatasource
                {
                    DatabaseConnectionOptions = new DatabaseOptions
                    {
                        DatabaseConnectionId = Constants.CoreDatabaseId,
                        EntityName = "databases",
                        Query = versionContext.ConnectionType == Core.Persistences.ConnectionType.MongoDB 
                        ? "{ \"$query\": { \"databases\": [ ] } }" 
                        : "Select * From databases"
                    },
                    SourceType = DynamicListSourceType.Database
                },                   
                CommandsList = new CommandsList
                {
                    CommandButtonsInList = new List<CommandButtonInList>
                    {
                        new CommandButtonInList
                        {
                            Name = "create",
                            DisplayName = "Create",
                            Color = "primary",
                            ActionCommandOptions = new ActionCommandOptions
                            {
                                ActionType = ActionType.Redirect,
                                RedirectOptions = new RedirectOptions
                                {
                                    IsSameDomain = true,
                                    RedirectUrl = "portal/page/database-form"
                                }
                            },
                            CommandPositionType = CommandPositionType.OutList,
                            Order = 0
                        },
                        new CommandButtonInList
                        {
                            Name = "edit",
                            DisplayName = "Edit",
                            Icon = "create",
                            Color = "primary",
                            ActionCommandOptions = new ActionCommandOptions
                            {
                                ActionType = ActionType.Redirect,
                                RedirectOptions = new RedirectOptions
                                {
                                    IsSameDomain = true,
                                    RedirectUrl = "portal/page/database-form?id={{data.id}}"
                                }
                            },
                            Order = 1
                        },
                        new CommandButtonInList
                        {
                            Name = "delete",
                            DisplayName = "Delete",
                            Icon = "delete",
                            Color = "warn",
                            ActionCommandOptions = new ActionCommandOptions
                            {
                                ActionType = ActionType.Redirect,
                                RedirectOptions = new RedirectOptions
                                {
                                    IsSameDomain = true,
                                    RedirectUrl = "portal/page/database-form?id={{data.id}}"
                                }
                            },
                            Order = 2
                        },
                        new CommandButtonInList
                        {
                            Name = "flush",
                            DisplayName = "Flush",
                            Icon = "sync",
                            Color = "accent",
                            ActionCommandOptions = new ActionCommandOptions
                            {
                                ActionType = ActionType.CallHttpService,
                                HttpServiceOptions = new HttpServiceOptions
                                {
                                    HttpServiceUrl = "{{configs.portalBaseEndpoint}}/api/entity-schemas/flush",
                                    JsonBody = "{\"databaseId\":\"{{data.id}}\",\"keptSameName\":true}",
                                    HttpMethod = "POST",
                                    HttpSuccessCode = "200"
                                }
                            },
                            Order = 3
                        }
                    }
                },
                ColumnsList = new ColumnsList
                {
                    ColumndDefs = new List<ColumndDef>
                    {
                        new ColumndDef
                        {
                            Name = "id",
                            DisplayName = "Id",
                            IsHidden = true,
                            DisplayFormat = "{0}",
                            SearchOptions = new SearchOptions
                            {
                                AllowInAdvancedMode = false,
                                AllowTextSearch = false
                            },
                            Order = 0
                        },
                        new ColumndDef
                        {
                            Name = "name",
                            DisplayName = "Name",
                            DisplayFormat = "{0}",
                            AllowSort = true,
                            SearchOptions = new SearchOptions
                            {
                                AllowInAdvancedMode = true,
                                AllowTextSearch = true
                            },
                            Order = 1
                        },
                        new ColumndDef
                        {
                            Name = "databaseConnectionType",
                            DisplayName = "Connection Type",
                            DisplayFormat = "{0}",
                            AllowSort = true,
                            DisplayFormatAsHtml = true,
                            SearchOptions = new SearchOptions
                            {
                                AllowInAdvancedMode = true,
                                AllowTextSearch = true,
                                FieldValueType = FieldValueType.Select
                            },
                            DatasourceOptions = new DynamicListDatasourceOptions
                            {
                                DatasourceStaticOptions = new DatasourceStaticOptions
                                {
                                    JsonResource = "[{\"name\":\"MongoDB\",\"value\":\"mongodb\"},{\"name\":\"SQL Server\",\"value\":\"sqlserver\"}, {\"name\":\"PostgreSQL\",\"value\":\"postgresql\"}, {\"name\":\"MySQL\",\"value\":\"mysql\"}]"
                                },
                                Type = DatasourceControlType.StaticResource
                            },
                            Order = 2
                        },
                        new ColumndDef
                        {
                            Name = "connectionString",
                            DisplayName = "Connection String",
                            DisplayFormat = "{0}",
                            AllowSort = true,
                            SearchOptions = new SearchOptions
                            {
                                AllowInAdvancedMode = true,
                                AllowTextSearch = true
                            },
                            Order = 3
                        },
                        new ColumndDef
                        {
                            Name = "dataSource",
                            DisplayName = "Datasource",
                            DisplayFormat = "{0}",
                            AllowSort = true,
                            SearchOptions = new SearchOptions
                            {
                                AllowInAdvancedMode = true,
                                AllowTextSearch = true
                            },
                            Order = 4
                        }
                    }
                }
            };

            var appListSectionPart = new DynamicList
            {
                Id = "5d0f2dca6ba2fd4ca49e3741",
                Name = "app-list-section-part",
                DisplayName = "Apps List",
                Options = Constants.DynamicListOptions,
                ListDatasource = new DynamicListDatasource
                {
                    DatabaseConnectionOptions = new DatabaseOptions
                    {
                        DatabaseConnectionId = Constants.CoreDatabaseId,
                        EntityName = "apps",
                        Query = versionContext.ConnectionType == Core.Persistences.ConnectionType.MongoDB ?
                            "{ \"$query\": { \"apps\": [ ] } }" 
                            : "Select * from apps"
                    },
                    SourceType = DynamicListSourceType.Database
                },
                CommandsList = new CommandsList
                {
                    CommandButtonsInList = new List<CommandButtonInList>
                    {
                        new CommandButtonInList
                        {
                            Name = "create",
                            DisplayName = "Create",
                            Color = "primary",
                            CommandPositionType = CommandPositionType.OutList,
                            ActionCommandOptions = new ActionCommandOptions
                            {
                                ActionType = ActionType.Redirect,
                                RedirectOptions = new RedirectOptions
                                {
                                    IsSameDomain = true,
                                    RedirectUrl = "portal/page/app-form"
                                }
                            },
                            Order = 0
                        },
                        new CommandButtonInList
                        {
                            Name = "edit",
                            DisplayName = "Edit",
                            Icon = "create",
                            Color = "primary",
                            ActionCommandOptions = new ActionCommandOptions
                            {
                                ActionType  = ActionType.Redirect,
                                RedirectOptions = new RedirectOptions
                                {
                                    IsSameDomain = true,
                                    RedirectUrl = "portal/page/app-form?id={{data.id}}"
                                }
                            },
                            Order = 1
                        },
                        new CommandButtonInList
                        {
                            Name = "delete",
                            DisplayName = "Delete",
                            Icon = "delete",
                            Color = "warn",
                            ActionCommandOptions = new ActionCommandOptions
                            {
                                ActionType  = ActionType.Redirect,
                                RedirectOptions = new RedirectOptions
                                {
                                    IsSameDomain = true,
                                    RedirectUrl = "portal/page/app-form?id={{data.id}}"
                                }
                            }
                        },
                        new CommandButtonInList
                        {
                            Name = "menu",
                            DisplayName = "Menu",
                            Icon = "menu",
                            Color = "primary",
                            ActionCommandOptions = new ActionCommandOptions
                            {
                                ActionType  = ActionType.Redirect,
                                RedirectOptions = new RedirectOptions
                                {
                                    IsSameDomain = true,
                                    RedirectUrl = "portal/builder/menus/{{data.id}}"
                                }
                            },
                            Order = 2
                        }
                    }
                },
                ColumnsList = new ColumnsList
                {
                    ColumndDefs = new List<ColumndDef>
                    {
                        new ColumndDef
                        {
                            Name = "id",
                            DisplayName = "Id",
                            IsHidden = true,
                            DisplayFormat = "{0}",
                            SearchOptions = new SearchOptions
                            {
                                AllowInAdvancedMode = false,
                                AllowTextSearch = false
                            },
                            Order = 0
                        },
                        new ColumndDef
                        {
                            Name = "displayName",
                            DisplayName = "Name",
                            DisplayFormat = "{0}",
                            AllowSort = true,
                            SearchOptions = new SearchOptions
                            {
                                AllowInAdvancedMode = true,
                                AllowTextSearch = true
                            },
                            Order = 1
                        },
                        new ColumndDef
                        {
                            Name = "currentVersionNumber",
                            DisplayName = "Version",
                            DisplayFormat = "{0}",
                            AllowSort = true,
                            SearchOptions = new SearchOptions
                            {
                                AllowInAdvancedMode = true,
                                AllowTextSearch = true
                            },
                            Order = 2
                        },
                        new ColumndDef
                        {
                            Name = "defaultUrl",
                            DisplayName = "Default Url",
                            DisplayFormat = "{0}",
                            SearchOptions = new SearchOptions
                            {
                                AllowInAdvancedMode = true,
                                AllowTextSearch = true
                            },
                            Order = 3
                        },
                        new ColumndDef
                        {
                            Name = "dateCreated",
                            DisplayName = "Date Created",
                            DisplayFormat = "{0:dd/MM/yyyy}",
                            SearchOptions = new SearchOptions
                            {
                                AllowInAdvancedMode = true,
                                AllowTextSearch = true,
                                FieldValueType = FieldValueType.DatePicker
                            },
                            Order = 4
                        }
                    }
                }
            };

            var pageListSectionPart = new DynamicList
            {
                Id = "5d0f2dca6ba2fd4ca49e3742",
                Name = "page-list-section-part",
                DisplayName = "Pages List",
                Options = Constants.DynamicListOptions,
                ListDatasource = new DynamicListDatasource
                {
                    DatabaseConnectionOptions = new DatabaseOptions
                    {
                        DatabaseConnectionId = Constants.CoreDatabaseId,
                        EntityName = "apps",
                        Query = versionContext.ConnectionType == Core.Persistences.ConnectionType.MongoDB ?
                        "{ \"$query\": { \"pages\": [ ] } }" 
                        : "Select * from pages"
                    },
                    SourceType = DynamicListSourceType.Database
                },
                ColumnsList = new ColumnsList
                {
                    ColumndDefs = new List<ColumndDef>
                    {
                        new ColumndDef
                        {
                            Name = "id",
                            DisplayName = "Id",
                            IsHidden = true,
                            DisplayFormat = "{0}",
                            SearchOptions = new SearchOptions
                            {
                                AllowInAdvancedMode = false,
                                AllowTextSearch = false
                            },
                            Order = 0
                        },
                        new ColumndDef
                        {
                            Name = "displayName",
                            DisplayName = "Name",
                            AllowSort = true,
                            DisplayFormat = "{0}",
                            SearchOptions = new SearchOptions
                            {
                                AllowInAdvancedMode = true,
                                AllowTextSearch = true
                            },
                            Order = 1
                        },
                        new ColumndDef
                        {
                            Name = "urlPath",
                            DisplayName = "Url",
                            AllowSort = true,
                            DisplayFormat = "{0}",
                            SearchOptions = new SearchOptions
                            {
                                AllowInAdvancedMode = true,
                                AllowTextSearch = true
                            },
                            Order = 2
                        },
                        new ColumndDef
                        {
                            Name = "name",
                            DisplayName = "Name",
                            IsHidden = true,
                            DisplayFormat = "{0}",
                            SearchOptions = new SearchOptions
                            {
                                AllowInAdvancedMode = true,
                                AllowTextSearch = true
                            },
                            Order = 3
                        }
                    }
                },
                CommandsList = new CommandsList
                {
                    CommandButtonsInList = new List<CommandButtonInList>
                    {
                        new CommandButtonInList
                        {
                            Name = "create",
                            DisplayName = "Create",
                            Color = "primary",
                            CommandPositionType = CommandPositionType.OutList,
                            ActionCommandOptions = new ActionCommandOptions
                            {
                                ActionType = ActionType.Redirect,
                                RedirectOptions = new RedirectOptions
                                {
                                    RedirectUrl = "portal/builder/page",
                                    IsSameDomain = true
                                }
                            },
                            Order = 0
                        },
                        new CommandButtonInList
                        {
                            Name = "edit",
                            DisplayName = "Edit",
                            Color = "primary",
                            Icon = "create",
                            ActionCommandOptions = new ActionCommandOptions
                            {
                                ActionType = ActionType.Redirect,
                                RedirectOptions = new RedirectOptions
                                {
                                    RedirectUrl = "portal/builder/page/{{data.name}}",
                                    IsSameDomain = true
                                }
                            },
                            Order = 1
                        }
                    }
                }
            };

            var userListSectionPart = new DynamicList
            {
                Id = "5d0f2dca6ba2fd4ca49e3743",
                Name = "user-list-section-part",
                DisplayName = "Users List",
                Options = Constants.DynamicListOptions,
                ListDatasource = new DynamicListDatasource
                {
                    DatabaseConnectionOptions = new DatabaseOptions
                    {
                        DatabaseConnectionId = Constants.CoreDatabaseId,
                        EntityName = "apps",
                        Query = versionContext.ConnectionType == Core.Persistences.ConnectionType.MongoDB ?
                        "{ \"$query\": { \"users\": [ ] } }" 
                        : "Select * from users"
                    },
                    SourceType = DynamicListSourceType.Database
                },                 
                ColumnsList = new ColumnsList
                {
                    ColumndDefs = new List<ColumndDef>
                    {
                        new ColumndDef
                        {
                            Name = "id",
                            DisplayName = "Id",
                            IsHidden = true,
                            DisplayFormat = "{0}",
                            SearchOptions = new SearchOptions
                            {
                                AllowInAdvancedMode = false,
                                AllowTextSearch = false
                            },
                            Order = 0
                        },
                        new ColumndDef
                        {
                            Name = "username",
                            DisplayName = "Username",
                            AllowSort = true,
                            DisplayFormat = "{0}",
                            SearchOptions = new SearchOptions
                            {
                                AllowInAdvancedMode = true,
                                AllowTextSearch = true
                            },
                            Order = 1
                        },
                        new ColumndDef
                        {
                            Name = "email",
                            DisplayName = "Email",
                            AllowSort = true,
                            DisplayFormat = "{0}",
                            SearchOptions = new SearchOptions
                            {
                                AllowInAdvancedMode = true,
                                AllowTextSearch = true
                            },
                            Order = 2
                        },
                        new ColumndDef
                        {
                            Name = "roles",
                            DisplayName = "Roles",
                            DisplayFormat = "{0}",
                            SearchOptions = new SearchOptions
                            {
                                AllowInAdvancedMode = true,
                                AllowTextSearch = true
                            },
                            Order = 3
                        },
                        new ColumndDef
                        {
                            Name = "isLockoutEnabled",
                            DisplayName = "Lock",
                            DisplayFormat = "{0}",
                            SearchOptions = new SearchOptions
                            {
                                AllowInAdvancedMode = true,
                                AllowTextSearch = true,
                                FieldValueType = FieldValueType.Slide
                            },
                            Order = 4
                        }
                    }
                },
                CommandsList = new CommandsList
                {
                    CommandButtonsInList = new List<CommandButtonInList>
                    {
                        new CommandButtonInList
                        {
                            Name = "create",
                            DisplayName = "Create",
                            Color = "primary",
                            CommandPositionType = CommandPositionType.OutList,
                            ActionCommandOptions = new ActionCommandOptions
                            {
                                ActionType = ActionType.Redirect,
                                RedirectOptions = new RedirectOptions
                                {
                                    RedirectUrl = "portal/page/add-user-form",
                                    IsSameDomain = true
                                }
                            },
                            Order = 0
                        },
                        new CommandButtonInList
                        {
                            Name = "edit",
                            DisplayName = "Edit",
                            Color = "primary",
                            Icon = "create",
                            ActionCommandOptions = new ActionCommandOptions
                            {
                                RedirectOptions = new RedirectOptions
                                {
                                    RedirectUrl = "portal/page/user-form?id={{data.id}}",
                                    IsSameDomain = true
                                }
                            },
                            Order = 1
                        }
                    }
                }
            };

            var roleListSectionPart = new DynamicList
            {
                Id = "5d0f2dca6ba2fd4ca49e3746",
                Name = "roles-list-section-part",
                DisplayName = "Roles List",
                Options = Constants.DynamicListOptions,
                ListDatasource = new DynamicListDatasource
                {
                    DatabaseConnectionOptions = new DatabaseOptions
                    {
                        DatabaseConnectionId = Constants.CoreDatabaseId,
                        EntityName = "apps",
                        Query = versionContext.ConnectionType == Core.Persistences.ConnectionType.MongoDB ?
                        "{ \"$query\": { \"roles\": [ ] } }" 
                        : "Select * from roles"
                    },
                    SourceType = DynamicListSourceType.Database
                },                  
                ColumnsList = new ColumnsList
                {
                    ColumndDefs = new List<ColumndDef>
                    {
                        new ColumndDef
                        {
                            Name = "id",
                            DisplayName = "Id",
                            IsHidden = true,
                            DisplayFormat = "{0}",
                            SearchOptions = new SearchOptions
                            {
                                AllowInAdvancedMode = false,
                                AllowTextSearch = false
                            },
                            Order = 0
                        },
                        new ColumndDef
                        {
                            Name = "name",
                            DisplayName = "Role",
                            AllowSort = true,
                            DisplayFormat = "{0}",
                            SearchOptions = new SearchOptions
                            {
                                AllowInAdvancedMode = true,
                                AllowTextSearch = true
                            },
                            Order = 1
                        }
                    }
                },
                CommandsList = new CommandsList
                {
                    CommandButtonsInList = new List<CommandButtonInList>
                    {
                        new CommandButtonInList
                        {
                            Name = "create",
                            DisplayName = "Create",
                            Color = "primary",
                            CommandPositionType = CommandPositionType.OutList,
                            ActionCommandOptions =new ActionCommandOptions
                            {
                                RedirectOptions = new RedirectOptions
                                {
                                    RedirectUrl = "portal/page/role-form",
                                    IsSameDomain = true
                                },
                                ActionType = ActionType.Redirect
                            },
                            Order = 0
                        },
                        new CommandButtonInList
                        {
                            Name = "edit",
                            DisplayName = "Edit",
                            Color = "primary",
                            Icon = "create",
                            ActionCommandOptions = new ActionCommandOptions
                            {
                                RedirectOptions = new RedirectOptions
                                {
                                    RedirectUrl = "portal/page/role-form?id={{data.id}}",
                                    IsSameDomain = true
                                },
                                ActionType = ActionType.Redirect
                            },
                            Order = 1
                        },
                        new CommandButtonInList
                        {
                            Name = "editclaims",
                            DisplayName = "Claims",
                            Color = "accent",
                            Icon = "https",
                            ActionCommandOptions = new ActionCommandOptions
                            {
                                RedirectOptions = new RedirectOptions
                                {
                                    RedirectUrl = "portal/builder/roles/{{data.name}}/claims",
                                    IsSameDomain = true
                                },
                                ActionType = ActionType.Redirect
                            },
                            Order = 2
                        }
                    }
                }
            };

            versionContext.InsertData(databaseListSectionPart);
            versionContext.InsertData(appListSectionPart);
            versionContext.InsertData(pageListSectionPart);
            versionContext.InsertData(userListSectionPart);
            versionContext.InsertData(roleListSectionPart);
        }
    }
}