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
// Copyright 2010-2011, Stephen Michael Czetty

#endregion

using System.Data;
using Machine.Specifications;
using Machine.Specifications.Annotations;
using Rhino.Mocks;
using UMMO.TestingUtils;

namespace PHydrate.Specs.Util.DbCommandExtensions
{
    public abstract class DbCommandExtensionsSpecificationBase
    {
        protected static IDbCommand Command;
        protected static IDbDataParameter DataParameter;
        protected static string ParameterName;
        protected static IDataParameterCollection Parameters;

        [ UsedImplicitly ]
        private Establish Context = () => {
                                        Command = MockRepository.GenerateMock< IDbCommand >();
                                        DataParameter = MockRepository.GenerateMock< IDbDataParameter >();
                                        Parameters = MockRepository.GenerateMock< IDataParameterCollection >();

                                        ParameterName = A.Random.String;

                                        Command.Expect( x => x.CreateParameter() ).Return( DataParameter );
                                        Command.Expect( x => x.Parameters ).Return( Parameters );
                                        DataParameter.Expect( x => x.ParameterName ).SetPropertyWithArgument(
                                            ParameterName );
                                        DataParameter.Expect( x => x.Value ).SetPropertyAndIgnoreArgument();
                                        DataParameter.Expect( x => x.Direction ).SetPropertyAndIgnoreArgument();
                                        Parameters.Expect( x => x.Add( null ) ).IgnoreArguments().Return( 0 );
                                    };
    }
}