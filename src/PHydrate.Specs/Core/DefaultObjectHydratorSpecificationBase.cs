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

using System.Collections.Generic;
using Machine.Specifications;
using Machine.Specifications.Annotations;
using PHydrate.Core;
using UMMO.TestingUtils;

namespace PHydrate.Specs.Core
{
    public abstract class DefaultObjectHydratorSpecificationBase
    {
        protected static IObjectHydrator ObjectHydrator;
        protected static int RandomInteger;
        protected static string RandomString;

        protected static Dictionary< string, object > ColumnValues;
        protected static object ReturnedObject;

        #region Test class

        protected class TestHydrationTarget
        {
            public int IntegerProperty { get; protected set; }

            public string StringProperty { get; protected set; }
        }

        #endregion

        [ UsedImplicitly ] private Establish Context = () => {
                                                           ObjectHydrator = new DefaultObjectHydrator();
                                                           RandomInteger = A.Random.Integer.Value;
                                                           RandomString = A.Random.String.Resembling.A.Noun;

                                                           ColumnValues = new Dictionary< string, object > {
                                                                                                               {
                                                                                                                   "IntegerProperty"
                                                                                                                   ,
                                                                                                                   RandomInteger
                                                                                                                   },
                                                                                                               {
                                                                                                                   "StringProperty"
                                                                                                                   ,
                                                                                                                   RandomString
                                                                                                                   }
                                                                                                           };
                                                       };
    }
}