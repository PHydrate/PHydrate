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

using System;
using Machine.Specifications;
using PHydrate.Attributes;
using UMMO.TestingUtils;

namespace PHydrate.Specs.Attributes
{
    public abstract class CrudAttributeSpecificationBase< T > where T : CrudAttributeBase
    {
        protected static Func< T > InstantiateAttribute;

        protected static string StoredProcedureName;
        protected static T CrudAttribute;

        [ UsedImplicitly ]
        private Establish GlobalContext = () => StoredProcedureName = A.Random.String.Resembling.A.Noun;

        // TODO: Add Verb to UMMO.RandomString

        [ UsedImplicitly ]
        private Because Of = () => CrudAttribute = InstantiateAttribute();
    }
}