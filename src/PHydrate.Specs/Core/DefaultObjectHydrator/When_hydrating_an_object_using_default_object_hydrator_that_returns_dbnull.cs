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
using System.Collections.Generic;
using Machine.Specifications;
using Machine.Specifications.Annotations;
using PHydrate.Core;
using UMMO.TestingUtils;

namespace PHydrate.Specs.Core.DefaultObjectHydrator
{
    [ Subject( typeof(PHydrate.Core.DefaultObjectHydrator) ) ]
    public sealed class When_hydrating_an_object_using_default_object_hydrator_that_returns_dbnull
    {
        private static IDefaultObjectHydrator _defaultObjectHydrator;
        private static int _randomInteger;

        #region Test class

        private class TestHydrationTarget
        {
            public string StringProperty { get; [UsedImplicitly] protected set; }
        }

        #endregion

        private Establish Context = () => {
                                        _defaultObjectHydrator = new PHydrate.Core.DefaultObjectHydrator();
                                        _randomInteger = A.Random.Integer.Value;

                                        _columnValues = new Dictionary< string, object > {
                                                                                             {
                                                                                                 "StringProperty"
                                                                                                 ,
                                                                                                 DBNull.Value
                                                                                                 }
                                                                                         };
                                    };

        private Because Of =
            () => _returnedObject = _defaultObjectHydrator.Hydrate< TestHydrationTarget >( _columnValues );

        private It Should_return_null_for_dbnull_value
            = () => _returnedObject.StringProperty.ShouldBeNull();

        private static IDictionary< string, object > _columnValues;
        private static TestHydrationTarget _returnedObject;
    }
}