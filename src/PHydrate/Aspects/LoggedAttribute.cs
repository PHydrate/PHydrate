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
using Afterthought;
using PHydrate.Aspects.Logging;

namespace PHydrate.Aspects
{
    /// <summary>
    /// Mark a class as logged.
    /// </summary>
    [ AttributeUsage( AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Assembly, Inherited = true ) ]
    [ CLSCompliant( false ) ]
    public sealed class LoggedAttribute : AmendmentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoggedAttribute"/> class.
        /// </summary>
        public LoggedAttribute() : base( typeof(LogAmendment< >) ) {}
    }
}