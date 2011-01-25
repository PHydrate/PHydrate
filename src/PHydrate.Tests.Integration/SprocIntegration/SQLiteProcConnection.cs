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

#endregion

using System;
using System.Data;

namespace PHydrate.Tests.Integration.SprocIntegration
{
    public sealed class SQLiteProcConnection : IDbConnection
    {
        private readonly IDbConnection _baseConnection;

        public SQLiteProcConnection( IDbConnection baseConnection )
        {
            _baseConnection = baseConnection;
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            throw new NotImplementedException();
            //_baseConnection.Dispose();
        }

        #endregion

        #region Implementation of IDbConnection

        public IDbTransaction BeginTransaction()
        {
            throw new NotImplementedException();
            //return _baseConnection.BeginTransaction();
        }

        public IDbTransaction BeginTransaction( IsolationLevel il )
        {
            throw new NotImplementedException();
            //return _baseConnection.BeginTransaction( il );
        }

        public void Close()
        {
            throw new NotImplementedException();
            //_baseConnection.Close();
        }

        public void ChangeDatabase( string databaseName )
        {
            throw new NotImplementedException();
            //_baseConnection.ChangeDatabase( databaseName );
        }

        public IDbCommand CreateCommand()
        {
            return new SQLiteProcCommand( _baseConnection.CreateCommand() );
        }

        public void Open()
        {
            _baseConnection.Open();
        }

        public string ConnectionString
        {
            get
            {
                throw new NotImplementedException();
                //return _baseConnection.ConnectionString; 
            }
            set
            {
                throw new NotImplementedException();
                //_baseConnection.ConnectionString = value;
            }
        }

        public int ConnectionTimeout
        {
            get
            {
                throw new NotImplementedException();
                // return _baseConnection.ConnectionTimeout);
            }
        }

        public string Database
        {
            get
            {
                throw new NotImplementedException();
                //return _baseConnection.Database; 
            }
        }

        public ConnectionState State
        {
            get
            {
                return _baseConnection.State;
            }
        }

        #endregion
    }
}