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

using System;
using System.Data;

namespace PHydrate.Tests.Integration.SprocIntegration
{
    public class SQLiteProcCommand : IDbCommand
    {
        private readonly IDbCommand _baseCommand;

        public SQLiteProcCommand( IDbCommand baseCommand )
        {
            _baseCommand = baseCommand;
        }

        #region Implementation of IDisposable

        [ CoverageExclude ]
        public void Dispose()
        {
            _baseCommand.Dispose();
        }

        #endregion

        #region Implementation of IDbCommand

        [ CoverageExclude ]
        public void Prepare()
        {
            _baseCommand.Prepare();
        }

        [ CoverageExclude ]
        public void Cancel()
        {
            _baseCommand.Cancel();
        }

        [ CoverageExclude ]
        public IDbDataParameter CreateParameter()
        {
            return _baseCommand.CreateParameter();
        }

        public int ExecuteNonQuery()
        {
            ConvertToTextQuery();
            return _baseCommand.ExecuteNonQuery();
        }

        public IDataReader ExecuteReader()
        {
            ConvertToTextQuery();
            return _baseCommand.ExecuteReader();
        }

        public IDataReader ExecuteReader( CommandBehavior behavior )
        {
            ConvertToTextQuery();
            return _baseCommand.ExecuteReader( behavior );
        }

        public object ExecuteScalar()
        {
            ConvertToTextQuery();
            return _baseCommand.ExecuteScalar();
        }

        public IDbConnection Connection
        {
            [ CoverageExclude ]
            get { return _baseCommand.Connection; }
            [ CoverageExclude ]
            set { _baseCommand.Connection = value; }
        }

        public IDbTransaction Transaction
        {
            [ CoverageExclude ]
            get { return _baseCommand.Transaction; }
            [ CoverageExclude ]
            set { _baseCommand.Transaction = value; }
        }

        public string CommandText
        {
            [ CoverageExclude ]
            get { return _baseCommand.CommandText; }
            [ CoverageExclude ]
            set { _baseCommand.CommandText = value; }
        }

        public int CommandTimeout
        {
            [ CoverageExclude ]
            get { return _baseCommand.CommandTimeout; }
            [ CoverageExclude ]
            set { _baseCommand.CommandTimeout = value; }
        }

        public CommandType CommandType { get; set; }

        public IDataParameterCollection Parameters
        {
            [ CoverageExclude ]
            get { return _baseCommand.Parameters; }
        }

        public UpdateRowSource UpdatedRowSource
        {
            [ CoverageExclude ]
            get { return _baseCommand.UpdatedRowSource; }
            [ CoverageExclude ]
            set { _baseCommand.UpdatedRowSource = value; }
        }

        private void ConvertToTextQuery()
        {
            if ( CommandType != CommandType.StoredProcedure )
                return;

            // Store the old values
            string procedureName = CommandText;
            var parameters = new IDataParameter[Parameters.Count];
            for ( int i = 0; i < Parameters.Count; i++ )
                parameters[ i ] = Parameters[ i ] as IDataParameter;

            // SQLite driver doesn't implement this!
            //Parameters.CopyTo( parameters, 0 );

            CommandText = @"SELECT Procedure FROM Procedures WHERE ProcedureName = :procName";
            _baseCommand.CommandType = CommandType.Text;
            Parameters.Clear();
            AddParameter( ":procName", procedureName );

            var procedure = _baseCommand.ExecuteScalar() as string;
            if ( procedure == null )
                throw new Exception( String.Format( "Could not find procedure named {0}", procedureName ) );

            CommandText = procedure;
            Parameters.Clear();

            foreach ( IDataParameter p in parameters )
                Parameters.Add( p );
        }

        private void AddParameter( string parameterName, string parameterValue )
        {
            IDbDataParameter parameter = CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = parameterValue;
            Parameters.Add( parameter );
        }

        #endregion
    }
}