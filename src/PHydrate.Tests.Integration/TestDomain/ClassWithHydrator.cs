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

using System.Collections.Generic;
using PHydrate.Attributes;

namespace PHydrate.Tests.Integration.TestDomain
{
    [ HydrateUsing( "GetClassWithHydrator" ) ]
    [ ObjectHydrator( typeof(CustomHydrator) ) ]
    public sealed class ClassWithHydrator
    {
        public long ClassWithHydratorId { get; set; }

        public long IntegerValue { get; set; }

        public string StringValue { get; set; }
    }

    public class CustomHydrator : IObjectHydrator< ClassWithHydrator >
    {
        #region Implementation of IObjectHydrator<ClassWithHydrator>

        public ClassWithHydrator Hydrate( IDictionary< string, object > columnValues )
        {
            return new ClassWithHydrator {
                                             ClassWithHydratorId = (long)columnValues[ "Id" ],
                                             IntegerValue = (long)columnValues[ "IntValue" ],
                                             StringValue = (string)columnValues[ "StrValue" ]
                                         };
        }

        #endregion
    }
}