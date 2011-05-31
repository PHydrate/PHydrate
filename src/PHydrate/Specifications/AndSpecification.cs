namespace PHydrate.Specifications
{
    internal class AndSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification< T > _spec1;
        private readonly ISpecification< T > _spec2;

        internal AndSpecification(ISpecification<T> s1, ISpecification<T> s2)
        {
            _spec1 = s1;
            _spec2 = s2;
        }

        public bool IsSatisfiedBy( T entity )
        {
            return _spec1.IsSatisfiedBy( entity ) && _spec2.IsSatisfiedBy( entity );
        }
    }
}