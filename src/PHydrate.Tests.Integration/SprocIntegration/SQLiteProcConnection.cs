#region Copyright

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
// Copyright 2010, Stephen Michael Czetty
// 

#endregion

using System.Data;
using System.Data.SQLite;

namespace PHydrate.Tests.Integration.SprocIntegration
{
    public class SQLiteProcConnection : IDbConnection
    {
        private readonly SQLiteConnection _baseConnection;

        public SQLiteProcConnection( string connectionString )
        {
            _baseConnection = new SQLiteConnection( connectionString );
        }

        #region Implementation of IDisposable

        [ CoverageExclude ]
        public void Dispose()
        {
            _baseConnection.Dispose();
        }

        #endregion

        #region Implementation of IDbConnection

        [ CoverageExclude ]
        public IDbTransaction BeginTransaction()
        {
            return _baseConnection.BeginTransaction();
        }

        [ CoverageExclude ]
        public IDbTransaction BeginTransaction( IsolationLevel il )
        {
            return _baseConnection.BeginTransaction( il );
        }

        [ CoverageExclude ]
        public void Close()
        {
            _baseConnection.Close();
        }

        [ CoverageExclude ]
        public void ChangeDatabase( string databaseName )
        {
            _baseConnection.ChangeDatabase( databaseName );
        }

        public IDbCommand CreateCommand()
        {
            return new SQLiteProcCommand( _baseConnection.CreateCommand() );
        }

        [ CoverageExclude ]
        public void Open()
        {
            _baseConnection.Open();
        }

        public string ConnectionString
        {
            [ CoverageExclude ]
            get { return _baseConnection.ConnectionString; }
            [ CoverageExclude ]
            set { _baseConnection.ConnectionString = value; }
        }

        public int ConnectionTimeout
        {
            [ CoverageExclude ]
            get { return _baseConnection.ConnectionTimeout; }
        }

        public string Database
        {
            [ CoverageExclude ]
            get { return _baseConnection.Database; }
        }

        public ConnectionState State
        {
            [ CoverageExclude ]
            get { return _baseConnection.State; }
        }

        #endregion
    }
}