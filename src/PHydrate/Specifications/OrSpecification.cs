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

namespace PHydrate.Specifications
{
    internal class OrSpecification< T > : ISpecification< T >
    {
        private readonly ISpecification< T > _spec1;
        private readonly ISpecification< T > _spec2;

        internal OrSpecification( ISpecification< T > s1, ISpecification< T > s2 )
        {
            _spec1 = s1;
            _spec2 = s2;
        }

        public bool IsSatisfiedBy( T entity )
        {
            return _spec1.IsSatisfiedBy( entity ) || _spec2.IsSatisfiedBy( entity );
        }
    }
}