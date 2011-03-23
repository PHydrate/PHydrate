#region Copyright

// This file is part of UMMO.
// 
// UMMO is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// UMMO is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with UMMO.  If not, see <http://www.gnu.org/licenses/>.
//  
// Copyright 2010, Stephen Michael Czetty

#endregion

using System;

// ReSharper disable CheckNamespace
// No namespace, so TestDriven.NET picks it up automatically

/// <summary>
/// Exclude the class or method from coverage
/// </summary>
/// <remarks>
/// You really shouldn't have to use this outside of test assemblies, or for severe edge cases!
/// </remarks>
[ AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Struct | AttributeTargets.Parameter |
    AttributeTargets.Constructor, Inherited = false ) ]
public class CoverageExcludeAttribute : Attribute {}

// ReSharper restore CheckNamespace