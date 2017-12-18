namespace poc_aggregates_repository.Data
{
    public class Entity
    {
        private bool _removed;

        public void Remove()
        {
            _removed = true;
        }

        public bool IsRemoved()
        {
            return _removed;
        }
    }
}