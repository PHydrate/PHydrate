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

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.

[ assembly: AssemblyTitle( "PHydrate" ) ]
[ assembly: AssemblyDescription( "" ) ]
[ assembly: AssemblyConfiguration( "" ) ]
[ assembly: AssemblyCompany( "" ) ]
[ assembly: AssemblyProduct( "PHydrate" ) ]
[ assembly: AssemblyCopyright( "Copyright © StephenCzetty 2010" ) ]
[ assembly: AssemblyTrademark( "" ) ]
[ assembly: AssemblyCulture( "" ) ]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.

[ assembly: ComVisible( false ) ]

// The following GUID is for the ID of the typelib if this project is exposed to COM

[ assembly: Guid( "6e137e41-d7e9-453b-b01c-b5b1f8ebc7a1" ) ]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]

[ assembly: AssemblyVersion( "0.1.0.*" ) ]
//[ assembly: AssemblyFileVersion( "0.1.0.0" ) ]

// Allow the Specifications to see the internals

[ assembly: InternalsVisibleTo( "PHydrate.Specs" ) ]