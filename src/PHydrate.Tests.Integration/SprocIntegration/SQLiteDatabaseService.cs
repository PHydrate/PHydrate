﻿#region Copyright

// This file is part of PHydrate.
// 
// PHydrate is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// PHydrate is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with PHydrate.  If not, see <http://www.gnu.org/licenses/>.
// 
// Copyright 2010-2011, Stephen Michael Czetty

#endregion

using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using PHydrate.Core;

namespace PHydrate.Tests.Integration.SprocIntegration
{
    internal sealed class SQLiteDatabaseService : DatabaseService, IDatabaseServiceProvider
    {
        private readonly IDbConnection _databaseConnection;

        public SQLiteDatabaseService()
        {
            _databaseConnection = new SQLiteConnection( String.Format( "Data Source={0};Version=3",
                                                                       Path.Combine( Environment.CurrentDirectory,
                                                                                     "IntegrationTestDb.sqlite" ) ) );
        }

        #region Overrides of DatabaseService

        protected override IDbConnection GetDatabaseConnection()
        {
            return
                new SQLiteProcConnection( _databaseConnection );
        }

        #endregion

        public IDatabaseService DatabaseService()
        {
            return this;
        }
    }
}